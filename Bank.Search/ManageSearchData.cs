using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;

namespace Bank.Search
{
    public class ManageSearchData : IManageSearchData
    {
        SearchClient _qryClient;
        
        public ManageSearchData()
        {
            string serviceName = Environment.GetEnvironmentVariable("SearchService");
            string indexName = "customersearch";
            string apiKey = Environment.GetEnvironmentVariable("SearchKey");
            Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);

            // Create a SearchClient to load and query documents
            _qryClient = new SearchClient(serviceEndpoint, indexName, credential);
        }

        public void CreateCustomerData(CustomerIndex customerToCreate)
        {
            IndexDocumentsBatch<CustomerIndex> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(customerToCreate));
            IndexDocumentsOptions idxoptions = new IndexDocumentsOptions { ThrowOnAnyError = true };

            _qryClient.IndexDocuments(batch,idxoptions);
        }
        public void UpdateCustomerData(CustomerIndex customerToUpdate)
        {
            IndexDocumentsBatch<CustomerIndex> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Merge(customerToUpdate));
            IndexDocumentsOptions idxoptions = new IndexDocumentsOptions { ThrowOnAnyError = true };

            _qryClient.IndexDocuments(batch, idxoptions);
        }
        public void DeleteCustomerData(CustomerIndex customerToDelete)
        {
            IndexDocumentsBatch<CustomerIndex> batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Delete(customerToDelete));
            IndexDocumentsOptions idxoptions = new IndexDocumentsOptions { ThrowOnAnyError = true };

            _qryClient.IndexDocuments(batch, idxoptions);
        }
    }
}
