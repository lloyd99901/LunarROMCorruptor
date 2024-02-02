using System;
using System.Drawing;

namespace LunarROMCorruptor
{
    internal class ByteView
    {
        public static Bitmap ConvertByteToImage(byte[] data, bool enableColour)
        {

            if (data == null) return null; //Ignore invalid null data

            int bytesPerPixel = 3; // Assuming 3 bytes for RGB

            int imageSize = (int)Math.Sqrt(data.Length / bytesPerPixel);
            int width = imageSize;
            int height = data.Length / (width * bytesPerPixel);

            Bitmap image = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int byteIndex = (i * width + j) * bytesPerPixel;

                    byte red, green, blue;

                    if (byteIndex + 2 < data.Length)
                    {
                        red = data[byteIndex];
                        green = data[byteIndex + 1];
                        blue = data[byteIndex + 2];
                    }
                    else
                    {
                        // Handle the case where there are not enough bytes left
                        // For example, you could set a default color or skip the pixel
                        red = green = blue = 0; // Default to black
                    }

                    Color color;

                    if (enableColour)
                    {
                        color = Color.FromArgb(red, green, blue);
                    }
                    else
                    {
                        byte grayscaleValue = (byte)((red + green + blue) / 3);
                        color = Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue);
                    }

                    image.SetPixel(j, i, color);
                }
            }
            return image;
        }

        public static Bitmap FlipImage(Bitmap original, bool horizontal, bool vertical)
        {
            if (original == null)
                return null;

            Bitmap flippedImage = (Bitmap)original.Clone();

            if (horizontal)
                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            if (vertical)
                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return flippedImage;
        }
    }
}
