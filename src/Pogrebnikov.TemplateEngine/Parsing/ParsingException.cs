using System;
using System.Runtime.Serialization;
using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.States;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	[Serializable]
	internal class ParsingException : Exception
	{
		internal ParsingException(IState state, Token token) : base(ConstructMessage(state, token))
		{
		}

		protected ParsingException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}

		private static string ConstructMessage(IState state, Token token)
		{
			string content = token.Content;
			if (content != null && content.Length > 100)
				content = token.Content.Substring(0, 100) + "...";

			return $"Unexpected token '{token.TokenType}' with content '{content}' in state '{state.GetType().Name}'." + Environment.NewLine +
			       "Line number: 1, column: 3, position: 3.";
		}
	}
}