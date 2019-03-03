using System;
using System.Runtime.Serialization;
using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.States;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	[Serializable]
	internal class ParsingException : Exception
	{
		internal ParsingException(IState state, Token token) : base($"Unexpected token for state: '{state.GetType().Name}'. TokenType: '{token.TokenType}' Content: '{token.Content}'")
		{
		}

		protected ParsingException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}