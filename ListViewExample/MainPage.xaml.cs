using System;
using ListViewExample.Model;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static ListViewExample.Model.DBConnection;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<MenuItem> MenuItems;
        
        public MainPage()
        {
            using (var db = DbConnection)
            {
                var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Task';";
                if (db.ExecuteScalar<string>(tableExistsQuery) == null) db.CreateTable<Task>();
                tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Category';";
                if (db.ExecuteScalar<string>(tableExistsQuery) == null) db.CreateTable<Category>();
            }

            
            MenuItems = ListManager.GetList();
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }

      
        private void ScenarioControl_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            MenuItem s = scenarioListBox.SelectedItem as MenuItem;

            if (scenarioListBox.SelectedItem != null)
            {
                if (s.ClassType == typeof(Page3)) Page3.SelectedMenuItem = s;
                else if (s.ClassType == typeof(Page1)) Page1.CurrentItem = null;
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
                var result = await MyContentDialog2.ShowAsync();
                if (result.ToString() == "Primary" && CategoryName.Text != null && CategoryName.Text != "")
                {
                Category cat = new Category();
                cat.name = CategoryName.Text;

                using (var db = DbConnection)
                    {
                        db.Insert(cat);
                    }
                    CategoryName.Text = "";
                MenuItems.Add(new MenuItem { Id = cat.id + 6, Title = cat.name, category = cat, ClassType = typeof(Page3) });
            }
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            
            MenuItem s = ScenarioControl.SelectedItem as MenuItem;
            if (s != null && s.category != null)
            {
                var result = await MyContentDialog.ShowAsync();
                if (result.ToString() == "Primary")
                {
                    using (var db = DbConnection)
                    {
                        db.Delete(s.category);
                    }
                    MenuItems.Remove(s);
                }
            }

            //refresh right side categories??
        }

        private void ToDo_List_Loaded(object sender, RoutedEventArgs e)
        {
            ScenarioControl.SelectedItem = ScenarioControl.Items[2];
            MenuItem s = ScenarioControl.SelectedItem as MenuItem;
            Page3.SelectedMenuItem = s;
            ScenarioFrame.Navigate(typeof(Page3));
        }
    }
}
