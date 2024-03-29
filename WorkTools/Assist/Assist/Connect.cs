using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace Assist
{
	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
        private RTFTextControl _rtfTextControl;
        private HighLightText _highLightText;

        public Connect()
		{
            ReInit();
        }

        private void ReInit()
        {
            Options.GetInstance.ReReadDataFromReg();
            if (Options.GetInstance.SetLogger)
            {
                Logger.Open(Options.GetInstance.LoggerFilePath);
            }
            else
            {
                Logger.Close();
            }
            _highLightText = new HighLightText(_highLightText);
        }
        #region CONNECT
        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
				Commands2 commands = (Commands2)_applicationObject.Commands;
				string toolsMenuName;

				try
				{
					//If you would like to move the command to a different menu, change the word "Tools" to the 
					//  English version of the menu. This code will take the culture, append on the name of the menu
					//  then add the command to that menu. You can find a list of all the top-level menus in the file
					//  CommandBar.resx.
					ResourceManager resourceManager = new ResourceManager("Assist.CommandBar", Assembly.GetExecutingAssembly());
					CultureInfo cultureInfo = new System.Globalization.CultureInfo(_applicationObject.LocaleID);
					string resourceName = String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools");
					toolsMenuName = resourceManager.GetString(resourceName);
				}
				catch
				{
					//We tried to find a localized version of the word Tools, but one was not found.
					//  Default to the en-US word, which may work for the current culture.
					toolsMenuName = "Tools";
				}

				//Place the command on the tools menu.
				//Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
				Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = (_applicationObject.CommandBars as Microsoft.VisualStudio.CommandBars.CommandBars)["MenuBar"];

				//Find the Tools command bar on the MenuBar command bar:
				CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
				CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

				//This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
				//  just make sure you also update the QueryStatus/Exec method to include the new command names.
				try
				{
					//Add a command to the Commands collection:
					Command command = commands.AddNamedCommand2(_addInInstance, "Assist", "Assist", "Executes the command for Assist", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported+(int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

					//Add a control for the command to the tools menu:
					if((command != null) && (toolsPopup != null))
					{
						command.AddControl(toolsPopup.CommandBar, 1);
					}
				}
				catch(System.ArgumentException)
				{
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
				}
			}

            InitCommands(null);
            InitToolWindow();
            ReInit();            
            Logger.WriteLine(string.Format("CONNECTION {0}", DateTime.Now));

		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
            Logger.WriteLine(string.Format("OnDisconnection {0}", DateTime.Now));
            Logger.Close();
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
            Logger.WriteLine(string.Format("OnAddInsUpdate {0}", DateTime.Now));

		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
            Logger.WriteLine(string.Format("OnStartupComplete {0}", DateTime.Now));

		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
            Logger.WriteLine(string.Format("OnBeginShutdown {0}", DateTime.Now));
            //Logger.Close();
		}
		
		/// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		/// <param term='commandName'>The name of the command to determine state for.</param>
		/// <param term='neededText'>Text that is needed for the command.</param>
		/// <param term='status'>The state of the command in the user interface.</param>
		/// <param term='commandText'>Text requested by the neededText parameter.</param>
		/// <seealso class='Exec' />
		public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
            //Logger.WriteLine(string.Format("QueryStatus with command {0} opt- {1} - {2}", commandName, neededText, DateTime.Now));
            if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				if(commandName == "Assist.Connect.Assist")
				{
					status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported|vsCommandStatus.vsCommandStatusEnabled;
					return;
				}
                if (commandName == "Assist.Connect.HighLight" 
                    || commandName == "Assist.Connect.HighLightMatchCase"
                    || commandName == "Assist.Connect.HighLightMatchCaseWholeWord"
                    || commandName == "Assist.Connect.GetProjectItemDescription"
                    || commandName == "Assist.Connect.GetAlgoritm"
                    || commandName == "Assist.Connect.SaveBreakpoints"
                    )
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }

            }
		}

		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
		public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
            Logger.WriteLine(string.Format("Exec with command {0} opt- {1} - {2}", commandName, executeOption, DateTime.Now));
			
            handled = false;
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				
                if(commandName == "Assist.Connect.Assist")
				{
					handled = true;
					return;
				}

                if (commandName == "Assist.Connect.HighLight")
                {
                    handled = true;
                    _highLightText.HandledSelectedText(_applicationObject, false, false, _rtfTextControl);
                }
                if (commandName == "Assist.Connect.HighLightMatchCase")
                {
                    handled = true;
                    _highLightText.HandledSelectedText(_applicationObject, true, false, _rtfTextControl);
                }
                if (commandName == "Assist.Connect.HighLightMatchCaseWholeWord")
                {
                    handled = true;
                    _highLightText.HandledSelectedText(_applicationObject, true, true, _rtfTextControl);
                }
                if (commandName == "Assist.Connect.GetProjectItemDescription")
                {
                    handled = true;
                    WorkWithProjectItem wi = new WorkWithProjectItem(_applicationObject);
                    wi.SetItemDescriptionToClipboard();
                }

                if (commandName == "Assist.Connect.GetAlgoritm")
                {
                    handled = true;
                    WorkWithDebuger dbg = new WorkWithDebuger(_applicationObject/*, Options.GetInstance.SkipInDebug*/);
                    dbg.GetAlgoritm();
                }
                if (commandName == "Assist.Connect.SaveBreakpoints")
                {
                    handled = true;
                    WorkWithBreakpoints brk = new WorkWithBreakpoints(_applicationObject);
                    brk.SaveBreakpoints();
                }
            }
		}

        private void InitCommands(CommandBarPopup toolsPopup)
        {
            Logger.WriteLine(string.Format("Method: InitHighlightCommand"));
            object[] contextGUIDS = new object[] { };
            Commands2 commands = (Commands2)_applicationObject.Commands;

            string commandName = "HighLight";
            string commandShortcut = "Text Editor::Shift+Ctrl+1";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

            commandName = "HighLightMatchCase";
            commandShortcut = "Text Editor::Shift+Ctrl+2";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

            commandName = "HighLightMatchCaseWholeWord";
            commandShortcut = "Text Editor::Ctrl+~";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

            commandName = "GetProjectItemDescription";
            commandShortcut = "Text Editor::Shift+Ctrl+3";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

            commandName = "GetAlgoritm";
            commandShortcut = "Text Editor::Shift+Ctrl+4";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

            commandName = "SaveBreakpoints";
            commandShortcut = "Text Editor::Shift+Ctrl+5";
            InitCommand(toolsPopup, ref contextGUIDS, commands, commandName, commandShortcut);

        }

        private void InitCommand(CommandBarPopup toolsPopup, ref object[] contextGUIDS, Commands2 commands, string commandName, string commandShortcut)
        {
            try
            {

                //Add a command to the Commands collection:
                Command command = commands.AddNamedCommand2(_addInInstance, commandName, commandName, commandName, true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);
                command.Bindings = commandShortcut;
                //Add a control for the command to the tools menu:
                if ((command != null) && (toolsPopup != null))
                {
                    command.AddControl(toolsPopup.CommandBar, 1);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(string.Format("Error in InitCommand {0} - {1} ", commandName, ex.Message));
            }
        }

        private void InitToolWindow()
        {
            // Коллекция DTE.ToolWindows 
            Windows2 windows = (Windows2)_applicationObject.Windows;
            // Объект указателя места заполнения; в конечном итоге ссылается 
            //на пользовательский элемент управления, находящийся 
            // в пользовательском элементе управления, 
            object rtfTextObject = null;
            // Этот раздел указывает путь и имя класса для элемента управления 
            // , который будет находиться в новом окне инструмента; 
            // нам нужно также указать его заголовок и уникальный GUID. 
            Window toolWindow;
            Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string assemblyPath = asm.Location;
            string className = "Assist.RTFTextControl";
            string guid = "{62175059-FD7E-407a-9EF3-5D07F2B704E8}";
            string caption = "Assist";
            try
            {
                // Создать новое окно инструмента и вставить в него 
                // пользовательский элемент управления. 
                toolWindow = windows.CreateToolWindow2(_addInInstance,
                assemblyPath, className, caption, guid, ref rtfTextObject);
                // Если окно инструмента было создано успешно, сделать его видимым 
                if (toolWindow != null)
                {
                    toolWindow.Visible = true;
                }
                // Получить ссылку на объект пользовательского элемента управления 
                _rtfTextControl = (RTFTextControl)rtfTextObject;

                _rtfTextControl.GoToLine += GoToLine;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception caught: " + ex.ToString());
            }
        }
        #endregion



        #region SELECT_MATCHES_METHODS

        public void GoToLine(int lineNumber)
        {
            try
            {
                DTEUtils.GoToLine(_applicationObject, lineNumber);
            }
            catch (Exception)
            {
            }
        }
        #endregion



    }
}