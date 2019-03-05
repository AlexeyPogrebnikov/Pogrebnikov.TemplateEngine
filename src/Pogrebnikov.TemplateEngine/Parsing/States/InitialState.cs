using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class InitialState : IState
	{
		private readonly TemplateModelBuilder _builder;

		internal InitialState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Text)
			{
				_builder.AddTextElement(token.Content);
				return new TextState(_builder);
			}

			if (token.TokenType == TokenType.OpenTemplate)
				return new OpenTemplateState(_builder);

			throw new ParsingException(this, token);
		}
	}
}