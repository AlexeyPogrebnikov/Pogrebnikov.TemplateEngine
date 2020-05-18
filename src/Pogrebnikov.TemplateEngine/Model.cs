using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine
{
	/// <summary>
	/// The wrapper for a data.
	/// </summary>
	public class Model
	{
		//TODO to property Data
		private readonly object _source;

		public Model(object source)
		{
			_source = source;
		}

		//TODO move to TemplateEvaluator
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