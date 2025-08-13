namespace SJS.Helpers
{
    public static class Helper
    {
        public static DATAHelper SJSData { get; private set; }
        public static INIHelper SJSIni { get; private set; }
        public static JSONHelper SJSJson { get; private set; }
        public static LOGHelper SJSLog { get; private set; }
        public static MATHHelper SJSMath { get; private set; }
        public static ROOTHelper SJSRoot { get; private set; }
        public static MONITORHelper SJSMonitor { get; private set; }
        static Helper()
        {
            SJSData = new DATAHelper();
            SJSIni = new INIHelper();
            SJSJson = new JSONHelper();
            SJSLog = new LOGHelper();
            SJSMath = new MATHHelper();
            SJSRoot = new ROOTHelper();
            SJSMonitor = new MONITORHelper();
        }
    }
}
