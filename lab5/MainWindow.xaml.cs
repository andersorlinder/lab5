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
        private User currentUser = null;
        private User currentAdmin = null;
        string nameInput;
        string mailInput;

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

        private void ClearTextFields()
        {
            NameTextBox.Clear();
            MailTextBox.Clear();
            MailUserLabel.Content = $"Mail: ";
            MailAdminLabel.Content = $"Mail: ";

        }

        private void SetUserButtons(bool aBool)
        {
            UpdateButton.IsEnabled = aBool;
            DeleteUserButton.IsEnabled = aBool;
            ToAdminButton.IsEnabled = aBool;
        }

        private void SetAdminButtons(bool aBool)
        {
            UpdateButton.IsEnabled = aBool;
            DeleteAdminButton.IsEnabled = aBool;
            ToUserButton.IsEnabled = aBool;
        }

        private bool IsNameAndMailCorrect(string name, string mail, string titel, string duplicate)
        {

            if (name == "" || mail == "")
            {
                MessageBox.Show("Name or mail is missing.", titel);
                return false;
            }
            else if (!mail.Contains("@"))
            {
                MessageBox.Show("Mail is not correct, use @.", titel);
                return false;
            }
            foreach (var user in User.users)
            {
                if (user.Name == name && user.Mail == mail)
                {
                    MessageBox.Show(duplicate, titel);
                    return false;
                }
            }
            foreach (var admin in User.admins)
            {
                if (admin.Name == name && admin.Mail == mail)
                {
                    MessageBox.Show(duplicate, titel);
                    return false;
                }
            }
            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            nameInput = NameTextBox.Text.Trim();
            mailInput = MailTextBox.Text.Replace(" ", "");
            if (!IsNameAndMailCorrect(nameInput, mailInput, "Register user", "User already exist."))
            {
                return;
            }
            User.users.Add(new User(nameInput, mailInput));
            RefreshListBoxes();
            ClearTextFields();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            nameInput = NameTextBox.Text.Trim();
            mailInput = MailTextBox.Text.Replace(" ", "");
            if (!IsNameAndMailCorrect(nameInput, mailInput, "Updating user", "No update."))
            {
                return;
            }
            if (currentUser != null)
            {
                currentUser.Name = nameInput();
                currentUser.Mail = mailInput;
                RefreshListBoxes();
                ClearTextFields();
                UserListBox.UnselectAll();
                SetUserButtons(false);

            }
            else if (currentAdmin != null)
            {
                currentAdmin.Name = nameInput.Trim();
                currentAdmin.Mail = mailInput.Replace(" ", "");
                RefreshListBoxes();
                ClearTextFields();
                AdminListBox.UnselectAll();
                SetAdminButtons(false);
            }
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                SetAdminButtons(false);
                SetUserButtons(true);
                currentAdmin = null;
                ClearTextFields();
                AdminListBox.UnselectAll();
                currentUser = (UserListBox.SelectedItem as User);
                NameTextBox.Text = currentUser.Name;
                MailTextBox.Text = currentUser.Mail;
                MailUserLabel.Content = $"Mail: {currentUser.Mail}";
            }
        }

        private void AdminListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdminListBox.SelectedItem != null)
            {
                SetUserButtons(false);
                SetAdminButtons(true);
                currentUser = null;
                ClearTextFields();
                UserListBox.UnselectAll();
                currentAdmin = (AdminListBox.SelectedItem as User);
                NameTextBox.Text = currentAdmin.Name;
                MailTextBox.Text = currentAdmin.Mail;
                MailAdminLabel.Content = $"Mail: {currentAdmin.Mail}";
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Remove(currentUser);
            currentUser = null;
            RefreshListBoxes();
            ClearTextFields();
            UserListBox.UnselectAll();
            SetUserButtons(false);
        }

        private void DeleteAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.admins.Remove(currentAdmin);
            currentAdmin = null;
            RefreshListBoxes();
            ClearTextFields();
            AdminListBox.UnselectAll();
            SetAdminButtons(false);
        }

        private void ToAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.ToAdmin(currentUser);
            currentUser = null;
            UserListBox.UnselectAll();
            RefreshListBoxes();
            ClearTextFields();
            UserListBox.UnselectAll();
            SetUserButtons(false);
        }

        private void ToUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.ToUser(currentAdmin);
            currentAdmin = null;
            AdminListBox.UnselectAll();
            RefreshListBoxes();
            ClearTextFields();
            AdminListBox.UnselectAll();
            SetAdminButtons(false);
        }
    }
}
