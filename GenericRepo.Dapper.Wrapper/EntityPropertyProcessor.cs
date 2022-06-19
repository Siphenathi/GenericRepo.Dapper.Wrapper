using GenericRepo.Dapper.Wrapper.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GenericRepo.Dapper.Wrapper
{
	public static class EntityPropertyProcessor
	{
		public static EntityPropertyProcessorResponse FormatQueryStatementBody<TEntity>(QueryStatement queryStatement, params string[] namesOfPropertiesToBeExcluded)
		{
			var entityPropertyRemovalResponse = namesOfPropertiesToBeExcluded.Any() ?
				RemoveProperties(GetEntityProperties(typeof(TEntity).GetProperties()), namesOfPropertiesToBeExcluded) :
				new EntityPropertyRemovalResponse { Properties = GetEntityProperties(typeof(TEntity).GetProperties()) };
			return entityPropertyRemovalResponse.Error != null ? new EntityPropertyProcessorResponse { Error = entityPropertyRemovalResponse.Error } 
				: new EntityPropertyProcessorResponse { Result = FormatQueryStatementBody(queryStatement, entityPropertyRemovalResponse.Properties) };
		}

		public static EntityPropertyRemovalResponse RemoveProperties(List<string> listOfProperties, params string[] namesOfPropertiesToBeExcluded)
		{
			foreach (var property in namesOfPropertiesToBeExcluded)
			{
				var (item1, item2) = InputIsNotValid(listOfProperties, property);
				if (item1)
					return new EntityPropertyRemovalResponse { Error = new Error { Message = item2 } };

				var propertyIndex = listOfProperties.FindIndex(word => word.Equals(property, StringComparison.CurrentCultureIgnoreCase));
				if (propertyIndex == -1) return new EntityPropertyRemovalResponse
				{ Error = new Error { Message = $"property name {property} is not found in the entity provided." } };
				listOfProperties.RemoveAt(propertyIndex);
			};

			return new EntityPropertyRemovalResponse { Properties = listOfProperties };
		}

		public static List<string> GetEntityProperties(IEnumerable<PropertyInfo> listOfProperties)
		{
			if (listOfProperties == null) return new List<string>();

			var entityProperties = (from prop in listOfProperties
									let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
									where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
									select prop.Name).ToList();
			return entityProperties;
		}

		private static string FormatQueryStatementBody(QueryStatement queryStatement, List<string> properties)
		{
			switch (queryStatement)
			{
				case QueryStatement.InsertQuery:
				{
					var tableFields = string.Join(",", properties);
					var modelFields = $"@{ string.Join(", @", properties) }";
					return $"({tableFields}) values ({modelFields})";
				}
				case QueryStatement.UpdateQuery:
				{
					var updateQuery = new StringBuilder("");
					properties.ForEach(property => { updateQuery.Append($"{property}=@{property}, "); });
					updateQuery.Remove(updateQuery.Length - 2, 2);

					return updateQuery.ToString();
				}
				default:
					return string.Empty;
			}
		}

		private static (bool, string) InputIsNotValid(ICollection listOfProperties, string property)
		{
			if (listOfProperties == null) return (true, "Invalid list of Properties entered.");
			if (listOfProperties.Count == 0) return (true, "list of Properties entered is empty.");
			return string.IsNullOrWhiteSpace(property) ? (true, "Invalid property name, check your property names.") : (false, null);
		}
	}
}
