using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal abstract class TemplateElement
	{
		internal abstract void Accept(TemplateEvaluator evaluator);
	}
}