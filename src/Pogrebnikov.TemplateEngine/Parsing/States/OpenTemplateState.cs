﻿using Pogrebnikov.TemplateEngine.LexicalAnalysis;
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
				var valueAccess = new PropertyValueAccess { Name = token.Content };
				return new IdentifierState(valueAccess, _builder);
			}

			throw new ParsingException(this, token);
		}
	}
}