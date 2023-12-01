using Lab.AppData;
using Lab.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditRef.xaml
    /// </summary>
    public partial class AddEditRef : Page
    {
        ReferenceTable RefTab;
        bool checkNew;
        public AddEditRef(ReferenceTable a)
        {
            InitializeComponent();
            if (a == null)
            {
                a = new ReferenceTable();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = RefTab = a;
        }

        private void BackBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.GoBack();
        }

        private void SaveBaton_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.context.ReferenceTable.Add(RefTab);
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

        public static bool CheckValue(ReferenceTable a)
        {
            if (string.IsNullOrEmpty(a.FullName)) return false;
            return true;
        }
    }
}
