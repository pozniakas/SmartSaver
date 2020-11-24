using Microcharts;
using SkiaSharp;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    public partial class AboutPage : ContentPage
    {
        private readonly ChartEntry[] entries = new[]
{
    new ChartEntry(100)
    {
        Label = "Groceries",
        ValueLabelColor = SKColor.Parse("#2c3e50"),
        Color = SKColor.Parse("#2c3e50")
    },
    new ChartEntry(200)
    {
        Label = "Transport",
        ValueLabelColor = SKColor.Parse("#77d065"),
        Color = SKColor.Parse("#77d065")
    },
    new ChartEntry(150)
    {
        Label = "Entertainment",
        ValueLabelColor = SKColor.Parse("#b455b6"),
        Color = SKColor.Parse("#b455b6")
    },
};
        public AboutPage()
        {
            InitializeComponent();
            chartViewDonut.Chart = new DonutChart { Entries = entries, LabelTextSize = 50, LabelMode = LabelMode.RightOnly };
        }
    }
}