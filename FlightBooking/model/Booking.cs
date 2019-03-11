namespace FlightBooking.model
{
    public class Booking
    {
        private int id;
        private int flightId;
        private string clientName;
        private string clientAddress;
        private string tourists;
        private int nrSeats;

        public Booking(int id, int flightId, string clientName, string clientAddress, string tourists, int nrSeats)
        {
            this.id = id;
            this.flightId = flightId;
            this.clientName = clientName;
            this.clientAddress = clientAddress;
            this.tourists = tourists;
            this.nrSeats = nrSeats;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int FlightId
        {
            get { return flightId;}
            set { flightId = value; }
        }

        public string ClientName
        {
            get { return clientName;}
            set { clientName = value; }
        }

        public string ClientAddress
        {
            get { return clientAddress; }
            set { clientAddress = value; }
        }

        public string Tourists
        {
            get { return tourists; }
            set { tourists = value; }
        }

        public int NrSeats
        {
            get { return nrSeats; }
            set { nrSeats = value; }
        }

        public override string ToString()
        {
            return string.Format("[Id={0}, FlightId={1}, ClientName={2}, ClientAddress={3}, Tourists={4}, NrSeats={5]", Id, FlightId, ClientName, ClientAddress, Tourists, NrSeats);
        }
    }
}