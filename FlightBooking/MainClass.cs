using System;
using System.Collections.Generic;
using System.Configuration;
using FlightBooking.model;
using tasks.repository;

namespace FlightBooking
{
    public class MainClass
    {
        public static void Main(String[] args)
        {
            Employee employee = new Employee(1, "tihamer", "pass");
            Flight flight = new Flight(1, "2019/03/11", "17:00", "Aeroport Cluj-Napoca", "Bucuresti", 250);
            
            IDictionary<String, string> props = new Dictionary<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("tasksDB"));
            
            EmployeeDbRepository employeeDbRepository = new EmployeeDbRepository(props);
            FlightDbRepository flightDbRepository = new FlightDbRepository(props);
                
            //employeeDbRepository.save(employee);
            employeeDbRepository.login("tihamer", "pass");
            
            //flightDbRepository.save(flight);
            flightDbRepository.findAllFlightsWithDestinationAndDate("Bucuresti", "2019/03/11");
            flightDbRepository.findAllFlightsWithDestinationAndDateAndTime("Bucuresti", "2019/03/11", "17:00");
        }
        
        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}