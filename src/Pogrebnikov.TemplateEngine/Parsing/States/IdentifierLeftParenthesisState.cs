using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierLeftParenthesisState : IState
	{
		private readonly string _identifier;
		private readonly TemplateModelBuilder _builder;

		internal IdentifierLeftParenthesisState(string identifier, TemplateModelBuilder builder)
		{
			_identifier = identifier;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.RightParenthesis)
			{
				_builder.AddMethod(_identifier);
				return new IdentifierLeftParenthesisRightParenthesisState(_builder);
			}

			throw new ParsingException(this, token);
		}
	}
}