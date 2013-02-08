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

namespace CsharpDatabase.Localization
{
	sealed class LocalizationConsole
	{
		public string Locale { get; set; }
		private LocalizationConsole() {}

		public string Exception(string Name)
		{
			switch(Name)
			{
				case "Error":
				{
					if(Locale == "huHU")
						return "Meghibásodás részletei: {0}";
					else if(Locale == "enUS")
						return "Failure details: {0}";
					else
						return "Failure details: {0}";
				}
				default:
					return string.Empty;
			}
		}

		public string Database(string Name)
		{
			switch(Name)
			{
				case "Text":
				{
					if(Locale == "huHU")
						return "Nincs az adatbázis típusa kiválasztva!";
					else if(Locale == "enUS")
						return "Database type's is not selected!";
					else
						return "Database type's is not selected!";
				}
				case "Text2":
				{
					if(Locale == "huHU")
						return "Nem ezen adatbázis lett kiválasztva!";
					else if(Locale == "enUS")
						return "Not this database were choosen!";
					else
						return "Not this database were choosen!";
				}
				default:
					return string.Empty;
			}
		}

		public string MySql(string Name)
		{
			switch(Name)
			{
				case "Text":
				{
					if(Locale == "huHU")
						return "Hiba történt az adatbázishoz való kapcsolodás során!";
					else if(Locale == "enUS")
						return "Error was handled when tried to connect to the database.";
					else
						return "Error was handled when tried to connect to the database.";
				}
				case "Text2":
				{
					if(Locale == "huHU")
						return "Query hiba: {0}";
					else if(Locale == "enUS")
						return "Query error: {0}";
					else
						return "Query error: {0}";
				}
				case "Text3":
				{
					if(Locale == "huHU")
						return "Sql kapcsolat összeomlott.";
					else if(Locale == "enUS")
						return "Sql connection crash.";
					else
						return "Sql connection crash.";
				}
				default:
					return string.Empty;
			}
		}

		public string SQLite(string Name)
		{
			switch(Name)
			{
				case "Text":
				{
					if(Locale == "huHU")
						return "Hiba történt az adatbázishoz való kapcsolodás során!";
					else if(Locale == "enUS")
						return "Error was handled when tried to connect to the database!";
					else
						return "Error was handled when tried to connect to the database!";
				}
				case "Text2":
				{
					if(Locale == "huHU")
						return "Query hiba: {0}";
					else if(Locale == "enUS")
						return "Query error: {0}";
					else
						return "Query error: {0}";
				}
				default:
					return string.Empty;
			}
		}
	}
}