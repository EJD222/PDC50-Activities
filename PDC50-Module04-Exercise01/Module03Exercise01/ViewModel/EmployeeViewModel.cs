using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Module03Exercise01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Module03Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _employee;
        private string _fullName;

        public EmployeeViewModel()
        {
            _employee = new Employee
            {
                FirstName = "Park",
                LastName = "Jinyoung",
                Position = "Manager",
                Department = "Executive",
                IsActive = true,
            };

            LoadEmployeeDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            Employees = new ObservableCollection<Employee>
            {
                new Employee { FirstName = "Im", LastName = "Nayeon", Position = "Manager", Department = "HR", IsActive=true},
                new Employee { FirstName = "Yoo", LastName = "Jeongyeon", Position = "Developer", Department = "IT", IsActive=true },
                new Employee { FirstName = "Hirai", LastName = "Momo", Position = "Designer", Department = "Creative", IsActive=true },
                new Employee { FirstName = "Minatozaki", LastName = "Sana", Position = "Manager", Department = "Finance", IsActive=true },
                new Employee { FirstName = "Park", LastName = "Jihyo", Position = "Developer", Department = "Sales", IsActive=true },
                new Employee { FirstName = "Myoui", LastName = "Mina", Position = "Designer", Department = "HR", IsActive=true },
                new Employee { FirstName = "Kim", LastName = "Dahyun", Position = "Manager", Department = "Finance", IsActive=true },
                new Employee { FirstName = "Son", LastName = "Chaeyoung", Position = "Developer", Department = "IT", IsActive=true },
                new Employee { FirstName = "Chou", LastName = "Tzuyu", Position = "Designer", Department = "Sales", IsActive=true },
            };
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public ICommand LoadEmployeeDataCommand { get; }
        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(1000);
            FullName = $"{_employee.FirstName} {_employee.LastName} : {_employee.Position}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
