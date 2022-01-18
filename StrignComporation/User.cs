using System;
using System.Collections.Generic;
using System.Text;

namespace StrignComporation
{
    public class User
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }

        public string FIO => $"{LastName} {FirstName} {MiddleName}".Trim();

        public User(string fio)
        {
            var names = fio.Split(';') ?? new string[3];

            if (names.Length > 0)
                LastName = names[0];
            if (names.Length > 1)
                FirstName = names[1];
            if (names.Length > 2)
                MiddleName = names[2];
        }
    }
}
