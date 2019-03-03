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

		internal void AddPropertyElement(Property property)
		{
			_elements.Add(new PropertyTemplateElement
			{
				Property = property
			});
		}

		internal TemplateModel GetResult()
		{
			return new TemplateModel(_elements);
		}
	}
}