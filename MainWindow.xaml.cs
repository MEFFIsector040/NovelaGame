using System.Windows;

namespace CourseProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Фокус на поле сохранения
            SaveNameTextBox.Focus();
            SaveNameTextBox.SelectAll();
        }
    }
}