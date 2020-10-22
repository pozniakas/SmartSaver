namespace SmartSaver.Models
{
    public class Tip
    {
        public Tip()
        {
        }

        public Tip(string aTipName, string atipLink)
        {
            tipName = aTipName;
            tipLink = atipLink;
        }

        public string tipName { get; set; }
        public string tipLink { get; set; }
    }
}
