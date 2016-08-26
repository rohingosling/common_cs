//--------------------------------------------------------------------------------
//
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace WhiteBoardCapturer
{
    public class SaveFileDialogExtended
    {   
        //--------------------------------------------------------------------------------
        // CLass member variables.
        //--------------------------------------------------------------------------------

        string fileName               = string.Empty;   // File name.
        string filter                 = string.Empty;   // Filter string.
        string defaultExtention       = string.Empty;   // Default file extention.  
        int    filterIndex            = 0;              // Last selected filter index.
        int    handleDialogBox        = 0;              // Handle to the dialog box.
        int    handleComboBoxFileName = 0;              // Handle to the file name combo box in the dialog box.
        int    handleComboBoxFileType = 0;              // HAndle to the file filter combo box in the dialog box.
        Screen activeDesktop;                           // Windows desktop.

        #region Properties

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public string DefaultExtention
	    {
		    get
		    {
			    return this.defaultExtention;
		    }
		    set
		    {
			    this.defaultExtention = value;
		    }
	    }

        public string FileName
	    {
		    get
		    {
			    return this.fileName;
		    }
		    set
		    {
			    this.fileName = value;
		    }
	    }

        public string Filter
	    {
		    get
		    {
			    return this.filter;
		    }
		    set
		    {
			    this.filter = value;
		    }
	    }

        public int FilterIndex
	    {
		    get
		    {
			    return this.filterIndex;
		    }
		    set
		    {
                this.filterIndex = value;
		    }
	    }

        #endregion

        //--------------------------------------------------------------------------------
        // HookProc
        //
        // dialogBox
        //
        //   A handle to the common save/open file dialog box.
        //
        // windowMessage
        //
        //   The message code that references a particular windows message event.
        //
        // messageParameter1 ( wParam )
        //
        //   General purpose parameter ascociated with the window message.
        //   In the windows API reference meterial this argument is usualy refered to
        //   as wParam.
        //
        // messageParameter2 ( lParam )
        //
        //   General purpose parameter ascociated with the window message.
        //   In the windows API reference meterial this argument is usualy refered to
        //   as lParam.  
        //
        //--------------------------------------------------------------------------------

        private int HookProc ( int dialogBox, int windowMessage, int messageParameter1, int messageParameter2 )
	    {
		    switch ( windowMessage )
		    {
			    case Win32.WM_INITDIALOG:
                
                    // Get a handel to the dialog box.

                    this.handleDialogBox = Win32.GetParent ( dialogBox );

				    // Get dimentions of active desktop and the dialog box.

				    Rectangle       rectangleDesktop   = activeDesktop.Bounds;      // Use a .NET rectangle class to store the desktop dimentions.	
			    	Win32.Rectangle rectangleDialogBox = new Win32.Rectangle();     // USe a Win32 rectanlge struct to store the dialog box dimentions.
                    int             parentWindow       = this.handleDialogBox;      // Get a Win32 handle on the active parent window.

				    Win32.GetWindowRect ( parentWindow, ref rectangleDialogBox );

                    // Set the position of the dialog box to be in the center of the screen.

                    int screenWidth     = rectangleDesktop.Right    - rectangleDesktop.Left;
                    int screenHeight    = rectangleDesktop.Bottom   - rectangleDesktop.Top;
                    int dialogBoxWidth  = rectangleDialogBox.Right  - rectangleDialogBox.Left;
                    int dialogBoxHeight = rectangleDialogBox.Bottom - rectangleDialogBox.Top;
				    int dialogBoxLeft   = ( screenWidth  - dialogBoxWidth  ) / 2;
				    int dialogBoxTop    = ( screenHeight - dialogBoxHeight ) / 2;                    

				    Win32.SetWindowPos ( parentWindow, 0, dialogBoxLeft, dialogBoxTop, dialogBoxWidth, dialogBoxHeight, Win32.SWP_NOZORDER );

                    break;

			    case Win32.WM_NOTIFY:

				    // Intercept the CDN_TYPECHANGE message sent when the user changes the file type.

				    Win32.NotificationMessage notificationMessage = (Win32.NotificationMessage) Marshal.PtrToStructure ( new IntPtr ( messageParameter2 ), typeof ( Win32.NotificationMessage ) );

				    if ( notificationMessage.Code == Win32.CDN_TYPECHANGE )
				    {   
                        // Get handels to the file name and file type combo boxes.

                        this.handleDialogBox        = Win32.GetParent  ( dialogBox );                       // Get a handel to the dialog box. 
                        this.handleComboBoxFileName = Win32.GetDlgItem ( this.handleDialogBox, 0x47c );     // Get a handel to the file name combo box.
                        this.handleComboBoxFileType = Win32.GetDlgItem ( this.handleDialogBox, 0x470 );     // Get a handel to the file type combo box.

                        // Get the current file name from the file name combo box.

                        StringBuilder fileName = new StringBuilder ( 255 );
                        Win32.GetWindowText ( handleComboBoxFileName, fileName, fileName.Capacity );

                        // Get the currently selected filter index and file name.
                        
                        this.filterIndex   = 1 + Win32.SendMessage ( this.handleComboBoxFileType, Win32.CB_GETCURSEL, 0, 0);
                        string newFileName = string.Empty;

                        // Set the file extention based on the currently selected file type.
                        
                        if ( this.filterIndex == 1 ) newFileName = Path.ChangeExtension ( fileName.ToString(), ".jpg" );
                        if ( this.filterIndex == 2 ) newFileName = Path.ChangeExtension ( fileName.ToString(), ".png" );
                        if ( this.filterIndex == 3 ) newFileName = fileName.ToString();

					    Win32.SetWindowText ( this.handleComboBoxFileName, newFileName );
				    }
				    break;

                case Win32.WM_SHOWWINDOW:
                    
                    break;

                case Win32.WM_DESTROY:
                    break;
		    }
		    return 0;
	    }

        //--------------------------------------------------------------------------------
        // ShowDialog
        //--------------------------------------------------------------------------------

        public DialogResult ShowDialog()
	    {
            // we need to find out the active screen so the dialog box is centred on the correct display.

		    this.activeDesktop = Screen.FromControl ( Form.ActiveForm );

		    // Set up the struct and populate it.

		    Win32.OPENFILENAME openFileName = new Win32.OPENFILENAME();

            openFileName.hwndOwner      = Form.ActiveForm.Handle;
            openFileName.lpfnHook       = new Win32.OFNHookProcDelegate ( HookProc );
		    openFileName.lStructSize    = Marshal.SizeOf ( openFileName );
		    openFileName.lpstrFilter    = this.filter.Replace ('|', '\0') + '\0';
		    openFileName.lpstrFile      = this.fileName + new string (' ', 512 );
		    openFileName.nMaxFile       = openFileName.lpstrFile.Length;
		    openFileName.lpstrFileTitle = Path.GetFileName ( this.fileName ) + new string (' ', 512 );
		    openFileName.nMaxFileTitle  = openFileName.lpstrFileTitle.Length;
		    openFileName.lpstrTitle     = "Save File As";
		    openFileName.lpstrDefExt    = this.defaultExtention;            
            openFileName.nFilterIndex   = this.filterIndex;
		    openFileName.Flags          = Win32.OFN_EXPLORER | 
                                          Win32.OFN_PATHMUSTEXIST | 
                                          Win32.OFN_NOTESTFILECREATE | 
                                          Win32.OFN_ENABLEHOOK | 
                                          Win32.OFN_HIDEREADONLY | 
                                          Win32.OFN_OVERWRITEPROMPT;                
		    
		    // If we're running on Windows 98/ME then the struct is smaller by the size of the last 3 fields in the Win32.OPENFILENAME struct.

		    if ( System.Environment.OSVersion.Platform != PlatformID.Win32NT )
		    {
			    openFileName.lStructSize -= 3 * sizeof ( int );
		    }

		    // Show the dialog box.

		    if ( !Win32.GetSaveFileName ( ref openFileName ) )
		    {
			    int ret = Win32.CommDlgExtendedError();

			    if ( ret != 0 )
			    {
				    throw new ApplicationException ( "Couldn't show file open dialog - " + ret.ToString() );
			    }

			    return DialogResult.Cancel;
		    }

		    this.fileName    = openFileName.lpstrFile;
            this.filterIndex = openFileName.nFilterIndex;

		    return DialogResult.OK;
	    }
    }
}
