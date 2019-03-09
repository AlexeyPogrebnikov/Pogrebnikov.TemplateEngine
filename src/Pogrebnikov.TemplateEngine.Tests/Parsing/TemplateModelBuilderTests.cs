using System.Linq;
using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Parsing;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests.Parsing
{
	[TestFixture]
	public class TemplateModelBuilderTests
	{
		private TemplateModelBuilder _builder;

		[SetUp]
		public void SetUp()
		{
			_builder = new TemplateModelBuilder();
		}

		[Test]
		public void AddTextElement_GetResult_templateModel_constains_TextTemplateElement()
		{
			_builder.AddTextElement("123");

			TemplateModel templateModel = _builder.GetResult();

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (TextTemplateElement) elements[0];
			Assert.AreEqual("123", element.Text);
		}

		[Test]
		public void AddOutputValueElement_GetResult_templateModel_contains_PropertyTemplateElement()
		{
			var property = new PropertyValueAccess { Name = "Count" };

			_builder.AddOutputValueElement(property);

			TemplateModel templateModel = _builder.GetResult();

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (OutputValueTemplateElement) elements[0];
			Assert.AreEqual("Count", element.ValueAccess.Name);
		}
	}
}