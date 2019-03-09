namespace Pogrebnikov.TemplateEngine.Parsing.Elements
{
	internal abstract class ValueAccess
	{
		internal string Name { get; set; }

		internal ValueAccess Next { get; set; }

		internal abstract object GetValue(PropertyBag propertyBag);

		internal void Append(ValueAccess valueAccess)
		{
			if (Next == null)
				Next = valueAccess;
			else
				Next.Append(valueAccess);
		}
	}
}