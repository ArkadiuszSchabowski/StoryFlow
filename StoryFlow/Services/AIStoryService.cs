using StoryFlow.Interfaces;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using System.Text.Json;

namespace StoryFlow.Services
{
    public class AIStoryService : IAIStoryService
    {
        private readonly IRepository<User> _repository;
        private readonly IEntityValidator<User> _userValidator;
        private readonly HttpClient _httpClient;

        public AIStoryService(IRepository<User> repository, IEntityValidator<User> userValidator, HttpClient httpClient)
        {
            _repository = repository;
            _userValidator = userValidator;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:11434");
        }

        public async Task<string?> GenerateStoryByUserHobby(int userId)
        {
            var user = await _repository.Get(userId);

            _userValidator.ThrowIsNull(user);

            var hobbyNames = user!.Hobbies.Select(h => h.Name).ToList();

            var prompt = $"Napisz krótką historię o osobie, która interesuje się: {string.Join(", ", hobbyNames)}. Max 5 zdań po angielsku.";

            var request = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("/api/generate", request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(
                json!,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (ollamaResponse!.Response == null)
            {
                throw new Exception("Unexpected server error. Try again later.");
            }
            return ollamaResponse.Response;
        }
    }
}
