using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Plugin.Media.Abstractions;
using System.IO;
using XamarinAdvanceDemo.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer_android))]
namespace XamarinAdvanceDemo.Droid
{
    class ImageResizer_android: ImageResizer
    {
        public byte[] resize(MediaFile photo,int compress)
        {
            Bitmap originalImage = BitmapFactory.DecodeFile(photo.Path);
        //    Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, Constant.ImageSize, originalImage.Height * originalImage.Width / Constant.ImageSize, false);
            using (MemoryStream ms = new MemoryStream())
            {
                originalImage.Compress(Bitmap.CompressFormat.Jpeg, compress, ms);
                return (ms.ToArray());
            }
        }
    }
}