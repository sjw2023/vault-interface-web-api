using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.CopyDesign;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
    public class VaultRecommendationService
    {
        private VDF.Vault.Currency.Connections.Connection _connection;
        private VDF.Vault.Results.LogInResult _result;

        // Authenticate and connect to Vault
        public void ConnectToVault(string server, string vaultName, string username, string password)
        {
            _result = VDF.Vault.Library.ConnectionManager.LogIn(
                "192.168.10.250", "DTcenter", "DTcenter", "1234"
                , VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);
            _connection = _result.Connection;
        }

        // Search items with advanced recommendation logic
        public List<ItemRecommendation> GetRecommendations(string query)
        {
            List<ItemRecommendation> recommendations = new List<ItemRecommendation>();

            SrchStatus tempStatus = null;
            string bookmark = null;
            using (_connection.WebServiceManager.DocumentService)
            {
                // Define fuzzy and partial match criteria
                SrchCond searchCondition = new SrchCond
                {
                    PropDefId = 56, // Vault Item Name Property
                    PropTyp = PropertySearchType.SingleProperty,
                    SrchOper = 7, // Use Wildcard for partial matches
                    SrchTxt = $"{query}*"

                };
                List<Autodesk.Connectivity.WebServices.Item> items = new List<Autodesk.Connectivity.WebServices.Item>();
                items.AddRange(_connection.WebServiceManager.ItemService.FindItemRevisionsBySearchConditions(
                    new[] { searchCondition }, null,
                    true, ref bookmark, out tempStatus));

                // Transform results into a ranked recommendation list
                foreach (var item in items)
                {
                    if (item.ItemNum.StartsWith(query))
                    {
                        recommendations.Add(new ItemRecommendation
                                            {
                                                Name = item.ItemNum,
                                                // Description = item.FilePath + " - " + item.Comment,
                                                // Popularity = item.Hits // Assuming each item tracks usage popularity
                                            });
                    Console.WriteLine(item.ItemNum);
                    }
                }
            }

            VDF.Vault.Library.ConnectionManager.LogOut(_connection);

            // Further refine results (e.g., remove duplicates, apply custom ranking)
            return recommendations
                .OrderByDescending(r => r.Popularity) // Rank by popularity
                .Take(10) // Limit to top 10 suggestions
                .ToList();
        }

        // Define a structured recommendation model
        public class ItemRecommendation
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Popularity { get; set; } // Popularity score (custom logic required)
        }
    }
}