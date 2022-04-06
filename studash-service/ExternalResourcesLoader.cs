using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using studash_service.Context;

namespace studash_service
{
    public class ExternalResourcesLoader
    {
        public static Dictionary<string,DatabaseConnectionData> LoadDatabasesData()
        {
            string jsonString = File
                .ReadAllText("C:/Repository/C#/studash-service/studash-service/databases.json");
            return JsonSerializer.Deserialize<Dictionary<string,DatabaseConnectionData>>(jsonString);
        }

        public static IDatabaseConnectionData LoadDatabaseData(string connectionName)
        {
            var databasesData = LoadDatabasesData();
            return databasesData[connectionName];
        }
    }
}