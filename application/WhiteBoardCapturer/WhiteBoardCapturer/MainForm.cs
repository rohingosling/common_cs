//--------------------------------------------------------------------------------
//
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Drawing.Imaging;

namespace WhiteBoardCapturer
{

	public partial class FormMain : Form
	{	
        //--------------------------------------------------------------------------------
		// Member variables
		//--------------------------------------------------------------------------------

        ApplicationState applicationState;    		
        FileInfo         fileInfo;
        FileFilterIndex  fileFilterIndex;
        ImageIndex       activeImageIndex;        
		string           applicationName;        
		float            imageScale;	
		float            imageScaleDelta;	
		float            imageScaleMax;
		float            imageScaleMin;	
		float            xAspect;
		float            yAspect;		
		int              workspaceMargin;
	    int              shadowSize;
        bool             shadowVisible;
		bool 		     fileOpen;
		bool             mouseZoomEnabled;
        bool             imageProcessed;
		
		//--------------------------------------------------------------------------------
		// Constructor
		//--------------------------------------------------------------------------------
		
		public FormMain()
		{
            // Inisialise application.

			InitializeComponent ();            
            SetApplicationState ( ApplicationState.START_UP );
			
			// Disable controls that are not in use in this version.
			
			this.toolStripButtonZoomOut.Visible = false;		// Not available in the first virsion.
			this.toolStripButtonZoomIn.Visible  = false;		// Not available in the first version.
			this.toolStripComboBoxZoom.Visible  = false;		// Not avaialble in the first version.
			this.toolStripSeparator2.Visible    = false;		// Not avaialble in the first version.
		}

        //--------------------------------------------------------------------------------
		// SetApplicationState
        //
        // Description:
        //
        //   Sets the application parameters acording to the specified state.
		//--------------------------------------------------------------------------------

        public void SetApplicationState ( ApplicationState applicationState )
        {
            // Set application parameters acording to the requested application state.

            switch ( applicationState )
            {   
                case ApplicationState.IDLE:
                    break;

                case ApplicationState.START_UP: 

                    SetApplicationStateStartUp ();
                    break;

                case ApplicationState.SHUT_DOWN:
                    break;

                case ApplicationState.FILE_OPEN: 

                    SetApplicationStateFileOpen ( true );
                    break;

                case ApplicationState.FILE_CLOSED: 

                    SetApplicationStateFileOpen ( false );
                    break;

                case ApplicationState.IMAGE_PROCESSING_IN_PROGRESS:

                    SetProgressBarVisibility ( true );
                    this.applicationState = ApplicationState.IMAGE_PROCESSING_IN_PROGRESS;
                    this.toolStripProgressBarProcessingImage.Tag = 0;
                    break;

                case ApplicationState.IMAGE_PROCESSED:

                    SetProgressBarVisibility ( false );
                    this.applicationState = ApplicationState.IMAGE_PROCESSED;
                    this.toolStripProgressBarProcessingImage.Tag = 0;
                    break;
            }

            // Update application state

            this.applicationState = applicationState;
        }

        //--------------------------------------------------------------------------------
		// SetApplicationStateStartUp
        //
        // Description:
        //
        //   Resets all appication parameters to there default start up settings.
        //
		//--------------------------------------------------------------------------------

        void SetApplicationStateStartUp ()
        {
            // Initialise application parameters
			
            this.applicationState = ApplicationState.IDLE;			
			this.applicationName  = Properties.Settings.Default.ApplicationName;			
			this.imageScaleMax    = Properties.Settings.Default.ImageScaleMax;
			this.imageScaleMin    = Properties.Settings.Default.ImageScaleMin;
			this.imageScale       = Properties.Settings.Default.ImageScale;	
			this.imageScaleDelta  = 0.05f;						
			this.xAspect          = 0.0f;
			this.yAspect          = 0.0f;
			this.workspaceMargin  = Properties.Settings.Default.WorkspaceMargin;
            this.shadowSize       = Properties.Settings.Default.shadowSize;
            this.shadowVisible    = Properties.Settings.Default.shadowVisible;
            this.fileFilterIndex  = FileFilterIndex.ALL_FILES;
            this.activeImageIndex = ImageIndex.SOURCE;            
	
            // Set application flags

			this.fileOpen         = false;
			this.mouseZoomEnabled = false;
            this.imageProcessed   = false;
			
			// Initialise controls
			
            SetFormTitleFileName ( string.Empty );

			float percentage                = ( this.imageScale / this.imageScaleMax ) * this.imageScaleMax * 100.0f;
			this.toolStripComboBoxZoom.Text = percentage.ToString ( "." ) + " %";            
			
			// Menu control enablement.
			
			this.menuItemFileClose.Enabled  = false;
			this.menuItemFileSave.Enabled   = false;
			this.menuItemFileSaveAs.Enabled = false;
			
			// Toolstrip control enablement.
			
			this.toolStripButtonSave.Enabled         = false;
			this.toolStripButtonSaveAs.Enabled       = false;
			this.toolStripButtonZoomIn.Enabled       = false;
			this.toolStripButtonZoomOut.Enabled      = false;
			this.toolStripComboBoxZoom.Enabled       = false;
            this.toolStripButtonProcessImage.Enabled = false;

            // Set control visibility.

            this.toolStripStatusLabelImageSize.Visible   = false;
            this.toolStripStatusLabelImageWidth.Visible  = false;
            this.toolStripStatusLabelImageHeight.Visible = false;

            // Set control paramters

            this.toolStripButtonProcessImage.CheckState = CheckState.Unchecked;
            SetProgressBarVisibility ( false ); 
            this.panelWorkSpace.Focus();            
        }

        //--------------------------------------------------------------------------------
		// SetApplicationStateFileOpen
        //
        // Description:
        //
        //   Sets the application state to FILE_OPEN or FILE_CLOSE.
        //
        // Arguments:
        //
        //   enabled:
        //
        //     IF enabled = true,  then application state = FILE_OPEN.
        //     If enabled = false, then application state = FILE_CLOSED;
        //
		//--------------------------------------------------------------------------------
		
		void SetApplicationStateFileOpen ( bool enabled )
		{
			// Menu control enablement.
			
			this.menuItemFileClose.Enabled  = enabled;
			this.menuItemFileSave.Enabled   = enabled;
			this.menuItemFileSaveAs.Enabled = enabled;
			
			// Toolstrip control enablement.
			
			this.toolStripButtonSave.Enabled         = enabled;
			this.toolStripButtonSaveAs.Enabled       = enabled;
			this.toolStripButtonZoomIn.Enabled       = enabled;
			this.toolStripButtonZoomOut.Enabled      = enabled;
			this.toolStripComboBoxZoom.Enabled       = enabled;
            this.toolStripButtonProcessImage.Enabled = enabled;

            // Set control visibility

            this.toolStripStatusLabelImageSize.Visible   = enabled;            
            this.toolStripStatusLabelImageWidth.Visible  = enabled;
            this.toolStripStatusLabelImageHeight.Visible = enabled;

            this.toolStripStatusLabelSpacer1.Visible = enabled;
            this.toolStripStatusLabelSpacer2.Visible = enabled;
            this.toolStripStatusLabelSpacer3.Visible = enabled;

            SetProgressBarVisibility ( false ); 

            // Set control states

            if ( !enabled )
            {
                this.toolStripButtonProcessImage.CheckState = CheckState.Unchecked;
            }
		}

        //--------------------------------------------------------------------------------
        // ShowException
        //
        // Descritpion:
        //   
        //   Display an exception in a message box.
        //
        //--------------------------------------------------------------------------------

        void ShowException ( Exception exception, string description )
        {
            string exceptionString = string.Empty;
            exceptionString += "Description:\n    "     + description              + "\n\n";
            exceptionString += "Exception:\n    "       + exception.Message        + "\n\n";
            exceptionString += "Inner Exception:\n    " + exception.InnerException + "\n\n";
            exceptionString += "Target Site:\n    "     + exception.TargetSite     + "\n\n";
            exceptionString += "Source:\n    "          + exception.Source         + "\n\n";
            exceptionString += "Stack Trace:\n"         + exception.StackTrace;

            MessageBox.Show ( exceptionString, "Exception" );
        }
		
		//--------------------------------------------------------------------------------
		// ToolStripButtonTest_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Test
		//--------------------------------------------------------------------------------
		
		void ToolStripButtonTest_Click ( object sender, EventArgs e )
		{
            ProcessImage ();
		}

        //--------------------------------------------------------------------------------
		// ProcessImage
        //
        // Description:
        //
        //   Perfrom a series of image processing steps.
		//--------------------------------------------------------------------------------

        void ProcessImage ()
        {
            if ( this.fileOpen )
			{	
				// Create a source and destination bitmaps.

                Image  imageSource       = this.pictureBoxSource.Image;				
				Size   imageSize         = new Size   ( imageSource.Width, imageSource.Height );				
				Bitmap bitmapSource      = new Bitmap ( imageSource, imageSize );
                Bitmap bitmapDestination = new Bitmap ( bitmapSource.Width, bitmapSource.Height, bitmapSource.PixelFormat );

                // Initialise progress bar

                int filterPassCount = 1;

                this.toolStripProgressBarProcessingImage.Maximum = imageSource.Height * filterPassCount;
                this.toolStripProgressBarProcessingImage.Step    = 1;
                this.toolStripProgressBarProcessingImage.Value   = 0;

    			// Apply image filter.

                ImageFilter imageFilter = new ImageFilter ();
                double      deviation   = 1.5;
                
                imageFilter.Greyscale    ( bitmapSource,      bitmapDestination, this.toolStripProgressBarProcessingImage );
                imageFilter.GaussianBlur ( bitmapDestination, bitmapDestination, deviation, ImageFilter.Orientation.HORIZONTAL, this.toolStripProgressBarProcessingImage );
                imageFilter.GaussianBlur ( bitmapDestination, bitmapDestination, deviation, ImageFilter.Orientation.VERTICAL,   this.toolStripProgressBarProcessingImage );
                //imageFilter.WhiteboardPen ( bitmapDestination, bitmapDestination, this.toolStripProgressBarProcessingImage );
                				
				// Assign the processed bitmap to the destination picturebox.
								
                this.pictureBoxDestination.Image = (Image) bitmapDestination;

                // Set image processed flag

                this.imageProcessed = true;
			}
        }
		
		//--------------------------------------------------------------------------------
		//
		//--------------------------------------------------------------------------------
		
		void ZoomIn ()
		{
			float fastZoom            = 5.0f;
			float scaleDelta          = 0.0f;
			float scaleDeltaThreshold = 1.0f;
			
			
			// Calculate the scale delta. 
			// Zoom faster abouve a specified threshold, say 100%, or what ever.
			
			if ( this.imageScale <= scaleDeltaThreshold )
			{
				scaleDelta = this.imageScaleDelta;
			}
			else
			{
				scaleDelta = this.imageScaleDelta * fastZoom;
			}			
			
			// Update zoom.
			
			this.imageScale += scaleDelta;

			UpdateScale ();
			
			float percentage = ( this.imageScale / this.imageScaleMax ) * this.imageScaleMax * 100.0f;
			this.toolStripComboBoxZoom.Text = percentage.ToString ( "." ) + " %";
			
			
			// Update scroll position
			
			this.panelWorkSpace.HorizontalScroll.Value = 0;
			this.panelWorkSpace.VerticalScroll.Value   = 0;
		}
		
		//--------------------------------------------------------------------------------
		// ZoomOut
        //
        // Description:
        //
        //   Set the scale of the active PictureBox to zoom out one zoom step.
        //
		//--------------------------------------------------------------------------------
		
		void ZoomOut ()
		{
            // Initialise zoom parameters

			float fastZoom            = 5.0f;
			float scaleDelta          = 0.0f;
			float scaleDeltaThreshold = 1.0f;
			
			// Calculate the scale delta. 
			// Zoom faster abouve a specified threshold, say 100%, or what ever.
			
			if ( this.imageScale <= scaleDeltaThreshold )
			{
				scaleDelta = this.imageScaleDelta;
			}
			else
			{
				scaleDelta = this.imageScaleDelta * fastZoom;
			}
			
			// Update zoom scale.
			
			this.imageScale -= scaleDelta;

			UpdateScale ();
			
			float percentage = ( this.imageScale / this.imageScaleMax ) * this.imageScaleMax * 100.0f;
			this.toolStripComboBoxZoom.Text = percentage.ToString ( "." ) + " %";
			
			// Update scroll position
			
			this.panelWorkSpace.HorizontalScroll.Value = 0;
			this.panelWorkSpace.VerticalScroll.Value   = 0;
		}
		
		//--------------------------------------------------------------------------------
		// pictureBox_MouseWheel
        //
        // Description:
        //
        //   PictureBox - Mouse Wheel Event
        //
        //   If the mouse wheel moves up then zoom in.
        //   If the mouse wheel moves down then zoom out.
        //
		//--------------------------------------------------------------------------------
		
		void pictureBox_MouseWheel ( object sender, MouseEventArgs e )
		{		
	        // We only zoom in or out if there is an image loaded, and mouse zooming is enabled.

			if ( ( this.pictureBoxSource != null ) && ( this.mouseZoomEnabled ) )
			{	
				if ( e.Delta > 0 )
				{	
					ZoomIn ();          // Zoom in if the mouse wheel is moved up ( mouse wheel delta > 0 ).
				}
				else
				{
					ZoomOut ();         // Zoom out if the mouse wheel is moved down ( mouse wheel delta <= 0 ).
				}
			}		
		}
		
		//--------------------------------------------------------------------------------
		// OpenToolStripMenuItem_Click
        //
        // Description:
        //
        //   Main menu - File / Open File
		//--------------------------------------------------------------------------------
		
		void OpenToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			OpenFile ();			
		}
		
		//--------------------------------------------------------------------------------
		// ToolStripButtonOpen_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Open File.
        //
		//--------------------------------------------------------------------------------
		
		void ToolStripButtonOpen_Click ( object sender, EventArgs e )
		{
			OpenFile ();
		}

        //--------------------------------------------------------------------------------
		// menuItemFileSaveAs_Click
        //
        // Description:
        //
        //   Menu Item - File / Save As... - Save File As.
        //
		//--------------------------------------------------------------------------------

        private void menuItemFileSaveAs_Click ( object sender, EventArgs e )
        {
            SaveFileAs ();
        }

         //--------------------------------------------------------------------------------
		// toolStripButtonSaveAs_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Save File As.
        //
		//--------------------------------------------------------------------------------

        private void toolStripButtonSaveAs_Click ( object sender, EventArgs e )
        {
            SaveFileAs ();
        }

        //--------------------------------------------------------------------------------
        //
        //--------------------------------------------------------------------------------

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFile ( this.fileInfo.FullName );
        }

        //--------------------------------------------------------------------------------
        //
        //--------------------------------------------------------------------------------

        private void menuItemFileSave_Click(object sender, EventArgs e)
        {
            SaveFile ( this.fileInfo.FullName );
        }

        //--------------------------------------------------------------------------------
		// SaveFile
        //
        // Description:
        //
        //   Save the current image to the specified file name using the image format 
        //   supplied through imageFormat.
        //
        //   The currently active image is used as the source for the image to save.
        //
		//--------------------------------------------------------------------------------
		
        public void SaveFile ( string fileName, ImageFormat imageFormat )
        {
            try
            {
                // Save image.

                if ( this.activeImageIndex == ImageIndex.SOURCE      ) this.pictureBoxSource.Image.Save      ( fileName, imageFormat );
                if ( this.activeImageIndex == ImageIndex.DESTINATION ) this.pictureBoxDestination.Image.Save ( fileName, imageFormat );
                
                // Update file info.

                this.fileInfo = new FileInfo ( fileName ); 
            }
            catch ( Exception exception )
            {                
                ShowException ( exception, MethodInfo.GetCurrentMethod().Name );
            }
        }

        //--------------------------------------------------------------------------------
		// SaveFile
        //
        // Description:
        //
        //   Save the current image to the specified file name using the image format 
        //   of the currently loaded image.
        //
		//--------------------------------------------------------------------------------

        public void SaveFile ( string fileName )
        {
            try
            {
                // Set the image format to the image format of the current source image.

                ImageFormat imageFormat = null;

                if ( this.fileFilterIndex == FileFilterIndex.JPEG ) imageFormat = ImageFormat.Jpeg;
                if ( this.fileFilterIndex == FileFilterIndex.PNG  ) imageFormat = ImageFormat.Png;
            
                // Save the image.

                SaveFile ( fileName, imageFormat );
            }
            catch ( Exception exception )
            {                
                ShowException ( exception, MethodInfo.GetCurrentMethod().Name );
            }
        }

        //--------------------------------------------------------------------------------
		// SaveFileAs
        //
        // Description:
        //
        //   - Open the standard Windows save file dialog.
        //   - Save the current destination image to the file name entered by the user.
        //
		//--------------------------------------------------------------------------------
		
		void SaveFileAs ()
        {
            try
            {
                // Only attempt to save a file if we already have an image loaded.

                if ( this.fileOpen )
                {
                    // Open the save file dialog box and get a file name to save to from the user.

                    SaveFileDialogExtended saveFileDialog = new SaveFileDialogExtended();
                    saveFileDialog.FileName               = this.fileInfo.Name;
                    saveFileDialog.FilterIndex            = (int) this.fileFilterIndex;
                    saveFileDialog.Filter                 = "JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All files (*.*)|*.*";

                    DialogResult dialogResult = saveFileDialog.ShowDialog ();

                    string fileName = saveFileDialog.FileName;

                    // If we have selected a file, then save it to the new file.

                    if ( ( dialogResult == DialogResult.OK ) && ( fileName != string.Empty ) )
                    {
                        // Save the file using the user selected format.

                        if ( saveFileDialog.FilterIndex == (int) FileFilterIndex.JPEG )
                        {   
                            SaveFile ( fileName, ImageFormat.Jpeg );
                            this.fileFilterIndex = FileFilterIndex.JPEG;
                        }

                        if ( saveFileDialog.FilterIndex == (int) FileFilterIndex.PNG )
                        {
                            SaveFile ( fileName, ImageFormat.Png );
                            this.fileFilterIndex = FileFilterIndex.PNG;
                        }

                        // Update form title to reflect new file name.
                        
                        SetFormTitleFileName ( this.fileInfo );
                    }
                }
            }
            catch ( Exception exception )
            {                
                ShowException ( exception, MethodInfo.GetCurrentMethod().Name );
            }
        }
		
		//--------------------------------------------------------------------------------
		// OpenFile
        //
        // Description:
        //
        //   - Open the standard Windows open file dialog.
        //   - Open the selected image file, if a file was selected from the open file dialog.
        //
		//--------------------------------------------------------------------------------
		
		void OpenFile ()
		{
            try
            {
			    // Close existing file.
	
                CloseFile ();
			
			    // Open the open file dialog box.
			
                openFileDialog.FileName = string.Empty;
			    openFileDialog.ShowDialog ();

                // If we have slected a file, then load it into the application.

                if ( openFileDialog.FileName != string.Empty )
                {			
			        // Load file into a temporary image object.

                    string     fileName  = openFileDialog.FileName;	            
                    FileStream imageFile = new FileStream   ( fileName, FileMode.Open );
                    Image      image     = Image.FromStream ( imageFile );
                    imageFile.Close();

                    // Update file info.

                    this.fileInfo = new FileInfo ( fileName ); 

                    if      ( image.RawFormat.Guid == ImageFormat.Jpeg.Guid ) this.fileFilterIndex = FileFilterIndex.JPEG;
                    else if ( image.RawFormat.Guid == ImageFormat.Png.Guid  ) this.fileFilterIndex = FileFilterIndex.PNG;

                    // Convert pixel format to 32bit ARBG if required. e.g. JPEG's need to be converted from RGB to ARGB.

                    if ( this.fileFilterIndex == FileFilterIndex.JPEG )
                    {
                        Bitmap oldImage = new Bitmap ( image );
                        Bitmap newImage = oldImage.Clone ( new Rectangle ( 0, 0, oldImage.Width, oldImage.Height ), PixelFormat.Format32bppArgb );
                        image           = (Image) newImage;
                    }

                    // Assign the image to the picture boxes.

                    this.pictureBoxSource.Image      = image;
                    this.pictureBoxDestination.Image = image;
			
			        // Calculate image aspect ratios.
			
			        float width  = image.Width;
			        float height = image.Height;
			
			        this.xAspect = width / height;
			        this.yAspect = height / width;
			
			        // Display the image.
			
			        ShowImage ( this.pictureBoxSource.Image );	

                    // Update form labels.

                    SetFormTitleFileName       ( fileInfo );
                    SetToolStripFileParameters ( fileInfo );

                    // Update menu and tool strip visibility
			
			        SetApplicationStateFileOpen ( true );

                    // Set application flags

			        this.fileOpen = true;	
		        }
            }
            catch ( Exception exception )
            {
                ShowException ( exception, MethodInfo.GetCurrentMethod().Name );
            }
		}

        //--------------------------------------------------------------------------------
		// SetFormTitleFileName
        //
        // Description:
        // 
        //   Update the forms title bar to show the name of the currently loaded file.
        //
        // Arguments:
        //
        //   Overload 1: fileName:
        //
        //     The name of the file who's name to display.
        //     If fileName is an empty string, then no file name will be displayed.
        //
        //   Overload 2: file:
        //
        //     A FileInfo object initialised with the currently loaded image file data.
        //
		//--------------------------------------------------------------------------------

        public void SetFormTitleFileName ( string fileName )
        {   
            if ( fileName != string.Empty )
            {
                // Display the title as, "Application Name - File Name.Extention"

                string labelSeperator = Properties.Resources.labelSeperator;
			    this.Text             = this.applicationName + labelSeperator + fileName;
            }
            else
            {
                // Display the title as, "Application Name"

                this.Text = this.applicationName;
            }
        }

        //--------------------------------------------------------------------------------

        public void SetFormTitleFileName ( FileInfo file )
        {   
            SetFormTitleFileName ( file.Name );
        }

        //--------------------------------------------------------------------------------
		// SetToolStripFileParameters
        //
        // Description:
        //
        //   Sets the tool strip data to reflect the currently loaded image parameters.
        //
        // Arguments:
        //
        //   file:
        //
        //     A FileInfo object initialised with the currently loaded image file data.
        //
		//--------------------------------------------------------------------------------

        public void SetToolStripFileParameters ( FileInfo file )
        {
            // Initialise label strings

            string labelFileLength       = Properties.Resources.labelFileSize;
            string labelWidth            = Properties.Resources.labelImageWidth;
            string labelHeight           = Properties.Resources.labelImageHeight;
            string labelSpace            = Properties.Resources.labelSpace;
            string labelOpenParenthesus  = Properties.Resources.labelParenthesusOpen;
            string labelCloseParenthesus = Properties.Resources.labelParenthesusClose;                    
            string labelUnitKilloByte    = Properties.Resources.labelKiloBytes;
            string labelSeperator        = Properties.Resources.labelSeperator;

            // File length in KB

            int    kiloByte             = 1024;
            long   fileLength           = file.Length;
            float  fFileLengthKB        = fileLength / kiloByte;
            long   fileLengthKB         = (long) Math.Ceiling ( fFileLengthKB );  
            string fileSizeNumberFormat = Properties.Settings.Default.ToolstripFileSizeNumberFormat;      
            string sfileLengthKB        = string.Format ( fileSizeNumberFormat, fileLengthKB );

            // File length in bytes.

            string sfileLength = fileLength.ToString();

            // Compile file length string.

            string fileLengthString  = string.Empty;
            fileLengthString += labelFileLength      + labelSpace;
            fileLengthString += sfileLength          + labelSpace;
            fileLengthString += labelOpenParenthesus + labelSpace;
            fileLengthString += sfileLengthKB        + labelSpace + labelUnitKilloByte + labelSpace;
            fileLengthString += labelCloseParenthesus;

            this.statusStrip.Items [ toolStripStatusLabelImageSize.Name ].Text = fileLengthString;

            // Image width and hieght dimentions.

            int width  = this.pictureBoxSource.Image.Width;
			int height = this.pictureBoxSource.Image.Height;

            this.statusStrip.Items [ toolStripStatusLabelImageWidth.Name  ].Text = labelWidth  + labelSpace + width;
            this.statusStrip.Items [ toolStripStatusLabelImageHeight.Name ].Text = labelHeight + labelSpace + height;
        }
		
		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void CloseFile ()
		{
			// Update form parameters.
						
			SetFormTitleFileName ( string.Empty );

			// Update menu and tool strip visibility
			
			SetApplicationStateFileOpen ( false );
			
			// Delete the file from the image processsor.

			ResetImage ();
			
			// updage application flags
			
			this.fileOpen       = false;
            this.imageProcessed = false;
		}


		
		//--------------------------------------------------------------------------------
		// ShowImage
        //
        // Description:
        //
        //   Shows an image in the active workspace.
        //
		//--------------------------------------------------------------------------------
		
		public void ShowImage ( Image image )
		{	
            // Get image data
                        
            float scale  = this.imageScale;
            int   margin = this.workspaceMargin;
            int   width  = image.Width;
            int   height = image.Height;

            // Initialise images.

            this.pictureBoxSource.Image      = image;			            
            this.pictureBoxDestination.Image = image;			
			
            // Resise source and destination images to fit the current workspace size.                    
			
            ResizeImage ( this.pictureBoxSource );
            ResizeImage ( this.pictureBoxDestination );

            // Set PictureBox control visibility.

            this.pictureBoxSource.Visible      = true;
            this.pictureBoxDestination.Visible = true;

            // Update picture box shadow.

            UpdateShadow ();

            if ( this.shadowVisible ) ShowShadow ();            
            else                      HideShadow ();

            // Set PictureBox control focus.

            switch ( toolStripButtonProcessImage.CheckState )
            {
                case CheckState.Checked:
                    this.pictureBoxSource.BringToFront();
                    break;

                case CheckState.Unchecked:
                    this.pictureBoxDestination.BringToFront();
                    break;

                case CheckState.Indeterminate:
                    break;
            }
		}

        //--------------------------------------------------------------------------------
        // 
        //--------------------------------------------------------------------------------

        public void UpdateShadow ()
        {
            if ( this.shadowVisible )
            {
                // Set shadow size.

                int imageWidth   = this.pictureBoxSource.Width;
                int imageHeight  = this.pictureBoxSource.Height;
                int shadowSize   = this.shadowSize;
                int shadowWidth  = imageWidth  - shadowSize;
                int shadowHeight = imageHeight - shadowSize;

                this.pictureBoxShadowTopRight.Width     = shadowSize;
                this.pictureBoxShadowTopRight.Height    = shadowSize;

                this.pictureBoxShadowRight.Width        = shadowSize;
                this.pictureBoxShadowRight.Height       = shadowHeight;

                this.pictureBoxShadowBottomRight.Width  = shadowSize;
                this.pictureBoxShadowBottomRight.Height = shadowSize;

                this.pictureBoxShadowBottom.Width       = shadowWidth;
                this.pictureBoxShadowBottom.Height      = shadowSize;

                this.pictureBoxShadowBottomLeft.Width   = shadowSize;
                this.pictureBoxShadowBottomLeft.Height  = shadowSize;

                // Set shadow position.

                int imageLeft = this.pictureBoxSource.Left;
                int imageTop  = this.pictureBoxSource.Top;   

                this.pictureBoxShadowTopRight.Left    = imageLeft + imageWidth;
                this.pictureBoxShadowTopRight.Top     = imageTop;
                
                this.pictureBoxShadowRight.Left       = imageLeft + imageWidth;
                this.pictureBoxShadowRight.Top        = imageTop  + shadowSize;
                
                this.pictureBoxShadowBottomRight.Left = imageLeft + imageWidth;
                this.pictureBoxShadowBottomRight.Top  = imageTop  + imageHeight;
                
                this.pictureBoxShadowBottom.Left      = imageLeft + shadowSize;
                this.pictureBoxShadowBottom.Top       = imageTop  + imageHeight;
                
                this.pictureBoxShadowBottomLeft.Left  = imageLeft;
                this.pictureBoxShadowBottomLeft.Top   = imageTop  + imageHeight;
            }
        }

        //--------------------------------------------------------------------------------
        // 
        //--------------------------------------------------------------------------------

        public void ShowShadow ()
        {
            SetShadowVisibility ( true );
        }

        //--------------------------------------------------------------------------------
        // 
        //--------------------------------------------------------------------------------

        public void HideShadow ()
        {
            SetShadowVisibility ( false );
        }

        //--------------------------------------------------------------------------------
        // 
        //--------------------------------------------------------------------------------

        public void SetShadowVisibility ( bool visible )
        {
            this.pictureBoxShadowTopRight.Visible    = visible;
            this.pictureBoxShadowRight.Visible       = visible;
            this.pictureBoxShadowBottomRight.Visible = visible;
            this.pictureBoxShadowBottom.Visible      = visible;
            this.pictureBoxShadowBottomLeft.Visible  = visible;
        }
		
		//--------------------------------------------------------------------------------
		// ResetImage
        //
        // Description:
        //
        //   Reset image parameters.
		//--------------------------------------------------------------------------------
		
		public void ResetImage ()
		{
            // We will only reset an image if there is a file already open.

			if ( this.fileOpen )
			{
                // Set PictureBox images to the default image.

                Image defaultImage = this.pictureBoxSource.BackgroundImage;

                this.pictureBoxSource.Image.Dispose();
                this.pictureBoxDestination.Image.Dispose();

                // Set PictureBox control visibility.

				this.pictureBoxSource.Visible      = false;
		        this.pictureBoxDestination.Visible = false; 
               
                // Hide the picture box shadow.

                HideShadow ();
			}
		}
		
		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void ToolStripComboBoxZoom_KeyPress ( object sender, KeyPressEventArgs e )
		{
			if ( e.KeyChar == (char) Keys.Enter )
			{
				this.imageScale = (float) Convert.ToDouble ( toolStripComboBoxZoom.Text.Replace ( " %", "" ) )/ 100.0f;
				
				UpdateScale ();			
			}
		}
		
		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		public void UpdateScale ()
		{
			if ( this.imageScale > this.imageScaleMax )
			{
				this.imageScale = this.imageScaleMax;
			}
			
			if ( this.imageScale < this.imageScaleMin )
			{
				this.imageScale = this.imageScaleMin;
			}
						
			this.pictureBoxSource.Width  = (int) ( this.pictureBoxSource.Image.Width  * this.imageScale );
			this.pictureBoxSource.Height = (int) ( this.pictureBoxSource.Image.Height * this.imageScale );		
		}

		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void ToolStripComboBoxZoom_Leave ( object sender, EventArgs e )
		{
			this.imageScale = (float) Convert.ToDouble ( toolStripComboBoxZoom.Text.Replace ( " %", "" ) )/ 100.0f;
			
			UpdateScale ();
		}
		
		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void ToolStripComboBoxZoom_SelectedIndexChanged ( object sender, EventArgs e ) 
		{
			this.imageScale = (float) Convert.ToDouble ( toolStripComboBoxZoom.Text.Replace ( " %", "" ) )/ 100.0f;
			
			UpdateScale ();	
		}
		


        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        void SetProgressBarVisibility ( bool visible )
        {
            this.toolStripProgressBarProcessingImage.Value   = 0;
            this.toolStripProgressBarProcessingImage.Visible = visible;
            this.toolStripStatusLabelProcessingImage.Visible = visible;             
            Refresh ();
        }
		
		//--------------------------------------------------------------------------------
		// ToolStripButtonZoomIn_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Zoom In.
		//--------------------------------------------------------------------------------
		
		void ToolStripButtonZoomIn_Click ( object sender, EventArgs e )
		{
			ZoomIn ();
		}
		
		//--------------------------------------------------------------------------------
		// ToolStripButtonZoomOut_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Zoom Out.
		//--------------------------------------------------------------------------------
		
		void ToolStripButtonZoomOut_Click ( object sender, EventArgs e )
		{
			ZoomOut ();
		}
		
		//--------------------------------------------------------------------------------
		// Resise the image.        
		//--------------------------------------------------------------------------------
		
		void ResizeImage ( PictureBox pictureBox )
		{
			// Initialise working variables.

			float      workSpaceBoarderSize = 2.0f;			
			float      workSpaceWidth       = this.panelWorkSpace.Width  - 2.0f * workSpaceBoarderSize;
			float      workSpaceHeight      = this.panelWorkSpace.Height - 2.0f * workSpaceBoarderSize;						
			float      workSpaceMargin      = this.workspaceMargin;
			float      xAspect              = this.xAspect;
			float      yAspect              = this.yAspect;
			
			// Calculate the apropriate image size for the current work space size.
			
			if ( workSpaceWidth >= workSpaceHeight )
			{
				pictureBox.Height = (int) ( workSpaceHeight - ( 2.0f * workSpaceMargin ) );
				pictureBox.Width  = (int) ( pictureBox.Height * xAspect );
			}
			
			if ( workSpaceWidth < workSpaceHeight ) 
			{
				pictureBox.Width  = (int) ( workSpaceWidth - ( 2.0f * workspaceMargin ) );
				pictureBox.Height = (int) ( pictureBox.Width * yAspect );
			}
			
			// Calculate image position.
			
			pictureBox.Left = (int) ( ( workSpaceWidth  - pictureBox.Width  ) / 2.0f );			
			pictureBox.Top  = (int) ( ( workSpaceHeight - pictureBox.Height ) / 2.0f );

            // Update the shadow

            UpdateShadow ();
		}

        //--------------------------------------------------------------------------------
        // toolStripButtonProcessImage_Click
        //
        // Description:
        //
        //   Toolstrip - Button Click - Process Image
        //
		//--------------------------------------------------------------------------------

        private void toolStripButtonProcessImage_Click ( object sender, EventArgs e )
        {
            ProcessImageHandler ();    
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        public void ProcessImageHandler ()
        {
            // We only going to handle the button click if there is an open file.

            if ( this.fileOpen )
            {
                // We dicide what we going to do based on the CheckState of the button.

                switch ( toolStripButtonProcessImage.CheckState )
                {
                    case CheckState.Unchecked:

                        // If the button is un-checked, then bring the source PictureBox to the front.

                        this.pictureBoxSource.BringToFront ();

                        // Update active image.

                        this.activeImageIndex = ImageIndex.SOURCE;

                        break;

                    case CheckState.Checked:

                        // If the button is checked, then bring the destination PictureBox to the front.

                        this.pictureBoxDestination.BringToFront();

                        // We only process the image if it is not already processed.

                        if ( this.imageProcessed == false )
                        {
                            SetApplicationState ( ApplicationState.IMAGE_PROCESSING_IN_PROGRESS );
                            ProcessImage ();
                            SetApplicationState ( ApplicationState.IMAGE_PROCESSED );
                        }

                        // Update active image.

                        this.activeImageIndex = ImageIndex.DESTINATION;

                        break;
                }
            }
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        private void toolStripButtonOpen_Click_1(object sender, EventArgs e)
        {
            OpenFile ();
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        private void menuItemFileOpen_Click ( object sender, EventArgs e )
        {
            OpenFile ();
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        private void menuItemFileClose_Click ( object sender, EventArgs e )
        {
            CloseFile ();
        }

        //--------------------------------------------------------------------------------
		// panelWorkSpace_Resize
        //
        // Description:
        //
        //  Workspace - On Resize - Event.
        //
		//--------------------------------------------------------------------------------

        private void panelWorkSpace_Resize ( object sender, EventArgs e )
        {
            // Set image size acording to new workspace size.

            ResizeImage ( this.pictureBoxSource      );
            ResizeImage ( this.pictureBoxDestination );
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        private void menuItemFileExit_Click ( object sender, EventArgs e )
        {
            Close();
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void MenuItemViewToolBar_Click ( object sender, EventArgs e )
		{
			if ( menuItemViewToolBar.Checked )
			{
				toolStrip.Visible = true;
			}
			else
			{
				toolStrip.Visible = false;
			}
		}
		
		//--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------
		
		void MenuItemViewStatusBar_Click ( object sender, EventArgs e )
		{
			if ( menuItemViewStatusBar.Checked )
			{
				statusStrip.Visible = true;
			}
			else
			{
				statusStrip.Visible = false;
			}	
		}

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        private void menuItemShowShadow_Click ( object sender, EventArgs e )
        {
            if ( this.menuItemShowShadow.Checked )
			{
				ShowShadow ();
			}
			else
			{
				HideShadow ();
			}	
        }

        //--------------------------------------------------------------------------------
		// FormMain_KeyPress
        //
        // Description:
        //
        //   Application key handler.
		//--------------------------------------------------------------------------------

        private void FormMain_KeyPress ( object sender, KeyPressEventArgs e )
        {
            switch ( (Keys) e.KeyChar )
            {
                case Keys.Escape:
                    break;
            }
        }
	}
}
