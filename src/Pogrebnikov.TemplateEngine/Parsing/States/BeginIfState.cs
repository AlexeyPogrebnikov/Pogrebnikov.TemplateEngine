using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class BeginIfState : IState
	{
		private readonly ConditionTemplateElement _conditionTemplateElement;
		private readonly TemplateModelBuilder _builder;

		internal BeginIfState(ConditionTemplateElement conditionTemplateElement, TemplateModelBuilder builder)
		{
			_conditionTemplateElement = conditionTemplateElement;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.Identifier)
			{
				var valueAccess = new PropertyValueAccess { Name = token.Content };
				_conditionTemplateElement.ValueAccess = valueAccess;
				return new BeginIfIdentifierState(valueAccess, _builder);
			}

			throw new ParsingException(this, token);
		}
	}
}