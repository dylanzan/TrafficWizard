using System;
using System.Net.Http;
using TrafficWizard.model;

namespace TrafficWizard.utils
{
    public class HttpClientUtils
    {

        private string TOKEN_KEY = "";
        private const string IPV4_REQUEST_URL = "http://101.133.135.241/";
        private const string IPV6_REQUEST_URL = "http://freeapi.ipip.net/";
        private const string USER_AGENT_VALUE = "Chrome";

        public HttpClientUtils(string tOKEN_KEY)
        {
            TOKEN_KEY = tOKEN_KEY;
        }

        private string HttpGetAsync(string httpUrl)
        {
            HttpClient hc = null;

            string responseData = String.Empty;
            try
            {
                hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("User-Agent", USER_AGENT_VALUE);
                hc.DefaultRequestHeaders.Add("Authorization",TOKEN_KEY);
                HttpResponseMessage hrm = hc.GetAsync(httpUrl).Result;

                if (hrm.IsSuccessStatusCode)
                {
                    responseData = hrm.Content.ReadAsStringAsync().Result;
                }
            }
            catch
            {
                throw;
            }
            return responseData;
        }

        public string InquireIpInfo(string ipAddress)
        {

            RegexUtils reu = new RegexUtils();
            JsonParseUtils jsu = null;

            string ipZone = "";

            //判断ip 类型
            switch (reu.IPCheckForS(ipAddress))
            {
                case ConstModel.IPV4:
                    if (reu.IPCheck(ipAddress))
                    {
                        jsu = new JsonParseUtils();
                        string ipv4JsonResponse = this.HttpGetAsync(IPV4_REQUEST_URL + ipAddress);
                        if (String.IsNullOrEmpty(ipv4JsonResponse)) //server端问题，有时需要请求两次，才能成功
                        {
                            ipv4JsonResponse = this.HttpGetAsync(IPV4_REQUEST_URL + ipAddress);
                            ipZone = jsu.JsonParse(ipv4JsonResponse);
                            break;
                        }
                        ipZone = jsu.JsonParse(ipv4JsonResponse);
                    }
                    break;
                case ConstModel.IPV6:
                    jsu = new JsonParseUtils();
                    ipZone = this.HttpGetAsync(IPV6_REQUEST_URL + ipAddress);
                    break;
                case ConstModel.NOTHING:
                    break;
                default:
                    ipZone = ConstModel.NO_VALUE;
                    break;
            }
            return ipZone;
        }

        //暂时仅支持IPV4
        public string InquireLocalIp()
        {
            string localIpZone = "";
            try
            {
                localIpZone = this.HttpGetAsync(IPV4_REQUEST_URL);
                if (!String.IsNullOrEmpty(localIpZone))
                {
                    return localIpZone;
                }
                else //server端问题，有时需要请求两次，才能获取结果，出现此现象概率很低
                {
                    localIpZone = this.HttpGetAsync(IPV4_REQUEST_URL);
                    return localIpZone;
                }
            }
            catch
            {
                throw;
            }
        }


    }
}

