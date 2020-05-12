using System.Collections.Generic;
using System.Linq;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Parsing
{
	internal class TemplateModelBuilder
	{
		private readonly IList<TemplateElement> _elements = new List<TemplateElement>();
		private readonly Stack<BlockTemplateElement> _blocks = new Stack<BlockTemplateElement>();

		internal void AddText(string text)
		{
			var textTemplateElement = new TextTemplateElement(text);

			if (_blocks.Any())
			{
				_blocks.Peek().AddInner(textTemplateElement);
				return;
			}

			_elements.Add(textTemplateElement);
		}

		internal void AddOutputValue(ValueAccess valueAccess)
		{
			_elements.Add(new OutputValueTemplateElement
			{
				ValueAccess = valueAccess
			});
		}

		internal ConditionTemplateElement BeginCondition()
		{
			var conditionTemplateElement = new ConditionTemplateElement();
			_elements.Add(conditionTemplateElement);
			_blocks.Push(conditionTemplateElement);
			return conditionTemplateElement;
		}

		internal void EndCondition()
		{
			_blocks.Pop();
		}

		internal TemplateModel GetResult()
		{
			return new TemplateModel(_elements);
		}

		internal void AddMethod(string name)
		{
			_elements.Add(new MethodCallTemplateElement
			{
				MethodName = name
			});
		}
	}
}