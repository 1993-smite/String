using System.Collections.Generic;

namespace CheckFIO.Core
{
    static class FileSQLUsers
    {
        private static string Path => "D:\\users.csv";

        public static List<string> LoadUsersSQL() => FileUsers.LoadUsers(Path, 2);
    }
}
