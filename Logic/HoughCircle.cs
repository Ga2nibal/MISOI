using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class HoughCircle
    {
        uint[] input;
		uint[] output;
		double progress;
		int width;
		int height;
		uint[] acc;
		int accSize=30;
		uint[] results;
		int r;

		public void circleHough() {
			progress=0;
		}

		public void init(uint[] inputIn, int widthIn, int heightIn, int radius) {
			r = radius;
			width=widthIn;
			height=heightIn;
			input = new uint[width*height];
			output = new uint[width*height];
			input=inputIn;
			for(int x=0;x<width;x++) {
				for(int y=0;y<height;y++) {
					output[x + (width*y)] = 0xff000000;
				}
			}
		}

        public void init(Bitmap inputBitmap, int radius)
        {
            r = radius;
            width = inputBitmap.Width;
            height = inputBitmap.Height;
            output = new uint[width * height];
            input = new uint[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    input[width*y + x] = (uint)inputBitmap.GetPixel(x, y).ToArgb();
                    output[x + (width * y)] = 0xff000000;
                }
            }
        }
		public void setLines(int lines) {
			accSize=lines;		
		}
		// hough transform for lines (polar), returns the accumulator array
		public uint[] process() {
	
			// for polar we need accumulator of 180degress * the longest length in the image
			int rmax = (int)Math.Sqrt(width*width + height*height);
			acc = new uint[width * height];
			for(int x=0;x<width;x++) {
				for(int y=0;y<height;y++) {
					acc[y*width+x] =0 ;
				}
			}			
			int x0, y0;
			double t;
			progress=0;
				
			for(int x=0;x<width;x++) {
				progress+=0.5;			
				for(int y=0;y<height;y++) {
				
					if ((input[y*width+x] & 0xff) > 250) {
					
						for (int theta=0; theta<360; theta++) {
							t = (theta * 3.14159265) / 180;
							x0 = (int)Math.Round(x - r * Math.Cos(t));
							y0 = (int)Math.Round(y - r * Math.Sin(t));
							if(x0 < width && x0 > 0 && y0 < height && y0 > 0) {
								acc[x0 + (y0 * width)] += 1;
							}
						}
					}
				}
			}
		
			// now normalise to 255 and put in format for a pixel array
			uint max=0;
		
			// Find max acc value
			for(int x=0;x<width;x++) {
				for(int y=0;y<height;y++) {

					if (acc[x + (y * width)] > max) {
						max = acc[x + (y * width)];
					}
				}
			}
		
			//System.out.println("Max :" + max);
		
			// Normalise all the values
			int value;
			for(int x=0;x<width;x++) {
				for(int y=0;y<height;y++) {
					value = (int)(((double)acc[x + (y * width)]/(double)max)*255.0);
					acc[x + (y * width)] = (uint)(0xff000000 | (value << 16 | value << 8 | value));
				}
			}
			findMaxima();

			Console.WriteLine("done");
			return output;
		}
		private uint[] findMaxima() {
			results = new uint[accSize*3];
			uint[] output = new uint[width*height];

		
			for(uint x=0;x<width;x++) {
				for(uint y=0;y<height;y++) {
					uint value = (acc[x + (y * width)] & 0xff);

					// if its higher than lowest value add it and then sort
					if (value > results[(accSize-1)*3]) {

						// add to bottom of array
						results[(accSize-1)*3] = value;
						results[(accSize-1)*3+1] = x;
						results[(accSize-1)*3+2] = y;
					
						// shift up until its in right place
						int i = (accSize-2)*3;
						while ((i >= 0) && (results[i+3] > results[i])) {
							for(uint j=0; j<3; j++) {
								uint temp = results[i+j];
								results[i+j] = results[i+3+j];
								results[i+3+j] = temp;
							}
							i = i - 3;
							if (i < 0) break;
						}
					}
				}
			}
		
			double ratio=(double)(width/2)/accSize;
			Console.WriteLine("top "+accSize+" matches:");
			for(int i=accSize-1; i>=0; i--){
				progress+=ratio;			
				//System.out.println("value: " + results[i*3] + ", r: " + results[i*3+1] + ", theta: " + results[i*3+2]);
				drawCircle(results[i*3], results[i*3+1], results[i*3+2]);
			}
			return output;
		}
	
		private void setPixel(uint value, uint xPos, uint yPos) {
			output[(yPos * width)+xPos] = (0xff000000 | (value << 16 | value << 8 | value));
		}
		
		// draw circle at x y
		private void drawCircle(uint pix, uint xCenter, uint yCenter) {
			pix = 250;
			
			uint x, y, r2;
			uint radius = (uint)r;
			r2 = (uint)(r * r);
			setPixel(pix, xCenter, yCenter + radius);
			setPixel(pix, xCenter, yCenter - radius);
			setPixel(pix, xCenter + radius, yCenter);
			setPixel(pix, xCenter - radius, yCenter);

			y = radius;
			x = 1;
			y = (uint) (Math.Sqrt(r2 - 1) + 0.5);
			while (x < y) {
				    setPixel(pix, xCenter + x, yCenter + y);
				    setPixel(pix, xCenter + x, yCenter - y);
				    setPixel(pix, xCenter - x, yCenter + y);
				    setPixel(pix, xCenter - x, yCenter - y);
				    setPixel(pix, xCenter + y, yCenter + x);
				    setPixel(pix, xCenter + y, yCenter - x);
				    setPixel(pix, xCenter - y, yCenter + x);
				    setPixel(pix, xCenter - y, yCenter - x);
				    x += 1;
				    y = (uint) (Math.Sqrt(r2 - x*x) + 0.5);
			}
			if (x == y) {
				    setPixel(pix, xCenter + x, yCenter + y);
				    setPixel(pix, xCenter + x, yCenter - y);
				    setPixel(pix, xCenter - x, yCenter + y);
				    setPixel(pix, xCenter - x, yCenter - y);
			}
		}

		public uint[] getAcc() {
			return acc;
		}

		public int getProgress() {
			return (int)progress;
		}

        public uint[] GetResult()
        {
            return output;
        }

        public Bitmap GetBitmapResult()
        {
            var resBitmap = new Bitmap(width, height);
            for(int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    resBitmap.SetPixel(x, y, Color.FromArgb((int)output[x + (width * y)]));
                }
            return resBitmap;
        }
    }
}
