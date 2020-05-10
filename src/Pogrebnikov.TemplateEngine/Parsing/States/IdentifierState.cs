using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierState : IState
	{
		private readonly string _identifier;
		private readonly TemplateModelBuilder _builder;

		internal IdentifierState(string identifier, TemplateModelBuilder builder)
		{
			_identifier = identifier;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
			{
				_builder.AddOutputValue(new PropertyValueAccess
				{
					Name = _identifier
				});
				return new CloseTemplateState(_builder);
			}

			if (token.TokenType == TokenType.Dot)
			{
				var valueAccess = new PropertyValueAccess
				{
					Name = _identifier
				};

				return new IdentifierDotState(valueAccess, _builder);
			}

			if (token.TokenType == TokenType.LeftParenthesis)
				return new IdentifierLeftParenthesisState(_identifier, _builder);

			throw new ParsingException(this, token);
		}
	}
}