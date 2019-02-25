using System;
using System.Runtime.Serialization;

namespace Pogrebnikov.TemplateEngine.LexicalAnalysis
{
	[Serializable]
	public class TokenizationException : Exception
	{
		public TokenizationException(string message) : base(message)
		{
		}

		protected TokenizationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}