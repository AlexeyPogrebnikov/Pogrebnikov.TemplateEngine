using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class ConditionTemplateElement : TemplateElement
	{
		internal ValueAccess ValueAccess { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			throw new System.NotImplementedException();
		}
	}
}