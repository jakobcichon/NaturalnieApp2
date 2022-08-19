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

namespace NaturalnieApp2.SharedControls.MVVM.Views.Menu
{
    /// <summary>
    /// Interaction logic for MenuBarItem.xaml
    /// </summary>
    public partial class MenuButtonView : UserControl
    {
        private static Button lastSelectedButton;
        private static Button lastSelectedMainButton;

        public MenuButtonView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? localButton = sender as Button;
            if (localButton == null)
            {
                return;
            }

            // Unselect first 
            UnselectButton(lastSelectedButton);
            UnselectButton(lastSelectedMainButton);

            // Assigne new button
            lastSelectedButton = localButton;
            lastSelectedMainButton = MainButton;

            // Select first 
            SelectButton(lastSelectedButton);
            SelectButton(lastSelectedMainButton);

        }

        private void UnselectButton(Button button)
        {
            if (button != null)
            {
                button.Style = FindResource("UnselectedButtonStyle") as Style;
            }
        }

        private void SelectButton(Button button)
        {
            if (button != null)
            {
                button.Style = FindResource("SelectedButtonStyle") as Style;
            }
        }
    }
}
