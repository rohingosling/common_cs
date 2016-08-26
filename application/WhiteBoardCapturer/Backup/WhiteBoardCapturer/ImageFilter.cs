//--------------------------------------------------------------------------------
//
//--------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WhiteBoardCapturer
{
	public class ImageFilter
	{	
        //--------------------------------------------------------------------------------
		// Enumerations
		//--------------------------------------------------------------------------------

        public enum BorderCondition
        {
            CROP,
            EXTEND,
            WRAP
        }

        //--------------------------------------------------------------------------------
		// Member variables
		//--------------------------------------------------------------------------------

        ToolStripProgressBar progressBar;
        BorderCondition      borderCondition;
        Bitmap               bitmapSource;
        Bitmap               bitmapDestination;

        //--------------------------------------------------------------------------------
		// Properties
		//--------------------------------------------------------------------------------

        public ToolStripProgressBar ProgressBar
        {
            get { return this.progressBar;  }
            set { this.progressBar = value; }
        }

        public BorderCondition BoarderCondition
        {
            get { return this.borderCondition;  }
            set { this.borderCondition = value; }
        }

        public Bitmap BitmapSource
        {
            get { return this.bitmapSource;  }
            set { this.bitmapSource = value; }
        }

        public Bitmap BitmapDestination
        {
            get { return this.bitmapDestination;  }
            set { this.bitmapDestination = value; }
        }

		//--------------------------------------------------------------------------------
		// Constructor
		//--------------------------------------------------------------------------------
		
		public ImageFilter ()
		{
            this.progressBar       = null;
            this.borderCondition   = (BorderCondition) Properties.Settings.Default.ConvolutionBorderCondition;
            this.bitmapSource      = null;
            this.bitmapDestination = null;
		}

        //--------------------------------------------------------------------------------
		// Clamp
        //
        // Description:
        //   Clamps the value of a variable with a specified minimum and maximum value.
        // 
		//--------------------------------------------------------------------------------
		
		public double Clamp ( double value, double min, double max )
		{
			double newValue = value;
			
			if ( newValue < min ) newValue = min;
			if ( newValue > max ) newValue = max;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------

        public int Clamp ( int value, int min, int max )
		{
			int newValue = value;
			
			if ( newValue < min ) newValue = min;
			if ( newValue > max ) newValue = max;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------
		// ClampDown
        //
        // Description:
        //   Clamps the value of a variable down to a specified minimum value.
        // 
		//--------------------------------------------------------------------------------
		
		public double ClampDown ( double value, double min )
		{
			double newValue = value;			
			
			if ( newValue < min ) newValue = min;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------

        public int ClampDown ( int value, int min )
		{
			int newValue = value;			
			
			if ( newValue < min ) newValue = min;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------
		// ClampUp
        //
        // Description:
        //   Clamps the value of a variable up to a specified maximum value.
        // 
		//--------------------------------------------------------------------------------
		
		public double ClampUp ( double value, double max )
		{
			double newValue = value;			
			
			if ( newValue > max ) newValue = max;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------

        public int ClampUp ( int value, int max )
		{
			int newValue = value;			
			
			if ( newValue > max ) newValue = max;
			
			return newValue;
		}

        //--------------------------------------------------------------------------------
		// BitmapToRGB
        //
        // Description:
        //   Extracts the red, green, blue an alpha components from a Bitmap object, and 
        //   returns them in seperate arrays.
		//--------------------------------------------------------------------------------
		
		public void BitmapToRGB ( Bitmap bitmap, out byte[] alpha, out byte[] red, out byte[] green, out byte[] blue )
		{	
            // Lock bitmap bits and retrieve bitmap data.  
            
            Rectangle  rectangle  = new Rectangle   ( 0, 0, bitmap.Width, bitmap.Height );            
            BitmapData bitmapData = bitmap.LockBits ( rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat );
            
            // Get the address of the first scan line of the source and destination bitmaps.
            
            IntPtr bitmapPointer = bitmapData.Scan0;
            
            // Create linear image buffers to store the RGB data.
                        
            int imageSize = bitmap.Width * bitmap.Height;

	        alpha = new byte [ imageSize ];
            red   = new byte [ imageSize ];
            green = new byte [ imageSize ];
            blue  = new byte [ imageSize ];

            // Create a temporary image buffer to store the raw bitmap data.

            int    bitmapSize  = bitmapData.Stride * bitmap.Height;
            byte[] bitmapArray = new byte [ bitmapSize ];
	        
            // Copy the bitmap data into the image buffer.            
            
            System.Runtime.InteropServices.Marshal.Copy ( bitmapPointer, bitmapArray, 0, bitmapSize );

            // Unlock the source and destination bitmap's bits.
            
            bitmap.UnlockBits ( bitmapData );

            // Copy the RGB components from the image buffer into the respective red, green, blue and alpha buffers.

            int pixelWidth   = 4;   // RGB = 3, ARGB = 4.
            int pixelAddress = 0;   // Offset address of pixel into the image buffer.
            int alphaOffset  = 3;
            int redOffset    = 2;
            int greenOffset  = 1;
            int blueOffset   = 0;

            for ( int index = 0; index < imageSize; index++ )
            {   
                pixelAddress = index * pixelWidth;

                alpha [ index ] = bitmapArray [ pixelAddress + alphaOffset ];
                red   [ index ] = bitmapArray [ pixelAddress + redOffset   ];
                green [ index ] = bitmapArray [ pixelAddress + greenOffset ];
                blue  [ index ] = bitmapArray [ pixelAddress + blueOffset  ];
            }
		}

        //--------------------------------------------------------------------------------
		// rgbToBitmap
        //
        // Description:
        //   Copies the red, green, blue and alpha data from seperate red, green, blue
        //   and alpha arrays into a bitmap.
		//--------------------------------------------------------------------------------
		
		public void rgbToBitmap ( Bitmap bitmap, byte[] alpha, byte[] red, byte[] green, byte[] blue )
		{	
            // Lock bitmap bits and retrieve bitmap data.  
            
            Rectangle  rectangle  = new Rectangle   ( 0, 0, bitmap.Width, bitmap.Height );            
            BitmapData bitmapData = bitmap.LockBits ( rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat );
            
            // Create a temporary image buffer to store the raw bitmap data.

            int    bitmapSize  = bitmapData.Stride * bitmap.Height;
            byte[] bitmapArray = new byte [ bitmapSize ];

            // Copy data from the respective red, green, blue and alpha buffers, into the image buffer.

            int imageSize    = bitmap.Width * bitmap.Height;
            int pixelWidth   = 4;   // RGB = 3, ARGB = 4.
            int pixelAddress = 0;   // Offset address of pixel into the image buffer.
            int alphaOffset  = 3;
            int redOffset    = 2;
            int greenOffset  = 1;
            int blueOffset   = 0;

            for ( int index = 0; index < imageSize; index++ )
            {   
                pixelAddress = index * pixelWidth;

                bitmapArray [ pixelAddress + alphaOffset ] = alpha [ index ];
                bitmapArray [ pixelAddress + redOffset   ] = red   [ index ];
                bitmapArray [ pixelAddress + greenOffset ] = green [ index ];
                bitmapArray [ pixelAddress + blueOffset  ] = blue  [ index ];
            }

            // Get the address of the first scan line of the source and destination bitmaps.
            
            IntPtr bitmapPointer = bitmapData.Scan0;

            // Copy the bitmap data into the image buffer.            
                        
            System.Runtime.InteropServices.Marshal.Copy ( bitmapArray, 0, bitmapPointer, bitmapSize );

            // Unlock the source and destination bitmap's bits.
            
            bitmap.UnlockBits ( bitmapData );
		}

        //--------------------------------------------------------------------------------
		// ApplyConvolutionMatrix
        //
        // Description:
        //   Applies a convolution matrix to a pixel matrix.
        //
        // Return - Returns the byte value of a pixel color component.
        //
        // pixels - Liniar representation of a pixel matrix where the center 
        //          pixel is the sample pixel.
        //
        // matrix - Liniar representation of a square convolution matrix.
        //
        // order  - Order of the square convolution matrix.
        // 
		//--------------------------------------------------------------------------------

        public byte ApplyConvolutionMatrix ( byte[] pixels, double[] matrix, double factor, int offset, int order )
        {
            // Perform convolution.

            double pixel = 0;
            int    size  = order * order;

            for ( int index = 0; index < size; index++ )
            {
                pixel += pixels [ index ] * matrix [ index ];
            }

            // Apply convolution factor and offset.

            pixel /= factor;
            pixel += offset;

            // Clamp the value to an 8-bit color range, and return it to the caller.

            pixel = Clamp ( pixel, byte.MinValue, byte.MaxValue ); 
            return (byte) pixel;
        }

        //--------------------------------------------------------------------------------
        // GetPixelMatrix
		//--------------------------------------------------------------------------------

        public byte[] GetPixelMatrix ( byte[] pixels, int width, int height, int x, int y, int order )
        {
            // Initialise border conditions

            BorderCondition borderCondition = this.borderCondition;     
            bool            borderPixel     = false;                      

            // Force order to be an odd number in the range [3..9].

            int orderMin = 3;
            int orderMax = 31;
            order = Clamp ( order, orderMin, orderMax );
            if ( order % 2 == 0 ) order++;

            // Get the pixel matrix.

            int    matrixSize   = order * order;            // A matrix of order n, is n x n in size.
            byte[] pixelMatrix  = new byte [ matrixSize ];  // Initialize a new pixel matrix to populate and return to the caller.            
            int    halfOrder    = order / 2;                // Half the matrix order. Used for performing symetrical calculations about the center pixel.
            long   pixelOffset  = ( y * width ) + x;        // Offset into the image buffer of the center pixel specified by its x and y coordinates.
            long   matrixOffset = 0;                        // Pixel offset into image buffer, offset by current matrix offset.
            long   matrixIndex  = 0;                        // Index of the pixel matrix, used for liniar access to the pixel matrix.

            for ( int v = -halfOrder; v <= halfOrder; v++ )
            {
                for ( int u = -halfOrder; u <= halfOrder; u++ )
                {   
                    // General case: Pixels are with in the edge boundaries of the image and do not require any special edge managment.
                    //               The border size of the images edges is defined by half the pixel matrix order.

                    if ( ( x >= halfOrder ) && ( x < width - halfOrder ) && ( y >= halfOrder ) && ( y < height - halfOrder ) )
                    {
                        matrixOffset = pixelOffset + ( v * width ) + u;                        
                        pixelMatrix [ matrixIndex ] = pixels [ matrixOffset ];
                    }

                    // Border pixel case: Pixels are on the edges of the image.
                    //                    Pixels on edge of the image will reference pixels out side the array index bouds of the image,
                    //                    and thus require out of bound condition to be handeled.

                    else
                    {
                        // Reset border pixel flag

                        borderPixel = false;

                        // Left and right Edges.

                        if ( ( y >= halfOrder ) && ( y < height - halfOrder ) )
                        {
                            if ( ( x < halfOrder ) || ( x >= width - halfOrder ) )
                            {
                                if ( ( x + u < 0 ) || ( x + u >= width ) )
                                {
                                    borderPixel = true;
                                }
                                else
                                {
                                    matrixOffset = pixelOffset + ( v * width ) + u;                        
                                    pixelMatrix [ matrixIndex ] = pixels [ matrixOffset ];
                                }
                            }
                        }

                        // Top and bottom edges.

                        if ( ( x >= halfOrder ) && ( x < width - halfOrder ) )
                        {
                            if ( ( y < halfOrder ) || ( y >= height - halfOrder ) )
                            {
                                if ( ( y + v < 0 ) || ( y + v >= height ) )
                                {
                                    borderPixel = true;
                                }
                                else
                                {
                                    matrixOffset = pixelOffset + ( v * width ) + u;                        
                                    pixelMatrix [ matrixIndex ] = pixels [ matrixOffset ];
                                }
                            }
                        }

                        // Top corners

                        if ( y < halfOrder )
                        {
                            if ( ( x < halfOrder ) || ( x >= width - halfOrder ) )
                            {
                                if ( ( y + v < 0 ) || ( x + u < 0 ) || ( x + u >= width ) )
                                {
                                    borderPixel = true;
                                }
                                else
                                {
                                    matrixOffset = pixelOffset + ( v * width ) + u;                        
                                    pixelMatrix [ matrixIndex ] = pixels [ matrixOffset ];
                                }
                            }
                        }

                        // Bottom corners

                        if ( y >= height - halfOrder )
                        {
                            if ( ( x < halfOrder ) || ( x >= width - halfOrder ) )
                            {
                                if ( ( x + u < 0 ) || ( x + u >= width ) || ( y + v >= height ) )
                                {
                                    borderPixel = true;
                                }
                                else
                                {
                                    matrixOffset = pixelOffset + ( v * width ) + u;                        
                                    pixelMatrix [ matrixIndex ] = pixels [ matrixOffset ];
                                }
                            }
                        }

                        // Set border pixel

                        if ( borderPixel )
                        {
                            // Set border pixel acording to border condition.

                            switch ( borderCondition )
                            {
                                case BorderCondition.CROP:
                                    pixelMatrix [ matrixIndex ] = 0;                        // Set pixel to black.
                                    break;

                                case BorderCondition.EXTEND:
                                    pixelMatrix [ matrixIndex ] = pixels [ pixelOffset ];   // Set pixel to the same color as the sample pixel.
                                    break;

                                case BorderCondition.WRAP:
                                    // To-do...                                             // Wrap to pixel on the oposite edge of the image.
                                    break;
                            }

                            // Reset border pixel flag for next itteration.

                            borderPixel = false;
                        }
                    }

                    matrixIndex++;
                }
            }

            return pixelMatrix;
        }

        //--------------------------------------------------------------------------------
		// ConvolutionFilter       
		//--------------------------------------------------------------------------------

        public void ConvolutionFilter
        (
            Bitmap               bitmapSource, 
            Bitmap               bitmapDestination, 
            double[]             convolutionMatrix, 
            double               factor, 
            int                  offset, 
            int                  order, 
            ToolStripProgressBar progressBar
        )
        {
            // Initialise the progress bar.

            progressBar.Maximum = bitmapSource.Height;
            progressBar.Step    = 1; 
            progressBar.Value   = 0;

            // Get RGB data from the source bitmap.

            byte[] sourceAlpha;
            byte[] sourceRed;
            byte[] sourceGreen;
            byte[] sourceBlue;

            BitmapToRGB ( bitmapSource, out sourceAlpha, out sourceRed, out sourceGreen, out sourceBlue );

            // Initialise destination RGB arrays.

            int width     = bitmapSource.Width;
            int height    = bitmapSource.Height;
            int imageSize = width * height;

            byte[] destinationAlpha = new byte [ imageSize ];
            byte[] destinationRed   = new byte [ imageSize ];
            byte[] destinationGreen = new byte [ imageSize ];
            byte[] destinationBlue  = new byte [ imageSize ];

            // Initialise pixel variables.

            byte alpha = 255;
            byte red   = 0;
            byte green = 0;
            byte blue  = 0;

            // Initialize pixel matricies

            int    matrixSize       = order * order;
            byte[] pixelMatrixAlpha = new byte [ matrixSize ];
            byte[] pixelMatrixRed   = new byte [ matrixSize ];
            byte[] pixelMatrixGreen = new byte [ matrixSize ];
            byte[] pixelMatrixBlue  = new byte [ matrixSize ];

            // Apply convolution filter.
            
            long index = 0;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    // Get pixel matrix for current pixel.

                    alpha            = 255;
                    pixelMatrixRed   = GetPixelMatrix ( sourceRed,   width, height, x, y, order );
                    pixelMatrixGreen = GetPixelMatrix ( sourceGreen, width, height, x, y, order );
                    pixelMatrixBlue  = GetPixelMatrix ( sourceBlue,  width, height, x, y, order );

                    // Apply convolution matrix.
                    
                    red   = ApplyConvolutionMatrix ( pixelMatrixRed,   convolutionMatrix, factor, offset, order );  
                    green = ApplyConvolutionMatrix ( pixelMatrixGreen, convolutionMatrix, factor, offset, order );
                    blue  = ApplyConvolutionMatrix ( pixelMatrixBlue,  convolutionMatrix, factor, offset, order );

                    // Put destination pixel.

                    destinationAlpha [ index ] = alpha;
                    destinationRed   [ index ] = red;
                    destinationGreen [ index ] = green;
                    destinationBlue  [ index ] = blue;

                    // Update pixel array index.

                    index++;
                }

                // Update the progress bar.

                progressBar.PerformStep();
            }            

            // Copy RGB data into the destination bitmap.

            rgbToBitmap ( bitmapDestination, destinationAlpha, destinationRed, destinationGreen, destinationBlue );
        }

        //--------------------------------------------------------------------------------
		// Emboss        
		//--------------------------------------------------------------------------------

        public void Emboss ( Bitmap bitmapSource, Bitmap bitmapDestination, ToolStripProgressBar progressBar )
        {
            double[] convolutionMatrix =
            {
                2,  1,  0,
                1,  1, -1,
                0, -1, -2
            };

            double factor = 1;
            int    offset = 0;
            int    order  = 3;

            ConvolutionFilter ( bitmapSource, bitmapDestination, convolutionMatrix, factor, offset, order, progressBar );
        }

        //--------------------------------------------------------------------------------
		// Gaussian Blur
        //
        // Description:           
        //                                 x²+y²                   
        //                         1     - ────
        //              G(x,y) = ───── e   2σ²      ...Gaussian transformation.
        //                        2πσ² 
        //           
        //        Matrix Order = 3σ                  ...Minimum acceptable performance over quality = 3σ.
        //                                              Minimum acceptable quality over performance = 6σ.
        //                       3σ  3σ
        //  Convolution Factor =  Σ   Σ G(x,y)       ...Convolution factor set to the sum of all values in the Gaussian kernal in order
        //                       x=0 y=0                normalise the result of the Gaussian transformation.
        //
        //  Convolution Offset = 0                   ...Convolution offset not required, and se we set it to zero. 
        //
		//--------------------------------------------------------------------------------

        public void GaussianBlur ( Bitmap bitmapSource, Bitmap bitmapDestination, double deviation, ToolStripProgressBar progressBar )
        {   
            // Calculate matrix size.

            double maxOrderFactor = Properties.Settings.Default.maxConvolutionMatrixOrderFactor;            
            int    order          = (int) Math.Ceiling ( maxOrderFactor * deviation );            

            // Force the convolution matrix to be symetrical.

            if ( order % 2 == 0 ) order++;

            // Declare a serial convolution matrix;
            
            int      convolutionMatrixSize = order * order;            
            double[] convolutionMatrix     = new double [ convolutionMatrixSize ];
            int      index                 = 0;

            // Build Gaussian kernel.

            int       sampleRange = order / 2;                      // Gausian kernel sample range = convolution matrix order / 2.
            double    σ           = deviation;                      // Standard deviation of gaussian transformation.            
            double    π           = Math.PI;                        // Value of π.
            double    e           = Math.E;                         // Value of e.
            double    term        = 0;                              // Term of the gaussian transformation.            
            double    power       = 0;                              // Power of the gaussian transformation.
            double    Gxy         = 0;                              // Gaussian transformation: G(x,y)            

            for ( int y = -sampleRange; y <= sampleRange; y++ )
            {
                for ( int x = -sampleRange; x <= sampleRange; x++ )
                {
                    // Calculate Gaussian transformation.

                    term  = 1.0 / ( 2.0 * π * σ * σ );              // Calculate the term of the gaussian transformation.
                    power = ( x * x + y * y ) / ( 2.0 * σ * σ );    // Calculate the power of the gaussian transformation.
                    Gxy   = term * Math.Pow ( e, -power );          // G(x,y) = term * ( e ^ -power ).

                    // Assign result to convolution matrix.

                    convolutionMatrix [ index ] = Gxy;                    
                    index++;
                }
            }

            // Initialize convolution filter parameters.

            int    offset = 0;
            double factor = 0;

            // Set convolution factor to normalise the Gaussian Kernel.
            
            for ( index = 0; index < convolutionMatrixSize; index++ )
            {                                                           //                     3σ  3σ
                factor += convolutionMatrix [ index ];                  // Convolution factor = Σ   Σ G(x,y)
            }                                                           //                     x=0 y=0

            // Apply convolution filter.

            ConvolutionFilter ( bitmapSource, bitmapDestination, convolutionMatrix, factor, offset, order, progressBar );
        }

        //--------------------------------------------------------------------------------
		// Invert        
		//--------------------------------------------------------------------------------

        public void Invert ( Bitmap bitmapSource, Bitmap bitmapDestination, ToolStripProgressBar progressBar )
        {
            // Initialise the progress bar.

            progressBar.Maximum = bitmapSource.Height;
            progressBar.Step    = 1; 
            progressBar.Value   = 0;

            // Get RGB data from the source bitmap.

            byte[] alpha;
            byte[] red;
            byte[] green;
            byte[] blue;

            BitmapToRGB ( bitmapSource, out alpha, out red, out green, out blue );

            // Invert pixels

            int width  = bitmapSource.Width;
            int height = bitmapSource.Height;
            int index  = 0;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    red   [ index ] = (byte) ( 255 - red   [ index ] );
                    green [ index ] = (byte) ( 255 - green [ index ] );
                    blue  [ index ] = (byte) ( 255 - blue  [ index ] );

                    index++;
                }
                progressBar.PerformStep();
            }            

            // Copy RGB data into the destination bitmap.

            rgbToBitmap ( bitmapDestination, alpha, red, green, blue );
        }
    }
}