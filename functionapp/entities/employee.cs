namespace lasercat {
  using System;
  using Microsoft.Azure.Cosmos.Table;

  public class employee: TableEntity {
    public employee(){

    }

    public employee(string storeId, string employeeId) {
      PartitionKey = storeId;
      RowKey = employeeId;
    }

    public string storeId {
      get {
        return PartitionKey;
      }
      set {
        PartitionKey = value;
      }
    }

    public string employeeId {
      get {
        return RowKey;
      }
      set {
        RowKey = value;
      }
    }

    public string firstName {get;set;}
    public string lastName {get;set;}
    public string SSN {get;set;}
    public string phone {get;set;}
  }
}