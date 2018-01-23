using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PeopleAPI.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        public List<PersonDTO> GetPeople()
        {
            try
            {
                HttpResponseMessage response = FetchPeople();

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var people = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Person>>(result);

                    var query = people
                                .Where(p => p.Pets != null)
                                .SelectMany(p => p.Pets, (parent, child) 
                                                            => new { parent.Gender,
                                                                     child.Type,
                                                                     child.Name })
                                .Where(t => t.Type.ToLower().Equals("cat"))
                                .OrderBy(t => t.Name)
                                .GroupBy(t => t.Gender)
                                .Select(g => new PersonDTO
                                {
                                    Gender = g.Key,
                                    PetName = g.Select(s => s.Name).ToList()
                                });

                    return query.ToList();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static HttpResponseMessage FetchPeople()
        {
            var url = "http://agl-developer-test.azurewebsites.net/people.json";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response;
        }
    }

    public class PersonDTO
    {
        public string Gender { get; set; }
        public List<String> PetName { get; set; }
    }
}
