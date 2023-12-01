using Lab.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Ref_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new ReferencePage());
        }

        private void Acc_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.MainFrame.Navigate(new AccountingPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void cReportBaton_Click(object sender, RoutedEventArgs e)
        {
            FrameConnection.exportBox.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
