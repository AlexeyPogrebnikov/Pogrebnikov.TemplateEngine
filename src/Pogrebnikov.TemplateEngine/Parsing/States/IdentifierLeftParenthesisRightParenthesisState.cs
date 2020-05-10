using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierLeftParenthesisRightParenthesisState : IState
	{
		private readonly TemplateModelBuilder _builder;

		internal IdentifierLeftParenthesisRightParenthesisState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
				return new CloseTemplateState(_builder);

			throw new ParsingException(this, token);
		}
	}
}