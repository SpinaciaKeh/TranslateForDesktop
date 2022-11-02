using System;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;

namespace TranslateForDesktop
{
    
    class Request
    {
        public RequestResult Translate(string content, string languageFrom,string languageTo)
        {
            string appid = "20221101001429865";
            string key = "UI8ryP4MwW0iFGFJpkRp";
            string salt = new Random().Next().ToString();
            string originStr = appid + content + salt + key;
            string sign = md5Compute(originStr);
            string test = md5Compute("2015063000000001apple143566028812345678");
            string FullRequest = "http://api.fanyi.baidu.com/api/trans/vip/translate?q=" + content + "&from=" + languageFrom + "&to=" + languageTo + "&appid=" + appid + "&salt=" + salt + "&sign=" + sign;
            string outputData=new WebClient().DownloadString(FullRequest);
            return JsonSerializer.Deserialize<RequestResult>(outputData); 
        }

        // q - query
        // from 
        // to
        // appid
        // salt - random string
        // sign - md5 of appid+q+salt+key

        // MD5 compute
        public static string md5Compute(string s)
        {
            StringBuilder sb = new StringBuilder();
            using(MD5 md5 = MD5.Create())
            {
                //byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                //foreach(byte b in hashValue)
                //{
                //    sb.Append(b);
                //}
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(s);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
            //return sb.ToString();
        }

        public class RequestResult
        {
            public string err_code { set; get; }
            public string err_msg { set; get; }
            public string from { set; get; }
            public string to { set; get; }
            public TranslateContent[] trans_result { set; get; }
        }

        public class TranslateContent
        {
            public string src { set; get; }
            public string dst { set; get; }
        }
    }
}
