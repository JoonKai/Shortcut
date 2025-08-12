using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SJSControls.Utils
{
    public  class UsingVNC
    {
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string ReadVNC(string section, string key, string savePath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, savePath);
            return temp.ToString().Trim();
        }
        public void setVNC(string savePath, string k1, string p1, string t1, string v1)
        {
            CreateWrite(savePath, "connection", k1, p1);
            CreateWrite(savePath, "connection", "port", "5900");
            CreateWrite(savePath, "options", "use_encoding_0", "1");
            CreateWrite(savePath, "options", "use_encoding_1", "1");
            CreateWrite(savePath, "options", "use_encoding_2", "1");
            CreateWrite(savePath, "options", "use_encoding_3", "1");
            CreateWrite(savePath, "options", "use_encoding_4", "1");
            CreateWrite(savePath, "options", "use_encoding_5", "1");
            CreateWrite(savePath, "options", "use_encoding_6", "1");
            CreateWrite(savePath, "options", "use_encoding_7", "1");
            CreateWrite(savePath, "options", "use_encoding_8", "1");
            CreateWrite(savePath, "options", "viewonly", "0");
            CreateWrite(savePath, "options", "fullscreen", "0");
            CreateWrite(savePath, "options", "8bit", "0");
            CreateWrite(savePath, "options", "shared", "1");
            CreateWrite(savePath, "options", "belldeiconify", "0");
            CreateWrite(savePath, "options", "disableclipboard", "0");
            CreateWrite(savePath, "options", "restricted", "0");
            CreateWrite(savePath, "options", "swapmouse", "0");
            CreateWrite(savePath, "options", "emulate3", "1");
            CreateWrite(savePath, "options", "fitwindow", "0");
            CreateWrite(savePath, "options", "cursorshape", "1");
            CreateWrite(savePath, "options", "noremotecursor", "0");
            CreateWrite(savePath, "options", "preferred_encoding", "7");
            CreateWrite(savePath, "options", "compresslevel", "1");
            CreateWrite(savePath, "options", "quality", "6");
            CreateWrite(savePath, "options", "emulate3timeout", "100");
            CreateWrite(savePath, "options", "emulate3fuzz", "4");
            CreateWrite(savePath, "options", "localcursor", "1");
            CreateWrite(savePath, "options", "scale_den", "1");
            CreateWrite(savePath, "options", "scale_num", "1");
            CreateWrite(savePath, "options", "local_cursor_shape", "1");
            CreateWrite(savePath, "Title", t1, v1);

        }
        private string CreateWrite(string path, string section, string key, string val)
        {
            string strError;

            try
            {
                WritePrivateProfileString(section, key, val, path);
                strError = null;

                return strError;
            }
            catch (Exception exError)
            {
                strError = string.Format("[SYSTEM] : {0}", exError.Message);

                return strError;
            }
        }
    }
}
