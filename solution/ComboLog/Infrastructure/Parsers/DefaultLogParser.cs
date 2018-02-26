using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using com.udragan.csharp.Combolog.Common.Extensions;
using com.udragan.csharp.Combolog.Common.Interfaces;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog.Infrastructure.Parsers
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="com.udragan.csharp.Combolog.Common.Interfaces.ILogParser" />
	/// <seealso cref="System.IDisposable" />
	public class DefaultLogParser : ILogParser, IDisposable
	{
		#region Members

		private readonly string dateRegex = @"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})?(.\d*)"; //TODO: expose in interface
		private readonly Regex _regex;
		private readonly StreamReader _stream;
		private readonly string _logName;

		private string _nextLine;
		private bool _disposed;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultLogParser"/> class.
		/// </summary>
		/// <param name="path">The path to log file.</param>
		public DefaultLogParser(string path)
		{
			_stream = File.Exists(path) ?
				new StreamReader(path) :
				StreamReader.Null;

			_logName = Path.GetFileNameWithoutExtension(path);
			_regex = new Regex(dateRegex, RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			_nextLine = string.Empty;
		}

		#endregion

		#region ILogParser Members

		/// <summary>
		/// Gets the "one line" of log.
		/// </summary>
		/// <returns>
		/// Log entry.
		/// </returns>
		public LogEntryModel GetEntry()
		{
			LogEntryModel result = new LogEntryModel(DateTime.MinValue, new StringBuilder(_logName));
			Match match = _regex.Match(_nextLine);

			if (CheckMatch(match, _nextLine))
			{
				string value = match.Value;
				result = new LogEntryModel(ParseTimestamp(value), new StringBuilder(_nextLine));
				result.Value.AppendLineIfNotNullOrEmpty(GetBlock());
			}
			else
			{
				_nextLine = _stream.ReadLine();
			}

			return result;
		}

		/// <summary>
		/// Parses the timestamp.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns>
		///   <see cref=" DateTime" /> representation of provided timestamp.
		/// </returns>
		[Pure]
		public DateTime ParseTimestamp(string timestamp)
		{
			DateTime result = new DateTime();

			result = Convert.ToDateTime(timestamp);

			return result;
		}

		/// <summary>
		/// Checks if there is any entry left in the stream.
		/// </summary>
		/// <returns>
		/// True if there are entries left, false otherwise.
		/// </returns>
		[Pure]
		public bool HasNext()
		{
			return _nextLine != null;
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				if (_stream != null)
				{
					_stream.Dispose();
				}
			}

			_disposed = true;
		}

		#endregion

		#region Private methods

		private bool CheckMatch(Match match, string logLine)
		{
			return match.Success && logLine.StartsWith(match.Value);
		}

		private string GetBlock()
		{
			if (_stream.EndOfStream)
			{
				_nextLine = null;
				return string.Empty;
			}

			StringBuilder stringBuilder = new StringBuilder();
			Match match;

			while (!_stream.EndOfStream)
			{
				_nextLine = _stream.ReadLine();

				match = _regex.Match(_nextLine);

				if (CheckMatch(match, _nextLine))
				{
					break;
				}
				else
				{
					stringBuilder.AppendFormat("{0}{1}", Environment.NewLine, _nextLine);
				}
			}

			return stringBuilder.ToString();
		}

		#endregion
	}
}
