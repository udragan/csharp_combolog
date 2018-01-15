using System;
using System.Collections.Generic;
using System.Linq;
using com.udragan.csharp.Combolog.Common.Interfaces;
using com.udragan.csharp.ComboLog.Infrastructure;
using com.udragan.csharp.ComboLog.Infrastructure.Parsers;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog
{
	class Program
	{
		static void Main(string[] args)
		{
			string path1 = @"";
			string path2 = @"";

			ILogParser parser1 = new DefaultLogParser(path1);
			ILogParser parser2 = new DefaultLogParser(path2);

			IList<LogEntryModel> logEntries = new List<LogEntryModel>();

			List<Pipe> pipes = new List<Pipe>(4);

			pipes.Add(new Pipe(parser1));
			pipes.Add(new Pipe(parser2));

			pipes.ForEach(x => x.Take());

			while (pipes.Count > 0)
			{
				Pipe nextPipe = pipes
					.Aggregate((current, next) =>
						DateTime.Compare(current.Peek().Timestamp, next.Peek().Timestamp) > 0 ? next : current);

				LogEntryModel logEntry = nextPipe.Take();

				logEntries.Add(logEntry);

				if (nextPipe.IsDrained())
				{
					pipes.Remove(nextPipe);
				}
			}

			foreach (var item in logEntries.Where(x => x != null))
			{
				Console.WriteLine(string.Format("timestamp: {0} , value: {1}", item.Timestamp, item.Value));
			}

			Console.ReadLine();
		}



	}
}
