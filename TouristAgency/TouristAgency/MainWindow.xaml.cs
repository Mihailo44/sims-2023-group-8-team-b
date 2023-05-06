using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TouristAgency.View.Display;
using TouristAgency.View.Home;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
       /* public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Required field";
                }
                else if (columnName == "Password")
                {
                    if (string.IsNullOrEmpty(Password))
                        return "Required field";
                }

                return null;
            }
        } */

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }
    }
}
