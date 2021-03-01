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
            
            rSFU = new ReadSrcFileUtils(srcFileContentQ, reportContentQ,config.srcFilePath,config.tagetFilePath);
            hc = new HttpClientUtils(config.token);
        }

        public void Run()
        {
            try
            {
                        
               var t1=this.doReadToQueue();
               var t2= this.doContentToReport();              
               var t3= this.doReportToCSV();

               Task.WaitAny(new Task[]{t1,t2,t3});
            }
            catch
            {
                throw;
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
                rSFU.ReadSrcFile();
            });
            
                
        }

        private async Task doContentToReport()
        {
            if (this.config == null)
            {
                return;
            }

           
            await Task.Run(() =>
            {
               
            rSFU.logLineHandler(config.token);
                       
            });
           
        }

        private async Task doReportToCSV()
        {
            if (this.config == null)
            {
                return;
            }

            await Task.Run(() =>
            {
                rSFU.reportToCSV();
            });
            
        }

    }
}
