using CsvHelper;
using CsvHelper.Configuration;
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class Program
    {

        public async static Task Main()
        {
            await OperationsCSV();
        }

        private async static Task OperationsCSV()
        {
            try
            {
                Console.WriteLine("Se esta leyendo el archivo CSV");
                string fileUri = Constants.urlCSV;

                using (var context = new DataAcces())
                {
                    await context.TruncateAsync<Importer>(typeof(Importer), default);
                }

                List<Importer> importers = new List<Importer>();

                WebClient client = new WebClient();
                Stream stream = client.OpenRead(fileUri);
                StreamReader readerFile = new StreamReader(stream);

                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    Delimiter = ";",
                };
                using (var memory = new MemoryStream())
                using (var reader = readerFile)
                using (var csv = new CsvReader(reader, config))
                {
                    memory.Position = 0;
                    csv.Context.RegisterClassMap<ImporterMapper>();
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    int flag = 0;
                    while (await csv.ReadAsync())
                    {
                        flag++;
                        var importer = csv.GetRecord<Importer>();
                        importers.Add(importer);
                        if (flag == 1000)
                        {
                            await Insert(importers);
                            importers = new List<Importer>();
                            flag = 0;
                        }
                    }
                    if (flag > 0)
                    {                         
                        Console.WriteLine("Se han insertado los datos en la BD", "/n", "Numero de registros insertados {}");
                    }
                }
                
            }
            catch (Exception ex)
            {
                var sss = ex.Message;
                throw;
            }
        }

        private async static Task Insert(List<Importer> importers)
        {
            using (var context = new DataAcces())
            {
                context.Database.SetCommandTimeout(0);
                var bulkConfig = new EFCore.BulkExtensions.BulkConfig
                {
                    PreserveInsertOrder = true,
                    SetOutputIdentity = true,
                    BatchSize = int.MaxValue,
                    UseTempDB = false,
                    CalculateStats = true
                };
                await context.BulkInsertAsync(importers, typeof(Importer), bulkConfig, null, default);
            }
        }
        
        private async static Task OperationsOld()
        {
            try
            {
                List<Importer> listImporter = new List<Importer>();
                Importer im = new Importer();
                WebClient myWebClient = new WebClient();
                string fileUri = Constants.urlCSV;
                //string fileUri = Constants.urlCSV2;
                List<string> bitString = new List<string>();
                byte[] myCSV = await myWebClient.DownloadDataTaskAsync(fileUri);
                //byte[] myCSV = myWebClient.DownloadData(fileUri);
                string result = System.Text.Encoding.UTF8.GetString(myCSV);
                //var s = JsonConvert.SerializeObject(result);
                String[] temp = result.Split('"');
                for (int i = 3; i < temp.Length; i = i + 2)
                {
                    var x = temp[i];
                    var r = x.Split(';');
                    for (int e = 0; e < r.Length; e++)
                    {

                        if (e == 0)
                        {
                            im.PointOfSale = r[e];
                        }
                        if (e == 1)
                        {
                            im.Product = r[e];
                        }
                        if (e == 2)
                        {
                            im.Date = r[e];
                        }
                        if (e == 3)
                        {
                            im.Stock = r[e];
                        }
                    }
                    listImporter.Add(im);
                }

                Console.WriteLine("My Data", listImporter.Count);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }
    }

    public class ImporterMapper : ClassMap<Importer>
    {
        public ImporterMapper()
        {
            Map(x => x.PointOfSale);
            Map(x => x.Product);
            Map(x => x.Date);
            Map(x => x.Stock);
        }
    }
}
