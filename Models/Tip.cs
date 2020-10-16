using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.Models
{
    public class Tip
    {
        public string tipName { get; set; }
        public string tipLink { get; set; }

        public Tip()
        {
        }

        public Tip(string aTipName, string atipLink)
        {
            tipName = aTipName;
            tipLink = atipLink;
        }

    }
}
