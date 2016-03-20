using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace System.Drawing
{
    public static class ImagePExt
    {
        /// <summary>
        /// 获取高质量的缩略位图
        /// </summary>
        /// <param name="source">源图</param>
        /// <param name="width">缩略位图高</param>
        /// <param name="height">缩略位图宽</param>
        /// <returns>高质量的缩略位图</returns>
        /// <example>
        /// SourceImg.GetThumbnailBitmap(8, 8);
        /// </example>
        public static Bitmap GetThumbnailBitmap(this Image source, int width, int height)
        {
            var result = new Bitmap(width, height);
            result.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (var graphic = Graphics.FromImage(result))
            {
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphic.DrawImage(source, 0, 0, width, height);
            }
            return result;
        }

        /// <summary>
        /// 保存高质量的缩略位图
        /// </summary>
        /// <param name="source">源图</param>
        /// <param name="width">缩略位图高</param>
        /// <param name="height">缩略位图宽</param>
        /// <example>
        /// SourceImg.SaveThumbnailBitmap(8, 8, "C:/", "a", "jpg");
        /// </example>
        public static void SaveThumbnailBitmap(this Image source, int width, int height, string directory, string filename, string extension)
        {
            var physicalPath = directory + filename + "." + extension;

            using (var newImage = source.GetThumbnailBitmap(width, height))
            {
                using (var encoderParameters = new EncoderParameters(1))
                {
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                    newImage.Save(physicalPath,
                        ImageCodecInfo.GetImageEncoders()
                            .Where(x => x.FilenameExtension.Contains(extension.ToUpperInvariant()))
                            .FirstOrDefault(),
                        encoderParameters);
                }
            }
        }
    }
}