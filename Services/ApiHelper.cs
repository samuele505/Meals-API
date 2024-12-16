using Newtonsoft.Json;
using RestSharp;

namespace Meals_API.Services
{
    public static class ApiHelper
    {
        public static async Task<T> ExecuteRequestAsync<T>(RestClient client, string endPoint) where T : class
        {
            try
            {
                var request = new RestRequest(endPoint, Method.Get);
                var response = await client.ExecuteAsync(request);

                if (response == null || !response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                {
                    throw new Exception($"La risposta non ha avuto successo: {response?.ErrorException?.Message ?? "Risposta inesistente"}");
                }

                var deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content);

                if (deserializedResponse == null)
                {
                    throw new Exception($"La deserializzazione della risposta non è riuscita");
                }

                return deserializedResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la richiesta all'endpoint '{endPoint}'\n{ex.Message}");
            }
        }
    }
}
