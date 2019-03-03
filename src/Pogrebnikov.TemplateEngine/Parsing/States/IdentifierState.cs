using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierState : IState
	{
		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
				return new CloseTemplateState();

			throw new ParsingException(this, token);
		}
	}
}