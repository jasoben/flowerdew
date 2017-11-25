using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using hg.ApiWebKit.core.http;
using hg.ApiWebKit.core.attributes;
using hg.ApiWebKit.converters;

namespace hg.ApiWebKit.mappers
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public sealed class HttpResponseXmlBodyAttribute : HttpResponseTextBodyAttribute
	{	
		public HttpResponseXmlBodyAttribute():base() 
		{ 
			Converter = typeof(DeserializeXml);
		}
	}
}
