using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
	public class JsonHelper
	{
		public static string ToJson( object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static T FromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static T FromJsonFile<T> (string filePath)
		{
			if (File.Exists(filePath))
			{
				return FromJson<T>(File.ReadAllText(filePath));
			}
			return default;
		}
	}
}
