using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WhiteBoardCapturer
{   
    static class Win32
    {
        #region Delegates

        //--------------------------------------------------------------------------------
        // Delegates
        //--------------------------------------------------------------------------------

        public delegate int OFNHookProcDelegate ( int dialogBox, int windowsMessage, int messageParameter1, int messageParameter2 );

        #endregion

        #region Constants

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        // General Windows messages.

	    public const int WM_INITDIALOG = 0x110;
        public const int WM_NOTIFY     = 0x004E;
        public const int WM_USER       = 0x0400;
        public const int WM_DESTROY    = 0x0002;
        public const int WM_CLOSE      = 0x0010;
        public const int WM_SHOWWINDOW = 0x0018;

        // Window position and sizing parameters.

	    public const int SWP_NOSIZE   = 0x0001;
	    public const int SWP_NOMOVE   = 0x0002;
	    public const int SWP_NOZORDER = 0x0004;

        // Window style parameters

	    public const int WS_VISIBLE = 0x10000000;
	    public const int WS_CHILD   = 0x40000000;
	    public const int WS_TABSTOP = 0x00010000;

        // Open File Name parameters.

        public const int OFN_ENABLEHOOK         = 0x00000020;
	    public const int OFN_EXPLORER           = 0x00080000;
	    public const int OFN_FILEMUSTEXIST      = 0x00001000;
	    public const int OFN_HIDEREADONLY       = 0x00000004;
	    public const int OFN_CREATEPROMPT       = 0x00002000;
	    public const int OFN_NOTESTFILECREATE   = 0x00010000;
	    public const int OFN_OVERWRITEPROMPT    = 0x00000002;
	    public const int OFN_PATHMUSTEXIST      = 0x00000800;
        public const int OFN_EXTENSIONDIFFERENT = 0x00000400;

        // ComboBox control messages.

	    public const int CB_SETCURSEL     = 0x014E;
	    public const int CB_GETCURSEL     = 0x0147;
        
        // Common dialog notifications.

        public const int CDN_FIRST      = -601;
        public const int CDN_SELCHANGE  = CDN_FIRST - 0x0001;
	    public const int CDN_FILEOK     = CDN_FIRST - 0x0005;
        public const int CDN_TYPECHANGE = CDN_FIRST - 0x0006;

        // Common dialog messages

        public const int CDM_FIRST          = WM_USER   + 0x0064;
        public const int CDM_SETCONTROLTEXT = CDM_FIRST + 0x0004;
        

        // dlgs.h 

        public const int cmb13 = 0x47c;     // Drop-down combo box that displays the name of the current file.

        #endregion

        #region Structs

        //--------------------------------------------------------------------------------
        // Structs
        //--------------------------------------------------------------------------------

        // OPENFILENAME is used by the Windows API function GetSaveFileName.

        [ StructLayout ( LayoutKind.Sequential, CharSet = CharSet.Auto ) ]
	    public struct OPENFILENAME
	    {
		                                           public int                 lStructSize;          //
		                                           public IntPtr              hwndOwner;            //
		                                           public int                 hInstance;            //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrFilter;          //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrCustomFilter;    //
		                                           public int                 nMaxCustFilter;       //
		                                           public int                 nFilterIndex;         //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrFile;            //
		                                           public int                 nMaxFile;             //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrFileTitle;       //
		                                           public int                 nMaxFileTitle;        //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrInitialDir;      //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrTitle;           //
		                                           public int                 Flags;                //
		                                           public short               nFileOffset;          //
		                                           public short               nFileExtension;       //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpstrDefExt;          //
		                                           public int                 lCustData;            //
		                                           public OFNHookProcDelegate lpfnHook;             //
		    [ MarshalAs ( UnmanagedType.LPTStr ) ] public string              lpTemplateName;       //

                                                   // Only used for Windows NT 5.0 and above.

		                                           public int                 pvReserved;           //
		                                           public int                 dwReserved;           //
		                                           public int                 FlagsEx;              //
        }

        // Win32 Notification message struc.
        
	    public struct NotificationMessage 
	    {
		    public int HwndFrom;    // Window handle to the control sending the message.
		    public int IdFrom;      // An Identifier of the control sending the message.
		    public int Code;        // Notification code.
	    }

        // Win32 Rectangle struct.

        public struct Rectangle
	    {
		    public int Left;
		    public int Top;
		    public int Right;
		    public int Bottom;
	    }

        #endregion

        #region External Win32 API Functions
        //--------------------------------------------------------------------------------
        // External Win32 API functions.
        //--------------------------------------------------------------------------------

        [ DllImport ( "Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true ) ]
	    public static extern bool GetSaveFileName ( ref OPENFILENAME lpofn );

	    [ DllImport ( "Comdlg32.dll" ) ]
	    public static extern int CommDlgExtendedError();

	    [ DllImport ( "user32.dll" ) ]
	    public static extern bool SetWindowPos ( int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags );

        [ DllImport ( "user32.dll" ) ]
	    public static extern bool GetWindowRect ( int hWnd, ref Rectangle lpRect );

	    [ DllImport ( "user32.dll" ) ]
	    public static extern int GetParent ( int hWnd );

	    [ DllImport ( "user32.dll", CharSet = CharSet.Auto ) ]
	    public static extern bool SetWindowText ( int hWnd, string lpString );

        [ DllImport ( "user32.dll", CharSet = CharSet.Auto ) ]
	    public static extern int GetWindowText ( int hWnd, StringBuilder lpString, int nMaxCount );     // What is this???

	    [ DllImport ( "user32.dll" ) ]
	    public static extern int SendMessage ( int hWnd, int Msg, int wParam, int lParam );

	    [ DllImport ( "user32.dll", CharSet = CharSet.Auto ) ]
	    public static extern int SendMessage ( int hWnd, int Msg, int wParam, string lParam );

	    [ DllImport ( "user32.dll" ) ]
	    public static extern bool DestroyWindow ( int hwnd );

        [ DllImport ( "user32.dll", CharSet = CharSet.Auto ) ]
	    public static extern int GetDlgItem ( int hDlg, int nIDDlgItem );

	    [ DllImport ( "user32.dll", CharSet = CharSet.Auto ) ]
	    public static extern int CreateWindowEx ( int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int hMenu, int hInstance, int lpParam );

        #endregion
    }
}
