using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;

namespace FishRandomSelector.Tools
{
    //此版本专为班级使用创建，不进行在线验证
    public enum State { 可用, 可用_不支持, 不可用 }
    public struct Result
    {
        public bool Can_use;
        public State Verify_state;
        public string Verify_Result;
    }
    static class Verify
    {

        public static Result Verify_Online()
        {
            Result result;/*
            result.Can_use = false;
            string pageHtml=null;
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(Info.Info.VerifyLink); //从指定网站下载数据
                pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句
            }
            catch { }
            string Zhengze =@"Canuse:\{.*\}";
            Match match = Regex.Match(pageHtml, Zhengze);
            if(match.Value=="Canuse:{True}")
            {
                result.Can_use = true;
            }
            else
            {
                result.Can_use = false;
            }*/
            result = new Result();
            result.Can_use = true;
            result.Verify_state = State.可用;
            result.Verify_Result = null;
            return result;
        }

    }
}
