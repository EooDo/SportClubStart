using System;
using System.IO;

namespace SportClubStart.Data
{
    public static class DbPathResolver
    {

        public static string GetDatabasePath(string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string pathNearExe = Path.Combine(baseDir, fileName);
            if (File.Exists(pathNearExe))
                return Path.GetFullPath(pathNearExe);

            DirectoryInfo dir = new DirectoryInfo(baseDir);
            for (int i = 0; i < 6 && dir != null; i++)
            {
                string candidate = Path.Combine(dir.FullName, fileName);
                if (File.Exists(candidate))
                    return Path.GetFullPath(candidate);

                dir = dir.Parent;
            }

            throw new FileNotFoundException("Не найден файл базы данных: " + fileName);
        }
    }
}
