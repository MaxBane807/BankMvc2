using System;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using System.Threading.Tasks;
using Bank.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace Bank.SetUpSearch
{
    class Program
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        static void Main(string[] args)
        {
            //Setting up db context
            var optionsBuilder = new DbContextOptionsBuilder<BankAppDataContext>();
            var context = new BankAppDataContext(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options);

            string serviceName = Environment.GetEnvironmentVariable("SearchService");
            string indexName = "customersearch";
            string apiKey = Environment.GetEnvironmentVariable("SearchKey");

            // Create a SearchIndexClient to send create/delete index commands
            Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);
            SearchIndexClient idxclient = new SearchIndexClient(serviceEndpoint, credential);

            // Create a SearchClient to load and query documents
            SearchClient qryclient = new SearchClient(serviceEndpoint, indexName, credential);


            // Define an index schema using SearchIndex
            // Create the index using SearchIndexClient
            SearchIndex index = new SearchIndex(indexName)
            {
                Fields =
                {
                    new SimpleField("customerId", SearchFieldDataType.String) {IsKey = true, IsSortable = true},
                    new SearchableField("givenName") {IsSortable = true,IsHidden = true},
                    new SearchableField("surname") {IsSortable = true, IsHidden = true},
                    new SearchableField("city") {IsSortable = true, IsHidden = true},
                    new SimpleField("streetAddress",SearchFieldDataType.String) {IsSortable = true, IsHidden = true},
                    new SimpleField("nationalId",SearchFieldDataType.String) {IsSortable = true, IsHidden = true}
                }
            };

            idxclient.CreateIndex(index);

            //Fyller index med data
            var actions = new List<IndexDocumentsAction<CustomerIndex>>();

            foreach (var customers in context.Customers)
            {
                actions.Add(IndexDocumentsAction.Upload(new CustomerIndex
                {
                    CustomerId = customers.CustomerId.ToString(),
                    City = customers.City,
                    Givenname = customers.Givenname,
                    NationalId = customers.NationalId,
                    Streetaddress = customers.Streetaddress,
                    Surname = customers.Surname
                }));
            };

            IndexDocumentsBatch<CustomerIndex> batch = IndexDocumentsBatch.Create(actions.ToArray());

            IndexDocumentsOptions idxoptions = new IndexDocumentsOptions { ThrowOnAnyError = true };

            qryclient.IndexDocuments(batch, idxoptions);
        }      
    }
}
