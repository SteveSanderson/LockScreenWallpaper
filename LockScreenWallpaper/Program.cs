namespace LockScreenWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = LockScreenContent.FindLatestWallpaper();
            Wallpaper.Set(filename);
        }
    }
}
