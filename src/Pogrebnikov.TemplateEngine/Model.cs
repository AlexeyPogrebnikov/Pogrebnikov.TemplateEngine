using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine
{
	/// <summary>
	/// The wrapper for a data.
	/// </summary>
	public class Model
	{
		private readonly object _source;

		public Model(object source)
		{
			_source = source;
		}

		internal object GetValue(ValueAccess valueAccess)
		{
			ValueAccess currentValueAccess = valueAccess;
			object value = _source;

			do
			{
				value = value.GetType().GetProperty(currentValueAccess.Name).GetValue(value);
				currentValueAccess = currentValueAccess.Next;
			} while (currentValueAccess != null);

			return value;
		}
	}
}