using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LockScreenWallpaper
{
    public static class LockScreenContent
    {
        const string contentDir = @"%localappdata%\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";

        public static string FindLatestWallpaper()
        {
            var contentDirExpanded = Environment.ExpandEnvironmentVariables(contentDir);
            var dirInfo = new DirectoryInfo(contentDirExpanded);
            var filesByDate = dirInfo.GetFiles().OrderByDescending(info => info.CreationTime);
            return filesByDate.Select(info => info.FullName).First(IsValidWallpaperImage);
        }

        private static bool IsValidWallpaperImage(string path)
            => HasJpegHeader(path) && HasAcceptableDimensions(path);

        private static bool HasJpegHeader(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var buf = new byte[4];
                return stream.Read(buf, 0, 4) == 4
                    && buf[0] == 0xff
                    && buf[1] == 0xd8
                    && buf[2] == 0xff
                    && (buf[3] == 0xe0 || buf[3] == 0xe1);
            }
        }

        private static bool HasAcceptableDimensions(string path)
        {
            var (width, height) = GetDimensions(path);
            return width > height // They always come in landscape/portrait pairs. Pick the landscape one.
                && height >= 1024; // The directory also contains non-wallpaper images, so ignore them
        }

        private static (int width, int height) GetDimensions(string path)
        {
            using (var img = Image.FromFile(path))
            {
                return (img.Width, img.Height);
            }
        }
    }
}
