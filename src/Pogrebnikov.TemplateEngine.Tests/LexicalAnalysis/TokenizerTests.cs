using System.Linq;
using NUnit.Framework;
using Pogrebnikov.TemplateEngine.LexicalAnalysis;

namespace Pogrebnikov.TemplateEngine.Tests.LexicalAnalysis
{
	[TestFixture]
	public class TokenizerTests
	{
		private Tokenizer _tokenizer;

		[SetUp]
		public void SetUp()
		{
			_tokenizer = new Tokenizer();
		}

		[Test]
		public void Tokenize_return_End_token_if_template_is_empty()
		{
			Token[] tokens = _tokenizer.Tokenize("").ToArray();

			Assert.AreEqual(1, tokens.Length);

			Assert.AreEqual(TokenType.End, tokens[0].TokenType);
			Assert.AreEqual(string.Empty, tokens[0].Content);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("<b>").ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("<b>", tokens[0].Content);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_End_token()
		{
			Token[] tokens = _tokenizer.Tokenize("{{").ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
		}

		[Test]
		public void Tokenize_return_Text_OpenTemplate_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("z{{").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("z", tokens[0].Content);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[1].TokenType);
			Assert.AreEqual("{{", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_End_token()
		{
			Token[] tokens = _tokenizer.Tokenize("{{zoo").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("zoo", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_End_tokens_without_whitespace()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ Model ").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_Dot_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{Model.").ToArray();

			Assert.AreEqual(4, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);

			Assert.AreEqual(TokenType.Dot, tokens[2].TokenType);
			Assert.AreEqual(".", tokens[2].Content);

			Assert.AreEqual(TokenType.End, tokens[3].TokenType);
			Assert.AreEqual(string.Empty, tokens[3].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_Dot_Identifier_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{Model.Name").ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("Model", tokens[1].Content);

			Assert.AreEqual(TokenType.Dot, tokens[2].TokenType);
			Assert.AreEqual(".", tokens[2].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[3].TokenType);
			Assert.AreEqual("Name", tokens[3].Content);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_LeftBracket_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{[").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.LeftBracket, tokens[1].TokenType);
			Assert.AreEqual("[", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_throw_exception_if_template_contains_unknown_symbol()
		{
			var exception = Assert.Throws<TokenizationException>(() => _tokenizer.Tokenize("{{*"));

			Assert.AreEqual("Can't tokenize '*' at position 3.", exception.Message);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{}}").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_RightBracket_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{]").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.RightBracket, tokens[1].TokenType);
			Assert.AreEqual("]", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Identifier_with_number_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{count1").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[1].TokenType);
			Assert.AreEqual("count1", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginLoop_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ #loop").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.BeginLoop, tokens[1].TokenType);
			Assert.AreEqual("#loop", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_Comma_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{,").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.Comma, tokens[1].TokenType);
			Assert.AreEqual(",", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_EndLoop_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ /loop").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.EndLoop, tokens[1].TokenType);
			Assert.AreEqual("/loop", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginLoop_Identifier_Comma_Identifier_CloseTemplate_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ #loop Names, i }}").ToArray();

			Assert.AreEqual(7, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.BeginLoop, tokens[1].TokenType);
			Assert.AreEqual("#loop", tokens[1].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[2].TokenType);
			Assert.AreEqual("Names", tokens[2].Content);

			Assert.AreEqual(TokenType.Comma, tokens[3].TokenType);
			Assert.AreEqual(",", tokens[3].Content);

			Assert.AreEqual(TokenType.Identifier, tokens[4].TokenType);
			Assert.AreEqual("i", tokens[4].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[5].TokenType);
			Assert.AreEqual("}}", tokens[5].Content);

			Assert.AreEqual(TokenType.End, tokens[6].TokenType);
			Assert.AreEqual(string.Empty, tokens[6].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_LeftParenthesis_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ (").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.LeftParenthesis, tokens[1].TokenType);
			Assert.AreEqual("(", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_RightParenthesis_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{ )").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.RightParenthesis, tokens[1].TokenType);
			Assert.AreEqual(")", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_BeginIf_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{#if").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.BeginIf, tokens[1].TokenType);
			Assert.AreEqual("#if", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_EndIf_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{/if").ToArray();

			Assert.AreEqual(3, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.EndIf, tokens[1].TokenType);
			Assert.AreEqual("/if", tokens[1].Content);

			Assert.AreEqual(TokenType.End, tokens[2].TokenType);
			Assert.AreEqual(string.Empty, tokens[2].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_Text_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{}}q").ToArray();

			Assert.AreEqual(4, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);

			Assert.AreEqual(TokenType.Text, tokens[2].TokenType);
			Assert.AreEqual("q", tokens[2].Content);

			Assert.AreEqual(TokenType.End, tokens[3].TokenType);
			Assert.AreEqual(string.Empty, tokens[3].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_OpenTemplate_CloseTemplate_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{}}{{}}").ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[2].TokenType);
			Assert.AreEqual("{{", tokens[2].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[3].TokenType);
			Assert.AreEqual("}}", tokens[3].Content);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
		}

		[Test]
		public void Tokenize_return_OpenTemplate_CloseTemplate_Text_OpenTemplate_CloseTemplate_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("{{}}i{{}}").ToArray();

			Assert.AreEqual(6, tokens.Length);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[0].TokenType);
			Assert.AreEqual("{{", tokens[0].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[1].TokenType);
			Assert.AreEqual("}}", tokens[1].Content);

			Assert.AreEqual(TokenType.Text, tokens[2].TokenType);
			Assert.AreEqual("i", tokens[2].Content);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[3].TokenType);
			Assert.AreEqual("{{", tokens[3].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[4].TokenType);
			Assert.AreEqual("}}", tokens[4].Content);

			Assert.AreEqual(TokenType.End, tokens[5].TokenType);
			Assert.AreEqual(string.Empty, tokens[5].Content);
		}

		[Test]
		public void Tokenize_return_Text_OpenTemplate_CloseTemplate_Text_End_tokens()
		{
			Token[] tokens = _tokenizer.Tokenize("foo{{}}zoo").ToArray();

			Assert.AreEqual(5, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("foo", tokens[0].Content);

			Assert.AreEqual(TokenType.OpenTemplate, tokens[1].TokenType);
			Assert.AreEqual("{{", tokens[1].Content);

			Assert.AreEqual(TokenType.CloseTemplate, tokens[2].TokenType);
			Assert.AreEqual("}}", tokens[2].Content);

			Assert.AreEqual(TokenType.Text, tokens[3].TokenType);
			Assert.AreEqual("zoo", tokens[3].Content);

			Assert.AreEqual(TokenType.End, tokens[4].TokenType);
			Assert.AreEqual(string.Empty, tokens[4].Content);
		}

		[Test]
		public void Tokenize_return_Text_End_tokens_for_two_closed_curly_brackets()
		{
			Token[] tokens = _tokenizer.Tokenize("}}").ToArray();

			Assert.AreEqual(2, tokens.Length);

			Assert.AreEqual(TokenType.Text, tokens[0].TokenType);
			Assert.AreEqual("}}", tokens[0].Content);

			Assert.AreEqual(TokenType.End, tokens[1].TokenType);
			Assert.AreEqual(string.Empty, tokens[1].Content);
		}
	}
}