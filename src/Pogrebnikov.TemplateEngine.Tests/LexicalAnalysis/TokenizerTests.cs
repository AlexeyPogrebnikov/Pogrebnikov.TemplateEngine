using System.Linq;
using NUnit.Framework;
using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Tests.LexicalAnalysis
{
	[TestFixture]
	public class TokenizerTests
	{
		[Test]
		public void Tokenize_return_End_token_if_template_is_empty()
		{
			var tokenizer = new Tokenizer("");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(1, tokens.Length);

			Token token = tokens[0];
			Assert.AreEqual(TokenType.End, token.TokenType);
			Assert.AreEqual(string.Empty, token.Content);
			Assert.AreEqual(1, token.LineNumber);
			Assert.AreEqual(1, token.ColumnNumber);
			Assert.AreEqual(1, token.Position);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens()
		{
			var tokenizer = new Tokenizer("<b>");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("<b>", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("{{");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);
		}

		[Test]
		public void Tokenize_return_Text_OpenTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("z{{");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("z", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[1].TokenType);
			Assert.AreEqual("{{", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(2, tokens[1].ColumnNumber);
			Assert.AreEqual(2, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(4, tokens[2].ColumnNumber);
			Assert.AreEqual(4, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_End_tokens()
		{
			var tokenizer = new Tokenizer("{{zoo");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("zoo", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(6, tokens[2].ColumnNumber);
			Assert.AreEqual(6, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_End_tokens_without_whitespace()
		{
			var tokenizer = new Tokenizer("{{ Model ");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(10, tokens[2].ColumnNumber);
			Assert.AreEqual(10, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_Dot_End_tokens()
		{
			var tokenizer = new Tokenizer("{{Model.");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(4, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.Dot, tokens[2].TokenType);
			Assert.AreEqual(".", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(8, tokens[2].ColumnNumber);
			Assert.AreEqual(8, tokens[2].Position);

			Assert.AreEqual(TokenType.End, tokens[3].TokenType);
			Assert.AreEqual(string.Empty, tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(9, tokens[3].ColumnNumber);
			Assert.AreEqual(9, tokens[3].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_Dot_Identifier_End_tokens()
		{
			var tokenizer = new Tokenizer("{{Model.Name");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.Dot, tokens[2].TokenType);
			Assert.AreEqual(".", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(8, tokens[2].ColumnNumber);
			Assert.AreEqual(8, tokens[2].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[3].TokenType);
			Assert.AreEqual("Name", tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(9, tokens[3].ColumnNumber);
			Assert.AreEqual(9, tokens[3].Position);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
			Assert.AreEqual(1, tokens[4].LineNumber);
			Assert.AreEqual(13, tokens[4].ColumnNumber);
			Assert.AreEqual(13, tokens[4].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_LeftBracket_End_tokens()
		{
			var tokenizer = new Tokenizer("{{[");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.LeftBracket, tokens[1].TokenType);
			Assert.AreEqual("[", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(4, tokens[2].ColumnNumber);
			Assert.AreEqual(4, tokens[2].Position);
		}

		[Test]
		public void Tokenize_throw_exception_if_template_contains_unknown_symbol()
		{
			var tokenizer = new Tokenizer("{{*");

			var exception = Assert.Throws<TokenizationException>(() => tokenizer.Tokenize());

			Assert.AreEqual("Can't tokenize '*' at position 3.", exception.Message);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("{{}}");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_RightBracket_End_tokens()
		{
			var tokenizer = new Tokenizer("{{]");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.RightBracket, tokens[1].TokenType);
			Assert.AreEqual("]", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(4, tokens[2].ColumnNumber);
			Assert.AreEqual(4, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_with_number_End_tokens()
		{
			var tokenizer = new Tokenizer("{{count1");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("count1", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(9, tokens[2].ColumnNumber);
			Assert.AreEqual(9, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginLoop_End_tokens()
		{
			var tokenizer = new Tokenizer("{{ #loop");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.BeginLoop, tokens[1].TokenType);
			Assert.AreEqual("#loop", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(9, tokens[2].ColumnNumber);
			Assert.AreEqual(9, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Comma_End_tokens()
		{
			var tokenizer = new Tokenizer("{{,");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.Comma, tokens[1].TokenType);
			Assert.AreEqual(",", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(4, tokens[2].ColumnNumber);
			Assert.AreEqual(4, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_EndLoop_End_tokens()
		{
			var tokenizer = new Tokenizer("{{ /loop");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.EndLoop, tokens[1].TokenType);
			Assert.AreEqual("/loop", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(9, tokens[2].ColumnNumber);
			Assert.AreEqual(9, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginLoop_Identifier_Comma_Identifier_CloseTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("{{ #loop Names, i }}");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(7, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.BeginLoop, tokens[1].TokenType);
			Assert.AreEqual("#loop", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[2].TokenType);
			Assert.AreEqual("Names", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(10, tokens[2].ColumnNumber);
			Assert.AreEqual(10, tokens[2].Position);

			Assert.AreEqual(TokenType.Comma, tokens[3].TokenType);
			Assert.AreEqual(",", tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(15, tokens[3].ColumnNumber);
			Assert.AreEqual(15, tokens[3].Position);

			Assert.AreEqual(TokenType.Identifier, tokens[4].TokenType);
			Assert.AreEqual("i", tokens[4].Content);
			Assert.AreEqual(1, tokens[4].LineNumber);
			Assert.AreEqual(17, tokens[4].ColumnNumber);
			Assert.AreEqual(17, tokens[4].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[5].TokenType);
			Assert.AreEqual("}}", tokens[5].Content);
			Assert.AreEqual(1, tokens[5].LineNumber);
			Assert.AreEqual(19, tokens[5].ColumnNumber);
			Assert.AreEqual(19, tokens[5].Position);

			Assert.AreEqual(TokenType.End, tokens[6].TokenType);
			Assert.AreEqual(string.Empty, tokens[6].Content);
			Assert.AreEqual(1, tokens[6].LineNumber);
			Assert.AreEqual(21, tokens[6].ColumnNumber);
			Assert.AreEqual(21, tokens[6].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_LeftParenthesis_End_tokens()
		{
			var tokenizer = new Tokenizer("{{ (");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.LeftParenthesis, tokens[1].TokenType);
			Assert.AreEqual("(", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_RightParenthesis_End_tokens()
		{
			var tokenizer = new Tokenizer("{{ )");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.RightParenthesis, tokens[1].TokenType);
			Assert.AreEqual(")", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginIf_End_tokens()
		{
			var tokenizer = new Tokenizer("{{#if");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.BeginIf, tokens[1].TokenType);
			Assert.AreEqual("#if", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(6, tokens[2].ColumnNumber);
			Assert.AreEqual(6, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_EndIf_End_tokens()
		{
			var tokenizer = new Tokenizer("{{/if");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.EndIf, tokens[1].TokenType);
			Assert.AreEqual("/if", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(6, tokens[2].ColumnNumber);
			Assert.AreEqual(6, tokens[2].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_Text_tokens()
		{
			var tokenizer = new Tokenizer("{{}}q");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(4, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.Text, tokens[2].TokenType);
			Assert.AreEqual("q", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);

			Assert.AreEqual(TokenType.End, tokens[3].TokenType);
			Assert.AreEqual(string.Empty, tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(6, tokens[3].ColumnNumber);
			Assert.AreEqual(6, tokens[3].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_OpenTemplate_CloseTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("{{}}{{}}");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[2].TokenType);
			Assert.AreEqual("{{", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[3].TokenType);
			Assert.AreEqual("}}", tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(7, tokens[3].ColumnNumber);
			Assert.AreEqual(7, tokens[3].Position);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
			Assert.AreEqual(1, tokens[4].LineNumber);
			Assert.AreEqual(9, tokens[4].ColumnNumber);
			Assert.AreEqual(9, tokens[4].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_Text_OpenTemplate_CloseTemplate_End_tokens()
		{
			var tokenizer = new Tokenizer("{{}}i{{}}");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(6, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);

			Assert.AreEqual(TokenType.Text, tokens[2].TokenType);
			Assert.AreEqual("i", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(5, tokens[2].ColumnNumber);
			Assert.AreEqual(5, tokens[2].Position);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[3].TokenType);
			Assert.AreEqual("{{", tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(6, tokens[3].ColumnNumber);
			Assert.AreEqual(6, tokens[3].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[4].TokenType);
			Assert.AreEqual("}}", tokens[4].Content);
			Assert.AreEqual(1, tokens[4].LineNumber);
			Assert.AreEqual(8, tokens[4].ColumnNumber);
			Assert.AreEqual(8, tokens[4].Position);

			Assert.AreEqual(TokenType.End, tokens[5].TokenType);
			Assert.AreEqual(string.Empty, tokens[5].Content);
			Assert.AreEqual(1, tokens[5].LineNumber);
			Assert.AreEqual(10, tokens[5].ColumnNumber);
			Assert.AreEqual(10, tokens[5].Position);
		}

		[Test]
		public void Tokenize_return_Text_OpenTemplate_CloseTemplate_Text_End_tokens()
		{
			var tokenizer = new Tokenizer("foo{{}}zoo");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("foo", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[1].TokenType);
			Assert.AreEqual("{{", tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(4, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[2].TokenType);
			Assert.AreEqual("}}", tokens[2].Content);
			Assert.AreEqual(1, tokens[2].LineNumber);
			Assert.AreEqual(6, tokens[2].ColumnNumber);
			Assert.AreEqual(6, tokens[2].Position);

			Assert.AreEqual(TokenType.Text, tokens[3].TokenType);
			Assert.AreEqual("zoo", tokens[3].Content);
			Assert.AreEqual(1, tokens[3].LineNumber);
			Assert.AreEqual(8, tokens[3].ColumnNumber);
			Assert.AreEqual(8, tokens[3].Position);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
			Assert.AreEqual(1, tokens[4].LineNumber);
			Assert.AreEqual(11, tokens[4].ColumnNumber);
			Assert.AreEqual(11, tokens[4].Position);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens_for_two_closed_curly_brackets()
		{
			var tokenizer = new Tokenizer("}}");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("}}", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(1, tokens[1].LineNumber);
			Assert.AreEqual(3, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens_for_LF()
		{
			var tokenizer = new Tokenizer("\n");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("\n", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(2, tokens[1].LineNumber);
			Assert.AreEqual(1, tokens[1].ColumnNumber);
			Assert.AreEqual(2, tokens[1].Position);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_End_tokens_for_LF()
		{
			var tokenizer = new Tokenizer("{{\n");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(2, tokens[1].LineNumber);
			Assert.AreEqual(2, tokens[1].ColumnNumber);
			Assert.AreEqual(4, tokens[1].Position);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens_for_LF_LF()
		{
			var tokenizer = new Tokenizer("\n\n");

			Token[] tokens = tokenizer.Tokenize().ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("\n\n", tokens[0].Content);
			Assert.AreEqual(1, tokens[0].LineNumber);
			Assert.AreEqual(1, tokens[0].ColumnNumber);
			Assert.AreEqual(1, tokens[0].Position);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
			Assert.AreEqual(3, tokens[1].LineNumber);
			Assert.AreEqual(1, tokens[1].ColumnNumber);
			Assert.AreEqual(3, tokens[1].Position);
		}
	}
}