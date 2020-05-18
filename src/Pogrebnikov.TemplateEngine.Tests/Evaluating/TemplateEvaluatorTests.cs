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

		private class ModelWithMethod : Model
		{
			private readonly string _text;

			internal ModelWithMethod(object source, string text) : base(source)
			{
				_text = text;
			}

			public string GetText()
			{
				return _text;
			}
		}

		[Test]
		public void EvalMethod_GetResult_return_result_of_method()
		{
			var model = new ModelWithMethod(null, "123");

			var evaluator = new TemplateEvaluator(model);

			var element = new MethodCallTemplateElement
			{
				MethodName = "GetText"
			};

			evaluator.EvalMethod(element);

			Assert.AreEqual("123", evaluator.GetResult());
		}

		[Test]
		public void EvalMethod_throw_TemplateEvaluateException_if_method_does_not_exist()
		{
			var model = new Model(null);

			var evaluator = new TemplateEvaluator(model);

			var element = new MethodCallTemplateElement
			{
				MethodName = "SomeMethod"
			};

			var exception = Assert.Throws<TemplateEvaluateException>(() => evaluator.EvalMethod(element));

			Assert.AreEqual("Method with name 'SomeMethod' does not exist in model.", exception.Message);
		}
	}
}