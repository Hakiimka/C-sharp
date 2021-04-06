using FirebirdTest.Interfaces;
using System.Windows;

namespace FirebirdTest.View
{
    /// <summary>
    /// Interaction logic for AddPermissionWindow.xaml
    /// </summary>
    public partial class AddPermissionWindow :Window
    {
        public AddPermissionWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
