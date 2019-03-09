using NUnit.Framework;
using Pogrebnikov.TemplateEngine.Parsing.Elements;

namespace Pogrebnikov.TemplateEngine.Tests.Parsing.Elements
{
	[TestFixture]
	public class PropertyValueAccessTests
	{
		[Test]
		public void Append_Next_same_appended_element()
		{
			var beginValueAccess = new PropertyValueAccess();

			var endValueAccess = new PropertyValueAccess();

			beginValueAccess.Append(endValueAccess);

			Assert.AreSame(endValueAccess, beginValueAccess.Next);
		}

		[Test]
		public void Append_Append_Next_same_appended_element()
		{
			var beginValueAccess = new PropertyValueAccess();

			var middleValueAccess = new PropertyValueAccess();
			beginValueAccess.Append(middleValueAccess);

			var endValueAccess = new PropertyValueAccess();
			beginValueAccess.Append(endValueAccess);

			Assert.AreSame(middleValueAccess, beginValueAccess.Next);
			Assert.AreSame(endValueAccess, middleValueAccess.Next);
		}
	}
}