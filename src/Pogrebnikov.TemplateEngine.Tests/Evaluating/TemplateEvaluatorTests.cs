using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Evaluating;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests.Evaluating
{
	[TestFixture]
	public class TemplateEvaluatorTests
	{
		[Test]
		public void GetResult_return_empty_string()
		{
			var model = new Model(null);

			var templateEvaluator = new TemplateEvaluator(model);

			string result = templateEvaluator.GetResult();

			Assert.IsEmpty(result);
		}

		[Test]
		public void EvalText_GetResult_return_text()
		{
			var model = new Model(null);
			var templateEvaluator = new TemplateEvaluator(model);

			var element = new TextTemplateElement("123");

			templateEvaluator.EvalText(element);

			string result = templateEvaluator.GetResult();

			Assert.AreEqual("123", result);
		}

		[Test]
		public void EvalOutputValue_GetResult_return_property_value_as_string()
		{
			var model = new Model(new { Count = 5 });
			var templateEvaluator = new TemplateEvaluator(model);

			var element = new OutputValueTemplateElement
			{
				ValueAccess = new PropertyValueAccess
				{
					Name = "Count"
				}
			};

			templateEvaluator.EvalOutputValue(element);

			string result = templateEvaluator.GetResult();

			Assert.AreEqual("5", result);
		}
	}
}