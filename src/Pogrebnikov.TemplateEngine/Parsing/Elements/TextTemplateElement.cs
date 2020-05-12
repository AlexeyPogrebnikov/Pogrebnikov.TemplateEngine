using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class TextTemplateElement : TemplateElement
	{
		public TextTemplateElement(string text)
		{
			Text = text;
		}

		internal string Text { get; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			evaluator.EvalText(this);
		}
	}
}