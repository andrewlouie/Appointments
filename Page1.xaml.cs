using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Organizer and Invitee properties are mutually exclusive.
        /// This radio button enables the organizer properties while disabling the invitees.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrganizerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            InviteeStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// Organizer and Invitee properties are mutually exclusive.
        /// This radio button enables the invitees properties while disabling the organizer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InviteeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            InviteeStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
        }

        /// <summary>
        /// Displays the combo box containing various reminder times.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReminderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ReminderComboBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Hides the combo box containing various reminder times.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReminderCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ReminderComboBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
