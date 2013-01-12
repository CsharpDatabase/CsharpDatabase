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

namespace CsharpDatabase.Extensions
{
	/// <summary>
	/// Some random extension stuff.
	/// </summary>
	static class GeneralExtensions
	{
		/// <summary>
		/// Determines whether the specified obj is null.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <returns>
		/// 	<c>true</c> if the specified obj is null; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNull(this object obj)
		{
			return (obj == null);
		}

		/// <summary>
		/// Determines whether the specified obj is a type of the specified type.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="type">The type.</param>
		/// <returns>
		/// 	<c>true</c> if the specified obj is a type of the specified type; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsOfType(this object obj, Type type)
		{
			if(obj.IsNull())
				return false;

			return (obj.GetType() == type);
		}

		public static string Remove(this string s, int min, int max, char value)
		{
			return (s.Length >= max && s.Substring(min, max) == value.ToString()) ? s.Remove(min, max) : s;
		}

		public static string Remove(this string s, int min, int max, string value)
		{
			return (s.Length >= max && s.Substring(min, max) == value) ? s.Remove(min, max) : s;
		}

		public static bool Contains(this string Text, string Name, char Parameter)
		{
			var s = Text.Split(Parameter);

			foreach(var ss in s)
			{
				if(ss.ToLower() == Name.ToLower())
					return true;
			}

			return false;
		}
	}
}