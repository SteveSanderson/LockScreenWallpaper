using System;
using System.Runtime.InteropServices;

namespace LockScreenWallpaper
{
    public static class Wallpaper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern Int32 SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            String pvParam,
            UInt32 fWinIni);

        const UInt32 SPI_SETDESKWALLPAPER = 20;
        const UInt32 SPIF_UPDATEINIFILE = 0x1;

        public static void Set(string filename)
            => SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, filename, SPIF_UPDATEINIFILE);
    }
}
