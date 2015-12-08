using System;
using System.Drawing;
using System.Drawing.Imaging;
using HydrantWiki.Library.Managers;
using TreeGecko.Library.Common.Helpers;

namespace HydrantWiki.Library.Helpers
{
    public static class ImageProcessingHelper
    {
        public static void CreateScaledImages(HydrantWikiManager _manager,
                                              Guid _originalGuid)
        {
            //Get the image from the originals folder from S3
            //Load the original
            byte[] data = _manager.GetOriginal(_originalGuid, ".jpg");

            try
            {
                using (Image full = ImageHelper.GetImage(data))
                {
                    //Standard sizes must fit in 800 x 800
                    SizeF thumbSize = ImageSizeHelper.SizeThatFits(full, 800);
                    using (Image reduced = ImageHelper.GetThumbnail(full,
                                                                    Convert.ToInt32(thumbSize.Height),
                                                                    Convert.ToInt32(thumbSize.Width)))
                    {
                        data = ImageHelper.GetBytes(reduced, ImageFormat.Jpeg);

                        _manager.PersistWebImage(_originalGuid, ".jpg", "image/jpg", data);
                    }


                    //Thumbnails must fit in 100 x 100
                    thumbSize = ImageSizeHelper.SizeThatFits(full, 100);
                    using (Image reduced = ImageHelper.GetThumbnail(full,
                                                                    Convert.ToInt32(thumbSize.Height),
                                                                    Convert.ToInt32(thumbSize.Width)))
                    {
                        data = ImageHelper.GetBytes(reduced, ImageFormat.Jpeg);

                        _manager.PersistThumbnailImage(_originalGuid, ".jpg", "image/jpg", data);
                    }
                }
            }
            catch (Exception ex)
            {
                //Bad Image
                TraceFileHelper.Exception(ex);
            }
        }

        public static string GetPath(Guid _fileGuid, string _folder, string _fileExtension, bool _isThumb)
        {
            string file = _fileGuid.ToString().ToLower();
            string first3 = file.Substring(0, 3);
            string key;

            if (_isThumb)
            {
                key = String.Format(@"{0}/{1}/{2}_thumb{3}", _folder, first3, file, _fileExtension);
            }
            else
            {
                key = String.Format(@"{0}/{1}/{2}{3}", _folder, first3, file, _fileExtension);
            }

            return key;
        }
    }
}
