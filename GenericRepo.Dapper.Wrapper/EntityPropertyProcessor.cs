using GenericRepo.Dapper.Wrapper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GenericRepo.Dapper.Wrapper
{
	public static class EntityPropertyProcessor
	{
		public static EntityPropertyProcessorResponse GetFormattedQueryStatementBody<TEntity>(QueryStatement queryStatement, params string[] namesOfPropertiesToBeExcluded)
		{
			var entityPropertyRemovalResponse = namesOfPropertiesToBeExcluded.Any() ?
				RemoveProperties(GetEntityProperties(typeof(TEntity).GetProperties()), namesOfPropertiesToBeExcluded) :
				new EntityPropertyRemovalResponse { Properties = GetEntityProperties(typeof(TEntity).GetProperties()) };
			if (entityPropertyRemovalResponse.Error != null)
				return new EntityPropertyProcessorResponse { Error = entityPropertyRemovalResponse.Error };

			return new EntityPropertyProcessorResponse { Result = FormatQueryStatementBody(queryStatement, entityPropertyRemovalResponse.Properties) };
		}

		public static EntityPropertyRemovalResponse RemoveProperties(List<string> listOfProperties, params string[] namesOfPropertiesToBeExcluded)
		{
			foreach (var property in namesOfPropertiesToBeExcluded)
			{
				var inputIsNotValid = InputIsNotValid(listOfProperties, property);
				if (inputIsNotValid.Item1)
					return new EntityPropertyRemovalResponse { Error = new Error { Message = inputIsNotValid.Item2 } };

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
			if (queryStatement == QueryStatement.InsertQuery)
			{
				var tableFields = string.Join(",", properties);
				var modelFields = $"@{ string.Join(", @", properties) }";
				return $"({tableFields}) values ({modelFields})";
			}
			if (queryStatement == QueryStatement.UpdateQuery)
			{
				var updateQuery = new StringBuilder("");
				properties.ForEach(property => { updateQuery.Append($"{property}=@{property}, "); });
				updateQuery.Remove(updateQuery.Length - 2, 2);

				return updateQuery.ToString();
			}
			return string.Empty;
		}

		private static (bool, string) InputIsNotValid(List<string> listOfProperties, string property)
		{
			if (listOfProperties == null) return (true, "Invalid list of Properties entered.");
			if (listOfProperties.Count == 0) return (true, "list of Properties entered is empty.");
			if (string.IsNullOrWhiteSpace(property)) return (true, "Invalid property name, check your property names.");
			return (false, null);
		}
	}
}
