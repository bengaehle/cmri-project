using System.Text.Json;
using CMRI_CandidateProject.Models;

namespace CMRI_CandidateProject.Services;

public sealed class CandidateService
{
    private readonly HttpClient _client;

    public CandidateService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CandidateInterview>?> GetCandidatesAsync()
    {
        var candidates = await _client.GetFromJsonAsync<List<CandidateInterview>>("api/getcandidates");

        return candidates;
    }

    // Testing with data from JSON so we don't repeatedly hit endpoint.
    public List<CandidateInterview>? GetCandidatesFromJson()
    {
        string fileName = Path.Combine("Data", "testGetCandidatesResponse.json");
        using var jsonFileReader = System.IO.File.OpenText(fileName);

        var candidates = JsonSerializer.Deserialize<List<CandidateInterview>>(jsonFileReader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

        return candidates;
    }
}
