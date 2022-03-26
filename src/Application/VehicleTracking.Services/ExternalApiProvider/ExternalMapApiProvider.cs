using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VehicleTracking.Service.ExternalApiProvider.Resource;

namespace VehicleTracking.Service.ExternalApiProvider
{
    public class ExternalMapApiProvider : IExternalMapApiProvider
    {
        private readonly HttpClient _client;        
        private readonly MapOption _provider;
        public ExternalMapApiProvider(HttpClient client, IOptions<MapOption> mapProvider)
        {
            _client = client;
            _provider = mapProvider.Value;
            
        }

        public async Task<string> GetLocation(decimal latitute, decimal logitute)
        {            
            string location = string.Empty;           
            string point = latitute.ToString() +"," + logitute.ToString();
            string uri = _provider.URL+ $"{point}?includeEntityTypes=Address,CountryRegion&key={_provider.ApiKey}";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.GetAsync(uri);
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                RootObject rootObj = JsonSerializer.Deserialize<RootObject>(json);
                Address add;
                var resources = rootObj.resourceSets != null && rootObj.resourceSets.Count > 0 ? rootObj.resourceSets[0].resources : null;

                if (resources != null && resources.Count > 0)
                {
                    add = resources[0].address;
                    location = add !=null ? add.locality + " " + add.countryRegion : "";
                }                
            }
           
            return location;
        }
    }
}
