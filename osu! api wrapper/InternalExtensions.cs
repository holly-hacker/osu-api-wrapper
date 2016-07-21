using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace osu_api_wrapper
{
    internal static class InternalExtensions
    {
        public static string ToUtf8String(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
        public static string ToMySqlDate(this DateTime datetime) => datetime.ToString("yyyy-MM-dd HH:mm:ss");

        public static string ToQueryString(this NameValueCollection nvc)    //found at https://stackoverflow.com/questions/829080/how-to-build-a-query-string-for-a-url-in-c
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
