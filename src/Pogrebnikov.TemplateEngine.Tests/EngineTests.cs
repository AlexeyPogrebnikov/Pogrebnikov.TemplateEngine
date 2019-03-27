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
			var obj = new
			{
				Count = 10
			};

			var model = new Model(obj);

			string eval = Engine.Eval("{{ Count }}", model);

			Assert.AreEqual("10", eval);
		}
	}
}