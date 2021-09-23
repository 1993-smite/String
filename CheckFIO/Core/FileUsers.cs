using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckFIO.Core
{
    public static class FileUsers
    {
        public static List<string> LoadUsers(string path, int activeIndex = 0) {
            var fio = new List<string>();
            using (var sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var arr = line.Split(';');

                    fio.Add(arr[activeIndex].Replace('"', ' ').Trim());
                }
            }

            return fio;
        }
    }
}
