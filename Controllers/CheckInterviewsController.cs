using CMRI_CandidateProject.Models;
using CMRI_CandidateProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CMRI_CandidateProject.Controllers;

[ApiController]
public class CheckInterviewsController(CandidateService candidateService) : ControllerBase
{
    [Route("api/CheckInterviews")]
    [HttpPost()]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<CheckInterviewsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CheckInterviewsRequest request)
    {
        // validate input request to not be in the past
        if(request.DateOfInterview < DateOnly.FromDateTime(DateTime.Now))
        {
            return BadRequest();
        }

        //var candidates = candidateService.GetCandidatesFromJson();
        var candidates = await candidateService.GetCandidatesAsync();

        if (candidates == null)
        {
            return NotFound();
        }

        var interviewCount = candidates?.Where(i => i.DateOfInterview == request.DateOfInterview).Count() ?? 0;
        
        return Ok(new CheckInterviewsResponse { NumberOfInterviews = interviewCount });
    }
}
