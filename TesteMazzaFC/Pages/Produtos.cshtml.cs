using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TesteMazzaFC.Pages.Shared
{
    public class ProdutosModel : PageModel
    {
        public List<ProdutosModel> produtos { get; set; }
        public async void OnGet()
        {
            produtos = new List<ProdutosModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
               
               

                var result = await client.GetAsync("/api/produtos");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(response);
                    var produtosJson = jObject.GetValue("produtos").ToString();

                    produtos = JsonConvert.DeserializeObject<List<ProdutosModel>>(produtosJson);
                   
                    
                }
            }
            }
        }
}
