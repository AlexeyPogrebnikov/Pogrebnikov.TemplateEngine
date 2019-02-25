namespace Pogrebnikov.TemplateEngine.LexicalAnalysis
{
	internal enum TokenType
	{
		/// <summary>
		/// Some text out template.
		/// </summary>
		Text,

		/// <summary>
		/// {{
		/// </summary>
		OpenTemplate,

		/// <summary>
		/// }}
		/// </summary>
		CloseTemplate,

		/// <summary>
		/// .
		/// </summary>
		Dot,

		/// <summary>
		/// ,
		/// </summary>
		Comma,

		/// <summary>
		/// [
		/// </summary>
		LeftBracket,

		/// <summary>
		/// ]
		/// </summary>
		RightBracket,

		/// <summary>
		/// (
		/// </summary>
		LeftParenthesis,

		/// <summary>
		/// )
		/// </summary>
		RightParenthesis,

		/// <summary>
		/// e.g. Model
		/// </summary>
		Identifier,

		/// <summary>
		/// #if
		/// </summary>
		BeginIf,

		/// <summary>
		/// /if
		/// </summary>
		EndIf,

		/// <summary>
		/// #loop
		/// </summary>
		BeginLoop,

		/// <summary>
		/// /loop
		/// </summary>
		EndLoop,

		/// <summary>
		/// End of line.
		/// </summary>
		End
	}
}