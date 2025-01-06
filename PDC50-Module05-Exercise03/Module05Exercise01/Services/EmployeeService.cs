using Module05Exercise01.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Module05Exercise01.Services
{
    public class EmployeeService
    {
        private readonly string _databaseConnectionString;
        private readonly List<string> _profilePictures = new List<string>
        {
            "nayeon.jpg",
            "jeongyeon.jpg",
            "momo.jpg",
            "sana.jpg",
            "jihyo.jpg",
            "mina.jpg",
            "dahyun.jpg",
            "chaeyoung.jpg",
            "tzuyu.jpg",
        };

        public EmployeeService()
        {
            var dbService = new DatabaseConnectionService();
            _databaseConnectionString = dbService.GetConnectionString();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employeeList = new List<Employee>();
            int pictureIndex = 0;
            int maxCustomPictures = 9;

            using (var conn = new MySqlConnection(_databaseConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT * FROM tblemployee", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var profilePicture = pictureIndex < maxCustomPictures
                            ? _profilePictures[pictureIndex]
                            : "employee_2.png";

                        employeeList.Add(new Employee
                        {
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            Name = reader.GetString("Name"),
                            Address = reader.GetString("Address"),
                            email = reader.GetString("email"),
                            ContactNo = reader.GetString("ContactNo"),
                            ProfilePicture = profilePicture
                        });

                        pictureIndex++;
                    }
                }
            }
            return employeeList;
        }

        public async Task<bool> InsertEmployeeAsync(Employee newEmployee)
        {
            try
            {
                using (var conn = new MySqlConnection(_databaseConnectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("Insert INTO tblemployee (Name, Address, email, ContactNo) VALUES (@Name, @Address, @email, @ContactNo)", conn);
                    cmd.Parameters.AddWithValue("@Name", newEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", newEmployee.Address);
                    cmd.Parameters.AddWithValue("@email", newEmployee.email);
                    cmd.Parameters.AddWithValue("@ContactNo", newEmployee.ContactNo);
                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee record: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee updatedEmployee)
        {
            try
            {
                using (var conn = new MySqlConnection(_databaseConnectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand(
                        "UPDATE tblemployee SET Name = @Name, Address = @Address, email = @email, ContactNo = @ContactNo WHERE EmployeeID=@EmployeeID",
                        conn
                    );
                    cmd.Parameters.AddWithValue("@Name", updatedEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", updatedEmployee.Address);
                    cmd.Parameters.AddWithValue("@email", updatedEmployee.email);
                    cmd.Parameters.AddWithValue("@ContactNo", updatedEmployee.ContactNo);
                    cmd.Parameters.AddWithValue("@EmployeeID", updatedEmployee.EmployeeID);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0; //Returns true if the update was successful  
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error updating personal record: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                using (var conn = new MySqlConnection(_databaseConnectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("DELETE FROM tblemployee WHERE employeeId=@employeeId", conn);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee record: {ex.Message}");
                return false;
            }
        }
    }
}
