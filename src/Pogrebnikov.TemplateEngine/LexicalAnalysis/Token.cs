namespace Pogrebnikov.TemplateEngine.LexicalAnalysis
{
	internal class Token
	{
		internal TokenType TokenType { get; set; }
		internal string Content { get; set; }
		internal int LineNumber { get; set; }
		internal int ColumnNumber { get; set; }

		/// <summary>
		/// Absolute start position of a token.
		/// </summary>
		internal int Position { get; set; }
	}
}