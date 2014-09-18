using System;
using Common.Logging;
using TRE.Config.Impl;
using TRE.Ext;
using TRE.Tools;

namespace TRETest
{
	/**
     * 用于海航、瑞丽、奥凯等航空公司的结算系统下载报表
	 * 【这个类无需修改，请勿修改！可以直接调用里面的public方法】
     */
    public class DownloadReport
    {

        public const String HUB2B = "HUB2B";
        public const String BKB2B = "BKB2B";
        public const String DRB2B = "DRB2B";
        public const String GJB2B = "GJB2B";
        public const String UQB2B = "UQB2B";
        public const String FUB2B = "FUB2B";

        public const String ZH_CN = "zh_CN";
        public const String EN_US = "en_US";

        /* 退票报表离线任务编号 */
        private const String TASK_NO_REFUND_HU = "MYUC201407301505491181";
        private const String TASK_NO_REFUND_BK = "PBTQ201407311028571201";
        private const String TASK_NO_REFUND_DR = "YCWD201407311029591202";
        private const String TASK_NO_REFUND_GJ = "VEMR201409121551101883";
        private const String TASK_NO_REFUND_UQ = "KYIA201409121552091884";
        private const String TASK_NO_REFUND_FU = "NJVY201409151509461902";


        /* 销售报表离线任务编号 */
        private const String TASK_NO_SALE_HU = "HXNW201408061455521301";
        private const String TASK_NO_SALE_BK = "LLWU201408061522381321";
        private const String TASK_NO_SALE_DR = "UOQJ201408061522411322";
        private const String TASK_NO_SALE_GJ = "FGBC201409121552121885";
        private const String TASK_NO_SALE_UQ = "RGXS201409121552131886";
        private const String TASK_NO_SALE_FU = "YUOL201409151509431901";

        private static ILog LOG = LogManager.GetCurrentClassLogger();

        /**
         * 下载指定航空公司的退票数据报表
         * 默认下载最新生成的报表
         * 
         * @param savePath 报表存储路径，以/结尾，如/home/rpt/
         * @param saveFileName 报表存储文件名，如refund-20140401.zip。如果存储的目标文件已存在，则会覆盖
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport 常量定义
         * @param lang 指定服务端返回信息所采用的语言 比如 zh_CN 或者 en_US，lang可以为空（默然用en_US）
         * @return 最终生成的文件名，如果生成失败，则返回"";
         * @下载过程中出现的异常
         */
        public static String downloadRefundReport(String savePath, String saveFileName, String airlineCode, String lang)
        {
            return downloadB2bRefundReport(savePath, saveFileName, airlineCode, lang, "");
        }

        /**
         * 下载指定航空公司的退票数据报表
         * 
         * @param savePath 报表存储路径，以/结尾，如/home/rpt/
         * @param saveFileName 报表存储文件名，如refund-20140401.zip。如果存储的目标文件已存在，则会覆盖
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport 常量定义
         * @param lang 指定服务端返回信息所采用的语言 比如 zh_CN 或者 en_US，lang可以为空（默然用en_US）
         * @param fileCreateTime 文件生成时间  如2014-09-16 即下载2014-09-16的统计报表 
         * @return 最终生成的文件名，如果生成失败，则返回"";
         * @下载过程中出现的异常
         */
        public static String downloadRefundReport(String savePath, String saveFileName, String airlineCode, String lang, String fileCreateTime)
        {
            return downloadB2bRefundReport(savePath, saveFileName, airlineCode, lang, fileCreateTime);
        }

        /**
         * 下载指定航空公司的销售数据报表
         * 默认下载最新生成的报表
         * 
         * @param savePath 报表存储路径，以/结尾，如/home/rpt/
         * @param saveFileName 报表存储文件名，如sales-20140401.zip。如果存储的目标文件已存在，则会覆盖
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport 常量定义
         * @param lang 指定服务端返回信息所采用的语言 比如 zh_CN 或者 en_US，lang可以为空（默然用en_US）
         * @return 最终生成的文件名，如果生成失败，则返回"";
         * @下载过程中出现的异常
         */
        public static String downloadSaleReport(String savePath, String saveFileName, String airlineCode, String lang)
        {
            return downloadB2bSaleReport(savePath, saveFileName, airlineCode, lang, "");
        }

        /**
         * 下载指定航空公司的销售数据报表
         * 默认下载最新生成的报表
         * 
         * @param savePath 报表存储路径，以/结尾，如/home/rpt/
         * @param saveFileName 报表存储文件名，如sales-20140401.zip。如果存储的目标文件已存在，则会覆盖
         * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport 常量定义
         * @param lang 指定服务端返回信息所采用的语言 比如 zh_CN 或者 en_US，lang可以为空（默然用en_US）
         * @param fileCreateTime 文件生成时间  如2014-09-16 即下载2014-09-16的统计报表 
         * @return 最终生成的文件名，如果生成失败，则返回"";
         * @下载过程中出现的异常
         */
        public static String downloadSaleReport(String savePath, String saveFileName, String airlineCode, String lang, String fileCreateTime)
        {
            return downloadB2bSaleReport(savePath, saveFileName, airlineCode, lang, fileCreateTime);
        }


        private static String downloadB2bRefundReport(String savePath, String saveFileName, String airlineCode, String lang, String fileCreateTime)
        {
            String taskNo = null;
            if (equalsIgnoreCase(HUB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_HU;
            }
            else if (equalsIgnoreCase(BKB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_BK;
            }
            else if (equalsIgnoreCase(DRB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_DR;
            }
            else if (equalsIgnoreCase(UQB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_UQ;
            }
            else if (equalsIgnoreCase(GJB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_GJ;
            }
            else if (equalsIgnoreCase(FUB2B, airlineCode))
            {
                taskNo = TASK_NO_REFUND_FU;
            }
            else
            {
                throw new TreClientException(ErrorCode.EC0002, "airlineCode = " + airlineCode);
            }
            return downloadReport(taskNo, savePath, saveFileName, airlineCode, lang, fileCreateTime);
        }

        private static String downloadB2bSaleReport(String savePath, String saveFileName, String airlineCode, String lang, String fileCreateTime)
        {
            String taskNo = null;
            if (equalsIgnoreCase(HUB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_HU;
            }
            else if (equalsIgnoreCase(BKB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_BK;
            }
            else if (equalsIgnoreCase(DRB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_DR;
            }
            else if (equalsIgnoreCase(GJB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_GJ;
            }
            else if (equalsIgnoreCase(UQB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_UQ;
            }
            else if (equalsIgnoreCase(FUB2B, airlineCode))
            {
                taskNo = TASK_NO_SALE_FU;
            }
            else
            {
                throw new TreClientException(ErrorCode.EC0002, "airlineCode = " + airlineCode);
            }
            return downloadReport(taskNo, savePath, saveFileName, airlineCode, lang, fileCreateTime);
        }

        private static String downloadReport(String taskNo, String savePath, String saveFileName, String airlineCode, String lang, String fileCreateTime)
        {
            LOG.Info("[DownloadReport begin...]taskNo=" + taskNo + ";savePath=" + savePath + ";saveFileName=" + saveFileName);

            if (isNullStr(taskNo))
            {
                throw new TreClientException(ErrorCode.EC0002, "parameter[taskNo] = null");
            }
            if (isNullStr(savePath))
            {
                throw new TreClientException(ErrorCode.EC0002, "parameter[savePath] = null");
            }
            if (isNullStr(saveFileName))
            {
                throw new TreClientException(ErrorCode.EC0002, "parameter[saveFileName] = null");
            }
            if (!ZH_CN.Equals(lang) && !EN_US.Equals(lang))
            {
                lang = null;
            }

            OAuth1ConfigImpl authConfig = new OAuth1ConfigImpl(airlineCode, lang);

            String fileFullPath = ServiceFactory.getEasyCycReportDownloadService().downloadCycReportFile(
            authConfig, savePath, saveFileName, taskNo, fileCreateTime);

            LOG.Info("[DownloadReport end]fileFullPath=" + fileFullPath);

            return fileFullPath;
        }

        private static Boolean isNullStr(String str)
        {
            if (str == null || str.Trim().Length == 0)
            {
                return true;
            }
            return false;
        }
        private static Boolean equalsIgnoreCase(String res, String target)
        {

            if (target == null)
            {
                return false;
            }
            return res.ToLower().Equals(target.ToLower());

        }

    }
}