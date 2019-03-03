using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class OpenTemplateState : IState
	{
		private readonly TemplateModelBuilder _builder;

		internal OpenTemplateState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Identifier)
			{
				var property = new Property { Name = token.Content };

				_builder.AddPropertyElement(property);
				return new IdentifierState();
			}

			throw new ParsingException(this, token);
		}
	}
}