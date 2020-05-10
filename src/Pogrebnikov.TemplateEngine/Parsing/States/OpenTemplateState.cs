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
				return new IdentifierState(token.Content, _builder);
			}

			if (token.TokenType == TokenType.BeginIf)
			{
				ConditionTemplateElement conditionTemplateElement = _builder.BeginCondition();
				return new BeginIfState(conditionTemplateElement, _builder);
			}

			if (token.TokenType == TokenType.EndIf)
				return new EndIfState(_builder);

			throw new ParsingException(this, token);
		}
	}
}