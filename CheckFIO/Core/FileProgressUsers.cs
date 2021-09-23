using System.Collections.Generic;

namespace CheckFIO.Core
{
    public static class FileProgressUsers
    {
        private static string Path => "D:\\progress.csv";
        public static List<string> LoadUsersProgress() => FileUsers.LoadUsers(Path);
    }
}
