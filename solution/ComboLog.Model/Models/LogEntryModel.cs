using System;

namespace com.udragan.csharp.ComboLog.Model.Models
{
	/// <summary>
	/// Model representing one log entry.
	/// </summary>
	public class LogEntryModel
	{
		#region Constructors

		public LogEntryModel(DateTime timestamp, string value)
		{
			Timestamp = timestamp;
			Value = value;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the timestamp of log entry.
		/// </summary>
		public DateTime Timestamp { get; private set; }

		/// <summary>
		/// Gets the value of log entry.
		/// </summary>
		public string Value { get; private set; }

		#endregion
	}
}
