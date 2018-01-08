using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.Combolog.Common.Interfaces
{
	/// <summary>
	/// Interface for log parsers.
	/// </summary>
	public interface ILogParser
	{
		/// <summary>
		/// Gets the "one line" of log.
		/// </summary>
		/// <returns>Log entry.</returns>
		LogEntryModel GetEntry();
	}
}
