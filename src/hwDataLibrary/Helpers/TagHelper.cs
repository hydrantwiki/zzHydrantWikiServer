using System;
using HydrantWiki.Library.Objects;
using TreeGecko.Library.Common.Helpers;

namespace HydrantWiki.Library.Helpers
{
    public static class TagHelper
    {
        static TagHelper()
        {
            BaseUrl = Config.GetSettingValue("S3Protocol", "https") + "://" + Config.GetSettingValue("S3Bucket_Images") + "/" + Config.GetSettingValue("S3MediaRootFolder");
        }

        public static string BaseUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetUrl(this Tag _tag,  bool _showThumb)
        {
            string temp = GetAWSPath(_tag, _showThumb);

            if (temp.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                return temp;
            }

            return Config.GetSettingValue("S3Protocol", "https") + "://" + temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetUrl(this TagRow _tag, bool _showThumb)
        {
            string temp = GetAWSPath(_tag, _showThumb);

            if (temp.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                return temp;
            }

            return Config.GetSettingValue("S3Protocol", "https") + "://" + temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_hydrant"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetUrl(this Hydrant _hydrant, bool _showThumb)
        {
            if (_hydrant.PrimaryImageGuid != null)
            {
                string temp = GetAWSPath(_hydrant.PrimaryImageGuid.Value, _showThumb);

                if (temp.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
                {
                    return temp;
                }

                return Config.GetSettingValue("S3Protocol", "https") + "://" + temp;
            }

            return Config.GetSettingValue("NoImageURL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_imageGuid"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetAWSPath(Guid _imageGuid, bool _showThumb)
        {
            string file = _imageGuid.ToString().ToLower();
            string first3 = file.Substring(0, 3);
            string key;

            if (_showThumb)
            {
                key = string.Format(@"{0}/{1}/{2}_thumb{3}", BaseUrl, first3, file, ".jpg");
            }
            else
            {
                key = string.Format(@"{0}/{1}/{2}{3}", BaseUrl, first3, file, ".jpg");
            }

            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetAWSPath(this TagRow _tag, bool _showThumb)
        {
            if (_tag.ImageGuid != null)
            {
                return GetAWSPath(_tag.ImageGuid.Value, _showThumb);
            }

            return Config.GetSettingValue("NoImageURL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_showThumb"></param>
        /// <returns></returns>
        public static string GetAWSPath(this Tag _tag, bool _showThumb)
        {
            if (_tag.ImageGuid != null)
            {
                return GetAWSPath(_tag.ImageGuid.Value, _showThumb);
            }

            return Config.GetSettingValue("NoImageURL");
        }

    }
}
