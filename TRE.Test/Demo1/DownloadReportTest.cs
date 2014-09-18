using System;
using Common.Logging;
using TRE.Tools;

namespace TRETest
{
    class DownloadReportTest
    {
        // 本SDK用的是Common.Logging，可以支持多种底层的Log库，比如Log4net、NLog等，具体用法请百度（Common.Logging）。
        private static ILog log = LogManager.GetCurrentClassLogger();

		
        public static void Main()
        {
            // 默认下载昨天的“销售报表”（SaleReport）
            testdownloadSaleReport(DownloadReport.HUB2B);
            // 也可以指定下载某一天的报表
            testdownloadSaleReport(DownloadReport.HUB2B, "2014-09-12");

            // 调用 testdownloadRefundReport() 可以下载“退票报表”(RefundReport)
            testdownloadRefundReport(DownloadReport.HUB2B);

            // 切换 DownloadReport.UQB2B 参数可以下载不同航空公司的报表（注意，目前测试系统上GJ[长龙航空]退票报表还用不了）
            testdownloadRefundReport(DownloadReport.UQB2B);

            //        testdownloadRefundReport(DownloadReport.UQB2B,"2014-09-15");
            //        testdownloadRefundReport(DownloadReport.UQB2B);
            //        testdownloadSaleReport(DownloadReport.UQB2B,"2014-09-15");
            //        testdownloadSaleReport(DownloadReport.UQB2B);
        }

        /**
         * 下载最新航空公司的退票报表
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport
         */
        public static void testdownloadRefundReport(String airlineCode)
        {
            try
            {
                // 本地存储文件路径 
                String localFilePath = "D:/";
                // 注意，返回的是一个压缩过的zip文件，解压方法：DownloadReport.unzip("D:/解压位置/", "D:/报表.zip", "GBK");
                String localFileName = "rpt" + "-refund-" + RandomUtils.getRadomStr09AZaz(10) + ".zip";

                String fileName = DownloadReport.downloadRefundReport(localFilePath, localFileName, airlineCode, DownloadReport.ZH_CN);
                log.Info(airlineCode + "退票报表下载成功， 下载到的文件名: " + fileName);
            }
            catch (TreClientException e)
            {
                log.Error("错误代码：" + e.ErrorCode);
                log.Error("错误信息：" + e.Message);
            }
        }

        /**
         * 下载指定航空公司的退票报表
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport
         */
        public static void testdownloadRefundReport(String airlineCode, String filetime)
        {
            try
            {
                // 本地存储文件路径 
                String localFilePath = "D:/";
                // 注意，返回的是一个压缩过的zip文件，解压方法：DownloadReport.unzip("D:/解压位置/", "D:/报表.zip", "GBK");
                String localFileName = "rpt" + "-refund-" + RandomUtils.getRadomStr09AZaz(10) + ".zip";

                String fileName = DownloadReport.downloadRefundReport(localFilePath, localFileName, airlineCode, DownloadReport.ZH_CN, filetime);
                log.Info(airlineCode + "退票报表下载成功， 下载到的文件名: " + fileName);
            }
            catch (TreClientException e)
            {
                log.Error("错误代码：" + e.ErrorCode);
                log.Error("错误信息：" + e.Message);
            }
        }

        /**
         * 下载指定航空公司的销售报表
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport
         */
        public static void testdownloadSaleReport(String airlineCode)
        {
            try
            {
                // 本地存储文件路径 
                String localFilePath = "D:/";
                // 注意，返回的是一个压缩过的zip文件，解压方法：DownloadReport.unzip("D:/解压位置/", "D:/报表.zip", "GBK");
                String localFileName = "rpt" + "-sale-" + RandomUtils.getRadomStr09AZaz(10) + ".zip";

                String fileName = DownloadReport.downloadSaleReport(localFilePath, localFileName, airlineCode, DownloadReport.ZH_CN);
                log.Info(airlineCode + "销售报表下载成功， 下载到的文件名: " + fileName);
            }
            catch (TreClientException e)
            {
                log.Error("错误代码：" + e.ErrorCode);
                log.Error("错误信息：" + e.Message);
            }
        }

        /**
         * 下载指定航空公司的销售报表
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport
         */
        public static void testdownloadSaleReport(String airlineCode, String filetime)
        {
            try
            {
                // 本地存储文件路径 
                String localFilePath = "D:/";
                // 注意，返回的是一个压缩过的zip文件，解压方法：DownloadReport.unzip("D:/解压位置/", "D:/报表.zip", "GBK");
                String localFileName = "rpt" + "-sale-" + RandomUtils.getRadomStr09AZaz(10) + ".zip";

                String fileName = DownloadReport.downloadSaleReport(localFilePath, localFileName, airlineCode, DownloadReport.ZH_CN, filetime);
                log.Info(airlineCode + "销售报表下载成功， 下载到的文件名: " + fileName);
            }
            catch (TreClientException e)
            {
                log.Error("错误代码：" + e.ErrorCode);
                log.Error("错误信息：" + e.Message);
            }
        }
    }
}