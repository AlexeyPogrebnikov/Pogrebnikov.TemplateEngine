using System;
using System.Collections.Generic;

namespace Pogrebnikov.TemplateEngine.LexicalAnalysis
{
	internal class Tokenizer
	{
		private const char LineFeedSymbol = '\n';
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
		private readonly string _s;
		private int _lineNumber = 1;
		private int _columnNumber = 1;
		private int _position;
		private bool _isTemplate;
		private IList<Token> _tokens;

		internal Tokenizer(string s)
		{
			_s = s;
		}

		internal IEnumerable<Token> Tokenize()
		{
			if (_tokens != null)
				throw new InvalidOperationException("Tokenization is already completed.");

			_tokens = new List<Token>();

			while (_position < _s.Length)
			{
				if (_isTemplate)
				{
					TokenizeIntoTemplateStatement();
					continue;
				}

				TokenizeOutsideTemplateStatement();
				_isTemplate = true;
			}

			_tokens.Add(new Token
			{
				TokenType = TokenType.End,
				Content = string.Empty,
				LineNumber = _lineNumber,
				ColumnNumber = _columnNumber,
				Position = _s.Length + 1
			});

			return _tokens;
		}

		private void TokenizeOutsideTemplateStatement()
		{
			int indexOfOpenTemplateKeyword = _s.IndexOf(OpenTemplateKeyword, _position, StringComparison.Ordinal);

			if (indexOfOpenTemplateKeyword >= _position)
			{
				if (indexOfOpenTemplateKeyword > _position)
				{
					_tokens.Add(new Token
					{
						TokenType = TokenType.Text,
						Content = _s.Substring(_position, indexOfOpenTemplateKeyword - _position),
						LineNumber = 1,
						ColumnNumber = _position + 1,
						Position = _position + 1
					});
				}

				_columnNumber = indexOfOpenTemplateKeyword + 1;

				_tokens.Add(new Token
				{
					TokenType = TokenType.OpenTemplate,
					Content = OpenTemplateKeyword,
					LineNumber = 1,
					ColumnNumber = _columnNumber,
					Position = indexOfOpenTemplateKeyword + 1
				});

				_columnNumber += OpenTemplateKeyword.Length;

				_position = indexOfOpenTemplateKeyword + OpenTemplateKeyword.Length;
				return;
			}

			string content = _s.Substring(_position);

			_columnNumber = _position + 1;

			_tokens.Add(new Token
			{
				TokenType = TokenType.Text,
				Content = content,
				LineNumber = _lineNumber,
				ColumnNumber = _columnNumber,
				Position = _position + 1
			});

			_columnNumber += content.Length;

			int indexOfNewLine = content.IndexOf(LineFeedSymbol);
			while (indexOfNewLine > -1)
			{
				_lineNumber++;
				indexOfNewLine = content.IndexOf(LineFeedSymbol, indexOfNewLine + 1);
				_columnNumber = 1;
			}

			_position = _s.Length;
		}

		private void TokenizeIntoTemplateStatement()
		{
			if (char.IsWhiteSpace(_s[_position]))
			{
				if (_s[_position] == LineFeedSymbol)
				{
					_lineNumber++;
					_columnNumber = 1;
				}

				_columnNumber++;
				_position++;
				return;
			}

			Token token = TryTokenizeCloseTemplate();
			if (token != null)
				_isTemplate = false;

			token = token ?? TryTokenizeDot();
			token = token ?? TryTokenizeComma();
			token = token ?? TryTokenizeLeftParenthesis();
			token = token ?? TryTokenizeRightParenthesis();
			token = token ?? TryTokenizeLeftBracket();
			token = token ?? TryTokenizeRightBracket();
			token = token ?? TryTokenizeIf();
			token = token ?? TryTokenizeEndIf();
			token = token ?? TryTokenizeLoop();
			token = token ?? TryTokenizeEndLoop();
			token = token ?? TryTokenizeIdentifier();

			if (token != null)
				_tokens.Add(token);
			else
				throw new TokenizationException($"Can't tokenize '{_s[_position]}' at position {_position + 1}.");
		}

		private Token TryTokenizeDot()
		{
			return TryTokenize(DotTokenContent, TokenType.Dot);
		}

		private Token TryTokenizeComma()
		{
			return TryTokenize(CommaTokenContent, TokenType.Comma);
		}

		private Token TryTokenizeLeftParenthesis()
		{
			return TryTokenize(LeftParenthesisTokenContent, TokenType.LeftParenthesis);
		}

		private Token TryTokenizeRightParenthesis()
		{
			return TryTokenize(RightParenthesisTokenContent, TokenType.RightParenthesis);
		}

		private Token TryTokenizeLeftBracket()
		{
			return TryTokenize(LeftBracketTokenContent, TokenType.LeftBracket);
		}

		private Token TryTokenizeRightBracket()
		{
			return TryTokenize(RightBracketTokenContent, TokenType.RightBracket);
		}

		private Token TryTokenizeCloseTemplate()
		{
			return TryTokenize(CloseTemplateTokenContent, TokenType.CloseTemplate);
		}

		private static bool IsIdentifierLetter(string s, int position, int newPosition)
		{
			return char.IsLetter(s, newPosition) || newPosition > position && char.IsDigit(s, newPosition);
		}

		private Token TryTokenizeIf()
		{
			return TryTokenize(IfTokenContent, TokenType.BeginIf);
		}

		private Token TryTokenizeEndIf()
		{
			return TryTokenize(EndIfTokenContent, TokenType.EndIf);
		}

		private Token TryTokenizeLoop()
		{
			return TryTokenize(LoopTokenContent, TokenType.BeginLoop);
		}

		private Token TryTokenizeEndLoop()
		{
			return TryTokenize(EndLoopTokenContent, TokenType.EndLoop);
		}

		private Token TryTokenize(string tokenContent, TokenType tokenType)
		{
			if (string.Compare(_s, _position, tokenContent, 0, tokenContent.Length, StringComparison.Ordinal) != 0)
				return null;

			int oldPosition = _position;
			_position += tokenContent.Length;
			_columnNumber = oldPosition + 1;

			var token = new Token
			{
				TokenType = tokenType,
				Content = tokenContent,
				LineNumber = 1,
				ColumnNumber = _columnNumber,
				Position = oldPosition + 1
			};

			_columnNumber += tokenContent.Length;
			return token;
		}

		private Token TryTokenizeIdentifier()
		{
			int newPosition = _position;

			while (newPosition < _s.Length && IsIdentifierLetter(_s, _position, newPosition))
				newPosition++;

			if (_position == newPosition)
				return null;

			string content = _s.Substring(_position, newPosition - _position);

			int oldPosition = _position;
			_position = newPosition;
			_columnNumber = oldPosition + 1;

			var token = new Token
			{
				TokenType = TokenType.Identifier,
				Content = content,
				LineNumber = 1,
				ColumnNumber = _columnNumber,
				Position = oldPosition + 1
			};

			_columnNumber += content.Length;

			return token;
		}
	}
}