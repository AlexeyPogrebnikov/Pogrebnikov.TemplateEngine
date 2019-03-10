using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class TextTemplateElement : TemplateElement
	{
		internal string Text { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			evaluator.EvalText(this);
		}
	}
}