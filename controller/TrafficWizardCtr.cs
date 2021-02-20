using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TrafficWizard.helpers;
using TrafficWizard.model;
using TrafficWizard.utils;

namespace TrafficWizard.controller
{
    public class TrafficWizardCtr
    {
        
        //配置文件
        ConfigModel config = null;

        //初始化操作队列
        static Queue srcFileContentQ = new Queue();
        static Queue reportContentQ = new Queue();

        ReadSrcFileUtils rSFU = null;
        HttpClientUtils hc = null;

        public TrafficWizardCtr()
        {
            config = ConfigHelpers.GetConfigHelper().GetConfig(ConstModel.CONFIG_FILE_PATH);
            
            rSFU = new ReadSrcFileUtils(srcFileContentQ, reportContentQ);
            hc = new HttpClientUtils(config.token);
        }

        public void Run()
        {


            bool loop = true; //标志位

            this.doReadToQueue();

            //ThreadPool.SetMaxThreads(10, 10);

            //DEBUG:
            //this.doContentToReport(null);

           while (true)
            {
                if(srcFileContentQ.Count > 0)
                {
                    //ThreadPool.QueueUserWorkItem(this.doContentToReport,config.token);
                    this.doContentToReport();
                    loop = false;
                }
                else if (srcFileContentQ.Count == 0 && !loop)
                {
                    break;
                }

            }

        }

        private async Task doReadToQueue()
        {
            if (this.config == null)
            {
                return;
            }

            await Task.Run(() =>
                {
                    rSFU.ReadSrcFile(this.config.srcFilePath);
                });
        }

        private void doContentToReport()
        {
            if (this.config == null)
            {
                return;
            }

            //await Task.Run(() =>
            //{
                rSFU.logLineHandler(config.token);
            //});        
        }

    }
}
