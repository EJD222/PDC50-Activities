﻿using Module05Exercise01.Model;
using Module05Exercise01.Services;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module05Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        public ObservableCollection<Employee> EmployeeList { get; set; }

        public ObservableCollection<Employee> FilteredEmployeeList { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterEmployeeList();
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                if (_selectedEmployee != null)
                {
                    NewEmployeeName = _selectedEmployee.Name;
                    NewEmployeeAddress = _selectedEmployee.Address;
                    NewEmployeeEmail = _selectedEmployee.email;
                    NewEmployeeContactNo = _selectedEmployee.ContactNo;
                    IsEmployeeSelected = true;
                    IsEmployeeSelectedAdd = false;
                }
                else
                {
                    IsEmployeeSelected = false;
                    IsEmployeeSelectedAdd = true;
                }

                OnPropertyChanged();
            }
        }

        private bool _IsEmployeeSelected;
        public bool IsEmployeeSelected
        {
            get => _IsEmployeeSelected;
            set
            {
                _IsEmployeeSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEmployeeSelectedAdd;
        public bool IsEmployeeSelectedAdd
        {
            get => _IsEmployeeSelectedAdd;
            set
            {
                _IsEmployeeSelectedAdd = value;
                OnPropertyChanged();
            }
        }

        //New Employee Entry
        private string _newEmployeeName;

        public string NewEmployeeName
        {
            get => _newEmployeeName;
            set
            {
                _newEmployeeName = value;
                OnPropertyChanged();
            }
        }

        private string _newEmployeeAddress;

        public string NewEmployeeAddress
        {
            get => _newEmployeeAddress;
            set
            {
                _newEmployeeAddress = value;
                OnPropertyChanged();
            }
        }

        private string _newEmployeeemail;

        public string NewEmployeeEmail
        {
            get => _newEmployeeemail;
            set
            {
                _newEmployeeemail = value;
                OnPropertyChanged();
            }
        }

        private string _newEmployeeContactNo;

        public string NewEmployeeContactNo
        {
            get => _newEmployeeContactNo;
            set
            {
                _newEmployeeContactNo = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand UpdateEmployeeCommand { get; }
        public ICommand SelectedEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }

        //EmployeeViewModel Constructor
        public EmployeeViewModel()
        {
            _employeeService = new EmployeeService();
            EmployeeList = new ObservableCollection<Employee>();
            FilteredEmployeeList = new ObservableCollection<Employee>();
            LoadDataCommand = new Command(async () => await LoadData());

            AddEmployeeCommand = new Command(async () => await AddEmployee());
            UpdateEmployeeCommand = new Command(async () => await UpdateEmployee());
            SelectedEmployeeCommand = new Command<Employee>(employee => SelectedEmployee = employee);
            DeleteEmployeeCommand = new Command(async () =>
                                            await DeleteEmployee(),
                                            () => SelectedEmployee != null);

            LoadData();
        }

        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
            StatusMessage = "Loading employee data...";
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                EmployeeList.Clear();
                foreach (var employee in employees)
                {
                    EmployeeList.Add(employee);
                }
                StatusMessage = "Data loaded succesfully!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to load data: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddEmployee()
        {
            if (IsBusy || string.IsNullOrWhiteSpace(NewEmployeeName) || string.IsNullOrWhiteSpace(NewEmployeeAddress) || string.IsNullOrWhiteSpace(NewEmployeeEmail) || string.IsNullOrWhiteSpace(NewEmployeeContactNo))
            {
                StatusMessage = "Please fill in all fields before adding";
                return;
            }
            IsBusy = true;
            StatusMessage = "Adding new employee...";

            try
            {
                var newEmployee = new Employee
                {
                    Name = NewEmployeeName,
                    Address = NewEmployeeAddress,
                    email = NewEmployeeEmail,
                    ContactNo = NewEmployeeContactNo
                };
                var isSuccess = await _employeeService.InsertEmployeeAsync(newEmployee);
                if (isSuccess)
                {
                    NewEmployeeName = string.Empty;
                    NewEmployeeAddress = string.Empty;
                    NewEmployeeEmail = string.Empty;
                    NewEmployeeContactNo = string.Empty;
                    StatusMessage = "New employee added successfully!";
                }
                else
                {
                    StatusMessage = "Failed to add new employee.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed adding person: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                await LoadData();
            }
        }

        private async Task UpdateEmployee()
        {
            if (IsBusy || SelectedEmployee == null)
            {
                StatusMessage = "Select an employee to update.";
                return;
            }

            IsBusy = true;
            try
            {
                SelectedEmployee.Name = NewEmployeeName;
                SelectedEmployee.Address = NewEmployeeAddress;
                SelectedEmployee.email = NewEmployeeEmail;
                SelectedEmployee.ContactNo = NewEmployeeContactNo;

                var success = await _employeeService.UpdateEmployeeAsync(SelectedEmployee);
                StatusMessage = success ? "Employee updated successfully!" : "Failed to update employee.";

                if (success)
                {
                    var index = EmployeeList.IndexOf(SelectedEmployee);
                    if (index >= 0)
                    {
                        EmployeeList[index] = SelectedEmployee;
                    }
                    ClearField();
                }
                else
                {
                    StatusMessage = "Failed to update employee.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteEmployee()
        {
            if (SelectedEmployee == null) return;
            var answer = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete", $"Are you sure you want to delete {SelectedEmployee.Name}?", "Yes", "No");

            if (!answer) return;

            IsBusy = true;
            StatusMessage = "Deleting person..";

            try
            {
                var success = await _employeeService.DeleteEmployeeAsync(SelectedEmployee.EmployeeID);
                StatusMessage = success ? "Employee deleted successfully!" : "Failed to delete employee.";

                if (success)
                {
                    EmployeeList.Remove(SelectedEmployee);
                    SelectedEmployee = null;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error deleting employee: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                await LoadData();
            }
        }

        private void ClearField()
        {
            NewEmployeeName = string.Empty;
            NewEmployeeAddress = string.Empty;
            NewEmployeeEmail = string.Empty;
            NewEmployeeContactNo = string.Empty;
            IsEmployeeSelected = false;
        }

        private void FilterEmployeeList()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredEmployeeList.Clear();
                foreach (var employee in EmployeeList)
                {
                    FilteredEmployeeList.Add(employee);
                }
            }
            else
            {
                var filtered = EmployeeList.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                                                    || p.Address.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                                                    || p.email.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                                                    || p.ContactNo.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                FilteredEmployeeList.Clear();
                foreach(var employee in filtered)
                {
                    FilteredEmployeeList.Add(employee);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}