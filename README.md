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
version 1.2.1 

## Dependencies
- Dapper
- System.Data.SqlClient 

## Download
![image](https://user-images.githubusercontent.com/32597717/114360191-0b6b6a80-9b75-11eb-9e73-04dac3bc6d05.png)


## Usage
- These are functions available in version 1.1.0

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
- Update, Insert and Delete return number of rows affected after executing the method
- This is how your specific repository declaration should look like

```C#
public class PersonRepository
{
  private readonly IRepository<Person> _personRepository;
  private const string TableName = "Persons";
  private const string PrimaryKeyName = "Code";
  
  public PersonRepository(string connectionString)
  {
    _personRepository = new Repository<Person>(TableName, connectionString);
  }
}
```
- Your function implementation should look like this :
```C#
public async Task<IEnumerable<Person>> GetAllPeopleAsync()
{
  return await _personRepository.GetAllAsync();
}
```
- For other available functions check IRepository interface provided at the top
> If you have any questions, suggestions, bugs or want to contribute, please don't hesitate to contact :-
- spantshwa.lukho@gmail.com



