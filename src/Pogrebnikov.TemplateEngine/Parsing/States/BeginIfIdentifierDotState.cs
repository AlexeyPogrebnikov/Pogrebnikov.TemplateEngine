using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class BeginIfIdentifierDotState : IState
	{
		private readonly ValueAccess _valueAccess;
		private readonly TemplateModelBuilder _builder;

		public BeginIfIdentifierDotState(ValueAccess valueAccess, TemplateModelBuilder builder)
		{
			_valueAccess = valueAccess;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Identifier)
			{
				_valueAccess.Append(new PropertyValueAccess
				{
					Name = token.Content
				});

				return new BeginIfIdentifierState(_valueAccess, _builder);
			}

			throw new ParsingException(this, token);
		}
	}
}