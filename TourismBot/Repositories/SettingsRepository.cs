using System.IO;

namespace TourismBot.Repositories
{
    public static class SettingsRepository
    {
        private static readonly string FileAddressPath = string.Format("..{0}..{0}..{0}..{0}Setting.txt", Path.DirectorySeparatorChar);

        public static string GetSetting()
        {
            string[] lines = File.ReadAllLines(FileAddressPath);
            return lines[0];
        }
    }
}