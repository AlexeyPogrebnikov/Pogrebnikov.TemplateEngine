using System;
using System.Collections.Generic;

namespace Pogrebnikov.TemplateEngine.LexicalAnalysis
{
	internal class Tokenizer
	{
		private const string OpenTemplateKeyword = "{{";
		private const string CloseTemplateTokenContent = "}}";
		private const string DotTokenContent = ".";
		private const string CommaTokenContent = ",";
		private const string LeftParenthesisTokenContent = "(";
		private const string RightParenthesisTokenContent = ")";
		private const string LeftBracketTokenContent = "[";
		private const string RightBracketTokenContent = "]";
		private const string IfTokenContent = "#if";
		private const string EndIfTokenContent = "/if";
		private const string LoopTokenContent = "#loop";
		private const string EndLoopTokenContent = "/loop";

		internal IEnumerable<Token> Tokenize(string s)
		{
			IList<Token> tokens = new List<Token>();

			var isTemplate = false;
			var position = 0;

			while (position < s.Length)
			{
				if (isTemplate)
				{
					TokenizeIntoTemplateStatement(s, ref position, ref isTemplate, tokens);
					continue;
				}

				int indexOfOpenTemplateKeyword = s.IndexOf(OpenTemplateKeyword, position, StringComparison.Ordinal);
				TokenizeOutsideTemplateStatement(s, indexOfOpenTemplateKeyword, ref position, tokens);
				isTemplate = true;
			}

			tokens.Add(new Token
			{
				TokenType = TokenType.End,
				Content = string.Empty
			});

			return tokens;
		}

		private static void TokenizeOutsideTemplateStatement(string s, int indexOfOpenTemplateKeyword, ref int position, ICollection<Token> tokens)
		{
			if (indexOfOpenTemplateKeyword >= position)
			{
				if (indexOfOpenTemplateKeyword > position)
				{
					tokens.Add(new Token
					{
						TokenType = TokenType.Text,
						Content = s.Substring(position, indexOfOpenTemplateKeyword - position)
					});
				}

				tokens.Add(new Token
				{
					TokenType = TokenType.OpenTemplate,
					Content = OpenTemplateKeyword
				});

				position = indexOfOpenTemplateKeyword + OpenTemplateKeyword.Length;
				return;
			}

			tokens.Add(new Token
			{
				TokenType = TokenType.Text,
				Content = s.Substring(position)
			});

			position = s.Length;
		}

		private static void TokenizeIntoTemplateStatement(string s, ref int position, ref bool isTemplate, ICollection<Token> tokens)
		{
			if (char.IsWhiteSpace(s[position]))
			{
				position++;
				return;
			}

			Token token = TryTokenizeCloseTemplate(s, ref position);
			if (token != null)
				isTemplate = false;

			token = token ?? TryTokenizeDot(s, ref position);
			token = token ?? TryTokenizeComma(s, ref position);
			token = token ?? TryTokenizeLeftParenthesis(s, ref position);
			token = token ?? TryTokenizeRightParenthesis(s, ref position);
			token = token ?? TryTokenizeLeftBracket(s, ref position);
			token = token ?? TryTokenizeRightBracket(s, ref position);
			token = token ?? TryTokenizeIf(s, ref position);
			token = token ?? TryTokenizeEndIf(s, ref position);
			token = token ?? TryTokenizeLoop(s, ref position);
			token = token ?? TryTokenizeEndLoop(s, ref position);
			token = token ?? TryTokenizeIdentifier(s, ref position);

			if (token != null)
				tokens.Add(token);
			else
				throw new TokenizationException($"Can't tokenize '{s[position]}' at position {position + 1}.");
		}

		private static Token TryTokenizeDot(string s, ref int position)
		{
			return TryTokenize(s, DotTokenContent, ref position, TokenType.Dot);
		}

		private static Token TryTokenizeComma(string s, ref int position)
		{
			return TryTokenize(s, CommaTokenContent, ref position, TokenType.Comma);
		}

		private static Token TryTokenizeLeftParenthesis(string s, ref int position)
		{
			return TryTokenize(s, LeftParenthesisTokenContent, ref position, TokenType.LeftParenthesis);
		}

		private static Token TryTokenizeRightParenthesis(string s, ref int position)
		{
			return TryTokenize(s, RightParenthesisTokenContent, ref position, TokenType.RightParenthesis);
		}

		private static Token TryTokenizeLeftBracket(string s, ref int position)
		{
			return TryTokenize(s, LeftBracketTokenContent, ref position, TokenType.LeftBracket);
		}

		private static Token TryTokenizeRightBracket(string s, ref int position)
		{
			return TryTokenize(s, RightBracketTokenContent, ref position, TokenType.RightBracket);
		}

		private static Token TryTokenizeCloseTemplate(string s, ref int position)
		{
			return TryTokenize(s, CloseTemplateTokenContent, ref position, TokenType.CloseTemplate);
		}

		private static Token TryTokenizeIdentifier(string s, ref int position)
		{
			int newPosition = position;

			while (newPosition < s.Length && IsIdentifierLetter(s, position, newPosition))
				newPosition++;

			if (position == newPosition)
				return null;

			string content = s.Substring(position, newPosition - position);

			position = newPosition;

			return new Token
			{
				TokenType = TokenType.Identifier,
				Content = content
			};
		}

		private static bool IsIdentifierLetter(string s, int position, int newPosition)
		{
			return char.IsLetter(s, newPosition) || newPosition > position && char.IsDigit(s, newPosition);
		}

		private static Token TryTokenizeIf(string s, ref int position)
		{
			return TryTokenize(s, IfTokenContent, ref position, TokenType.BeginIf);
		}

		private static Token TryTokenizeEndIf(string s, ref int position)
		{
			return TryTokenize(s, EndIfTokenContent, ref position, TokenType.EndIf);
		}

		private static Token TryTokenizeLoop(string s, ref int position)
		{
			return TryTokenize(s, LoopTokenContent, ref position, TokenType.BeginLoop);
		}

		private static Token TryTokenizeEndLoop(string s, ref int position)
		{
			return TryTokenize(s, EndLoopTokenContent, ref position, TokenType.EndLoop);
		}

		private static Token TryTokenize(string s, string tokenContent, ref int position, TokenType tokenType)
		{
			if (string.Compare(s, position, tokenContent, 0, tokenContent.Length, StringComparison.Ordinal) != 0)
				return null;

			position += tokenContent.Length;
			return new Token
			{
				TokenType = tokenType,
				Content = tokenContent
			};
		}
	}
}