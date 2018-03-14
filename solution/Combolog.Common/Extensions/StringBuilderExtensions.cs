using System.Text;

namespace com.udragan.csharp.Combolog.Common.Extensions
{
	/// <summary>
	/// Extensions for <see cref="StringBuilder"/> class.
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Appends the text if not null or empty.
		/// </summary>
		/// <param name="value">The <see cref="StringBuilder" /> object to append the text to.</param>
		/// <param name="text">The text.</param>
		public static void AppendLineIfNotNullOrEmpty(this StringBuilder value, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}

			value.Append(text);
		}
	}
}
