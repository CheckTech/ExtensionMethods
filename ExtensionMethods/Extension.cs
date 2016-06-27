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
        ///Converts 24bpp format to 32bpp
        public static System.Windows.Media.Imaging.BitmapSource GetImageFromClipboard()
        {
            if (System.Windows.Clipboard.GetDataObject() == null) return null;
            if (System.Windows.Clipboard.GetDataObject().GetDataPresent(System.Windows.DataFormats.Dib))
            {
                var dib = ((System.IO.MemoryStream)System.Windows.Clipboard.GetData(System.Windows.DataFormats.Dib)).ToArray();
                var width = BitConverter.ToInt32(dib, 4);
                var height = BitConverter.ToInt32(dib, 8);
                var bpp = BitConverter.ToInt16(dib, 14);
                if (bpp == 32)
                {
                    var gch = System.Runtime.InteropServices.GCHandle.Alloc(dib, System.Runtime.InteropServices.GCHandleType.Pinned);
                    System.Drawing.Bitmap bmp = null;
                    try
                    {
                        var ptr = new IntPtr((long)gch.AddrOfPinnedObject() + 40);
                        bmp = new System.Drawing.Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, ptr);
                        return new System.Drawing.Bitmap(bmp).genereteBitmapSource();
                    }
                    finally
                    {
                        gch.Free();
                        if (bmp != null) bmp.Dispose();
                    }
                }
            }
            
            return System.Windows.Clipboard.ContainsImage() ? System.Windows.Clipboard.GetImage() : null;
        }
    }
}
