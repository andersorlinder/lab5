using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private User currentAdmin;

        public MainWindow()
        {
            InitializeComponent();
            UserListBox.ItemsSource = User.users;
            UserListBox.DisplayMemberPath = "Name";
            AdminListBox.ItemsSource = User.admins;
            AdminListBox.DisplayMemberPath = "Name";
        }

        private void RefreshListBoxes()
        {
            UserListBox.Items.Refresh();
            AdminListBox.Items.Refresh();
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            MailTextBox.Clear();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "" || MailTextBox.Text == "")
            {
                MessageBox.Show("Name or mail is missing!", "Register user");
                return;
            }
            User.users.Add(new User(NameTextBox.Text, MailTextBox.Text));
            RefreshListBoxes();
            NameTextBox.Clear();
            MailTextBox.Clear();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem != null || AdminListBox.SelectedItem != null)
            {
                UpdateButton.IsEnabled = true;
                if (!MailTextBox.Text.Contains("@"))
                {
                    MessageBox.Show("Mail is not correct!", "Updating user");
                }
                else if (currentAdmin != null)
                {
                    currentAdmin.Name = NameTextBox.Text;
                    currentAdmin.Mail = MailTextBox.Text.Trim();
                    RefreshListBoxes();
                    ClearTextBoxes();
                    MailLabelGridTwo.Content = $"Mail: ";
                }
                else if (currentUser != null)
                {
                    currentUser.Name = NameTextBox.Text;
                    currentUser.Mail = MailTextBox.Text.Trim();
                    RefreshListBoxes();
                    ClearTextBoxes();
                    MailLabelGridOne.Content = $"Mail: ";
                }
                UpdateButton.IsEnabled = false;
            }
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                currentAdmin = null;
                MailLabelGridTwo.Content = $"Mail: ";
                AdminListBox.UnselectAll();
                currentUser = (UserListBox.SelectedItem as User);
                NameTextBox.Text = currentUser.Name;
                MailTextBox.Text = currentUser.Mail;
                MailLabelGridOne.Content = $"Mail: {currentUser.Mail}";
            }
        }

        private void AdminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdminListBox.SelectedItem != null)
            {
                currentUser = null;
                MailLabelGridOne.Content = $"Mail: ";
                UserListBox.UnselectAll();
                currentAdmin = (AdminListBox.SelectedItem as User);
                NameTextBox.Text = currentAdmin.Name;
                MailTextBox.Text = currentAdmin.Mail;
                MailLabelGridTwo.Content = $"Mail: {currentAdmin.Mail}";
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Remove(currentUser);
            currentUser = null;
            RefreshListBoxes();
            ClearTextBoxes();
            MailLabelGridOne.Content = $"Mail: ";
        }

        private void DeleteAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.admins.Remove(currentAdmin);
            currentUser = null;
            RefreshListBoxes();
            ClearTextBoxes();
            MailLabelGridTwo.Content = $"Mail: ";
        }

        private void ToAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.ToAdmin(currentUser);
            RefreshListBoxes();
            ClearTextBoxes();
            MailLabelGridOne.Content = $"Mail: ";
        }

        private void ToUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.ToUser(currentAdmin);
            RefreshListBoxes();
            ClearTextBoxes();
            MailLabelGridTwo.Content = $"Mail: ";
        }
    }
}
