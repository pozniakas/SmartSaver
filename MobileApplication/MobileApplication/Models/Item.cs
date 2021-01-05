using System;

namespace MobileApplication.Models
{
    public class Item
    {
        public Item(string id, string text, string description)
        {
            Id = id;
            Text = text;
            Description = description;
        }

        public Item() { }

        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}