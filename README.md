# GenericRepo.Dapper.Wrapper
Provides a simple Generic Repository to fluently map model properties with database columns using Dapper to interect with the Db.

---
## Introduction
> This [Dapper](https://github.com/StackExchange/Dapper) wrapper allows you to fluently configure the mapping between model properties and database columns. 
> This keeps your model clean of mapping attributes. 
> Also you do not have to rewrite SQL query each time you want to interact with different table through Dapper.
> This guy handles all of that by providing functions that wrap specific dapper functions into generic functions. 
> Therefore, add that into what we call generic repository.

NuGet |
------------ |
version 1.0.0

## Dependencies
- Dapper
- System.Data.SqlClient 

## Download
![image](https://user-images.githubusercontent.com/32597717/114327603-05a46380-9b3a-11eb-84d0-64d4b89b02de.png)

## Usage
These are functions available with version 1.0.0

Key | Description
------------ | ------------
Id | Id is a table key
primarykeyName | primarykeyName is a key Column name.
namesOfPropertiesToBeExcluded | names of keys/columns that are auto generated or do not want to provide values for. You can provide as many as you want.
T entity | T represent the table/entity.

```C#
public interface IRepository<T>
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetAsync(object id, string primaryKeyName);
  Task<IEnumerable<T>> GetAllAsync(object id, string primaryKeyName);
  Task<int> InsertAsync(T entity, params string[] namesOfPropertiesToBeExcluded);
  Task<int> UpdateAsync(string primaryKeyName, T entity, params string[] namesOfPropertiesToBeExcluded);
  Task<int> DeleteAsync(object id, string primaryKeyName);
}
```
