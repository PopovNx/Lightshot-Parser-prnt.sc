using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prnt.sc_Pars
{
    class Program
    {
        public static void GetScrin(string Url)
        {
            using (var wc = new WebClient())
            {
                var nm = $@"Images\{Url.Split("sc/")[1]}.png";
                if (File.Exists(nm)) return;
                try
                {
                    wc.Headers.Set(HttpRequestHeader.AcceptLanguage, "eu");
                    wc.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.102011-10-16 20:23:10");
                    Console.WriteLine($"GetPage {Url}");
                    var str = wc.DownloadString(Url);
                    var r = new Regex(@"<meta name=\""twitter:image:src\"" content=\""(.*?)\""\/>");
                    var m = r.Match(str).Groups[1].Value;
                    Console.WriteLine($"GetFile {m}");
                    wc.DownloadFile(m, nm);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }

        }
        static void Main(string[] args)
        {
            var Chars = "0123456789abcdefghijkmnopqrstuvwxyz";
            var Start = "v83";
            for (int ia = 0; ia < Chars.Length; ia++)
                for (int ib = 0; ib < Chars.Length; ib++)
                    for (int ic = 0; ic < Chars.Length; ic++)
                        GetScrin($"https://prnt.sc/{Start}{Chars[ia]}{Chars[ib]}{Chars[ic]}");
        }
    }
}
