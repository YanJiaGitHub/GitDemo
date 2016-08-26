using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DispatchClient;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace DispatchClientDemo
{
    public static class Base
    {        
        public static DispatchClient_cs myClient;
        public static string g_DialNum;
        public static frmMain g_MainForm;
        public static Hashtable g_GroupNum_IsLoaded;
        public static TreeNode g_VideoTreeRootNode;
        public static SIPPhoneSDK.AudioDeviceInfo[] g_AudioDevices = new SIPPhoneSDK.AudioDeviceInfo[10];
        public static SIPPhoneSDK.VideoDeviceInfo[] g_VideoDevices = new SIPPhoneSDK.VideoDeviceInfo[10];

        public static int write_log(string strPath, string strMsg)
        {
            int nReturn = 0;
            try
            {
                strMsg = String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff "), strMsg);
                string strFileName = String.Format("{0}\\log\\Msg_Log{1}.txt", strPath, DateTime.Now.ToString("yyyyMMdd"));
                if ((strFileName == null) || (strFileName == "")) return 1;
                strPath = String.Format("{0}\\log", strPath);

                if (System.IO.Directory.Exists(strPath) == false)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                System.IO.StreamWriter sWrite = new System.IO.StreamWriter(strFileName, true);
                sWrite.WriteLine(strMsg);
                sWrite.Close();
                nReturn = 1; ;
            }
            catch 
            {
                nReturn = -1;
            }
            return nReturn;
        }
    }

    public static class pmPhone
    {
        public static bool IsRegistered = false;
        public static string strSipServerIP = "";
        public static int nServerPort = 5060;
        public static int nLocalPort = 5060;
        public static string strLocalIP = "";
    }
}
