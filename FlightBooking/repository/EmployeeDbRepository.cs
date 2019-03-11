using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using FlightBooking.model;
using log4net;

namespace tasks.repository
{
    public class EmployeeDbRepository: ICrudRepository<int, Employee>
    {
        private static readonly ILog log = LogManager.GetLogger("EmployeeDbRepository");

        private IDictionary<String, string> props;

        public EmployeeDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating EmployeeDbRepository");
            this.props = props;
        }

        public Employee findOne(int id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,username, password from employees where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idE = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        String password = dataR.GetString(2);
                        
                        Employee employee = new Employee(idE, username, password);
                        
                        log.InfoFormat("Exiting findOne with value {0}", employee);
                        return employee;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }
        
        public Employee login(string username, string password)
        {
            log.InfoFormat("Entering findOne with value {0}", username);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,username, password from employees where username=@username and password=@password";
                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = password;
                comm.Parameters.Add(paramPassword);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idE = dataR.GetInt32(0);
                        String usernameFound = dataR.GetString(1);
                        String passwordFound = dataR.GetString(2);
                        
                        Employee employee = new Employee(idE, usernameFound, passwordFound);
                        
                        log.InfoFormat("Exiting findOne with value {0}", employee);
                        Console.Write("login successful for " + usernameFound + "\n");
                        return employee;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }
        
        public IEnumerable<Employee> findAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Employee> employeeList = new List<Employee>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, username, password from employees";
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idE = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        String password = dataR.GetString(2);
                       
                        Employee employee = new Employee(idE, username, password);
                        employeeList.Add(employee);
                    }
                }
            }

            return employeeList;
        }
        
        public void save(Employee entity)
        {
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into employees (username, password) values (@username, @password)";
               

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = entity.Username;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = entity.Password;
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No employee added !");
            }

            Console.Write("saveee!!");;
        }
        
        public void update(int integer, Employee entity)
        {
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update employees set username=@username, password=@password where id=@idE";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@idE";
                paramId.Value = integer;
                comm.Parameters.Add(paramId);
				
                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@flightId";
                paramUsername.Value = entity.Username;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@clientName";
                paramPassword.Value = entity.Password;
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No employee updated !");
            }
			
        }
        
        public void delete(int id)
        {
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from employees where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No employee deleted!");
            }
        }
    }
}