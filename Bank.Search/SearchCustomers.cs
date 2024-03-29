﻿using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using System.IO;
using System.Linq;

namespace Bank.Search
{
    public class SearchCustomers : ISearchCustomers
    {

        SearchClient _qryClient;
        
        public SearchCustomers(string serviceName, string indexName, string apiKey)
        {           
            Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);

            // Create a SearchClient to load and query documents
            _qryClient = new SearchClient(serviceEndpoint, indexName, credential);
        }
        public CustomerResult GetPagedCustomerIds(string search, string sortField, bool asc, int pageSize, int currentPage)
        {
            
            SearchOptions options;
            SearchResults<CustomerIndex> results;

            if (string.IsNullOrWhiteSpace(sortField))
            {
                sortField = "customerId";
            }
            if (pageSize == 0)
            {
                pageSize = 50;
            }
            if (currentPage == 0)
            {
                currentPage = 1;
            }

            options = new SearchOptions()
            {
                OrderBy = { sortField + " " + (asc ? "asc" : "desc") },
                SearchMode = SearchMode.All,
                Select = { "customerId" },
                IncludeTotalCount = true,
                Size = pageSize,
                Skip = (currentPage - 1) * pageSize,
                QueryType = SearchQueryType.Full               
            };

            if (string.IsNullOrWhiteSpace(search))
            {
                results = _qryClient.Search<CustomerIndex>("*", options);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] terms = search.Split(new char[0],StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in terms)
                {
                    term.Append('~');
                }
                var searchString = builder.AppendJoin(' ', terms).ToString();
                
                results = _qryClient.Search<CustomerIndex>(searchString, options);
            }

            var resultContainer = new CustomerResult();

            foreach (var result in results.GetResults())
            {
                resultContainer.PagedResultIds.Add(int.Parse(result.Document.CustomerId));
            }
            resultContainer.ResultCount = int.Parse(results.TotalCount.Value.ToString());

            return resultContainer;
        }
    }
}
