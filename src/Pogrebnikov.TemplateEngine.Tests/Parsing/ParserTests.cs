using System.Linq;
using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Parsing;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests.Parsing
{
	[TestFixture]
	public class ParserTests
	{
		private Parser _parser;

		[SetUp]
		public void SetUp()
		{
			_parser = new Parser();
		}

		[Test]
		public void Parse_returns_TextTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("123");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (TextTemplateElement) elements[0];
			Assert.AreEqual("123", element.Text);
		}

		[Test]
		public void Parse_returns_CallGraphTemplateElement_for_property_in_template()
		{
			TemplateModel templateModel = _parser.Parse("{{ Name }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (PropertyTemplateElement) elements[0];
			Property property = element.Property;
			Assert.AreEqual("Name", property.Name);
		}
	}
}