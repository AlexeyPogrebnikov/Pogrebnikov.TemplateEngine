using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class TextState : IState
	{
		private readonly TemplateModelBuilder _builder;

		internal TextState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.End)
				return new EndState();

			if (token.TokenType == TokenType.OpenTemplate)
				return new OpenTemplateState(_builder);

			throw new ParsingException(this, token);
		}
	}
}