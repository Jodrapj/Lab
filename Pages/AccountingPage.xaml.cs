using Lab.AppData;
using Lab.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountingPage.xaml
    /// </summary>
    public partial class AccountingPage : Page
    {

        public AccountingPage()
        {
            InitializeComponent();
            AccountingLV.ItemsSource = Connect.context.AccountingTable.ToList();
        }

        private void AddBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new AddEditAcc(null));
        }

        private void DelBaton_Click(object sender, RoutedEventArgs e)
        {

            var DelAccs = AccountingLV.SelectedItems.Cast<AccountingTable>().ToList();
            foreach (var delAcc in DelAccs)
                if (Connect.context.ReferenceTable.Any(x => x.AccountNumber == delAcc.AccountNumber))
                {
                    MessageBox.Show("Данные используются в таблице учета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {DelAccs.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
                Connect.context.AccountingTable.RemoveRange(DelAccs);
            try
            {
                Connect.context.SaveChanges();
                AccountingLV.ItemsSource = Connect.context.AccountingTable.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefrBaton_Click(object sender, RoutedEventArgs e)
        {
            AccountingLV.ItemsSource = Connect.context.AccountingTable.ToList();
        }

        private void BackBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AccountingLV.ItemsSource = Connect.context.AccountingTable.ToList();
        }

        private void EditBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new AddEditAcc((sender as Button).DataContext as AccountingTable));
        }

        private void SortMenu_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.AccountingTable
                        orderby item.AccountNumber ascending
                        select item;
            List<AccountingTable> resultGrid = query.ToList();
            AccountingLV.ItemsSource = resultGrid;
        }

        private void FilterMenu_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.AccountingTable
                        where item.Rate == SearchBox.Text
                        select item;
            List<AccountingTable> filterlist = query.ToList();
            AccountingLV.ItemsSource = filterlist;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == string.Empty)
                AccountingLV.ItemsSource = Connect.context.AccountingTable.ToList();
        }

        private void SearchBaton_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.AccountingTable
                        where item.PaymentMonth.Contains(SearchBox.Text) && item.Rate.Contains(SearchBox.Text)
                        select item;
            List<AccountingTable> resultList = query.ToList();
            AccountingLV.ItemsSource = resultList;
        }
    }
}
