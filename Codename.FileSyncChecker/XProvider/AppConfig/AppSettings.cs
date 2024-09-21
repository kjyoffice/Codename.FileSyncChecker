using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Codename.FileSyncChecker.XProvider.AppConfig
{
    public class AppSettings
    {
        private static string CheckWorkDirectory(string sDirectory)
        {
            return ((String.IsNullOrEmpty(sDirectory) == false) ? ((Directory.Exists(sDirectory) == true) ? sDirectory : String.Empty) : String.Empty);
        }

        private static string[] ParsingBooleanAndStringMixedValue(string sValue)
        {
            List<string> lListX = new List<string>();
            string[] sSplitTemp;
            string[] sResult;

            if (sValue.IndexOf(":") > -1)
            {
                sSplitTemp = sValue.Split(new string[] { ":" }, StringSplitOptions.None);

                // boolean이라 지정했지만 결국 false일 경우 사용하지 않는다고 정의했으므로 결국
                // 데이터 형 컨버팅이고 뭐고 문자열로 "true"인지의 경우만 체크하면 됨 ㅋㅋㅋ
                if (sSplitTemp[0].ToLower() == "true")
                {
                    if (String.IsNullOrEmpty(sSplitTemp[1]) == false)
                    {
                        if (sSplitTemp[1].IndexOf("|") > -1)
                        {
                            foreach (string sItem in sSplitTemp[1].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                lListX.Add(sItem);
                            }
                        }
                        else
                        {
                            lListX.Add(sSplitTemp[1]);
                        }
                    }
                }
            }

            sResult = new string[lListX.Count];
            lListX.CopyTo(sResult);

            return sResult;
        }

        private static string GetValue(string sKey)
        {
            return ConfigurationManager.AppSettings[sKey].ToString().Trim();
        }

        // -----------------------------------------------------------

        public static string sDefaultLeftDirectory
        {
            get
            {
                return AppSettings.CheckWorkDirectory(AppSettings.GetValue("DefaultLeftDirectory"));
            }
        }

        public static string sDefaultRightDirectory
        {
            get
            {
                return AppSettings.CheckWorkDirectory(AppSettings.GetValue("DefaultRightDirectory"));
            }
        }

        public static int iWorkerDelayTime
        {
            get
            {
                return Convert.ToInt32(AppSettings.GetValue("WorkerDelayTime"));
            }
        }

        public static string[] sSkipFileName
        {
            get
            {
                return AppSettings.ParsingBooleanAndStringMixedValue(AppSettings.GetValue("SkipFileName"));
            }
        }

        public static string[] sSkipFileExtension
        {
            get
            {
                return AppSettings.ParsingBooleanAndStringMixedValue(AppSettings.GetValue("SkipFileExtension"));
            }
        }

        public static bool bSkipNoneFileExtension
        {
            get
            {
                return (AppSettings.GetValue("SkipNoneFileExtension").ToLower() == "true");
            }
        }

        public static bool bSkipZeroFileSize
        {
            get
            {
                return (AppSettings.GetValue("SkipZeroFileSize").ToLower() == "true");
            }
        }

        public static bool bFileMatchWithSize
        {
            get
            {
                return (AppSettings.GetValue("FileMatchWithSize").ToLower() == "true");
            }
        }

        public static bool bFileMatchWithEditDate
        {
            get
            {
                return (AppSettings.GetValue("FileMatchWithEditDate").ToLower() == "true");
            }
        }

        public static bool bTabTextUpdateItemCount
        {
            get
            {
                return (AppSettings.GetValue("TabTextUpdateItemCount").ToLower() == "true");
            }
        }
    }
}
