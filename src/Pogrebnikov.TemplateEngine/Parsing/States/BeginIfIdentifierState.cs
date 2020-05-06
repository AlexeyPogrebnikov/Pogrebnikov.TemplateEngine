using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class BeginIfIdentifierState : IState
	{
		private readonly ValueAccess _beginValueAccess;
		private readonly TemplateModelBuilder _builder;

		public BeginIfIdentifierState(ValueAccess beginValueAccess, TemplateModelBuilder builder)
		{
			_beginValueAccess = beginValueAccess;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
				return new CloseTemplateState(_builder);

			if (token.TokenType == TokenType.Dot)
				return new BeginIfIdentifierDotState(_beginValueAccess, _builder);

			throw new ParsingException(this, token);
		}
	}
}