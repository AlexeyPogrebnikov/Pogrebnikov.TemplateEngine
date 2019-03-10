using NUnit.Framework;

namespace Pogrebnikov.TemplateEngine.Tests
{
	[TestFixture]
	public class EngineTests
	{
		[Test]
		public void Eval_simple_text()
		{
			string eval = Engine.Eval("456", new Model(new PropertyBag()));

			Assert.AreEqual("456", eval);
		}
	}
}