using System.Collections.ObjectModel;
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DictionaryOfOrganizationsAndEmployees
{
    public class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path && !string.IsNullOrEmpty(path))
            {
                try
                {
                    return new BitmapImage(new Uri(path));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Organization> _organizations;
        private ObservableCollection<Person> _people;
        private string _logFilePath = "personnel_log.txt";
        private Organization _selectedOrganization;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Organization> Organizations
        {
            get => _organizations;
        }
        public Organization SelectedOrganization
        {
            get => _selectedOrganization;
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged(nameof(SelectedOrganization));
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _organizations = new ObservableCollection<Organization>();
            _people = new ObservableCollection<Person>();

            OrganizationsListBox.ItemsSource = _organizations;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OrganizationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrganizationsListBox.SelectedItem is Organization selectedOrganization)
            {
                SelectedOrganization = selectedOrganization;
            }
        }

        private void AddOrganizationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteOrganizationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditOrganizationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add10OrganizationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditPersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowEmployeesButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Organization : INotifyPropertyChanged
    {
        private string _name;
        private ObservableCollection<Person> _people = new ObservableCollection<Person>();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string _fullName;
        private string _position;
        private string _phoneNumber;
        private string _photoPath;
        private Organization _organization;

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                _photoPath = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }

        public Organization Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                OnPropertyChanged(nameof(Organization));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}