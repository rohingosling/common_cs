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

        public enum ColorPlane
        {
            ALPHA = 0, 
            RED   = 1,
            GREEN = 2,
            BLUE  = 3
        };

        public enum PenColor
        {   
            BLACK = 0,
            WHITE = 1,
            RED   = 2,
            GREEN = 3,
            BLUE  = 4
        };

        public enum BorderCondition
        {
            CROP,
            EXTEND,
            WRAP
        }

        public enum Orientation
        {
            HORIZONTAL,
            VERTICAL
        }

        public enum PenState
        {
            PEN_UP, 
            PEN_DOWN
        };

        //--------------------------------------------------------------------------------
		// Member variables
		//--------------------------------------------------------------------------------

        ToolStripProgressBar progressBar;
        BorderCondition      borderCondition;
        Bitmap               bitmapSource;
        Bitmap               bitmapDestination;

        #region Properties

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

        #endregion

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
		// BitmapToGreyscale
        //
        // Description:
        //   Extracts a greyscale representation of an image from a Bitmap object.
		//--------------------------------------------------------------------------------
		
		public void BitmapToGreyscale ( Bitmap bitmap, out byte[] grey )
		{	
            // Lock bitmap bits and retrieve bitmap data.  
            
            Rectangle  rectangle  = new Rectangle   ( 0, 0, bitmap.Width, bitmap.Height );            
            BitmapData bitmapData = bitmap.LockBits ( rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat );
            
            // Get the address of the first scan line of the source and destination bitmaps.
            
            IntPtr bitmapPointer = bitmapData.Scan0;
            
            // Create linear image buffers to store the RGB data.
                        
            int imageSize = bitmap.Width * bitmap.Height;

	        grey = new byte [ imageSize ];

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
            int red          = 0;
            int green        = 0;
            int blue         = 0;

            for ( int index = 0; index < imageSize; index++ )
            {   
                pixelAddress = index * pixelWidth;

                red   = bitmapArray [ pixelAddress + redOffset   ];
                green = bitmapArray [ pixelAddress + greenOffset ];
                blue  = bitmapArray [ pixelAddress + blueOffset  ];

                grey [ index ] = (byte) ( ( red + green + blue ) / 3 );
            }
		}

        //--------------------------------------------------------------------------------
		// greyscaleToBitmap
        //
        // Description:
        //   Generates the red, green, blue and alpha components of an ARGB image and
        //   copies them to a bitmap.
		//--------------------------------------------------------------------------------
		
		public void greyscaleToBitmap ( Bitmap bitmap, byte[] grey )
		{	
            // Lock bitmap bits and retrieve bitmap data.  
            
            Rectangle  rectangle  = new Rectangle   ( 0, 0, bitmap.Width, bitmap.Height );            
            BitmapData bitmapData = bitmap.LockBits ( rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat );
            
            // Create a temporary image buffer to store the raw bitmap data.

            int    bitmapSize  = bitmapData.Stride * bitmap.Height;
            byte[] bitmapArray = new byte [ bitmapSize ];

            // Copy data from the respective red, green, blue and alpha buffers, into the image buffer.

            int  imageSize    = bitmap.Width * bitmap.Height;
            int  pixelWidth   = 4;   // RGB = 3, ARGB = 4.
            int  pixelAddress = 0;   // Offset address of pixel into the image buffer.
            int  alphaOffset  = 3;
            int  redOffset    = 2;
            int  greenOffset  = 1;
            int  blueOffset   = 0;
            byte alpha        = 255;

            for ( int index = 0; index < imageSize; index++ )
            {   
                pixelAddress = index * pixelWidth;

                bitmapArray [ pixelAddress + alphaOffset ] = alpha;
                bitmapArray [ pixelAddress + redOffset   ] = grey [ index ];
                bitmapArray [ pixelAddress + greenOffset ] = grey [ index ];
                bitmapArray [ pixelAddress + blueOffset  ] = grey [ index ];
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

        public byte ApplyConvolutionMatrix ( byte[] pixels, double[] matrix, double factor, int offset, int width, int height )
        {
            // Perform convolution.

            double pixel = 0;
            int    size  = width * height;

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
        // GetConvolutionWindow
        //
        // Description:
        //
        //   Returns a convolution window matrix that represents a floating square region with in the boundaries of
        //   a source image.
        //
        //   The examples below diagram various window matricies retunred from an image of size 10 x 10 ( Width 10, height 10 ).
        //
        //   Example 1 ( General case ):
        //
        //     Sample Pixel: (3,2)      Sample Pixel: (3,4)      Sample Pixel: (3,4)      Sample Pixel: (5,5)
        //     Matrix Order: 3          Matrix Order: 5          Matrix Order: 7          Matrix Order: 9
        //                                                                                
        //       0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9
        //     0 □ □ □ □ □ □ □ □ □ □    0 □ □ □ □ □ □ □ □ □ □    0 □ □ □ □ □ □ □ □ □ □    0 □ □ □ □ □ □ □ □ □ □
        //     1 □ □ ■ ■ ■ □ □ □ □ □    1 □ □ □ □ □ □ □ □ □ □    1 ■ ■ ■ ■ ■ ■ ■ □ □ □    1 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     2 □ □ ■ + ■ □ □ □ □ □    2 □ ■ ■ ■ ■ ■ □ □ □ □    2 ■ ■ ■ ■ ■ ■ ■ □ □ □    2 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     3 □ □ ■ ■ ■ □ □ □ □ □    3 □ ■ ■ ■ ■ ■ □ □ □ □    3 ■ ■ ■ ■ ■ ■ ■ □ □ □    3 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     4 □ □ □ □ □ □ □ □ □ □    4 □ ■ ■ + ■ ■ □ □ □ □    4 ■ ■ ■ + ■ ■ ■ □ □ □    4 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     5 □ □ □ □ □ □ □ □ □ □    5 □ ■ ■ ■ ■ ■ □ □ □ □    5 ■ ■ ■ ■ ■ ■ ■ □ □ □    5 □ ■ ■ ■ ■ + ■ ■ ■ ■
        //     6 □ □ □ □ □ □ □ □ □ □    6 □ ■ ■ ■ ■ ■ □ □ □ □    6 ■ ■ ■ ■ ■ ■ ■ □ □ □    6 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     7 □ □ □ □ □ □ □ □ □ □    7 □ □ □ □ □ □ □ □ □ □    7 ■ ■ ■ ■ ■ ■ ■ □ □ □    7 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     8 □ □ □ □ □ □ □ □ □ □    8 □ □ □ □ □ □ □ □ □ □    8 □ □ □ □ □ □ □ □ □ □    8 □ ■ ■ ■ ■ ■ ■ ■ ■ ■
        //     9 □ □ □ □ □ □ □ □ □ □    9 □ □ □ □ □ □ □ □ □ □    9 □ □ □ □ □ □ □ □ □ □    9 □ ■ ■ ■ ■ ■ ■ ■ ■ ■  
        //
        //   Example 2 ( Corner and edge cases ):
        //
        //     Sample Pixel: (8,8)      Sample Pixel: (9,0)      Sample Pixel: (0,5)      Sample Pixel: (6,8)
        //     Matrix Order: 5          Matrix Order: 5          Matrix Order: 7          Matrix Order: 5
        //
        //       0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9      0 1 2 3 4 5 6 7 8 9
        //     0 □ □ □ □ □ □ □ □ □ □    0 □ □ □ □ □ □ □ ■ ■ +    0 □ □ □ □ □ □ □ □ □ □    0 □ □ □ □ □ □ □ □ □ □
        //     1 □ □ □ □ □ □ □ □ □ □    1 □ □ □ □ □ □ □ ■ ■ ■    1 □ □ □ □ □ □ □ □ □ □    1 □ □ □ □ □ □ □ □ □ □
        //     2 □ □ □ □ □ □ □ □ □ □    2 □ □ □ □ □ □ □ ■ ■ ■    2 ■ ■ ■ ■ □ □ □ □ □ □    2 □ □ □ □ □ □ □ □ □ □
        //     3 □ □ □ □ □ □ □ □ □ □    3 □ □ □ □ □ □ □ □ □ □    3 ■ ■ ■ ■ □ □ □ □ □ □    3 □ □ □ □ □ □ □ □ □ □
        //     4 □ □ □ □ □ □ □ □ □ □    4 □ □ □ □ □ □ □ □ □ □    4 ■ ■ ■ ■ □ □ □ □ □ □    4 □ □ □ □ □ □ □ □ □ □
        //     5 □ □ □ □ □ □ □ □ □ □    5 □ □ □ □ □ □ □ □ □ □    5 + ■ ■ ■ □ □ □ □ □ □    5 □ □ □ □ □ □ □ □ □ □
        //     6 □ □ □ □ □ □ ■ ■ ■ ■    6 □ □ □ □ □ □ □ □ □ □    6 ■ ■ ■ ■ □ □ □ □ □ □    6 □ □ □ □ ■ ■ ■ ■ ■ □
        //     7 □ □ □ □ □ □ ■ ■ ■ ■    7 □ □ □ □ □ □ □ □ □ □    7 ■ ■ ■ ■ □ □ □ □ □ □    7 □ □ □ □ ■ ■ ■ ■ ■ □
        //     8 □ □ □ □ □ □ ■ ■ + ■    8 □ □ □ □ □ □ □ □ □ □    8 ■ ■ ■ ■ □ □ □ □ □ □    8 □ □ □ □ ■ ■ + ■ ■ □
        //     9 □ □ □ □ □ □ ■ ■ ■ ■    9 □ □ □ □ □ □ □ □ □ □    9 □ □ □ □ □ □ □ □ □ □    9 □ □ □ □ ■ ■ ■ ■ ■ □
        //
        //
        // Return:
        //
        //   Returns a square matrix of size order x order, centered around an arbetary 
        //   pixel (x,y) in an image plane, pixels.
        //
        // Arguments:
        //
        //   pixels - An image array of size width x height, containing one of the 
        //            ARGB components of an image.
        //
        //   width  - Width of the image, pixels.
        //
        //   height - Height of the image, pixels.
        //
        //   x      - x co-ordinate of the sample pixel.
        //
        //   y      - y co-ordinate of the sample pixel.
        //
        //   order  - Order of the square matrix to be pupulated and returned.
        //
		//--------------------------------------------------------------------------------

        public byte[] GetPixelWindow ( byte[] pixels, int width, int height, int x, int y, int order )
        {
            // Initialise border conditions

            BorderCondition borderCondition = this.borderCondition;     
            bool            borderPixel     = false;                      

            // Force order to be an odd number in the range [3..31].

            int orderMin = Properties.Settings.Default.minConvolutionMatrixOrder;
            int orderMax = Properties.Settings.Default.maxConvolutionMatrixOrder;
            order        = Clamp ( order, orderMin, orderMax );

            if ( order % 2 == 0 ) order++;

            // Get the pixel matrix

            int    matrixSize        = order * order;           // A matrix of order n, is n x n in size.
            byte[] convolutionWindow = new byte [ matrixSize ]; // Initialize a new convolution window matrix to populate and return to the caller.            
            int    halfOrder         = order / 2;               // Half the matrix order. Used for performing symetrical calculations about the center pixel.
            long   pixelOffset       = ( y * width ) + x;       // Offset into the image buffer of the center pixel specified by its x and y coordinates.
            long   matrixOffset      = 0;                       // Pixel offset into image buffer, offset by current matrix offset.
            long   matrixIndex       = 0;                       // Index of the window matrix, used for liniar access to the pixel matrix.

            for ( int v = -halfOrder; v <= halfOrder; v++ )
            {
                for ( int u = -halfOrder; u <= halfOrder; u++ )
                {   
                    // General case: Pixels are with in the edge boundaries of the image and do not require any special edge managment.
                    //               The border size of the images edges is defined by half the window matrix order.

                    if ( ( x >= halfOrder ) && ( x < width - halfOrder ) && ( y >= halfOrder ) && ( y < height - halfOrder ) )
                    {
                        matrixOffset = pixelOffset + ( v * width ) + u;                        
                        convolutionWindow [ matrixIndex ] = pixels [ matrixOffset ];
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
                                    convolutionWindow [ matrixIndex ] = pixels [ matrixOffset ];
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
                                    convolutionWindow [ matrixIndex ] = pixels [ matrixOffset ];
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
                                    convolutionWindow [ matrixIndex ] = pixels [ matrixOffset ];
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
                                    convolutionWindow [ matrixIndex ] = pixels [ matrixOffset ];
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
                                    convolutionWindow [ matrixIndex ] = 0;                        // Set pixel to black.
                                    break;

                                case BorderCondition.EXTEND:
                                    convolutionWindow [ matrixIndex ] = pixels [ pixelOffset ];   // Set pixel to the same color as the sample pixel.
                                    break;

                                case BorderCondition.WRAP:
                                    // To-do.
                                    break;
                            }

                            // Reset border pixel flag for next itteration.

                            borderPixel = false;
                        }
                    }

                    matrixIndex++;
                }
            }

            return convolutionWindow;
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
            byte[] pixelWindowAlpha = new byte [ matrixSize ];
            byte[] pixelWindowRed   = new byte [ matrixSize ];
            byte[] pixelWindowGreen = new byte [ matrixSize ];
            byte[] pixelWindowBlue  = new byte [ matrixSize ];

            // Apply convolution filter.
            
            long index = 0;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    // Get pixel window for current pixel.

                    alpha            = 255;
                    pixelWindowRed   = GetPixelWindow ( sourceRed,   width, height, x, y, order );
                    pixelWindowGreen = GetPixelWindow ( sourceGreen, width, height, x, y, order );
                    pixelWindowBlue  = GetPixelWindow ( sourceBlue,  width, height, x, y, order );

                    // Apply convolution matrix.
                    
                    red   = ApplyConvolutionMatrix ( pixelWindowRed,   convolutionMatrix, factor, offset, order );  
                    green = ApplyConvolutionMatrix ( pixelWindowGreen, convolutionMatrix, factor, offset, order );
                    blue  = ApplyConvolutionMatrix ( pixelWindowBlue,  convolutionMatrix, factor, offset, order );

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
		// Gaussian Blur
        //
        // Description:           
        //                                 x²+y²                   
        //                         1     - ────
        //              G(x,y) = ───── e   2σ²       ...Gaussian transformation.
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

            double maxOrderMultiplier = Properties.Settings.Default.maxConvolutionMatrixOrderMultiplier;            
            int    order              = (int) Math.Ceiling ( maxOrderMultiplier * deviation );            

            // Force the convolution matrix to be symetrical.

            if ( order % 2 == 0 ) order++;

            // Declare a serial convolution matrix;
            
            int      convolutionMatrixSize = order * order;            
            double[] convolutionMatrix     = new double [ convolutionMatrixSize ];
            int      index                 = 0;

            // Build Gaussian kernel.

            int    sampleRange = order / 2;                      // Gausian kernel sample range = convolution matrix order / 2.
            double σ           = deviation;                      // Standard deviation of gaussian transformation.            
            double π           = Math.PI;                        // Value of π.
            double e           = Math.E;                         // Value of e.
            double term        = 0;                              // Term of the gaussian transformation.            
            double power       = 0;                              // Power of the gaussian transformation.
            double Gxy         = 0;                              // Gaussian transformation: G(x,y)            

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
            Orientation          orientation,
            ToolStripProgressBar progressBar
        )
        {
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

            int    matrixSize  = order;
            byte[] windowAlpha = new byte [ matrixSize ];
            byte[] windowRed   = new byte [ matrixSize ];
            byte[] windowGreen = new byte [ matrixSize ];
            byte[] windowBlue  = new byte [ matrixSize ];

            // Apply convolution filter.

            int  halfOrder    = order / 2;  // Half the matrix order. Used for performing symetrical calculations about the center pixel.            
            long imageOffset  = 0;          // Pixel offset into image buffer, offset by current pixel window row offset.
            long windowOffset = 0;          // Index of the pixel window row, used for liniar access to the pixel window.                        
            long index        = 0;          // Pixel offset into image buffer              

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    // Get pixel window row for current pixel.

                    windowOffset = 0;

                    for ( int t = -halfOrder; t <= halfOrder; t++ )
                    {   
                        // General case. The entire pixel window falls with in the boundaries of the image.

                        switch ( orientation )
                        {
                            case Orientation.HORIZONTAL:

                                if ( ( x >= halfOrder ) && ( x < width - halfOrder ) )
                                {   
                                    imageOffset = index + t;

                                    windowRed   [ windowOffset ] = sourceRed   [ imageOffset ];
                                    windowGreen [ windowOffset ] = sourceGreen [ imageOffset ];
                                    windowBlue  [ windowOffset ] = sourceBlue  [ imageOffset ];
                                }

                                break;

                            case Orientation.VERTICAL:

                                if ( ( y >= halfOrder ) && ( y < height - halfOrder ) )
                                {   
                                    imageOffset = index + ( width * t );

                                    windowRed   [ windowOffset ] = sourceRed   [ imageOffset ];
                                    windowGreen [ windowOffset ] = sourceGreen [ imageOffset ];
                                    windowBlue  [ windowOffset ] = sourceBlue  [ imageOffset ];
                                }
                                break;
                        }

                        // Edge case. The pixel window stradles one or more edges of the image.

                        if ( ( x < halfOrder ) || ( x >= width - halfOrder ) || ( y < halfOrder ) || ( y >= height - halfOrder ) )
                        {
                            windowRed   [ windowOffset ] = sourceRed   [ index ];
                            windowGreen [ windowOffset ] = sourceGreen [ index ];
                            windowBlue  [ windowOffset ] = sourceBlue  [ index ];
                        }

                        windowOffset++;
                    }

                    // Apply convolution matrix.

                    red   = 0;
                    green = 0;
                    blue  = 0;

                    for ( int u = 0; u < matrixSize; u++ )
                    {
                        red   += (byte) ( windowRed   [ u ] * convolutionMatrix [ u ] );
                        green += (byte) ( windowGreen [ u ] * convolutionMatrix [ u ] );
                        blue  += (byte) ( windowBlue  [ u ] * convolutionMatrix [ u ] );
                    }

                    // Apply convolution factors and offsets.

                    red   = (byte) ( ( red   / factor ) + offset );
                    green = (byte) ( ( green / factor ) + offset );
                    blue  = (byte) ( ( blue  / factor ) + offset );
                    
                    // Clamp colors to an 8-bit color range.

                    red   = (byte) Clamp ( red,   byte.MinValue, byte.MaxValue );
                    green = (byte) Clamp ( green, byte.MinValue, byte.MaxValue );
                    blue  = (byte) Clamp ( blue,  byte.MinValue, byte.MaxValue );

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
		// Horizontal Gaussian Blur
		//--------------------------------------------------------------------------------

        public void GaussianBlur ( Bitmap bitmapSource, Bitmap bitmapDestination, double deviation, Orientation orientation, ToolStripProgressBar progressBar )
        {   
            // Calculate matrix size.

            double maxOrderMultiplier = Properties.Settings.Default.maxConvolutionMatrixOrderMultiplier;            
            int    order              = (int) Math.Ceiling ( maxOrderMultiplier * deviation );            

            // Force the convolution matrix to be symetrical.

            if ( order % 2 == 0 ) order++;

            // Declare a serial convolution matrix;
            
            int      convolutionMatrixSize = order;            
            double[] convolutionMatrix     = new double [ convolutionMatrixSize ];
            int      index                 = 0;

            // Build Gaussian kernel.

            int    sampleRange = order / 2;                      // Gausian kernel sample range = convolution matrix order / 2.
            double σ           = deviation;                      // Standard deviation of gaussian transformation.            
            double π           = Math.PI;                        // Value of π.
            double e           = Math.E;                         // Value of e.
            double term        = 0;                              // Term of the gaussian transformation.            
            double power       = 0;                              // Power of the gaussian transformation.
            double fx          = 0;                              // Gaussian transformation: f(x)            

            for ( int x = -sampleRange; x <= sampleRange; x++ )
            {
                // Calculate Gaussian transformation.

                term  = 1.0 / Math.Sqrt ( 2.0 * π * σ * σ );    // Calculate the term of the gaussian transformation.
                power = ( x * x ) / ( 2.0 * σ * σ );            // Calculate the power of the gaussian transformation.
                fx    = term * Math.Pow ( e, -power );          // f(x) = term * ( e ^ -power ).

                // Assign result to convolution matrix.

                convolutionMatrix [ index ] = fx;                    
                index++;
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

            switch ( orientation )
            {
                case Orientation.HORIZONTAL:
                    ConvolutionFilter
                    (
                        bitmapSource, 
                        bitmapDestination, 
                        convolutionMatrix, 
                        factor, 
                        offset, 
                        order, 
                        Orientation.HORIZONTAL, 
                        progressBar
                    );
                    break;

                case Orientation.VERTICAL:
                    ConvolutionFilter
                    (
                        bitmapSource, 
                        bitmapDestination, 
                        convolutionMatrix, 
                        factor, 
                        offset, 
                        order, 
                        Orientation.VERTICAL, 
                        progressBar
                    );
                    break;
            }
        }

        //--------------------------------------------------------------------------------
		// MedianBlur
        //
        // Description:
        //
        //   Apply a median filter to a source image 'bitmapSource', and return the 
        //   resulting image through 'bitmapDestination'.
        //   
		//--------------------------------------------------------------------------------

        public void MedianBlur ( Bitmap bitmapSource, Bitmap bitmapDestination, double radius, ToolStripProgressBar progressBar )
        {
            // Calculate convolution matrix size. 

            int order = (int) ( radius * 2.0 );     // A square matrix of dimentions n x n, has order = n, where n = |radius x 2|.

            // Force order to a number in the range [orderMin..orderMax].

            int orderMin = Properties.Settings.Default.minConvolutionMatrixOrder;
            int orderMax = Properties.Settings.Default.maxConvolutionMatrixOrder;
            order        = Clamp ( order, orderMin, orderMax );

            // Force order to be an odd number. 
            // We would like the convolution matrix to conform to a pixel window that prepresents a central pixel with an 
            // even distribution of neighbor pixels around it.

            if ( order % 2 == 0 ) order++;

            // Configure convolution matrix.

            int      convolutionMatrixSize = order * order;            
            double[] convolutionMatrix     = new double [ convolutionMatrixSize ];

            for ( int index = 0; index < convolutionMatrixSize; index++ )
            {
                convolutionMatrix [ index ] = 1.0;          // For a median filter the convolution matrix is simply the identiy matrix.
            }

            // Configure convolution matrix parameters.

            double factor = convolutionMatrixSize;          // A median filter averages the pixels in the pixel window, so we going to divide by the matrix size.
            int    offset = 0;                              // No offset required for a median filter.

            ConvolutionFilter ( bitmapSource, bitmapDestination, convolutionMatrix, factor, offset, order, progressBar );
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
		// Invert        
		//--------------------------------------------------------------------------------

        public void Invert ( Bitmap bitmapSource, Bitmap bitmapDestination, ToolStripProgressBar progressBar )
        {
            // Get RGB data from the source bitmap.

            byte[] alpha;
            byte[] red;
            byte[] green;
            byte[] blue;

            BitmapToRGB ( bitmapSource, out alpha, out red, out green, out blue );

            // Invert pixels

            int width      = bitmapSource.Width;
            int height     = bitmapSource.Height;
            int index      = 0;
            int colorWidth = 255;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    red   [ index ] = (byte) ( colorWidth - red   [ index ] );
                    green [ index ] = (byte) ( colorWidth - green [ index ] );
                    blue  [ index ] = (byte) ( colorWidth - blue  [ index ] );

                    index++;
                }
                progressBar.PerformStep();
            }            

            // Copy RGB data into the destination bitmap.

            rgbToBitmap ( bitmapDestination, alpha, red, green, blue );
        }

        //--------------------------------------------------------------------------------
		// 
		//--------------------------------------------------------------------------------

        public void Greyscale ( Bitmap bitmapSource, Bitmap bitmapDestination, ToolStripProgressBar progressBar )
        {
            // Get RGB data from the source bitmap.

            byte[] sourceAlpha;
            byte[] sourceRed;
            byte[] sourceGreen;
            byte[] sourceBlue;

            BitmapToRGB ( bitmapSource, out sourceAlpha, out sourceRed, out sourceGreen, out sourceBlue );

            // Invert pixels

            int width      = bitmapSource.Width;
            int height     = bitmapSource.Height;
            int index      = 0;            
            int red        = 0;
            int green      = 0;
            int blue       = 0;
            int grey       = 0;

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    red   = sourceRed   [ index ];
                    green = sourceGreen [ index ];
                    blue  = sourceBlue  [ index ];
                    grey  = ( red + green + blue ) / 3;

                    sourceRed   [ index ] = (byte) ( grey );
                    sourceGreen [ index ] = (byte) ( grey );
                    sourceBlue  [ index ] = (byte) ( grey );

                    index++;
                }
                progressBar.PerformStep();
            }            

            // Copy RGB data into the destination bitmap.

            rgbToBitmap ( bitmapDestination, sourceAlpha, sourceRed, sourceGreen, sourceBlue );
        }

        //--------------------------------------------------------------------------------
		// WhiteboardPen         
		//--------------------------------------------------------------------------------

        //public void WhiteboardPen ( Bitmap bitmapSource, Bitmap bitmapDestination, ToolStripProgressBar progressBar )
        //{
        //    // Image and filter parameters.

        //    int  width            = bitmapSource.Width;                 // Get the width from the source bitmap.
        //    int  height           = bitmapSource.Height;                // Get the height from the source bitmap.
        //    int  imageSize        = width * height;                     // Calculate the image size of one color plane.                       
        //    int  convolutionWidth = 3;                                  // Convolution width.             
            
        //    // Initialise greyscale image buffers.

        //    byte[] imageSource      = new byte [ imageSize ];           // 2D array to hold the grey scale pixel data for the source bitmap.
        //    byte[] imageDestination = new byte [ imageSize ];           // 2D array to hold the grey scale pixel data for the destination bitmap.

        //    // Get source image data.

        //    BitmapToGreyscale ( bitmapSource, out imageSource );

        //    // Initialise pixel variables.
            
        //    byte   pixelSource       = 0;                               // Source pixel from source image.
        //    byte   pixelDestination  = 0;                               // Destination pixel after processing.
        //    byte[] convolutionWindow = new byte [ convolutionWidth ];   // Pixel sample window array. 

        //    // Pixel deltas.

        //    int   deltaUp   = 0;
        //    int   deltaDown = 0;

        //    // Initialise edge detection thresholds.

        //    int thresholdUp   = 10;
        //    int thresholdDown = -10;

        //    // Initialise white board pen.

        //    PenState penState = PenState.PEN_UP;

        //    // Coordinate index variables.
                  
        //    long indexPixelSource       = 0;
        //    long indexPixelOffset       = 0;
        //    long indexConvolutionWindow = 0;

        //    // Apply filter.
            
        //    for ( int y = 0; y < height - 1; y++ )
        //    {
        //        // Reset whiteboard pen.

        //        penState = PenState.PEN_UP;

        //        // Process pixel row.

        //        for ( int x = 0; x < width; x++ )           
        //        {
        //            if ( ( x > 0 ) && ( x < width - 1 ) )                   // Start the pen one pixel after the first, and stop the pen one pixel before the last.
        //            {
        //                // Get convolution window.
        //                // Convolution window = (p0(x-1,y), p1(x,y), p2(x+1,y))

        //                indexPixelOffset = -1;
        //                for ( indexConvolutionWindow = 0; indexConvolutionWindow < convolutionWidth; indexConvolutionWindow++ )
        //                {   
        //                    convolutionWindow [ indexConvolutionWindow ] = imageSource [ indexPixelSource + indexPixelOffset ];
        //                    indexPixelOffset++;
        //                }

        //                // Calculate color delta between previous pixel and the current pixel. 
        //                // dx = p1(x,y) - p0(x-1,y)
        //                // dy = p1(x,y) - p0(x,y-1)

        //                // Red delta.

        //                deltaRed   = red1   - red0;
        //                deltaGreen = green1 - green0;
        //                deltaBlue  = blue1  - blue0;
        //                deltaBlack = ( deltaRed + deltaGreen + deltaBlue ) / 3;

        //                // Calculate the color average across the current and next two pixels.

        //                averageRed   = ( red1   + red2   + red3   ) / 3;
        //                averageGreen = ( green1 + green2 + green3 ) / 3;
        //                averageBlue  = ( blue1  + blue2  + blue3  ) / 3;
        //                greayScale   = ( averageRed + averageGreen + averageBlue ) / 3;

        //                // Determine the dominant color.

        //                if ( ( averageRed   > averageGreen ) && ( averageRed   > averageBlue  ) ) penColor = Color.Red;
        //                if ( ( averageGreen > averageRed   ) && ( averageGreen > averageBlue  ) ) penColor = Color.Green;
        //                if ( ( averageBlue  > averageRed   ) && ( averageBlue  > averageGreen ) ) penColor = Color.Blue;

        //                int thresholdBlack = 10;

        //                //if ( ( averageRed > thresholdBlack ) && ( averageGreen > thresholdBlack ) && ( averageBlue > thresholdBlack ) ) penColor = Color.Black;

        //                // Process pen.

        //                if ( penColor == Color.Red )
        //                {
        //                    // Determin pen state.

        //                    if ( deltaRed >= thresholdHighRed ) redPenDown = true;
        //                    if ( deltaRed <= thresholdLowRed  ) redPenDown = false;

        //                    // Put destination pixel.

        //                    destinationAlpha [ index ] = alpha;
        //                    destinationRed   [ index ] = (byte) ( ( redPenDown ) ? penDownColorRed : penUpColorRed );
        //                    destinationGreen [ index ] = penUpColorGreen;
        //                    destinationBlue  [ index ] = penUpColorBlue;
        //                }

        //                if ( penColor == Color.Green )
        //                {
        //                    // Determin pen state.

        //                    if ( deltaGreen >= thresholdHighGreen ) greenPenDown = true;
        //                    if ( deltaGreen <= thresholdLowGreen  ) greenPenDown = false;

        //                    // Put destination pixel.

        //                    destinationAlpha [ index ] = alpha;
        //                    destinationGreen [ index ] = penUpColorRed; 
        //                    destinationGreen [ index ] = (byte) ( ( greenPenDown ) ? penDownColorGreen : penUpColorGreen );
        //                    destinationBlue  [ index ] = penUpColorBlue; 
        //                }

        //                if ( penColor == Color.Blue )
        //                {
        //                    // Determin pen state.

        //                    if ( deltaBlue >= thresholdHighBlue ) bluePenDown = true;
        //                    if ( deltaBlue <= thresholdLowBlue  ) bluePenDown = false;

        //                    // Put destination pixel.

        //                    destinationAlpha [ index ] = alpha;
        //                    destinationBlue  [ index ] = penUpColorRed; 
        //                    destinationBlue  [ index ] = penUpColorGreen;
        //                    destinationBlue  [ index ] = (byte) ( ( bluePenDown ) ? penDownColorBlue : penUpColorBlue ); 
        //                }

        //                if ( penColor == Color.Black )
        //                {
        //                    // Determin pen state.

        //                    if ( deltaBlack >= thresholdHighBlack ) blackPenDown = true;
        //                    if ( deltaBlack <= thresholdLowBlack  ) blackPenDown = false;

        //                    // Put destination pixel.

        //                    destinationAlpha [ index ] = alpha;
        //                    destinationBlue  [ index ] = (byte) ( ( blackPenDown ) ? penDownColorBlack : penUpColorBlack ); 
        //                    destinationBlue  [ index ] = (byte) ( ( blackPenDown ) ? penDownColorBlack : penUpColorBlack );
        //                    destinationBlue  [ index ] = (byte) ( ( blackPenDown ) ? penDownColorBlack : penUpColorBlack ); 
        //                }
        //            }
                    
        //            // Update pixel array index.

        //            indexSourcePixel++;
        //        }

        //        // Update the progress bar.

        //        progressBar.PerformStep();
        //    }            

        //    // Copy RGB data into the destination bitmap.

        //    greyscaleToBitmap ( bitmapDestination, imageDestination );

        //}   

    }
}