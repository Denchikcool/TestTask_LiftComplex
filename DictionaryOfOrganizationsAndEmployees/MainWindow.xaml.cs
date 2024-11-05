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
using System.IO;

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

            LoadFromFileAsync();
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
            var organization = new Organization { Name = "Новая организация" };
            _organizations.Add(organization);
            Log("Добавлена организация: " + organization.Name);
        }

        private void DeleteOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizationsListBox.SelectedItem is Organization selectedOrganization)
            {
                Dispatcher.Invoke(() => _organizations.Remove(selectedOrganization));
                Log("Удалена организация: " + selectedOrganization.Name);
            }
        }

        private void EditOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrganizationsListBox.SelectedItem is Organization selectedOrganization)
            {
                var editWindow = new EditOrganizationWindow(selectedOrganization);
                if (editWindow.ShowDialog() == true)
                {
                    Log("Изменена организация: " + selectedOrganization.Name);
                }
            }
        }

        private void Add10OrganizationsButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var organization = new Organization { Name = $"Организация {i + 1}" };

                    Dispatcher.Invoke(() =>
                    {
                        _organizations.Add(organization);
                        OnPropertyChanged(nameof(Organizations));
                    });

                    for (int j = 0; j < 100; j++)
                    {
                        var person = new Person
                        {
                            Organization = organization,
                            FullName = $"Сотрудник {j + 1}",
                            Position = $"Должность {j + 1}",
                            PhoneNumber = $"Телефон {j + 1}"
                        };

                        Dispatcher.Invoke(() =>
                        {
                            organization.People.Add(person);
                        });
                    }
                }
            });

            Log("Добавлены 10 организаций с 100 сотрудниками в каждой");
        }

        private async void LoadFromFileAsync()
        {
            await Task.Run(() =>
            {
                if (File.Exists(_logFilePath))
                {
                    string[] lines = File.ReadAllLines(_logFilePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(';');

                        if (parts[0] == "Organization")
                        {
                            var organization = new Organization
                            {
                                Name = parts[1]
                            };

                            Dispatcher.Invoke(() => _organizations.Add(organization));

                        }
                        else if (parts[0] == "Person")
                        {
                            if (parts.Length >= 6)
                            {
                                var organizationName = parts[5];
                                var organization = _organizations.FirstOrDefault(o => o.Name == organizationName);
                                if (organization != null)
                                {
                                    var person = new Person
                                    {
                                        FullName = parts[1],
                                        Position = parts[2],
                                        PhoneNumber = parts[3],
                                        PhotoPath = parts[4],
                                        Organization = organization
                                    };

                                    Dispatcher.Invoke(() => _people.Add(person));
                                    Dispatcher.Invoke(() => organization.People.Add(person));
                                }
                            }
                            else
                            {
                                Log("Строка с данными сотрудника неполная");
                            }
                        }
                    }
                }
            });

            Log("Данные загружены из файла");
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath))
                {
                    foreach (var organization in _organizations)
                    {
                        writer.WriteLine($"Organization;{organization.Name}");
                        foreach (var person in organization.People)
                        {
                            writer.WriteLine($"Person;{person.FullName};{person.Position};{person.PhoneNumber};{person.PhotoPath};{organization.Name}");
                        }
                    }
                }

                Log("Данные сохранены в файл");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
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

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, DateTime.Now + ": " + message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи в лог: {ex.Message}");
            }
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