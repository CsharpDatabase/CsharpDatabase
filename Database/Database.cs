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

			if(_type == DatabaseType.SQLite)
			{
				var split = text.Split('\'');
				if(split.Length > 1)
				{
					int x = 0;
					var sb = new StringBuilder();

					foreach(var s in split)
					{
						x++;

						if(s.Length == 0 && x != split.Length)
							sb.Append(@"''");
						else if(s.Length == 1)
						{
							if(s.Substring(0, 1) != @"'")
							{
								sb.Append(s);
								sb.Append(@"''");
							}
							else
								sb.Append(@"' ''");
						}
						else
						{
							int i = 0;
							string ss = s;

							for(;;)
							{
								if(ss.Length > 0 && ss.Substring(ss.Length-1) != @"'")
								{
									if(ss.Length-1 > 0)
										sb.Append(ss.Substring(0, ss.Length));

									for(int a = 0; a < i; a++)
										sb.Append(@"'");

									if(x != split.Length && i % 2 == 0)
										sb.Append(@"''");
									else if(x != split.Length)
										sb.Append(@" ''");

									break;
								}
								else if(ss.Length <= 0)
								{
									for(int a = 0; a < i; a++)
										sb.Append(@"'");

									if(x != split.Length && i % 2 == 0)
										sb.Append(@"''");
									else if(x != split.Length)
										sb.Append(@" ''");

									break;
								}

								i++;
								ss = ss.Remove(ss.Length-1);
							}
						}
					}

					text = sb.ToString();
				}
			}
			else
				return MySqlHelper.EscapeString(text);

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
