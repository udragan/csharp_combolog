using System.Collections.Generic;
using com.udragan.csharp.CommandLineParser.Arguments;
using com.udragan.csharp.CommandLineParser.Attributes;
using com.udragan.csharp.CommandLineParser.Depencencies;

namespace com.udragan.csharp.ComboLog.Infrastructure.CommandLineArguments
{
	internal class CommandLineArguments : GenericArguments
	{
		#region Constructors

		public CommandLineArguments(string[] args)
			: base(args, new ConsoleLogger())
		{ }

		#endregion

		#region Properties

		/// <summary>
		/// Gets the output path.
		/// </summary>
		[Option("-o", "Output path of the combined log file.", DefaultValue = "./merged.txt")]
		public string OutputPath { get; private set; }

		/// <summary>
		/// Gets the list of paths to input logs.
		/// </summary>
		[OptionList("-p", "Path to the input log file.", Required = true)]
		public IList<string> Inputs { get; private set; }

		#endregion

	}
}
