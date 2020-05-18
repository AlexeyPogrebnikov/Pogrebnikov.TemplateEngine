using System;
using System.Runtime.Serialization;

namespace Pogrebnikov.TemplateEngine.Evaluating
{
	[Serializable]
	public class TemplateEvaluateException : Exception
	{
		public TemplateEvaluateException()
		{
		}

		public TemplateEvaluateException(string message) : base(message)
		{
		}

		public TemplateEvaluateException(string message, Exception inner) : base(message, inner)
		{
		}

		protected TemplateEvaluateException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}