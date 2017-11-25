using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using hg.LitJson;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace hg.ApiWebKit.converters
{
	public class DeserializeXml : IValueConverter
	{
		public object Convert (object input, FieldInfo targetField, out bool successful, params object[] parameters)
		{ 
			successful = false;

			if(input == null)
				return null;

			try
			{
				object result = null;

				using (var reader = new StringReader(input.ToString()))
				{
					XmlSerializer serializer = new XmlSerializer(targetField.FieldType);
					result = serializer.Deserialize(reader);
				}
										
				successful = true;

				return result;

			}
			catch (Exception ex)
			{
				Configuration.Log("(DeserializeXml)(Convert) Failure on field '" + targetField.Name + "' : " + ex.Message, LogSeverity.ERROR);
				if(ex.InnerException!=null)
					Configuration.Log("(DeserializeXml)(Convert) Failure-Inner : " + ex.InnerException.Message, LogSeverity.ERROR);
				return null;
			}
		}
	}
}

