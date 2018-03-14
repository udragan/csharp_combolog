using System;
using System.Text;

namespace com.udragan.csharp.ComboLog.Model.Models
{
	/// <summary>
	/// Model representing one log entry.
	/// </summary>
	public class LogEntryModel
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LogEntryModel"/> class.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <param name="value">The value.</param>
		public LogEntryModel(DateTime timestamp, StringBuilder value)
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
		public StringBuilder Value { get; private set; }

		#endregion
	}
}
