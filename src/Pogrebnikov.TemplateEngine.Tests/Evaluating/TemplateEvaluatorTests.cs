using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Evaluating;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests.Evaluating
{
	[TestFixture]
	public class TemplateEvaluatorTests
	{
		private TemplateEvaluator _templateEvaluator;

		[SetUp]
		public void SetUp()
		{
			_templateEvaluator = new TemplateEvaluator();
		}

		[Test]
		public void GetResult_return_empty_string()
		{
			string result = _templateEvaluator.GetResult();

			Assert.IsEmpty(result);
		}

		[Test]
		public void EvalText_GetResult_return_text()
		{
			var element = new TextTemplateElement { Text = "123" };

			_templateEvaluator.EvalText(element);
			string result = _templateEvaluator.GetResult();

			Assert.AreEqual("123", result);
		}
	}
}