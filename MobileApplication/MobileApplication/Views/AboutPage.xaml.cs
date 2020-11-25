using DbEntities.Models;
using Microcharts;
using MobileApplication.Services.Rest;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{

    public partial class AboutPage : ContentPage
    {

        public ObservableCollection<Category> ItemsCategory { get; }
        public ObservableCollection<Transaction> ItemsTrancations { get; }
        public Command LoadItemsCommand { get; }
        private readonly IRestService<Category> RestServiceCategory;
        private readonly IRestService<Transaction> RestServiceTransaction;

        void PresentBudgetChart(IEnumerable<Category> categories)
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            try
            {
                var random = new Random();
                foreach (var iteam in categories)
                {
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    if (iteam.DedicatedAmount != null)
                    {
                        entries.Add(new ChartEntry((float)iteam.DedicatedAmount)
                        {
                            Label = iteam.Title,
                            ValueLabelColor = SKColor.Parse(color),
                            Color = SKColor.Parse(color)
                        }
                    );
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


            chartViewDonut.Chart = new DonutChart
            {
                Entries = entries,
                LabelTextSize = 50,
                LabelMode = LabelMode.RightOnly
            };
        }
        void PresentTransactionChart(IEnumerable<Transaction> transactions)
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            try
            {
                foreach (var iteam in transactions)
                {
                        entries.Add(new ChartEntry((float)iteam.Amount)
                        {
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


            chartViewLine.Chart = new LineChart
            {
                Entries = entries,
            };
        }

        public AboutPage()
        {
            RestServiceCategory = new RestService<Category>("api/Categories");
            ItemsCategory = new ObservableCollection<Category>();

            _ = ExecuteLoadCategoryItemsCommand();

            RestServiceTransaction = new RestService<Transaction>("api/Transactions");
            ItemsTrancations = new ObservableCollection<Transaction>();
            _ = ExecuteLoadTransactionItemsCommand();
            InitializeComponent();


        }
        async Task ExecuteLoadCategoryItemsCommand()
        {
            IsBusy = true;
            ItemsCategory.Clear();

            try
            {
                var items = await RestServiceCategory.RefreshDataAsync();
                items.ForEach(Category => ItemsCategory.Add(Category));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                PresentBudgetChart(ItemsCategory);
            }
        }
        async Task ExecuteLoadTransactionItemsCommand()
        {
            IsBusy = true;
            ItemsTrancations.Clear();

            try
            {
                var items = await RestServiceTransaction.RefreshDataAsync();
                items.ForEach(Category => ItemsTrancations.Add(Category));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                PresentTransactionChart(ItemsTrancations);
            }
        }
    }
}