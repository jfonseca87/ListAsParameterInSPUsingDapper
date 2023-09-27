using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connection = "Server=localhost;Port=3306;Database=Test;Uid=root;Pwd=Abc.123456;";
            List<int> ids = new List<int>
            {
                1,2,3,4
            };

            string jsonIds = JsonConvert.SerializeObject(ids);

            using var conn = new MySqlConnection(connection);

            try
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync<AssetReview>("new_procedure_two", new { numberlist = jsonIds }, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    internal class AssetReview
    {
        public int id { get; set; }
        public string version { get; set; }
        public string review_action { get; set; }
        public DateTime date_action { get; set; }
        public int asset_id { get; set; }
    }
}
