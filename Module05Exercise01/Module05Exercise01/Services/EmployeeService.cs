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
            "tzuyu.jpg"
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

            using (var conn = new MySqlConnection(_databaseConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT * FROM tblemployee", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var profilePicture = _profilePictures[pictureIndex % _profilePictures.Count];

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
    }
}
