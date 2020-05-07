using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class ConditionTemplateElement : BlockTemplateElement
	{
		internal ValueAccess ValueAccess { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			evaluator.EvalCondition(this);
		}
	}
}