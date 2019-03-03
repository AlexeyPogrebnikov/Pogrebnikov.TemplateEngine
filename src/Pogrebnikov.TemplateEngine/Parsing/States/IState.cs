using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Parsing.States
{
	internal interface IState
	{
		IState Next(Token token);
	}
}