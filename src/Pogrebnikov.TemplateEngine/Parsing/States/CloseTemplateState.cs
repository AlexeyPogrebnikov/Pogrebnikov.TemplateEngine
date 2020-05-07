using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class CloseTemplateState : IState
	{
		private readonly TemplateModelBuilder _builder;

		public CloseTemplateState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Text)
			{
				_builder.AddText(token.Content);
				return new TextState(_builder);
			}

			if (token.TokenType == TokenType.OpenTemplate)
				return new OpenTemplateState(_builder);

			if (token.TokenType == TokenType.End)
				return new EndState();

			throw new ParsingException(this, token);
		}
	}
}