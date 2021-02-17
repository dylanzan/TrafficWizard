using System;
using TrafficWizard.model;
using Newtonsoft.Json;

namespace TrafficWizard.utils
{
    public class JsonParseUtils
    {
        public string JsonParse(string jsonRes)
        {
            // Console.WriteLine(jsonRes);

            string ipinfoRes = "";
            //Console.WriteLine(ipifo.code);
            if (!string.IsNullOrEmpty(jsonRes))
            {
                IpInfoModel ipinfo = JsonConvert.DeserializeObject<IpInfoModel>(jsonRes);
                ipinfoRes = string.Format(" Area:{0}  Local:{1}", ipinfo.data.cuntry, ipinfo.data.local);
                return ipinfoRes;
            }
            else
            {
                return ConstModel.PROMPT_RETRY;
            }
        }
    }
}
