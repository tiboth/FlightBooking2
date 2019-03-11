using System;
using System.Collections.Generic;
using System.Data;
using FlightBooking.model;
using log4net;

namespace tasks.repository
{
    public class FlightDbRepository: ICrudRepository<int, Flight>
    {
        private static readonly ILog log = LogManager.GetLogger("FlightDbRepository");

		IDictionary<String, string> props;
		
		public FlightDbRepository(IDictionary<String, string> props)
		{
			log.Info("Creating FlightDbRepository ");
			this.props = props;
		}

		public Flight findOne(int id)
		{
			log.InfoFormat("Entering findOne with value {0}", id);
			IDbConnection con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, departuredate, departuretime, airport, destination, places  from flights where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idF = dataR.GetInt32(0);
						String departureDate = dataR.GetString(1);
						String departureTime = dataR.GetString(2);
						String airport = dataR.GetString(3);
						String destination = dataR.GetString(4);
						int places = dataR.GetInt32(5);
						
						Flight flight = new Flight(idF, departureDate, departureTime, airport, destination, places);
						log.InfoFormat("Exiting findOne with value {0}", flight);
						return flight;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}

		public IEnumerable<Flight> findAll()
		{
			IDbConnection con = DBUtils.getConnection(props);
			IList<Flight> flightList = new List<Flight>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, departureDate, departureTime, airport, destination, places from flights";
                
				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int idF = dataR.GetInt32(0);
						String departureDate = dataR.GetString(1);
						String departureTime = dataR.GetString(2);
						String airport = dataR.GetString(3);
						String destination = dataR.GetString(4);
						int places = dataR.GetInt32(5);
						
						Flight flight = new Flight(idF, departureDate, departureTime, airport, destination, places);
						flightList.Add(flight);
					}
				}
			}

			return flightList;
		}
		public IEnumerable<Flight> findAllFlightsWithDestinationAndDate(String fDestination, String fDate)
		{
			IDbConnection con = DBUtils.getConnection(props);
			IList<Flight> flightList = new List<Flight>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, departureDate, departureTime, airport, destination, places from flights where destination=@destination and departureDate=@departureDate";
				IDbDataParameter paramDestinaton = comm.CreateParameter();
				paramDestinaton.ParameterName = "@destination";
				paramDestinaton.Value = fDestination;
				comm.Parameters.Add(paramDestinaton);
				
				IDbDataParameter paramDate = comm.CreateParameter();
				paramDate.ParameterName = "@departureDate";
				paramDate.Value = fDate;
				comm.Parameters.Add(paramDate);
				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int idF = dataR.GetInt32(0);
						String departureDate = dataR.GetString(1);
						String departureTime = dataR.GetString(2);
						String airport = dataR.GetString(3);
						String destination = dataR.GetString(4);
						int places = dataR.GetInt32(5);
						
						Flight flight = new Flight(idF, departureDate, departureTime, airport, destination, places);
						flightList.Add(flight);
					}
				}
			}

			return flightList;
		}
		public IEnumerable<Flight> findAllFlightsWithDestinationAndDateAndTime(String fDestination, String fDate, String fTime)
		{
			IDbConnection con = DBUtils.getConnection(props);
			IList<Flight> flightList = new List<Flight>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, departureDate, departureTime, airport, destination, places from flights where destination=@destination and departureDate=@departureDate and departureTime=@departureTime";
				IDbDataParameter paramDestinaton = comm.CreateParameter();
				paramDestinaton.ParameterName = "@destination";
				paramDestinaton.Value = fDestination;
				comm.Parameters.Add(paramDestinaton);
				
				IDbDataParameter paramDate = comm.CreateParameter();
				paramDate.ParameterName = "@departureDate";
				paramDate.Value = fDate;
				comm.Parameters.Add(paramDate);
				
				IDbDataParameter paramTime = comm.CreateParameter();
				paramTime.ParameterName = "@departureTime";
				paramTime.Value = fTime;
				comm.Parameters.Add(paramTime);
				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int idF = dataR.GetInt32(0);
						String departureDate = dataR.GetString(1);
						String departureTime = dataR.GetString(2);
						String airport = dataR.GetString(3);
						String destination = dataR.GetString(4);
						int places = dataR.GetInt32(5);
						
						Flight flight = new Flight(idF, departureDate, departureTime, airport, destination, places);
						flightList.Add(flight);
					}
				}
			}

			return flightList;
		}
		public void save(Flight entity)
		{
			var con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "insert into flights (departureDate, departureTime, airport, destination, places)  values ( @departureDate, @departureTime, @airport, @destination, @places)";

				var paramDepartureDate = comm.CreateParameter();
				paramDepartureDate.ParameterName = "@departureDate";
				paramDepartureDate.Value = entity.DepartureDate;
				comm.Parameters.Add(paramDepartureDate);
				
				var paramDepartureTime = comm.CreateParameter();
				paramDepartureTime.ParameterName = "@departureTime";
				paramDepartureTime.Value = entity.DepartureTime;
				comm.Parameters.Add(paramDepartureTime);
				
				var paramAirport = comm.CreateParameter();
				paramAirport.ParameterName = "@airport";
				paramAirport.Value = entity.Airport;
				comm.Parameters.Add(paramAirport);
				
				var paramDestination = comm.CreateParameter();
				paramDestination.ParameterName = "@destination";
				paramDestination.Value = entity.Destination;
				comm.Parameters.Add(paramDestination);
				
				var paramPlaces = comm.CreateParameter();
				paramPlaces.ParameterName = "@places";
				paramPlaces.Value = entity.Places;
				comm.Parameters.Add(paramPlaces);

				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No flight added !");
			}
			
		}
		public void update(int integer, Flight entity)
		{
			var con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "update flights set departureDate=@departureDate, departureTime=@departureTime, airport=@airport, destination=@destination, places=@places where id=@idF";
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@idF";
				paramId.Value = integer;
				comm.Parameters.Add(paramId);
				
				var paramDepartureDate = comm.CreateParameter();
				paramDepartureDate.ParameterName = "@departureDate";
				paramDepartureDate.Value = entity.DepartureDate;
				comm.Parameters.Add(paramDepartureDate);
				
				var paramDepartureTime = comm.CreateParameter();
				paramDepartureTime.ParameterName = "@departureTime";
				paramDepartureTime.Value = entity.DepartureTime;
				comm.Parameters.Add(paramDepartureTime);
				
				var paramAirport = comm.CreateParameter();
				paramAirport.ParameterName = "@airport";
				paramAirport.Value = entity.Airport;
				comm.Parameters.Add(paramAirport);
				
				var paramDestination = comm.CreateParameter();
				paramDestination.ParameterName = "@destination";
				paramDestination.Value = entity.Destination;
				comm.Parameters.Add(paramDestination);
				
				var paramPlaces = comm.CreateParameter();
				paramPlaces.ParameterName = "@places";
				paramPlaces.Value = entity.Places;
				comm.Parameters.Add(paramPlaces);

				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No flight updated !");
			}
			
		}
		public void delete(int id)
		{
			IDbConnection con = DBUtils.getConnection(props);
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "delete from flights where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);
				var dataR = comm.ExecuteNonQuery();
				if (dataR == 0)
					throw new RepositoryException("No flight deleted!");
			}
		}
    }
}