using System;
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
		public void Parse_return_TextTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("123");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (TextTemplateElement) elements[0];
			Assert.AreEqual("123", element.Text);
		}

		[Test]
		public void Parse_return_OutputValueTemplateElement_for_property_in_template()
		{
			TemplateModel templateModel = _parser.Parse("{{ Name }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (OutputValueTemplateElement) elements[0];
			ValueAccess valueAccess = element.ValueAccess;
			Assert.AreEqual("Name", valueAccess.Name);
		}

		[Test]
		public void Parse_throw_ParsingException()
		{
			var exception = Assert.Throws<ParsingException>(() => _parser.Parse("{{."));

			string expected = "Unexpected token 'Dot' with content '.' in state 'OpenTemplateState'." + Environment.NewLine +
			                  "Line number: 1, column: 3, position: 3.";
			Assert.AreEqual(expected, exception.Message);
		}

		[Test]
		public void Parse_return_TextTemplateElement_and_OutputValueTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("text{{ Count }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(2, elements.Length);

			var textTemplateElement = (TextTemplateElement) elements[0];
			Assert.AreEqual("text", textTemplateElement.Text);

			var outputValueTemplateElement = (OutputValueTemplateElement) elements[1];
			ValueAccess valueAccess = outputValueTemplateElement.ValueAccess;
			Assert.AreEqual("Count", valueAccess.Name);
			Assert.IsNull(valueAccess.Next);
		}

		[Test]
		public void Parse_return_OutputValueTemplateElement_for_property_dot_property()
		{
			TemplateModel templateModel = _parser.Parse("{{ Model.Name }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (OutputValueTemplateElement) elements[0];

			ValueAccess modelValueAccess = element.ValueAccess;
			Assert.AreEqual("Model", modelValueAccess.Name);

			ValueAccess nameValueAccess = modelValueAccess.Next;
			Assert.AreEqual("Name", nameValueAccess.Name);
			Assert.IsNull(nameValueAccess.Next);
		}

		[Test]
		public void Parse_return_OutputValueTemplateElement_and_TextTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("{{ Param }}Sql");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(2, elements.Length);

			var outputValueTemplateElement = (OutputValueTemplateElement) elements[0];
			Assert.AreEqual("Param", outputValueTemplateElement.ValueAccess.Name);
			Assert.IsNull(outputValueTemplateElement.ValueAccess.Next);

			var templateElement = (TextTemplateElement) elements[1];
			Assert.AreEqual("Sql", templateElement.Text);
		}
	}
}