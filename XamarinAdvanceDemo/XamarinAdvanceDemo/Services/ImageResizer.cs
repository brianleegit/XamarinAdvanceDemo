using Plugin.Media.Abstractions;
using System.IO;

//https://github.com/xamarin/xamarin-forms-samples/blob/master/XamFormsImageResize/XamFormsImageResize/ImageResizer.cs
public interface ImageResizer
{
    byte[] resize(MediaFile photo,int compress); //note that interface members are public by default
}