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
    public partial class EditPersonWindow : Window
    {
        private Person _person;
        public EditPersonWindow(Person person)
        {
            InitializeComponent();

            _person = person;

            FullNameTextBox.Text = _person.FullName;
            PositionTextBox.Text = _person.Position;
            PhoneNumberTextBox.Text = _person.PhoneNumber;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _person.FullName = FullNameTextBox.Text;
            _person.Position = PositionTextBox.Text;
            _person.PhoneNumber = PhoneNumberTextBox.Text;

            DialogResult = true;
        }
    }
}
