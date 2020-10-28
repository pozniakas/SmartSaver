namespace SmartSaver.Models
{
    public class Tip
    {
        public Tip()
        {

        }
        public Tip(string aTipName, string aTipLink)
        {
            TipName = aTipName;
            TipLink = aTipLink;
        }

        public string TipName { get; set; }
        public string TipLink { get; set; }
    }
}
