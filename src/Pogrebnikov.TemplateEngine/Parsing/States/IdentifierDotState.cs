using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierDotState : IState
	{
		private readonly ValueAccess _valueAccess;
		private readonly TemplateModelBuilder _builder;

		internal IdentifierDotState(ValueAccess valueAccess, TemplateModelBuilder builder)
		{
			_valueAccess = valueAccess;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Identifier)
			{
				var endValueAccess = new PropertyValueAccess { Name = token.Content };
				_valueAccess.Append(endValueAccess);
				return new IdentifierDotIdentifierState(_valueAccess, _builder);
			}

			throw new ParsingException(this, token);
		}
	}
}