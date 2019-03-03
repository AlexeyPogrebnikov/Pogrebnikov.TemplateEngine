using System.Collections.Generic;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	internal class TemplateModel
	{
		internal TemplateModel(IEnumerable<TemplateElement> elements)
		{
			Elements = elements;
		}

		internal IEnumerable<TemplateElement> Elements { get; }
	}
}