/*
 * This file is part of CsharpDatabase.
 * 
 * Copyright (C) 2012-2013 Megax <http://megax.yeahunter.hu/>
 * 
 * Schumix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Schumix is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with CsharpDatabase.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;
using CsharpDatabase.Exceptions;
using CsharpDatabase.Extensions;
using CsharpDatabase.Localization;

namespace CsharpDatabase
{
	sealed class MySql
	{
		private readonly LocalizationConsole sLConsole = Singleton<LocalizationConsole>.Instance;
		private MySqlConnection Connection;

		public MySql(string host, string username, string password, string database)
		{
			if(!Initialize(host, username, password, database))
				throw new CDatabaseException(sLConsole.MySql("Text"));
		}

		public MySql(string host, string username, string password, string database, string charset)
		{
			if(!Initialize(host, username, password, database, charset))
				throw new CDatabaseException(sLConsole.MySql("Text"));
		}

		~MySql()
		{
			Connection.Close();
		}

		private bool Initialize(string host, string username, string password, string database)
		{
			try
			{
				Connection = new MySqlConnection(string.Format("SERVER={0};DATABASE={1};UID={2};PWD={3};", host, database, username, password));
				Connection.Open();
				return true;
			}
			catch(MySqlException m)
			{
				throw new CDatabaseException(m.Message);
			}
		}

		private bool Initialize(string host, string username, string password, string database, string charset)
		{
			try
			{
				Connection = new MySqlConnection(string.Format("SERVER={0};DATABASE={1};UID={2};PWD={3};charset={4};", host, database, username, password, charset));
				Connection.Open();
				return true;
			}
			catch(MySqlException m)
			{
				throw new CDatabaseException(m.Message);
			}
		}

		public DataTable Query(string query, bool logerror = true)
		{
			try
			{
				IsConnect();
				var adapter = new MySqlDataAdapter();
				var command = Connection.CreateCommand();
				command.CommandText = query;
				adapter.SelectCommand = command;

				var table = new DataTable();
				adapter.Fill(table);

				command.Dispose();
				adapter.Dispose();

				return table;
			}
			catch(MySqlException m)
			{
				Crash(m, logerror);
				return null;
			}
		}

		public DataRow QueryFirstRow(string query)
		{
			var table = Query(query);
			return !table.Equals(null) && table.Rows.Count > 0 ? table.Rows[0] : null;
		}

		private void ExecuteNonQuery(string sql, bool logerror = true)
		{
			try
			{
				IsConnect();
				var command = Connection.CreateCommand();
				command.CommandText = sql;
				command.ExecuteNonQuery();
			}
			catch(MySqlException m)
			{
				Crash(m, logerror);
			}
		}

		private void IsConnect()
		{
			try
			{
				if(!Connection.Ping())
					Connection.Open();

				if(Connection.State == ConnectionState.Broken || Connection.State == ConnectionState.Closed)
					throw new CDatabaseException(sLConsole.MySql("Text3"));
			}
			catch(MySqlException m)
			{
				Crash(m, true);
			}
		}

		private void Crash(MySqlException m, bool logerror)
		{
			if(logerror)
				throw new CDatabaseException(string.Format(sLConsole.MySql("Text2"), m.Message));
		}

		public bool Update(string sql)
		{
			try
			{
				ExecuteNonQuery("UPDATE " + sql);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Update(string TableName, string Set)
		{
			try
			{
				return Update(TableName + " SET " + Set);
			}
			catch
			{
				return false;
			}
		}

		public bool Update(string TableName, string Set, string Where)
		{
			try
			{
				return Update(TableName + " SET " + Set + " WHERE " + Where);
			}
			catch
			{
				return false;
			}
		}

		public bool Insert(string sql)
		{
			try
			{
				ExecuteNonQuery("INSERT INTO " + sql);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Insert(string TableName, string Values)
		{
			try
			{
				return Insert(TableName + " VALUES (" + Values + ")");
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(string sql)
		{
			try
			{
				ExecuteNonQuery("DELETE FROM " + sql);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(string TableName, string Where)
		{
			try
			{
				return Delete(TableName + " WHERE " + Where);
			}
			catch
			{
				return false;
			}
		}

		public bool RemoveTable(string Table)
		{
			try
			{
				ExecuteNonQuery(string.Format("DROP TABLE IF EXISTS `{0}`", Table));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool IsCreatedTable(string Table)
		{
			return !Query(string.Format("SHOW CREATE TABLE {0}", Table), false).IsNull();
		}
	}
}