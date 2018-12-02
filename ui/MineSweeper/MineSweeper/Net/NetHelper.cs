using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Net
{
    class NetHelper
    {
        public static async Task<string> HttpGet(string url, string getDataStr = null, string encode = "utf-8", CookieCollection cc = null)
        {
            //Get 方式提交数据只需要在网址后面使用？即可，如果多组数据，需要在提交的时候使用&连接
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (getDataStr == null ? "" : "?") + getDataStr);
            //将 Cookie 写入
            request.CookieContainer = new CookieContainer();
            if (cc != null)
            {
                request.CookieContainer.Add(new Uri(url), cc);
            }
            //设置 request 的方式为 GET
            request.Method = "GET";
            //设置 HTTP 头的内容类型,如果需要在 Http 头中加入其他内容，可以直接使用 request.Headers["头名称"]="头内容" 来添加
            request.ContentType = "text/html;charset=UTF-8";
            //通过异步方法拿到回应
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            //写入流
            Stream myResponseStream = response.GetResponseStream();
            //注册编码转换器（这里同之前 WPF 开发中不同，需要事先注册编码转换器才能使用）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //进行内容编码转换
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            //将转换后的内容转化为字符串并返回
            string retString = myStreamReader.ReadToEnd();
            return retString;
        }
    }
}
