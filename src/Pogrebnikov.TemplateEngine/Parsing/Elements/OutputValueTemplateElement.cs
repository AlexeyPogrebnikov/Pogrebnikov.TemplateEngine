using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class OutputValueTemplateElement : TemplateElement
	{
		internal ValueAccess ValueAccess { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			evaluator.EvalOutputValue(this);
		}
	}
}