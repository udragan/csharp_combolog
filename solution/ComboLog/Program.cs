using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"";
			string dateRegex = @"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})?(.\d*)"; //TODO: should be part of parser
			Regex regex = new Regex(dateRegex, RegexOptions.ExplicitCapture | RegexOptions.Compiled); //TODO: should be part of parser

			IList<LogEntryModel> logEntries = new List<LogEntryModel>();
			LogEntryModel logEntry = new LogEntryModel(DateTime.MinValue, new StringBuilder());

			using (StreamReader stream = new StreamReader(path))
			{
				if (stream.BaseStream.Length > 0)
				{
					do
					{
						string line = stream.ReadLine();
						Match match = regex.Match(line);

						if (match.Success)
						{
							string value = match.Value;

							logEntry = new LogEntryModel(ParseTimestamp(value), new StringBuilder(line.Substring(value.Length)));

							logEntries.Add(logEntry);

						}
						else
						{
							logEntry.Value.AppendFormat("{0}{1}", Environment.NewLine, line);
						}
					}
					while (!stream.EndOfStream);
				}
				else
				{
					Console.WriteLine("log file is empty!");
				}
			}

			foreach (var item in logEntries.Where(x => x != null))
			{
				Console.WriteLine(string.Format("timestamp: {0} , value: {1}", item.Timestamp, item.Value));
			}


			Console.ReadLine();
		}

		//TODO: should be part of parser
		private static DateTime ParseTimestamp(string timestampString)
		{
			DateTime result = new DateTime();

			result = Convert.ToDateTime(timestampString);


			return result;
		}
	}
}
