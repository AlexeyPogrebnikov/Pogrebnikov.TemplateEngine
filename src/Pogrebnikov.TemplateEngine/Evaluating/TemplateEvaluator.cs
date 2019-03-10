using System.Text;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Evaluating
{
	internal class TemplateEvaluator
	{
		private readonly StringBuilder _result = new StringBuilder();

		internal void EvalText(TextTemplateElement element)
		{
			_result.Append(element.Text);
		}

		internal string GetResult()
		{
			return _result.ToString();
		}
	}
}