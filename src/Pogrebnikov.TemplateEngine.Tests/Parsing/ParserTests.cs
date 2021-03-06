﻿using System;
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

		[Test]
		public void Parse_return_ConditionTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("{{ #if Flag }}{{ /if }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);

			var conditionTemplateElement = (ConditionTemplateElement) elements[0];

			Assert.AreEqual("Flag", conditionTemplateElement.ValueAccess.Name);
			Assert.IsNull(conditionTemplateElement.ValueAccess.Next);
		}

		[Test]
		public void Parse_return_ConditionTemplateElement_for_property_dot_property()
		{
			TemplateModel templateModel = _parser.Parse("{{ #if Model.Flag }}{{ /if }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);

			var conditionTemplateElement = (ConditionTemplateElement) elements[0];

			ValueAccess modelValueAccess = conditionTemplateElement.ValueAccess;
			Assert.AreEqual("Model", modelValueAccess.Name);

			ValueAccess flagValueAccessNext = modelValueAccess.Next;
			Assert.AreEqual("Flag", flagValueAccessNext.Name);
			Assert.IsNull(flagValueAccessNext.Next);
		}

		[Test]
		public void Parse_return_ConditionTemplateElement_with_inner()
		{
			TemplateModel templateModel = _parser.Parse("{{ #if IsValid }}Valid{{ /if }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);

			var conditionTemplateElement = (ConditionTemplateElement) elements[0];

			Assert.AreEqual("IsValid", conditionTemplateElement.ValueAccess.Name);
			Assert.IsNull(conditionTemplateElement.ValueAccess.Next);

			TemplateElement[] inner = conditionTemplateElement.Inner.ToArray();
			Assert.AreEqual(1, inner.Length);
			Assert.IsInstanceOf<TextTemplateElement>(inner[0]);
			var textElement = (TextTemplateElement) inner[0];
			Assert.AreEqual("Valid", textElement.Text);
		}

		[Test]
		public void Parse_return_ConditionTemplateElement_and_TextTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("{{ #if IsValid }}{{ /if }}Some text");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(2, elements.Length);

			var conditionTemplateElement = (ConditionTemplateElement) elements[0];

			Assert.AreEqual("IsValid", conditionTemplateElement.ValueAccess.Name);
			Assert.IsNull(conditionTemplateElement.ValueAccess.Next);

			TemplateElement[] inner = conditionTemplateElement.Inner.ToArray();
			Assert.AreEqual(0, inner.Length);

			var textTemplateElement = (TextTemplateElement) elements[1];
			Assert.AreEqual("Some text", textTemplateElement.Text);
		}

		[Test]
		public void Parse_return_MethodCallTemplateElement()
		{
			TemplateModel templateModel = _parser.Parse("{{ Method() }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);

			var methodCallTemplateElement = (MethodCallTemplateElement) elements[0];

			Assert.AreEqual("Method", methodCallTemplateElement.MethodName);
		}

		[Test]
		public void Parse_return_OutputValueTemplateElement_for_property_dot_property_dot_property()
		{
			TemplateModel templateModel = _parser.Parse("{{ First.Second.Third }}");

			TemplateElement[] elements = templateModel.Elements.ToArray();
			Assert.AreEqual(1, elements.Length);
			var element = (OutputValueTemplateElement) elements[0];

			ValueAccess firstValueAccess = element.ValueAccess;
			Assert.AreEqual("First", firstValueAccess.Name);

			ValueAccess secondValueAccess = firstValueAccess.Next;
			Assert.AreEqual("Second", secondValueAccess.Name);

			ValueAccess thirdValueAccess = secondValueAccess.Next;
			Assert.AreEqual("Third", thirdValueAccess.Name);

			Assert.IsNull(thirdValueAccess.Next);
		}
	}
}