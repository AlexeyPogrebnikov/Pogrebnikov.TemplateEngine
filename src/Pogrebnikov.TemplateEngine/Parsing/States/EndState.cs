using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class EndState : IState
	{
		public IState Next(Token token)
		{
			throw new ParsingException(this, token);
		}
	}
}