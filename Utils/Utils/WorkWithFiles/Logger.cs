using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils.WorkWithFiles
{
    public class Logger : IDisposable
    {
        private static Logger logger;
        private bool isDisposed;
        private IWriter writer;
        
        public static Logger GetInstance()
        {
            if(logger == null)
            {
                logger = new Logger();
                logger.writer = new DebugWriter();
            }
            return logger;
        }
        
        private Logger()
        {

        }

        public void Init()
        {
            Close();
            this.writer = new DebugWriter();
        }

        public void Init(string file)
        {
            Close();
            this.writer = new Writer(file);
        }

        public void WriteLine(object data)
        {
            writer.WriteLine(data);
        }

        public void Write(object data)
        {
            writer.Write(data);
        }

        public void WriteBuffer(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return;
            }
            writer.WriteLine("");
            if (buffer.GetLength(0) == 0)
            {
                return;
            }
            int buffLength = 16;
            byte[] writebuffer = new byte[buffLength];
            int readed = 0;
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                if (i > 0 && i % buffLength == 0)
                {
                    Write(writebuffer, buffLength);
                    readed = 0;
                }
                writebuffer[readed++] = buffer[i];
            }
            if (readed > 0)
            {
                Write(writebuffer, readed);
            }

            writer.WriteLine("");
        }

        private void Write(byte[] writebuffer, int readed)
        {
            for (int j = 0; j < writebuffer.GetLength(0) && j < readed; j++)
            {
                writer.Write(string.Format("{0:X2} ", writebuffer[j]));
            }
            writer.Write("    ");

            Encoding enc = Encoding.GetEncoding(866);
            char[] chars = enc.GetChars(writebuffer);

            for (int j = 0; j < chars.GetLength(0) && j < readed; j++)
            {
                if (Char.IsSymbol(chars[j]) || Char.IsPunctuation(chars[j]) || Char.IsLetterOrDigit(chars[j]))
                {
                    writer.Write(string.Format("{0}", chars[j]));
                }
                else
                {
                    writer.Write('.');
                }
            }
            writer.Write('\n');
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        public void Close()
        {
            writer.Close();
        }

        ~Logger()
        {
            Dispose(false);
        }

        private void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                Close();
                isDisposed = true;
            }
        }

        #endregion
    }

    public class DebugWriter : IWriter
    {

        #region IWriter Members

        public void WriteLine(object data)
        {
            Debug.WriteLine(data);
        }

        public void Write(object data)
        {
            Debug.Write(data);
        }

        public void Close()
        {
            
        }

        #endregion
    }

    public class Writer : IWriter
    {
        private StreamWriter writer;

        public Writer(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                this.writer = new StreamWriter(file);
                Console.SetOut(writer);
            }
        }

        #region IWriter Members

        public void WriteLine(object data)
        {
            Console.WriteLine(data);
        }

        public void Write(object data)
        {
            Console.Write(data);
        }

        public void Close()
        {
            if (writer != null)
            {
                try
                {
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception)
                {
                }
            }
        } 
        
        #endregion
    }
}
