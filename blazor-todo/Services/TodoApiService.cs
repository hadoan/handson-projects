using System.Net.Http;
using blazor_todo.Dto;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace blazor_todo.Services
{
    public class TodoApiService
    {
        private HttpClient httpClient = new HttpClient();
        public async Task<System.Collections.Generic.List<Todo>> GetAll(){
            // return new System.Collections.Generic.List<Todo>{
            //     new Todo{ id=1, title="sample todo 1", completed=true},
            //     new Todo{ id=2, title="sample todo 2", completed=false},
            // };
            var jsonStr = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
            System.Console.WriteLine("Json string from api: "+jsonStr);
            var data = JsonConvert.DeserializeObject<List<Todo>>(jsonStr);

            return data;
        }
    }
}