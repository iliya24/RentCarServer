using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerRentCar.Common.Util
{
    public class GenericClinetTableBuilder
    {
        /// <summary>
        /// Returns JSON string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableRows"></param>
        /// <returns></returns>
        public string BuildJsonTable<T>(List<T> tableRows)
        {
            GenericClinetTable tmp = new GenericClinetTable();
            tmp.metaData = new List<metaData>();
            tmp.data = new List<Dictionary<string, string>>();
            int index = 0;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                tmp.metaData.Add(new metaData() { field = property.Name.ToLower(), header = property.Name });
            }
            foreach (var row in tableRows)
            {
                tmp.data.Add(new Dictionary<string, string>());

                foreach (PropertyInfo property in properties)
                {
                    var val = property.GetValue(row, null);
                    val = val == null ? "" : val.ToString();
                    string filed = property.Name.ToLower();
                    tmp.data[index].Add(filed, val.ToString());
                }

                index++;


            }           
            return JsonConvert.SerializeObject(tmp); ;
        }
      
    }

    public class GenericClinetTable
    {
        public List<metaData> metaData { get; set; }
        public List<Dictionary<string, string>> data { get; set; }
    }
    public class metaData
    {
        public string field { get; set; }
        public string header { get; set; }

    }
}
