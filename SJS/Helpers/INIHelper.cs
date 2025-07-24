﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SJS.Helpers
{
    public class INIHelper
    {
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        string savePath = null;
        private string SavePath
        {
            get
            {
                if (savePath == null)
                    return @AppDomain.CurrentDomain.BaseDirectory + "\\Settings.Ini";
                else
                    return @AppDomain.CurrentDomain.BaseDirectory + savePath;
            }
        }
        public string Read(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, SavePath);
            return temp.ToString().Trim();
        }
        public string Read(string section, string key, string init_val)
        {
            /** 저장변수 */
            string val = Read(section, key);

            /** 읽은값이 Null 이면 init_val 값으로 변경 */
            if (val == null || val == "")
            {
                val = init_val;
                Write(section, key, val);
            }

            return val;
        }
        public string Write(string section, string key, string val)
        {
            string strError;

            try
            {
                WritePrivateProfileString(section, key, val, SavePath);
                strError = null;

                return strError;
            }
            catch (Exception exError)
            {
                strError = string.Format("[SYSTEM] : {0}", exError.Message);

                return strError;
            }
        }
        public void Write(string filename, string section, string key, string val)
        {
            savePath = filename;
            Write(section, key, val);

        }
    }
}
