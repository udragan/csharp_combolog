using System;
using System.Collections.Generic;
using System.Linq;
using com.udragan.csharp.Combolog.Common.Interfaces;
using com.udragan.csharp.ComboLog.Infrastructure.Parsers;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"";

			ILogParser parser = new DefaultLogParser(path);

			IList<LogEntryModel> logEntries = new List<LogEntryModel>();

			while (parser.HasNext())
			{
				logEntries.Add(parser.GetEntry());
			}

			(parser as DefaultLogParser).Dispose();

			foreach (var item in logEntries.Where(x => x != null))
			{
				Console.WriteLine(string.Format("timestamp: {0} , value: {1}", item.Timestamp, item.Value));
			}

			Console.ReadLine();
		}
	}
}
