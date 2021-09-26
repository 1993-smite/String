using System;
using System.Collections.Generic;
using System.Text;

namespace Elastic
{
    public class Test : ESModel
    {
        public string Title;
        public string Description;

        public Test(): base("test")
        {

        }

        public override string ToString()
        {
            return $"\tTitle: {Title} \n\tDescription: {Description}";
        }
    }
}
