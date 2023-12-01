using Lab.AppData;
using Lab.Classes;
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

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReferencePage.xaml
    /// </summary>
    public partial class ReferencePage : Page
    {
        public ReferencePage()
        {
            InitializeComponent();
            ReferenceLV.ItemsSource = Connect.context.ReferenceTable.ToList();
        }

        private void AddBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new AddEditRef(null));
        }

        private void BackBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.GoBack();
        }

        private void DelBaton_Click(object sender, RoutedEventArgs e)
        {
            var DelRefs = ReferenceLV.SelectedItems.Cast<ReferenceTable>().ToList();
            foreach(var delRef in DelRefs)
                if (Connect.context.AccountingTable.Any(x => x.AccountNumber == delRef.AccountNumber))
                {
                    MessageBox.Show("Данные используются в таблице учета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {DelRefs.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
                Connect.context.ReferenceTable.RemoveRange(DelRefs);
                try
                {
                Connect.context.SaveChanges();
                ReferenceLV.ItemsSource = Connect.context.ReferenceTable.ToList();
                return;
                }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void RefrBaton_Click(object sender, RoutedEventArgs e)
        {
            ReferenceLV.ItemsSource = Connect.context.ReferenceTable.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReferenceLV.ItemsSource = Connect.context.ReferenceTable.ToList();
        }

        private void EditBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new AddEditRef((sender as Button).DataContext as ReferenceTable));
        }
        private void SearchBaton_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.ReferenceTable
                        where item.FullName.Contains(SearchBox.Text)
                        select item;
            List<ReferenceTable> resultList = query.ToList();
            ReferenceLV.ItemsSource = resultList;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == string.Empty)
                ReferenceLV.ItemsSource = Connect.context.ReferenceTable.ToList();
        }

        private void SortMenu_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.ReferenceTable
                        orderby item.AccountNumber ascending
                        select item;
            List<ReferenceTable> resultlist = query.ToList();
            ReferenceLV.ItemsSource = resultlist;
        }

        private void FilterMenu_Click(object sender, RoutedEventArgs e)
        {
            var query = from item in Connect.context.ReferenceTable
                        where item.FullName == SearchBox.Text
                        select item;
            List<ReferenceTable> filterlist = query.ToList();
            ReferenceLV.ItemsSource = filterlist;
        }
    }
}
