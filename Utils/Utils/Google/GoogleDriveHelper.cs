using System;
using System.Collections.Generic;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;

namespace Utils.Google
{
    public class GoogleDriveHelper
    {
        public DriveService Service { get; private set; }

        public bool Init()
        {
            try
            {
                UserCredential credential;
                using (var stream = new System.IO.FileStream("Google\\client_id.json", System.IO.FileMode.Open,
                                                System.IO.FileAccess.Read))
                {
                    GoogleWebAuthorizationBroker.Folder = "Tasks.Auth.Store";
                    ClientSecrets secrets = GoogleClientSecrets.Load(stream).Secrets;
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                                            secrets,
                                                            new[] { DriveService.Scope.Drive,
                                                                DriveService.Scope.DriveFile },
                                                            "user",
                                                            CancellationToken.None,
                                                            new FileDataStore("Drive.Auth.Store")).Result;
                }
                Service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Sample",
                });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DownloadFile(string fileName, string dirName, string saveTo, Action<string> backup)
        {
            var directory = GetDirectories(dirName).FirstOrDefault();
            if (directory == null)
            {
                return false;
            }

            var file = GetFilesInDirectory(dirName).FirstOrDefault(item => item.Title == System.IO.Path.GetFileName(fileName));
            if (file == null)
            {
                return false;
            }

            bool res = DownloadFile(file, saveTo, backup);
            if(res)
            {
                new System.IO.FileInfo(fileName).LastWriteTime = file.ModifiedDate.GetValueOrDefault();
            }
            return res;
        }

        /// <summary>
        /// Download a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/get
        /// </summary>
        /// <param name="fileResource">File resource of the file to download</param>
        /// <param name="saveTo">location of where to save the file including the file name to save it as.</param>
        /// <returns></returns>
        private bool DownloadFile(File fileResource, string saveTo, Action<string> backup)
        {

            if (!string.IsNullOrEmpty(fileResource.DownloadUrl))
            {
                try
                {
                    backup(saveTo);
                    var x = Service.HttpClient.GetByteArrayAsync(fileResource.DownloadUrl);
                    byte[] arrBytes = x.Result;
                    System.IO.File.WriteAllBytes(saveTo, arrBytes);
                    return true;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("An error occurred: " + e.Message);
                    return false;
                }
            }
            else
            {
                // The file doesn't have any content stored on Drive.
                return false;
            }
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public bool UploadOrUpdateFile(string fileName, string dirName)
        {
            var directory = GetDirectories(dirName).FirstOrDefault();
            if (directory == null)
            {
                return false;
            }

            var file = GetFilesInDirectory(dirName).FirstOrDefault(item => item.Title == System.IO.Path.GetFileName(fileName));

            var res = file != null ?
                    UpdateFile(fileName, directory.Id, file.Id) :
                    UploadFile(fileName, directory.Id);
            return res != null;
        }

        public bool TryGetModificationDate(string fileName, string dirName, out DateTime date)
        {
            var directory = GetDirectories(dirName).FirstOrDefault();
            if (directory == null)
            {
                date = new DateTime();
                return false;
            }

            var file = GetFilesInDirectory(dirName).FirstOrDefault(item => item.Title == System.IO.Path.GetFileName(fileName));
            if (file == null || !file.ModifiedDate.HasValue)
            {
                date = new DateTime();
                return false;
            }

            date = file.ModifiedDate.Value;
            return true;
        }

        /// <summary>
        /// Uploads a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/insert
        /// </summary>
        /// <param name="_service">a Valid authenticated DriveService</param>
        /// <param name="fileName">path to the file to upload</param>
        /// <param name="_parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <returns>If upload succeeded returns the File resource of the uploaded file 
        ///          If the upload fails returns null</returns>
        public File UploadFile(string fileName, string _parent)
        {

            if (System.IO.File.Exists(fileName))
            {
                File body = new File();
                body.Title = System.IO.Path.GetFileName(fileName);
                body.Description = "File uploaded";
                body.MimeType = GetMimeType(fileName);
                body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };

                // File's content.
                byte[] byteArray = System.IO.File.ReadAllBytes(fileName);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    FilesResource.InsertMediaUpload request = Service.Files.Insert(body, stream, GetMimeType(fileName));
                    //request.Convert = true;   // uncomment this line if you want files to be converted to Drive format
                    request.Upload();
                    return request.ResponseBody;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("An error occurred: " + e.Message);
                    return null;
                }
            }
            else
            {
                //Console.WriteLine("File does not exist: " + _uploadFile);
                return null;
            }

        }

        /// <summary>
        /// Updates a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/update
        /// </summary>
        /// <param name="_service">a Valid authenticated DriveService</param>
        /// <param name="fileName">path to the file to upload</param>
        /// <param name="_parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <param name="_fileId">the resource id for the file we would like to update</param>                      
        /// <returns>If upload succeeded returns the File resource of the uploaded file 
        ///          If the upload fails returns null</returns>
        public File UpdateFile(string fileName, string _parent, string _fileId)
        {

            if (System.IO.File.Exists(fileName))
            {
                File body = new File();
                body.Title = System.IO.Path.GetFileName(fileName);
                body.Description = "File updated";
                body.MimeType = GetMimeType(fileName);
                body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };

                // File's content.
                byte[] byteArray = System.IO.File.ReadAllBytes(fileName);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    FilesResource.UpdateMediaUpload request = Service.Files.Update(body, _fileId, stream, GetMimeType(fileName));
                    request.Upload();
                    return request.ResponseBody;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("An error occurred: " + e.Message);
                    return null;
                }
            }
            else
            {
                //Console.WriteLine("File does not exist: " + _uploadFile);
                return null;
            }

        }


        /// <summary>
        /// Create a new Directory.
        /// Documentation: https://developers.google.com/drive/v2/reference/files/insert
        /// </summary>
        /// <param name="_service">a Valid authenticated DriveService</param>
        /// <param name="_title">The title of the file. Used to identify file or folder name.</param>
        /// <param name="_description">A short description of the file.</param>
        /// <param name="_parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <returns></returns>
        public File CreateDirectory(string _title, string _description, string _parent)
        {

            File NewDirectory = null;

            // Create metaData for a new Directory
            File body = new File();
            body.Title = _title;
            body.Description = _description;
            body.MimeType = "application/vnd.google-apps.folder";
            body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };
            try
            {
                FilesResource.InsertRequest request = Service.Files.Insert(body);
                NewDirectory = request.Execute();
            }
            catch (Exception e)
            {
                //Console.WriteLine("An error occurred: " + e.Message);
            }

            return NewDirectory;
        }

        public IList<File> GetDirectories(string name)
        {
            string Q = string.Format("title = '{0}' and mimeType = 'application/vnd.google-apps.folder'", name);
            return GetFiles(Q);
        }

        public IList<File> GetFilesInDirectory(string dirName)
        {
            File dir = GetDirectories(dirName).FirstOrDefault();
            if (dir == null)
            {
                return new List<File>();
            }

            string query = string.Format("'{0}' in parents", dir.Id);
            return GetFiles(query);
        }

        /// <summary>
        /// List all of the files and directories for the current user.  
        /// 
        /// Documentation: https://developers.google.com/drive/v2/reference/files/list
        /// Documentation Search: https://developers.google.com/drive/web/search-parameters
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>        
        /// <param name="search">if Search is null will return all files</param>        
        /// <returns></returns>
        public IList<File> GetFiles(string search = null)
        {

            IList<File> Files = new List<File>();

            try
            {
                //List all of the files and directories for the current user.  
                // Documentation: https://developers.google.com/drive/v2/reference/files/list
                FilesResource.ListRequest list = Service.Files.List();
                list.MaxResults = 1000;
                if (search != null)
                {
                    list.Q = search;
                }
                FileList filesFeed = list.Execute();

                //// Loop through until we arrive at an empty page
                while (filesFeed.Items != null)
                {
                    // Adding each item  to the list.
                    foreach (File item in filesFeed.Items)
                    {
                        Files.Add(item);
                    }

                    // We will know we are on the last page when the next page token is
                    // null.
                    // If this is the case, break.
                    if (filesFeed.NextPageToken == null)
                    {
                        break;
                    }

                    // Prepare the next page of results
                    list.PageToken = filesFeed.NextPageToken;

                    // Execute and process the next page request
                    filesFeed = list.Execute();
                }
            }
            catch (Exception ex)
            {
                // In the event there is an error with the request.
                //Console.WriteLine(ex.Message);
            }
            return Files;
        }
    }
}
