using System;
using System.Collections.Generic;
using System.Text;


namespace ExtensionMethods
{
    public static class Extension
    {
        public static System.Windows.Media.SolidColorBrush ToSystemColor(this VGCore.Color corelColor)
        {
            string hexValue = corelColor.HexValue;
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hexValue);
            return new System.Windows.Media.SolidColorBrush(color);
            
        }
        public static System.Windows.Media.Imaging.BitmapSource genereteBitmapSource(this System.Drawing.Bitmap resource)
        {
            var bitmap = new System.Drawing.Bitmap(resource);
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                                                  IntPtr.Zero,
                                                                                  System.Windows.Int32Rect.Empty,
                                                                                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
                  );

            bitmap.Dispose();
            return bitmapSource;
        }
    }
}
