using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GenericRepo.Dapper.Wrapper.Utilities
{
	public static class DataTableProcessor
	{
		public static DataTable MapToDataTable<T>(List<T> list)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));
			var dataTable = new DataTable(typeof(T).Name);
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var propertyInfo in properties)
			{
				dataTable.Columns.Add(propertyInfo.Name);
			}
			foreach (var item in list)
			{
				var values = new object[properties.Length];
				for (var index = 0; index < properties.Length; index++)
				{
					values[index] = properties[index].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}
			return dataTable;
		}

		public static DataTable MapToDataTable(string jsonObject)
		{
			if (string.IsNullOrWhiteSpace(jsonObject))
				throw new ArgumentNullException(nameof(jsonObject));
			var dataTable = (DataTable)JsonConvert.DeserializeObject(jsonObject, typeof(DataTable));
			return dataTable;
		}
    }
}
