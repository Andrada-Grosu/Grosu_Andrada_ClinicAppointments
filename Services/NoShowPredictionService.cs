using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Grosu_Andrada_ClinicAppointments.Models;

namespace Grosu_Andrada_ClinicAppointments.Services
{
    public class NoShowPredictionService : INoShowPredictionService
    {
        private readonly HttpClient _httpClient;

        public NoShowPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PredictNoShowAsync(ModelInput input)
        {
            // POST către endpoint-ul generat de Model Builder
            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<NoShowApiResponse>();

            if (result == null)
            {
                return "Nu s-a putut calcula predicția.";
            }

            // Interpretăm predictedLabel + probability într-un mesaj pentru UI
            if (result.PredictedLabel)
            {
                return $"Pacientul are probabilitate mai mare să NU se prezinte (probabilitate ~{result.Probability:P0}).";
            }
            else
            {
                return $"Pacientul are probabilitate mai mare să se prezinte (probabilitate ~{result.Probability:P0}).";
            }
        }

        // Mapare a răspunsului JSON
        private class NoShowApiResponse
        {
            public bool PredictedLabel { get; set; }
            public float Probability { get; set; }
        }
    }
}
