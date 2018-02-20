﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using com.udragan.csharp.Combolog.Common.Interfaces;
using com.udragan.csharp.ComboLog.Infrastructure;
using com.udragan.csharp.ComboLog.Infrastructure.CommandLineArguments;
using com.udragan.csharp.ComboLog.Infrastructure.Parsers;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog
{
	class Program
	{
		static void Main(string[] args)
		{
			CommandLineArguments arguments = new CommandLineArguments(args);

			if (!arguments.IsValid)
			{
				Console.ReadLine();

				return;
			}

			IList<LogEntryModel> logEntries = new List<LogEntryModel>();
			List<Pipe> pipes = new List<Pipe>(4);

			foreach (string inputPath in arguments.Inputs)
			{
				ILogParser parser = new DefaultLogParser(inputPath);

				pipes.Add(new Pipe(parser));
			}

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

			using (StreamWriter writer = new StreamWriter(arguments.OutputPath))
			{
				foreach (var item in logEntries.Where(x => x != null))
				{
					writer.WriteLine(item.Value);
				}
			}

			//foreach (var item in logEntries.Where(x => x != null))
			//{
			//	Console.WriteLine(string.Format("timestamp: {0} , value: {1}", item.Timestamp, item.Value));
			//}

			Console.ReadLine();
		}



	}
}
