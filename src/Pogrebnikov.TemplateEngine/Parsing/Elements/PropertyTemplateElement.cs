using System;
using Pogrebnikov.TemplateEngine.Evaluating;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal class PropertyTemplateElement : TemplateElement
	{
		internal Property Property { get; set; }

		internal override void Accept(TemplateEvaluator evaluator)
		{
			throw new NotImplementedException();
		}
	}
}