using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests
{
	[TestFixture]
	public class ModelTests
	{
		[Test]
		public void GetValue_return_value()
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

		[Test]
		public void GetValue_return_value_if_Next_not_null()
		{
			var obj = new
			{
				Language = new
				{
					Name = "C++"
				}
			};

			var model = new Model(obj);

			var valueAccess = new PropertyValueAccess
			{
				Name = "Language",
				Next = new PropertyValueAccess
				{
					Name = "Name",
					Next = null
				}
			};

			object value = model.GetValue(valueAccess);

			Assert.AreEqual("C++", value);
		}
	}
}