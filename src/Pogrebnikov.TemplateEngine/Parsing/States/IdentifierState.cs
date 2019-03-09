﻿using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal class IdentifierState : IState
	{
		private readonly ValueAccess _beginValueAccess;
		private readonly TemplateModelBuilder _builder;

		public IdentifierState(ValueAccess beginValueAccess, TemplateModelBuilder builder)
		{
			_beginValueAccess = beginValueAccess;
			_builder = builder;
		}

		public IState Next(Token token)
		{
			if (token.TokenType == TokenType.CloseTemplate)
			{
				_builder.AddOutputValueElement(_beginValueAccess);
				return new CloseTemplateState(_builder);
			}

			if (token.TokenType == TokenType.Dot)
				return new IdentifierDotState(_beginValueAccess, _builder);

			throw new ParsingException(this, token);
		}
	}
}