using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoClient.Model;

namespace ToDoClient.DataService
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5209" : "http://localhost:5290";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddToDoAsync(ToDoModel toDoModel)
        {
            if (!HasNetworkAccess(Connectivity.Current.NetworkAccess)) return;

            try
            {
                var jsonToDo = JsonSerializer.Serialize<ToDoModel>(toDoModel);
                var content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}/todo", content);
               
                if (response.IsSuccessStatusCode) await Shell.Current.DisplayAlert("Add ToDo", "Successfully added ToDo", "Ok");
                else Debug.WriteLine($"----> Response problem {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Some problems {ex.Message}");
            }
            return;
        }

        public async Task DeleteToDoAsync(int id)
        {
            if (!HasNetworkAccess(Connectivity.Current.NetworkAccess)) return;

            try
            {
                var response = await _httpClient.DeleteAsync($"{_url}/todo/del/{id}");
                
                if (response.IsSuccessStatusCode) await Shell.Current.DisplayAlert("Delete ToDo", "Successfully deleted ToDo", "Ok");
                else Debug.WriteLine($"----> Response problem {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Some problems {ex.Message}");
            }
            return;
        }

        public async Task<List<ToDoModel>> GetAllToDosAsync()
        {
            var toDos = new List<ToDoModel>();

            if (!HasNetworkAccess(Connectivity.Current.NetworkAccess)) return toDos;

            try
            {
                var response = await _httpClient.GetAsync($"{_url}/todo");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toDos = JsonSerializer.Deserialize<List<ToDoModel>>(content, _jsonSerializerOptions);
                }
                else Debug.WriteLine(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }

            return toDos;
        }

        public async Task UpdateToDoModelAsync(ToDoModel toDoModel)
        {
            if (!HasNetworkAccess(Connectivity.Current.NetworkAccess)) return;

            try
            {
                var jsonToDo = JsonSerializer.Serialize<ToDoModel>(toDoModel);
                var content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_url}/todo/{toDoModel.Id}", content);
               
                if (response.IsSuccessStatusCode) await Shell.Current.DisplayAlert("Update ToDo", "Successfully updated ToDo", "Ok");
                else Debug.WriteLine($"----> Response problem {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Some problems {ex.Message}");
            }
            return;
        }

        private bool HasNetworkAccess(NetworkAccess networkAccess)
        {
            if (networkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> Intenet problem");
                return false;
            }
            return true;
        }
    }
}
