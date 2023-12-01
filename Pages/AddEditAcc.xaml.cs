using Lab.AppData;
using Lab.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditAcc.xaml
    /// </summary>
    public partial class AddEditAcc : Page
    {
        AccountingTable AccTab;
        bool checkNew;
        public AddEditAcc(AccountingTable a)
        {
            InitializeComponent();
            if (a == null)
            {
                a = new AccountingTable();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = AccTab = a;
        }

        private void BackBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.GoBack();
        }

        private void SaveBaton_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.context.AccountingTable.Add(AccTab);
            }
            try { Connect.context.SaveChanges(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            FrameConnection.MainFrame.GoBack();
        }

        public static bool CheckAcc(AccountingTable a)
        {
            if(string.IsNullOrEmpty(a.KWNumber)) return false;
            return true;
        }
    }
}
