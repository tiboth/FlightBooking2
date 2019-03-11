using System;

namespace FlightBooking.model
{
    public class Employee
    {
        private int id;
        private string username;
        private string password;

        public Employee(int id, String username, String password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ToString()
        {
            return string.Format("[Id={0}, Username={1}, Password={2}]", Id, Username, Password);
        }
    }
}