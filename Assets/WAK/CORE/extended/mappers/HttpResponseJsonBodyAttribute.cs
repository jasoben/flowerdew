using System;
using hg.ApiWebKit.core.http;
using hg.ApiWebKit.core.attributes;
using System.Reflection;
using hg.ApiWebKit.converters;

namespace hg.ApiWebKit.mappers
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public sealed class HttpResponseJsonBodyAttribute : HttpResponseTextBodyAttribute
	{	
#if UNITY_5_4_OR_NEWER
		public HttpResponseJsonBodyAttribute(bool useUnitySerializer = false):base() 
		{ 
			Converter = useUnitySerializer ? typeof(DeserializeUnityJsonUtility) : typeof(DeserializeLitJson);
		}
#else
		public HttpResponseJsonBodyAttribute(bool useUnitySerializer = false):base() 
		{ 
			Converter = typeof(DeserializeLitJson);
		}
#endif
	}
}
