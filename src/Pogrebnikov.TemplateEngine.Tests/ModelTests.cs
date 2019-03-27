using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests
{
	[TestFixture]
	public class ModelTests
	{
		[Test]
		public void Constructor_GetValue_return_value()
		{
			var obj = new
			{
				Count = 10
			};

			var model = new Model(obj);

			var valueAccess = new PropertyValueAccess { Name = "Count" };

			object value = model.GetValue(valueAccess);

			Assert.AreEqual(10, value);
		}
	}
}