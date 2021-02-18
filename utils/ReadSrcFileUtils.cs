using System;
using System.Collections;
using TrafficWizard.model;
using System.IO;

namespace TrafficWizard.utils
{
    public class ReadSrcFileUtils
    {
        //正确请求tag
        const string ACCEPTED_TAG = "accepted";

        //条文参数索引
        const int LOG_DATE_YMD_INDEX = 0;
        const int LOG_DATE_HMS_INDEX = 1;
        const int LOG_IP_ADDRESS_INDEX = 2;
        const int LOG_LINK_URL_INDEX = 4;

        //IP 地址索引
        const int IP_PROTOCOL_INDEX = 0;
        const int IP_ADDR_INDEX = 1;
        const int IP_PORT_INDEX = 2;

        //URL 地址索引
        const int URL_PROTOCOL_INDEX = 0;
        const int URL_LINK_URL_INDEX = 1;
        const int URL_PORT_INDEX = 2;

        private Queue srcFileQueue; //日志待处理队列
        private Queue reportQueue; //落盘对队列

        public ReadSrcFileUtils(Queue srcFileQueue,Queue reportQueue)
        {
            this.srcFileQueue = srcFileQueue;
            this.reportQueue = reportQueue;
        }


        //读取源文件，符合正常请求，则入队；失败则清空队，并反回异常
        public string ReadSrcFile(string srcFilepath)
        {

            if (this.srcFileQueue == null)
            {
                return ConstModel.QUEUE_IS_NULL; 
            }
            string line = "";

            StreamReader file = null;
            

            try
            {
                file=new StreamReader(srcFilepath);

                while ((line = file.ReadLine())
                    != null)
                {
                    srcFileQueue.Enqueue(line);
                }

            }
            catch(Exception e)
            {
                srcFileQueue.Clear();
                return e.ToString();
            }
            finally
            {
                file.Close();
            }
            return "";
        }

        //处理日志字符串,异步多线程处理
        //最终格式： ${date},${ipaddress},${ip} ${zone},${link url}\n
        public void logLineHandler()
        {
            if (this.srcFileQueue == null)
            {
                return;
            }

            //2021/02/17 11:09:39 tcp:116.230.177.246:0 accepted tcp:phd.aws.amazon.com:443
            string line =(string)this.srcFileQueue.Dequeue();

            if (!line.Contains(ACCEPTED_TAG)) { //期待处理的内容
                return;
            }

            string[] lines = line.Split(" ");
            string dateStr = string.Format(@"{0} {1}", lines[LOG_DATE_YMD_INDEX], line[LOG_DATE_HMS_INDEX]);

            string[] ipaddrs = lines[LOG_IP_ADDRESS_INDEX].Split(":");
            string ipAddrStr = ipaddrs[IP_ADDR_INDEX];

            //TODO：此处会对ip地址进行查询,http调用处暂未调试，暂不实现
            string ipZoneStr = "";

            string[] urls = lines[LOG_LINK_URL_INDEX].Split(":");
            string urlStr = urls[URL_LINK_URL_INDEX];

            this.reportQueue.Enqueue(string.Format(@"{0},{1},{2},{3}",dateStr,ipAddrStr,ipZoneStr, urlStr));
        }

    
    }
}
