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
using System.Windows.Shapes;

namespace DictionaryOfOrganizationsAndEmployees
{
    public partial class EditOrganizationWindow : Window
    {
        private Organization _organization;
        public EditOrganizationWindow(Organization organization)
        {
            InitializeComponent();

            _organization = organization;

            OrganizationNameTextBox.Text = _organization.Name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _organization.Name = OrganizationNameTextBox.Text;

            DialogResult = true;
        }
    }
}
