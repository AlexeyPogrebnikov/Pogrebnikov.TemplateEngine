using System.Collections.Generic;
using System.Linq;
using Pogrebnikov.TemplateEngine.LexicalAnalysis;
using Pogrebnikov.TemplateEngine.Parsing.States;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	internal class Parser
	{
		internal TemplateModel Parse(string template)
		{
			var tokenizer = new Tokenizer(template);
			IEnumerable<Token> tokens = tokenizer.Tokenize();

			var builder = new TemplateModelBuilder();
			IState state = new InitialState(builder);

			tokens.Aggregate(state, (current, token) => current.Next(token));

			return builder.GetResult();
		}
	}
}