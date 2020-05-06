using System.Collections.Generic;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	internal class TemplateModelBuilder
	{
		private readonly IList<TemplateElement> _elements = new List<TemplateElement>();

		internal void AddTextElement(string text)
		{
			_elements.Add(new TextTemplateElement
			{
				Text = text
			});
		}

		internal void AddOutputValueElement(ValueAccess valueAccess)
		{
			_elements.Add(new OutputValueTemplateElement
			{
				ValueAccess = valueAccess
			});
		}

		internal ConditionTemplateElement AddConditionElement()
		{
			var conditionTemplateElement = new ConditionTemplateElement();
			_elements.Add(conditionTemplateElement);
			return conditionTemplateElement;
		}

		internal TemplateModel GetResult()
		{
			return new TemplateModel(_elements);
		}
	}
}