using System.Collections.Generic;

namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal abstract class BlockTemplateElement : TemplateElement
	{
		private readonly IList<TemplateElement> _inner = new List<TemplateElement>();

		internal IEnumerable<TemplateElement> Inner => _inner;

		internal void AddInner(TemplateElement inner)
		{
			_inner.Add(inner);
		}
	}
}