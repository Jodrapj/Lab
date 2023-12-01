using Lab.Classes;
using System.Windows;
using Lab.Pages;

namespace Lab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameConnection.MainFrame = MFrame;
            FrameConnection.MainFrame.Navigate(new Page1());
        }
    }
}
