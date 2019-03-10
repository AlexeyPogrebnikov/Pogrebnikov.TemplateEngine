using Pogrebnikov.TemplateEngine.Evaluating;
using Pogrebnikov.TemplateEngine.Parsing;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine
{
	public static class Engine
	{
		public static string Eval(string template, Model model)
		{
			var parser = new Parser();

			TemplateModel templateModel = parser.Parse(template);

			var evaluator = new TemplateEvaluator();

			foreach (TemplateElement element in templateModel.Elements)
				element.Accept(evaluator);

			return evaluator.GetResult();
		}
	}
}