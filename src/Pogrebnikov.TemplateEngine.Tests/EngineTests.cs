using NUnit.Framework;

namespace Pogrebnikov.TemplateEngine.Tests
{
	[TestFixture]
	public class EngineTests
	{
		[Test]
		public void Eval_simple_text()
		{
			var model = new Model(null);

			string eval = Engine.Eval("456", model);

			Assert.AreEqual("456", eval);
		}

		[Test]
		public void Eval_int_property()
		{
			var source = new
			{
				Count = 10
			};

			var model = new Model(source);

			string eval = Engine.Eval("{{ Count }}", model);

			Assert.AreEqual("10", eval);
		}

		[Test]
		public void Eval_complex_property()
		{
			var source = new
			{
				Language = new
				{
					Name = "C#"
				}
			};

			var model = new Model(source);

			string eval = Engine.Eval("{{ Language.Name }}", model);

			Assert.AreEqual("C#", eval);
		}

		[Test]
		public void Eval_condition_with_property()
		{
			var source = new
			{
				IsValid = true
			};

			var model = new Model(source);

			string eval = Engine.Eval("{{ #if IsValid }}Valid{{ /if }}", model);

			Assert.AreEqual("Valid", eval);
		}
	}
}