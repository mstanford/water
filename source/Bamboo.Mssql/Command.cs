// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006 Swampware, Inc.
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// ------------------------------------------------------------------------------
using System;

namespace Bamboo.Mssql
{
	/// <summary>
	/// Summary description for Command.
	/// </summary>
	public class Command
	{

		public static void ExecuteNonQuery(string connection_string, string text)
		{
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
			sqlConnection.ConnectionString = connection_string;
			sqlConnection.Open();

			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandText = text;
			sqlCommand.CommandType = System.Data.CommandType.Text;
			sqlCommand.ExecuteNonQuery();

			sqlConnection.Close();
		}

		public static System.Data.SqlClient.SqlDataReader ExecuteReader(string connection_string, string text)
		{
			System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
			sqlConnection.ConnectionString = connection_string;
			sqlConnection.Open();

			System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandText = text;
			sqlCommand.CommandType = System.Data.CommandType.Text;
			System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();

			return dataReader;
		}

		private Command()
		{
		}

	}
}
