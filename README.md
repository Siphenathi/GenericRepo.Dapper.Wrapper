# GenericRepo.Dapper.Wrapper

This is a c# library that provides a simple Generic Repository to fluently map model properties with database columns using Dapper to interact with the Database.

---

## Introduction

> This [Dapper](https://github.com/StackExchange/Dapper) wrapper allows you to fluently configure the mapping between model properties and database columns.
> This keeps your model clean of mapping attributes.
> Also you do not have to rewrite SQL query each time you want to interact with different table through Dapper.
> This guy handles all of that by providing functions that wrap specific dapper functions into generic functions.
> It does not only end there, this library allows us to also manipulate data in the database using stored procedures.
> Therefore, add that into what we call generic repository.

NuGet | Support |
------------ | ------------
Latest [version 3.0.1](https://www.nuget.org/packages/GenericDapperRepo.Wrapper/#versions-body-tab) | All C# stack (.Net Core, .Net Framework, .Net Standard and many more)

## Dependencies

- Dapper
- Newtonsoft.Json
- System.Data.SqlClient 
- MySql.Data
- Oracle.ManagedDataAccess.Core
- System.Data.SQLite.Core
- Npgsq

## Download

```
Install-Package GenericDapperRepo.Wrapper -Version 3.0.1
```

## Usage

Key | Description
------------ | ------------
Id | Id is a table key
keyName | keyName is a unique Column name ie primary key (Id, code).
namesOfPropertiesToBeExcluded | names of columns that their values cannot be changed/columns that are keys ie Composite key, Id Number, Foreign Key, Candidate Key etc. You can provide as many as you want.
T entity | T represent the table/entity.

```C#
public interface IRepository<T>
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetAsync(object id, string keyName);
  Task<IEnumerable<T>> GetAllAsync(object id, string keyName);
  Task<int> InsertAsync(T entity, params string[] namesOfPropertiesToBeExcluded);
  Task<int> UpdateAsync(string keyName, T entity, params string[] namesOfPropertiesToBeExcluded);
  Task<int> DeleteAsync(object id, string keyName);
}
```

- Update, Insert and Delete returns number of rows affected after executing the method
- This is how your specific repository declaration should look like

```C#
public class PersonRepository
{
  private readonly IRepository<Person> _personRepository;
  private const string TableName = "dbo.Persons";  //NB: prefix the table schema your table belongs to
  private const string keyName = "Code";

  
  public PersonRepository(string connectionString, DatabaseProvider databaseProvider)
  {
    _personRepository = new Repository<Person>(TableName, connectionString, databaseProvider);
  }
}
```
- Database Provider names
```C#
public enum DatabaseProvider
{
  MsSql = 1,
  MySql = 2,
  Oracle = 3,
  SqLite = 4,
  PostGreSql = 5
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

# Working with Stored Procedures

## Usage
Key | Description
------------ | ------------
connectionString | A connection string is a string that specifies information about a data source, means of connecting to it.
databaseProvider | Enum that contains different database provider names ie MsSql, MySql, Oracle, PostGreSql n SqLite.
procName | Name of the stored procedure that will be using to query/manipulate data.
parameters | These are key-value pairs that are required when calling stored procedure.
object | Key-value pairs/object that consist of a name(s) that are tied to the stored procdure variable(s).
T entity | T is the model.

```C#
public interface IStoredProcProcessor
{
  Task<IEnumerable<T>> GetDataAsync<T>(string procName, object parameters = null);
  Task<int> ExecuteAsync(string procName, object parameters);
  Task<int> ExecuteInBulkAsync(string procName, object @object );
}
```

- Parameters are optional base on the created stored procedure
- This is the only way to create StoredProcProcessor instance :

```C#
  IStoredProcProcessor storedProcProcessor = new StoredProcProcessor(ConnectionString, DatabaseProvider.MySql);
```

- GetDataAsync returns number of records in a list. This function is used to query data from Database
- ExecuteAsync() and ExecuteInBulkAsync() return number of rows affected after execution. These functions are to be used for data manipulation (Add, Delete and Update)

## Configuration to consume ExecuteInBulkAsync()

- **NB** : This function works with [table-valued parameter](https://docs.microsoft.com/en-us/sql/relational-databases/tables/use-table-valued-parameters-database-engine?view=sql-server-ver16) approach.

```C#
async Task<int> ExecuteInBulkAsync(string procName, object @object );
```

- Constructing an object has been made easy by the use of a utility/helper class called **DataTableProcessor**. This utility maps data to DataTable but feel free to use your own helper if you want to.

```C#
public static class DataTableProcessor
{
  public static DataTable MapToDataTable<T>(List<T> list); //maps any model into DataTable
  public static DataTable MapToDataTable(string jsonObject); //maps json data into DataTable
}
```

```C#
  var listOfEmployees = ListOfEmployees();  // data type : List<RegisterEmployeeModel>
  var dataTable = DataTableProcessor.MapToDataTable(listOfEmployees);
            // OR
  var dataTable = DataTableProcessor.MapToDataTable(GetJsonData()); //takes json data

  var obj = new { employeeType = dataTable };
  var results = await sut.ExecuteInBulkAsync(storedProcName, obj);
```

- **NB** : **employeeType** is the variable name in the stored procedure. This name must match exactly the variable name in the store procedure. Variable name is case sensitive.
- You can add more than one property inside the object as long as your stored procedure takes those arguments(parameters)

> If you have any questions, suggestions, bugs or want to contribute, please don't hesitate to contact :-

- spantshwa.lukho@gmail.com
