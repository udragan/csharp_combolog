using System;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.Combolog.Common.Interfaces
{
	/// <summary>
	/// Interface for log parsers.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ILogParser : IDisposable
	{
		/// <summary>
		/// Gets the "one line" of log.
		/// </summary>
		/// <returns>Log entry.</returns>
		LogEntryModel GetEntry();

		/// <summary>
		/// Parses the timestamp.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns><see cref=" DateTime"/> representation of provided timestamp.</returns>
		DateTime ParseTimestamp(string timestamp);

		/// <summary>
		/// Checks if there is any entry left in the stream.
		/// </summary>
		/// <returns>True if there are entries left, false otherwise.</returns>
		bool HasNext();
	}
}
