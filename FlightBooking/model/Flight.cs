using System;

namespace FlightBooking.model
{
    public class Flight
    {
        private int id;
        private string departureDate;
        private string departureTime;
        private string airport;
        private string destination;
        private int places;

        public Flight(int id, string departureDate, string departureTime, string airport, string destination, int places)
        {
            this.id = id;
            this.departureDate = departureDate;
            this.departureTime = departureTime;
            this.airport = airport;
            this.destination = destination;
            this.places = places;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }

        public string DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        public string Airport
        {
            get { return airport; }
            set { airport = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public int Places
        {
            get { return places; }
            set { places = value; }
        }

        public override string ToString()
        {
            return String.Format("[Id={0}, DepartureDate={1}, DepartureTime={2}, Airport={3}, Destination={4}, Places={5]", Id, DepartureDate, DepartureTime, Airport, Destination, Places);
        }
    }
}