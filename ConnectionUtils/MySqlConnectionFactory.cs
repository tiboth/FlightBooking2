using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConnectionUtils
{
    public class MySqlConnectionFactory:ConnectionFactory
	{
		public override IDbConnection createConnection(IDictionary<string,string> props)
		{
//			MySql Connection
			String connectionString = "Database=problema1;" +
										"Data Source=localhost;" +
										"User id=root;" +
										"Password=;";
			//String connectionString = props["ConnectionString"];
			Console.WriteLine("MySql ---se deschide o conexiune la  ... {0}", connectionString);
			
			return new MySqlConnection(connectionString);

	
		}
	}
}