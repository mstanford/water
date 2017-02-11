// ------------------------------------------------------------------------------
// 
// Copyright (c) 2007-2008 Swampware, Inc.
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

namespace Bamboo.Query
{
    public class Index
    {
		private System.Collections.IEnumerable _indexes;
		private System.Collections.Hashtable _dictionary;

		public Index(System.Collections.IEnumerable indexes)
		{
			this._indexes = indexes;
			this._dictionary = new System.Collections.Hashtable();
		}

		public Index(System.Collections.IEnumerable indexes, int capacity)
		{
			this._indexes = indexes;
			this._dictionary = new System.Collections.Hashtable(capacity);
		}

		public System.Collections.IEnumerable Keys
		{
			get { return this._dictionary.Keys; }
		}

		// Returns a list of rows.
        public System.Collections.IList this[string key]
        {
            get
			{
				if (!this._dictionary.ContainsKey(key))
				{
					return null;
				}
				return (System.Collections.IList)this._dictionary[key];
			}
        }

		public void Add(System.Collections.IList row)
        {
			string key = GetHash(row, this._indexes);

			if (this._dictionary.ContainsKey(key))
			{
				System.Collections.IList rows = (System.Collections.IList)this._dictionary[key];
				rows.Add(row);
			}
			else
			{
				System.Collections.IList rows = new System.Collections.ArrayList();
				this._dictionary.Add(key, rows);
				rows.Add(row);
			}
		}

		public void Delete(System.Collections.IList row)
		{
			string key = GetHash(row, this._indexes);

			System.Collections.IList rows = (System.Collections.IList)this._dictionary[key];
			rows.Remove(row);
		}

		public void Close()
		{
			foreach (System.Collections.DictionaryEntry dictionaryEntry in this._dictionary)
			{
				System.Collections.IList rows = (System.Collections.IList)dictionaryEntry.Value;
				rows.Clear();
			}
			this._dictionary.Clear();
		}

		public static string GetHash(System.Collections.IList row, System.Collections.IEnumerable indexes)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			bool first = true;
			foreach (int index in indexes)
			{
				object cell = row[index];

				if (first)
				{
					first = false;
				}
				else
				{
					stringBuilder.Append(",");
				}

				if (cell == null)
				{
				}
				else if (cell is bool)
				{
					stringBuilder.Append(cell.ToString());
				}
				else if (cell is int)
				{
					stringBuilder.Append(cell.ToString());
				}
				else if (cell is double)
				{
					stringBuilder.Append(cell.ToString());
				}
				else if (cell != null && (!cell.Equals("")))
				{
					stringBuilder.Append("\"" + Escape(cell.ToString()) + "\"");
				}
			}

			return stringBuilder.ToString();
		}

		public static string GetHash(System.Collections.IList row, int index)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			object cell = row[index];
			if (cell is bool)
			{
				stringBuilder.Append(cell.ToString());
			}
			else if (cell is int)
			{
				stringBuilder.Append(cell.ToString());
			}
			else if (cell is double)
			{
				stringBuilder.Append(cell.ToString());
			}
			else if (cell != null && (!cell.Equals("")))
			{
				stringBuilder.Append("\"" + Escape(cell.ToString()) + "\"");
			}

			return stringBuilder.ToString();
		}

		private static string Escape(string s)
		{
			return s.Replace("\"", "\"\"");
		}

	}
}
