using System;
using com.udragan.csharp.Combolog.Common.Interfaces;
using com.udragan.csharp.ComboLog.Model.Models;

namespace com.udragan.csharp.ComboLog.Infrastructure
{
	/// <summary>
	/// Wrapper around parser to simplify access.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	internal sealed class Pipe : IDisposable
	{
		#region Members

		private readonly ILogParser _parser;

		private LogEntryModel _nextEntry;
		private bool _disposed;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Pipe"/> class.
		/// </summary>
		/// <param name="parser">The parser.</param>
		public Pipe(ILogParser parser)
		{
			_parser = parser;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Peeks next <see cref="LogEntryModel"/> in the pipe.
		/// </summary>
		/// <returns></returns>
		public LogEntryModel Peek()
		{
			return _nextEntry;
		}

		/// <summary>
		/// Takes next <see cref="LogEntryModel"/> from the pipe.
		/// </summary>
		/// <returns>Next <see cref="LogEntryModel"/> in the pipe.</returns>
		public LogEntryModel Take()
		{
			LogEntryModel result = _nextEntry;
			_nextEntry = _parser.HasNext() ? _parser.GetEntry() : null;

			return result;
		}

		/// <summary>
		/// Determines whether this instance of pipe is drained.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is drained; otherwise, <c>false</c>.
		/// </returns>
		public bool IsDrained()
		{
			return _nextEntry == null;
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
				if (_parser != null)
				{
					_parser.Dispose();
				}
			}

			_disposed = true;
		}

		#endregion
	}
}
