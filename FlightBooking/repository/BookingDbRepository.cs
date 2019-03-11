using System;
using System.Collections.Generic;
using System.Data;
using FlightBooking.model;
using log4net;

namespace tasks.repository
{
    public class BookingDbRepository: ICrudRepository<int, Booking>
    {
        private static readonly ILog log = LogManager.GetLogger("BookingDbRepository");

		IDictionary<String, string> props;
		
		public BookingDbRepository(IDictionary<String, string> props)
		{
			log.Info("Creating BookingDbRepository ");
			this.props = props;
		}

		public Booking findOne(int id)
		{
			log.InfoFormat("Entering findOne with value {0}", id);
			IDbConnection con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, flightId, clientName, clientAddress, tourists, nrSeats   from bookings where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idB = dataR.GetInt32(0);
						int idF = dataR.GetInt32(1);
						String clientName = dataR.GetString(2);
						String clientAddress = dataR.GetString(3);
						String tourists = dataR.GetString(4);
						int nrSeats = dataR.GetInt32(5);
						
						Booking booking = new Booking(idB,idF, clientName, clientAddress, tourists, nrSeats);
						log.InfoFormat("Exiting findOne with value {0}", booking);
						return booking;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}

		public IEnumerable<Booking> findAll()
		{
			IDbConnection con = DBUtils.getConnection(props);
			IList<Booking> bookingList = new List<Booking>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id, flightId, clientName, clientAddress, tourists, nrSeats from bookings";
                
				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int idB = dataR.GetInt32(0);
						int idF = dataR.GetInt32(1);
						String clientName = dataR.GetString(2);
						String clientAddress = dataR.GetString(3);
						String tourists = dataR.GetString(4);
						int nrSeats = dataR.GetInt32(5);
						
						Booking booking = new Booking(idB,idF, clientName, clientAddress, tourists, nrSeats);
						bookingList.Add(booking);
					}
				}
			}

			return bookingList;
		}
		public void save(Booking entity)
		{
			var con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "insert into bookings (flightId, clientName, clientAddress, tourists, nrSeats)  values ( @flightId, @clientName, @clientAddress, @tourists, @nrSeats)";
				var paramFlightId = comm.CreateParameter();
				paramFlightId.ParameterName = "@flightId";
				paramFlightId.Value = entity.FlightId;
				comm.Parameters.Add(paramFlightId);

				var paramClientName = comm.CreateParameter();
				paramClientName.ParameterName = "@clientName";
				paramClientName.Value = entity.ClientName;
				comm.Parameters.Add(paramClientName);
				
				var paramClientAddress = comm.CreateParameter();
				paramClientAddress.ParameterName = "@clientAddress";
				paramClientAddress.Value = entity.ClientAddress;
				comm.Parameters.Add(paramClientAddress);
				
				var paramTourists = comm.CreateParameter();
				paramTourists.ParameterName = "@tourists";
				paramTourists.Value = entity.Tourists;
				comm.Parameters.Add(paramTourists);
				
				var paramNrSeats = comm.CreateParameter();
				paramNrSeats.ParameterName = "@nrSeats";
				paramNrSeats.Value = entity.NrSeats;
				comm.Parameters.Add(paramNrSeats);

				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No booking added !");
			}
			
		}
		public void update(int integer, Booking entity)
		{
			var con = DBUtils.getConnection(props);

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "update bookings set flightId=@flightId, clientName=@clientName, clientAddress=@clientAddress, tourists=@tourist, nrSeats=@nrSeats where id=@idB";
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@idB";
				paramId.Value = integer;
				comm.Parameters.Add(paramId);
				
				var paramFlightId = comm.CreateParameter();
				paramFlightId.ParameterName = "@flightId";
				paramFlightId.Value = entity.FlightId;
				comm.Parameters.Add(paramFlightId);

				var paramClientName = comm.CreateParameter();
				paramClientName.ParameterName = "@clientName";
				paramClientName.Value = entity.ClientName;
				comm.Parameters.Add(paramClientName);
				
				var paramClientAddress = comm.CreateParameter();
				paramClientAddress.ParameterName = "@clientAddress";
				paramClientAddress.Value = entity.ClientAddress;
				comm.Parameters.Add(paramClientAddress);
				
				var paramTourists = comm.CreateParameter();
				paramTourists.ParameterName = "@tourists";
				paramTourists.Value = entity.Tourists;
				comm.Parameters.Add(paramTourists);
				
				var paramNrSeats = comm.CreateParameter();
				paramNrSeats.ParameterName = "@nrSeats";
				paramNrSeats.Value = entity.NrSeats;
				comm.Parameters.Add(paramNrSeats);

				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No booking updated !");
			}
			
		}
		public void delete(int id)
		{
			IDbConnection con = DBUtils.getConnection(props);
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "delete from bookings where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);
				var dataR = comm.ExecuteNonQuery();
				if (dataR == 0)
					throw new RepositoryException("No booking deleted!");
			}
		}
    }
}