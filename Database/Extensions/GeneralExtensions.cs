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
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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
			if(obj == null)
				return false;

			return (obj.GetType() == type);
		}

		/// <summary>
		/// Determines whether this instance can be casted to the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The obj.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can be casted to the specified type; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanBeCastedTo<T>(this object obj)
		{
			if(obj == null)
				throw new ArgumentNullException("obj");

			return (obj is T);
		}

		public static string SplitToString(this string[] split, char c)
		{
			string ss = string.Empty;

			for(int x = 0; x < split.Length; x++)
				ss += c + split[x];

			if(ss.Length > 0 && ss.Substring(0, c.ToString().Length) == c.ToString())
				ss = ss.Remove(0, c.ToString().Length);

			return ss;
		}

		public static string SplitToString(this string[] split, int min, char c)
		{
			string ss = string.Empty;

			for(int x = min; x < split.Length; x++)
				ss += c + split[x];

			if(ss.Length > 0 && ss.Substring(0, c.ToString().Length) == c.ToString())
				ss = ss.Remove(0, c.ToString().Length);

			return ss;
		}

		public static string SplitToString(this string[] split)
		{
			string ss = string.Empty;

			foreach(var s in split)
				ss += s;

			return ss;
		}

		public static string SplitToString(this string[] split, string s)
		{
			string ss = string.Empty;

			for(int x = 0; x < split.Length; x++)
				ss += s + split[x];

			if(ss.Length > 0 && ss.Substring(0, s.Length) == s)
				ss = ss.Remove(0, s.Length);

			return ss;
		}

		public static string SplitToString(this string[] split, int min, string s)
		{
			string ss = string.Empty;

			for(int x = min; x < split.Length; x++)
				ss += s + split[x];

			if(ss.Length > 0 && ss.Substring(0, s.Length) == s)
				ss = ss.Remove(0, s.Length);

			return ss;
		}

		public static string SplitToString(this char[] split, char c)
		{
			string ss = string.Empty;

			for(int x = 0; x < split.Length; x++)
				ss += c + split[x];

			if(ss.Length > 0 && ss.Substring(0, c.ToString().Length) == c.ToString())
				ss = ss.Remove(0, c.ToString().Length);

			return ss;
		}

		public static string SplitToString(this char[] split, int min, char c)
		{
			string ss = string.Empty;

			for(int x = min; x < split.Length; x++)
				ss += c + split[x];

			if(ss.Length > 0 && ss.Substring(0, c.ToString().Length) == c.ToString())
				ss = ss.Remove(0, c.ToString().Length);

			return ss;
		}

		public static string SplitToString(this char[] split)
		{
			string ss = string.Empty;

			foreach(var s in split)
				ss += s;

			return ss;
		}

		public static string SplitToString(this char[] split, string s)
		{
			string ss = string.Empty;

			for(int x = 0; x < split.Length; x++)
				ss += s + split[x];

			if(ss.Length > 0 && ss.Substring(0, s.Length) == s)
				ss = ss.Remove(0, s.Length);

			return ss;
		}

		public static string SplitToString(this char[] split, int min, string s)
		{
			string ss = string.Empty;

			for(int x = min; x < split.Length; x++)
				ss += s + split[x];

			if(ss.Length > 0 && ss.Substring(0, s.Length) == s)
				ss = ss.Remove(0, s.Length);

			return ss;
		}

		public static string Reverse(this string value)
		{
			return value.Reverse().ToArray().SplitToString();
		}

		public static string Remove(this string s, int min, int max, char value)
		{
			return (s.Length >= max && s.Substring(min, max) == value.ToString()) ? s.Remove(min, max) : s;
		}

		public static string Remove(this string s, int min, int max, string value)
		{
			return (s.Length >= max && s.Substring(min, max) == value) ? s.Remove(min, max) : s;
		}

		public static bool IsUpper(this string value)
		{
			// Consider string to be uppercase if it has no lowercase letters.
			for(int i = 0; i < value.Length; i++)
			{
				if(char.IsLower(value[i]))
					return false;
			}

			return true;
		}

		public static bool IsLower(this string value)
		{
			// Consider string to be lowercase if it has no uppercase letters.
			for(int i = 0; i < value.Length; i++)
			{
				if(char.IsUpper(value[i]))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Waits for the pending tasks in the specified collection.
		/// </summary>
		/// <param name="coll">The collection.</param>
		public static void WaitTasks(this IEnumerable<Task> coll)
		{
			if(coll == null)
				throw new ArgumentNullException("coll");

			Task.WaitAll(coll.ToArray());
		}

		public static bool CompareDataInBlock(this string[] split)
		{
			int i = 0;
			string ss = string.Empty;

			foreach(var s in split)
			{
				if(i == 0)
					ss = s;
				else
				{
					if(ss != s)
						return false;
				}

				i++;
			}

			return true;
		}

		public static bool CompareDataInBlock(this List<string> list)
		{
			int i = 0;
			string ss = string.Empty;

			foreach(var s in list)
			{
				if(i == 0)
					ss = s;
				else
				{
					if(ss != s)
						return false;
				}

				i++;
			}

			return true;
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

		public static bool IsNumber(this string Text)
		{
			double number;
			return double.TryParse(Text, out number);
		}

		public static double ToNumber(this string Text)
		{
			double number;
			return double.TryParse(Text, out number) ? number : 0;
		}

		public static double ToNumber(this string Text, int Else)
		{
			double number;
			return double.TryParse(Text, out number) ? number : Else;
		}

		public static int ToInt(this double Double)
		{
			return Convert.ToInt32(Double);
		}
	}
}