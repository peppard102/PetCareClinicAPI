using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Services;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralQuestionsController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly ILogger<GeneralQuestionsController> _logger;

        public GeneralQuestionsController(OpenAIService openAIService, ILogger<GeneralQuestionsController> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            try
            {
                string answer = await _openAIService.ProcessConversationAsync("You are a helpful veterinary assistant giving general medical advice.", request.ConversationHistory);

                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing chat service.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class QuestionRequest
    {
        public required QuestionAnswer[] ConversationHistory { get; set; }
    }
    
    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
} 