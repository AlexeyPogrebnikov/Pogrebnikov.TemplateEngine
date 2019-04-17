using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine
{
	public class Model
	{
		private readonly object _obj;

		public Model(object obj)
		{
			_obj = obj;
		}

		internal object GetValue(ValueAccess valueAccess)
		{
			ValueAccess currentValueAccess = valueAccess;
			object currentObject = _obj;

			do
			{
				currentObject = currentObject.GetType().GetProperty(currentValueAccess.Name).GetValue(currentObject);
				currentValueAccess = currentValueAccess.Next;
			} while (currentValueAccess != null);

			return currentObject;
		}
	}
}