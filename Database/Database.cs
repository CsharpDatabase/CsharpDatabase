/*
 * This file is part of CsharpDatabase.
 * 
 * Copyright (C) 2012 Megax <http://www.megaxx.info/>
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
 * along with Schumix.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SQLite;
using System.Threading;
using MySql.Data;
using CsharpDatabase.Exceptions;
using CsharpDatabase.Extensions;
using CsharpDatabase.Localization;

namespace CsharpDatabase
{
	public enum DatabaseType
	{
		MySql,
		SQLite,
		None
	}

	public sealed class Database
	{
		private readonly LocalizationConsole sLConsole = Singleton<LocalizationConsole>.Instance;
		private readonly object _lock = new object();
		private readonly DatabaseType _type;
		private SQLite sdatabase;
		private MySql mdatabase;

		public Database(DatabaseType Type)
		{
			_type = Type;
			sLConsole.Locale = "enUS";
		}

		public Database(DatabaseType Type, string locale)
		{
			_type = Type;
			sLConsole.Locale = locale;
		}

		public void Locale(string locale)
		{
			sLConsole.Locale = locale;
		}

		public string Escape(string text)
		{
			if(text.IsNull() || text == string.Empty)
				return string.Empty;

			text = Regex.Replace(text, @"'", @"\'");
			text = Regex.Replace(text, @"\\'", @" \'");
			text = Regex.Replace(text, @"`", @"\`");
			text = Regex.Replace(text, @"\\`", @" \`");
			return text;
		}

		public void Mysql(string Host, string User, string Password, string Database)
		{
			if(_type == DatabaseType.MySql)
				mdatabase = new MySql(Host, User, Password, Database);
			else
				throw new CDatabaseException(sLConsole.Database("Text2"));
		}

		public void Mysql(string Host, string User, string Password, string Database, string Charset)
		{
			if(_type == DatabaseType.MySql)
				mdatabase = new MySql(Host, User, Password, Database, Charset);
			else
				throw new CDatabaseException(sLConsole.Database("Text2"));
		}

		public void Sqlite(string FileName)
		{
			if(_type == DatabaseType.SQLite)
				sdatabase = new SQLite(FileName);
			else
				throw new CDatabaseException(sLConsole.Database("Text2"));
		}

		public DataTable Query(string sql)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Query(sql);
					case DatabaseType.SQLite:
						return sdatabase.Query(sql);
				}

				return null;
			}
		}

		public DataTable Query(string query, params object[] args)
		{
			lock(_lock)
			{
				return Query(string.Format(query, args));
			}
		}

		public DataRow QueryFirstRow(string query)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.QueryFirstRow(query);
					case DatabaseType.SQLite:
						return sdatabase.QueryFirstRow(query);
				}

				return null;
			}
		}

		public DataRow QueryFirstRow(string query, params object[] args)
		{
			lock(_lock)
			{
				return QueryFirstRow(string.Format(query, args));
			}
		}

		public bool Update(string sql)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Update(sql);
					case DatabaseType.SQLite:
						return sdatabase.Update(sql);
				}

				return false;
			}
		}

		public bool Update(string TableName, string Set)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Update(TableName, Set);
					case DatabaseType.SQLite:
						return sdatabase.Update(TableName, Set);
				}

				return false;
			}
		}

		public bool Update(string TableName, string Set, string Where)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Update(TableName, Set, Where);
					case DatabaseType.SQLite:
						return sdatabase.Update(TableName, Set, Where);
				}

				return false;
			}
		}

		public bool Insert(string sql)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Insert(sql);
					case DatabaseType.SQLite:
						return sdatabase.Insert(sql);
				}

				return false;
			}
		}

		public bool Insert(string TableName, params object[] Values)
		{
			lock(_lock)
			{
				var sb = new StringBuilder();

				foreach(var value in Values)
					sb.Append(",'" + value + "'");

				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Insert(TableName, sb.ToString().Remove(0, 1, ','));
					case DatabaseType.SQLite:
						return sdatabase.Insert(TableName, sb.ToString().Remove(0, 1, ','));
				}

				return false;
			}
		}

		public bool Delete(string sql)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Delete(sql);
					case DatabaseType.SQLite:
						return sdatabase.Delete(sql);
				}

				return false;
			}
		}

		public bool Delete(string TableName, string Where)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.Delete(TableName, Where);
					case DatabaseType.SQLite:
						return sdatabase.Delete(TableName, Where);
				}

				return false;
			}
		}

		public bool RemoveTable(string Table)
		{
			lock(_lock)
			{
				switch(_type)
				{
					case DatabaseType.MySql:
						return mdatabase.RemoveTable(Table);
					case DatabaseType.SQLite:
						return sdatabase.RemoveTable(Table);
				}

				return false;
			}
		}
	}
}