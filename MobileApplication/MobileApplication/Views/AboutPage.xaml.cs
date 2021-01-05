using DbEntities.Entities;
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
using static MobileApplication.Views.NewCategoryPage;
using static MobileApplication.Views.NewTransactionPage;

namespace MobileApplication.Views
{
    public delegate void EventDelegate(object sender, EventArgs e);

    public partial class AboutPage : ContentPage
    {
        public CategoryPage categoryPageNew;
        private decimal net;
        private decimal income;
        private decimal expenses;

        public event EventHandler Refresh;

        public ObservableCollection<Category> ItemsCategory { get; }
        public ObservableCollection<Transaction> ItemsTrancations { get; }
        public Command LoadItemsCommand { get; }
        private readonly IRestService<Category> RestServiceCategory;
        private readonly IRestService<Transaction> RestServiceTransaction;


        public AboutPage()
        {
            RestServiceCategory = new RestService<Category>("api/Categories");
            ItemsCategory = new ObservableCollection<Category>();

            _ = ExecuteLoadCategoryItemsCommand();

            RestServiceTransaction = new RestService<Transaction>("api/Transactions");
            ItemsTrancations = new ObservableCollection<Transaction>();
            _ = ExecuteLoadTransactionItemsCommand();
            InitializeComponent();
            ToolRefresh.Clicked += ToolRefresh_Clicked;
            Refresh += new EventHandler(HandelEvent);
            OnUpdateStatusCategory += new StatusUpdateHandlerCategory(UpdateStatus);
            OnUpdateStatusTransaction += new StatusUpdateHandler(UpdateStatus);
        }
// Updating charts
        public async void UpdateStatus(object sender, ProgressEventArgs e)
        { 
            await Task.Delay(1000);
            if(e.Status == "category")
                await ExecuteLoadCategoryItemsCommand();
            if (e.Status == "transaction")
                await ExecuteLoadTransactionItemsCommand();

        }

        private async void HandelEvent(object sender, EventArgs e)
        {
            await ExecuteLoadCategoryItemsCommand();
            _ = ExecuteLoadTransactionItemsCommand();
        }

        private void ToolRefresh_Clicked(object sender, EventArgs e)
        {
            Refresh(this, null);

        }
        void PresentBudgetChart(IEnumerable<Category> categories)
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            try
            {
                var random = new Random();
                foreach (var iteam in categories)
                {
                    var color = string.Format("#{0:X6}", random.Next(0x1000000));
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
                    net += iteam.Amount;
                    if ((float)iteam.Amount > 0) { 
                        income += (decimal)iteam.Amount;}
                    else { 
                        expenses += (decimal)iteam.Amount;}
                    entries.Add(new ChartEntry((float)net)
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

            incomeLabel.Text = income.ToString();
            expensesLabel.Text = expenses.ToString();
            net = 0;
            income = 0;
            expenses = 0;
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