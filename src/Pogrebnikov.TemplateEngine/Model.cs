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
			return _obj.GetType().GetProperty(valueAccess.Name).GetValue(_obj);
		}
	}
}