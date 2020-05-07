using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class EndIfState : IState
	{
		private readonly TemplateModelBuilder _builder;

		public EndIfState(TemplateModelBuilder builder)
		{
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
			{
				_builder.EndCondition();
				return new CloseTemplateState(_builder);
			}

			throw new ParsingException(this, token);
		}
	}
}