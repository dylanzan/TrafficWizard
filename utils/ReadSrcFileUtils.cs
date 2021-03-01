using System;
using System.Collections;
using TrafficWizard.model;
using System.IO;
using System.Text;

namespace TrafficWizard.utils
{
    public class ReadSrcFileUtils
    {
        //正确请求tag
        private const string ACCEPTED_TAG = "accepted";
        private const string TCP_TAG = "tcp";

        private const string REPORT_SUFFIX = ".csv";

        //条文参数索引
        private const int LOG_DATE_YMD_INDEX = 0;
        private const int LOG_DATE_HMS_INDEX = 1;
        private const int LOG_IP_ADDRESS_INDEX = 2;
        private const int LOG_LINK_URL_INDEX = 4;

        //IP 地址索引
        private const int IP_PROTOCOL_INDEX = 0;
        private const int IP_ADDR_INDEX = 1;
        private const int IP_PORT_INDEX = 2;

        //URL 地址索引
        private const int URL_PROTOCOL_INDEX = 0;
        private const int URL_LINK_URL_INDEX = 1;
        private const int URL_PORT_INDEX = 2;

        private Queue srcFileQueue; //日志待处理队列
        private Queue reportQueue; //落盘对队列

        private string srcFilePath;
        private string reportTagetFilePath;

        public ReadSrcFileUtils(Queue srcFileQueue,Queue reportQueue,string srcFilePath, string reportTagetFilePath)
        {
            this.srcFileQueue = srcFileQueue;
            this.reportQueue = reportQueue;

            this.srcFilePath = srcFilePath;
            this.reportTagetFilePath = reportTagetFilePath;
        }


        //读取源文件，符合正常请求，则入队；失败则清空队，并反回异常
        public string ReadSrcFile()
        {
            Console.WriteLine("read src file");

            if (this.srcFileQueue == null)
            {
                return ConstModel.QUEUE_IS_NULL; 
            }
            string line = "";

            StreamReader sr=null;
            
            try
            {
                sr=new StreamReader(this.srcFilePath, Encoding.Default);

                while ((line = sr.ReadLine())
                    != null)
                {
                    srcFileQueue.Enqueue(line);
                    //Console.WriteLine(" srcfile's content is : "+line);
                }

            }
            catch(Exception e)
            {
                throw;
                //srcFileQueue.Clear();
                //return e.ToString();
            }
            finally
            {
                sr.Close();
            }
            return "";
        }

        //处理日志字符串,异步多线程处理
        //最终格式： ${date},${ipaddress},${ip} ${zone},${link url}\n
        public void logLineHandler(string token)
        {
            Console.WriteLine("log line handler to check ipinfo:{0} ",this.srcFileQueue.Count);

            /*if (this.srcFileQueue == null && !loop)
            {
                return;
            }*/

            HttpClientUtils hc = null;
            bool loop;
            try
            {

                hc = new HttpClientUtils(token);
                //2021/02/17 11:09:39 tcp:116.230.177.246:0 accepted tcp:phd.aws.amazon.com:443
                string line = (string)this.srcFileQueue.Dequeue();
                loop = true;
                while (true)
                {
                    if (this.srcFileQueue.Count > 0)
                    {
                        if (!line.Contains(ACCEPTED_TAG))
                        { //期待处理的内容
                            return;
                        }

                        string[] lines = line.Split(" ");
                        string dateStr = string.Format(@"{0} {1}", lines[LOG_DATE_YMD_INDEX], line[LOG_DATE_HMS_INDEX]);

                        string[] ipaddrs = lines[LOG_IP_ADDRESS_INDEX].Split(":");
                        string ipAddrStr = "";

                        if (lines[LOG_IP_ADDRESS_INDEX].Contains(TCP_TAG))
                        {
                            ipAddrStr = ipaddrs[IP_ADDR_INDEX];
                        }
                        else
                        {
                            ipAddrStr = ipaddrs[0];
                        }

                        string ipZoneStr = "";
                        //TODO：此处会对ip地址进行查询,http调用处暂未调试，暂不实现
                        ipZoneStr = hc.InquireIpInfo(ipAddrStr);

                        string[] urls = lines[LOG_LINK_URL_INDEX].Split(":");
                        string urlStr = urls[URL_LINK_URL_INDEX];


                        //dylan:DEBUG
                        Console.WriteLine(string.Format(@"{0},{1},{2},{3}", dateStr, ipAddrStr, ipZoneStr, urlStr));

                        this.reportQueue.Enqueue(string.Format(@"{0},{1},{2},{3}", dateStr, ipAddrStr, ipZoneStr, urlStr));
                        loop = false;
                    }
                    else if (this.srcFileQueue.Count == 0 && !loop)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

           }

        public void reportToCSV()
        {

            Console.WriteLine("reprot to csv");
            /*if (this.reportQueue == null)
            {
                return;
            }*/

            bool loop = true;

            string fielNamePrefix = DateTime.Now.ToString("yyyyMMdd");

            try
            {
                string reportFileName=string.Format("{0}{1}{2}", this.reportTagetFilePath,fielNamePrefix,REPORT_SUFFIX);
                using (StreamWriter sw = new StreamWriter(reportFileName, true))
                {

                    while (true)
                    {
                        if (this.reportQueue.Count > 0)
                        {
                            string line = (string)this.reportQueue.Dequeue();
                            sw.WriteLine(line);
                            loop = false;
                        }
                        else if (reportQueue.Count == 0 && !loop)
                        {
                            break;
                        }
                    }

                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

    
    }
}
