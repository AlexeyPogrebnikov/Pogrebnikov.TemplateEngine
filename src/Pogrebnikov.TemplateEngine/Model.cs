namespace Pogrebnikov.TemplateEngine
{
	public class Model
	{
		public Model(PropertyBag bag)
		{
			Bag = bag;
		}

		public PropertyBag Bag { get; }
	}
}