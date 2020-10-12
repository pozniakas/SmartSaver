using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.Models
{
    public class Tip
    {

        private string tipName;
        private string tipLink;

        public Tip()
        {
        }

        public Tip(string aTipName, string atipLink)
        {
            tipName = aTipName;
            tipLink = atipLink;
        }

        public string TipName
        {
            get { return tipName; }
            set { tipName = value; }
        }

        public string TipLink
        {
            get { return tipLink; }
            set { tipLink = value; }
        }

    }
}
