using System.Text;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Evaluating
{
	internal class TemplateEvaluator
	{
		private readonly Model _model;
		private readonly StringBuilder _result = new StringBuilder();

		internal TemplateEvaluator(Model model)
		{
			_model = model;
		}

		internal void EvalText(TextTemplateElement element)
		{
			_result.Append(element.Text);
		}

		internal void EvalOutputValue(OutputValueTemplateElement element)
		{
			_result.Append(_model.GetValue(element.ValueAccess));
		}

		internal void EvalCondition(ConditionTemplateElement element)
		{
			var condition = (bool) _model.GetValue(element.ValueAccess);
			if (condition)
			{
				foreach (TemplateElement inner in element.Inner)
					inner.Accept(this);
			}
		}

		internal string GetResult()
		{
			return _result.ToString();
		}
	}
}