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

namespace bamboo.Controls.DatabaseExplorer
{
	/// <summary>
	/// Summary description for SqlSchemaReader.
	/// </summary>
	public class SqlSchemaReader
	{

		public static Bamboo.Mssql.Database Read(string connectionString)
		{
			System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
			connection.Open();

			Bamboo.Mssql.Database database = new Bamboo.Mssql.Database();

			database.Name = connection.Database;
			database.Tables = ReadTables(connection);
			database.Relationships = ReadRelationships(connection);
			database.Views = ReadViews(connection);
			database.Procedures = ReadProcedures(connection);

			connection.Close();

			return database;
		}

		public static Bamboo.Mssql.TableCollection ReadTables(System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.TableCollection tables = new Bamboo.Mssql.TableCollection();

			string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME != 'dtproperties' ORDER BY TABLE_NAME";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.Table table = new Bamboo.Mssql.Table();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("TABLE_NAME"))) 
				{
					table.Name = dataReader.GetString(dataReader.GetOrdinal("TABLE_NAME"));
				}

				tables.Add(table);
			}

			dataReader.Close();

			foreach(Bamboo.Mssql.Table table in tables)
			{
				table.Columns = ReadTableColumns(table.Name, connection);
			}

			return tables;
		}

		public static Bamboo.Mssql.TableColumnCollection ReadTableColumns(string tableName, System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.TableColumnCollection columns = new Bamboo.Mssql.TableColumnCollection();

			string query = "SELECT COLUMN_NAME, IS_NULLABLE, (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ON INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME WHERE INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE = 'PRIMARY KEY' AND INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS IS_PRIMARY_KEY, (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ON INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME WHERE INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE = 'FOREIGN KEY' AND INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS IS_FOREIGN_KEY, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' ORDER BY TABLE_NAME, ORDINAL_POSITION";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.TableColumn column = new Bamboo.Mssql.TableColumn();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("COLUMN_NAME")))
				{
					column.Name = dataReader.GetString(dataReader.GetOrdinal("COLUMN_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("IS_NULLABLE")))
				{
					string is_nullable = dataReader.GetString(dataReader.GetOrdinal("IS_NULLABLE")).Trim().ToUpper();
					if(is_nullable == "YES")
					{
						column.IsNullable = true;
					}
					else if(is_nullable == "NO")
					{
						column.IsNullable = false;
					}
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("IS_PRIMARY_KEY")))
				{
					int count = dataReader.GetInt32(dataReader.GetOrdinal("IS_PRIMARY_KEY"));
					if(count > 0)
					{
						column.IsPrimaryKey = true;
					}
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("IS_FOREIGN_KEY")))
				{
					int count = dataReader.GetInt32(dataReader.GetOrdinal("IS_FOREIGN_KEY"));
					if(count > 0)
					{
						column.IsForeignKey = true;
					}
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("DATA_TYPE")))
				{
					column.Datatype = dataReader.GetString(dataReader.GetOrdinal("DATA_TYPE"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH")))
				{
					column.Length = dataReader.GetInt32(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH"));
				}

				columns.Add(column);
			}

			dataReader.Close();

			return columns;
		}

		public static Bamboo.Mssql.RelationshipCollection ReadRelationships(System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.RelationshipCollection relationships = new Bamboo.Mssql.RelationshipCollection();

			string query = "SELECT (SELECT INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.UNIQUE_CONSTRAINT_NAME) AS PRIMARY_TABLE_NAME, (SELECT INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.UNIQUE_CONSTRAINT_NAME) AS PRIMARY_COLUMN_NAME, (SELECT INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.CONSTRAINT_NAME) AS FOREIGN_TABLE_NAME, (SELECT INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.CONSTRAINT_NAME) AS FOREIGN_COLUMN_NAME FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.Relationship relationship = new Bamboo.Mssql.Relationship();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("PRIMARY_TABLE_NAME"))) 
				{
					relationship.PrimaryKeyTable = dataReader.GetString(dataReader.GetOrdinal("PRIMARY_TABLE_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("PRIMARY_COLUMN_NAME"))) 
				{
					relationship.PrimaryKeyColumn = dataReader.GetString(dataReader.GetOrdinal("PRIMARY_COLUMN_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("FOREIGN_TABLE_NAME"))) 
				{
					relationship.ForeignKeyTable = dataReader.GetString(dataReader.GetOrdinal("FOREIGN_TABLE_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("FOREIGN_COLUMN_NAME"))) 
				{
					relationship.ForeignKeyColumn = dataReader.GetString(dataReader.GetOrdinal("FOREIGN_COLUMN_NAME"));
				}

				relationships.Add(relationship);
			}

			dataReader.Close();

			return relationships;
		}

		public static Bamboo.Mssql.ViewCollection ReadViews(System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.ViewCollection views = new Bamboo.Mssql.ViewCollection();

			string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME NOT LIKE 'sys%' ORDER BY TABLE_NAME";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.View view = new Bamboo.Mssql.View();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("TABLE_NAME"))) 
				{
					view.Name = dataReader.GetString(dataReader.GetOrdinal("TABLE_NAME"));
				}

				views.Add(view);
			}

			dataReader.Close();

			foreach(Bamboo.Mssql.View view in views)
			{
				view.Columns = ReadViewColumns(view.Name, connection);
			}

			return views;
		}

		public static Bamboo.Mssql.ViewColumnCollection ReadViewColumns(string viewName, System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.ViewColumnCollection columns = new Bamboo.Mssql.ViewColumnCollection();

			string query = "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + viewName + "' ORDER BY TABLE_NAME, ORDINAL_POSITION";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.ViewColumn column = new Bamboo.Mssql.ViewColumn();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("COLUMN_NAME")))
				{
					column.Name = dataReader.GetString(dataReader.GetOrdinal("COLUMN_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("DATA_TYPE")))
				{
					column.Datatype = dataReader.GetString(dataReader.GetOrdinal("DATA_TYPE"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH")))
				{
					column.Length = dataReader.GetInt32(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH"));
				}

				columns.Add(column);
			}

			dataReader.Close();

			return columns;
		}

		public static Bamboo.Mssql.ProcedureCollection ReadProcedures(System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.ProcedureCollection procedures = new Bamboo.Mssql.ProcedureCollection();

			string query = "SELECT ROUTINE_NAME, ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME NOT LIKE 'dt_%' ORDER BY ROUTINE_NAME";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.Procedure procedure = new Bamboo.Mssql.Procedure();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("ROUTINE_NAME"))) 
				{
					procedure.Name = dataReader.GetString(dataReader.GetOrdinal("ROUTINE_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("ROUTINE_DEFINITION"))) 
				{
					procedure.Definition = dataReader.GetString(dataReader.GetOrdinal("ROUTINE_DEFINITION"));
				}

				procedures.Add(procedure);
			}

			dataReader.Close();

			return procedures;
		}

		public static Bamboo.Mssql.ProcedureParameterCollection ReadProcedureParameters(string procedureName, System.Data.SqlClient.SqlConnection connection)
		{
			Bamboo.Mssql.ProcedureParameterCollection parameters = new Bamboo.Mssql.ProcedureParameterCollection();

			string query = "SELECT PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME = '" + procedureName + "' ORDER BY ORDINAL_POSITION";

			System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, connection);

			System.Data.SqlClient.SqlDataReader dataReader = command.ExecuteReader();

			while(dataReader.Read())
			{
				Bamboo.Mssql.ProcedureParameter parameter = new Bamboo.Mssql.ProcedureParameter();

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("PARAMETER_NAME"))) 
				{
					parameter.Name = dataReader.GetString(dataReader.GetOrdinal("PARAMETER_NAME"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("DATA_TYPE"))) 
				{
					parameter.Datatype = dataReader.GetString(dataReader.GetOrdinal("DATA_TYPE"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH"))) 
				{
					parameter.Length = dataReader.GetInt32(dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH"));
				}

				if(!dataReader.IsDBNull(dataReader.GetOrdinal("PARAMETER_MODE"))) 
				{
					parameter.Direction = dataReader.GetString(dataReader.GetOrdinal("PARAMETER_MODE"));
				}

				parameters.Add(parameter);
			}

			dataReader.Close();

			return parameters;
		}

		private SqlSchemaReader()
		{
		}

	}
}
