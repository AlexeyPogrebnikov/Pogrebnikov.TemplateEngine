using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierDotIdentifierState : IState
	{
		private readonly ValueAccess _valueAccess;
		private readonly TemplateModelBuilder _builder;

		internal IdentifierDotIdentifierState(ValueAccess valueAccess, TemplateModelBuilder builder)
		{
			_valueAccess = valueAccess;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
			{
				_builder.AddOutputValue(_valueAccess);
				return new CloseTemplateState(_builder);
			}

			if (token.TokenType == TokenType.Dot)
				return new IdentifierDotState(_valueAccess, _builder);

			throw new ParsingException(this, token);
		}
	}
}