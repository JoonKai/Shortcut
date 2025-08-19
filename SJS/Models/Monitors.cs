using System.Windows;

namespace SJS.Models
{
    public class Monitors
    {
        public Rect DipBounds { get; private set; }
        public Rect DipWorkArea { get; private set; }
        public bool IsPrimary { get; private set; }
        public string DeviceName { get; private set; }

        public Monitors(Rect dipBounds, Rect dipWorkArea, bool isPrimary, string deviceName)
        {
            DipBounds = dipBounds;
            DipWorkArea = dipWorkArea;
            IsPrimary = isPrimary;
            DeviceName = deviceName ?? string.Empty;
        }
    }
}
