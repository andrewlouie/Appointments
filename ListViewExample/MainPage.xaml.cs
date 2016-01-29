using System.Diagnostics;
using ListViewExample.Model;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<MenuItem> MenuItems;

        public MainPage()
        {
            MenuItems = ListManager.GetList();
            this.InitializeComponent();
        }

        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ListBox scenarioListBox = sender as ListBox;
            MenuItem s = scenarioListBox.SelectedItem as MenuItem;

            if (scenarioListBox.SelectedItem != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }


        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (MenuItem)e.ClickedItem;
        }
    }
}
