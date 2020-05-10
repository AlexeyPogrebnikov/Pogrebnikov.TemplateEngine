using System;
using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class MethodCallTemplateElement : TemplateElement
	{
		internal string MethodName { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			throw new NotImplementedException();
		}
	}
}