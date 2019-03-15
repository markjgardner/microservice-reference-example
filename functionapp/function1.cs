namespace lasercat {
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.Cosmos.Table;
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.Extensions.Logging;
  using Newtonsoft.Json;

  public static class functions {

    private static readonly string STORAGE_CONNECTION = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
    private const string EMPLOYEE_TABLE = "employees";


    [FunctionName("GetEmployee")]
    public static async Task<employee> Get (
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employee/")]HttpRequest req, 
      ILogger log
    ) {
      var partitionkey = req.Query["storeId"];
      var rowkey = req.Query["employeeId"];

      var result = await ExecEmployeeOperation(TableOperation.Retrieve<employee>(partitionkey, rowkey));
      
      return (employee)result.Result;
    }


    [FunctionName("PutEmployee")]
    public static async Task<IActionResult> PutPost (
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", "put", Route = "employee/")]HttpRequest req, 
      ILogger log
    ) {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var employee = JsonConvert.DeserializeObject<employee>(requestBody);

      var result = await ExecEmployeeOperation(TableOperation.InsertOrReplace(employee));
      
      var response = new CreatedResult(string.Empty, JsonConvert.SerializeObject(result.Result));
      return response;
    }

    private static async Task<TableResult> ExecEmployeeOperation(TableOperation operation) {
      var storageAccount = CloudStorageAccount.Parse(STORAGE_CONNECTION);
      var client = storageAccount.CreateCloudTableClient();
      var table = client.GetTableReference(EMPLOYEE_TABLE);
      await table.CreateIfNotExistsAsync();
      return await table.ExecuteAsync(operation);
    }
  }
}