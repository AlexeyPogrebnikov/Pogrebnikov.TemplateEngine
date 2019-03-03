using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class CloseTemplateState : IState
	{
		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.End)
				return new EndState();

			throw new ParsingException(this, token);
		}
	}
}