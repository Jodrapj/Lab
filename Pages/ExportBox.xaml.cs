using Lab.Classes;
using System.Windows;

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExportBox.xaml
    /// </summary>
    public partial class ExportBox : Window
    {
        public ExportBox()
        {
            InitializeComponent();
        }

        private void PDFbaton_Click(object sender, RoutedEventArgs e)
        {
            ExportClass.exportToPDF();
            FrameConnection.exportBox.Hide();
        }

        private void EXCELbaton_Click(object sender, RoutedEventArgs e)
        {
            ExportClass.exportToExcel();
            FrameConnection.exportBox.Hide();
        }
    }
}
