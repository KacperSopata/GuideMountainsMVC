using GuideMountainsMVC.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatBotService _chatBotService;

        public ChatController(ChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userPrompt)
        {
            if (string.IsNullOrWhiteSpace(userPrompt))
            {
                return Json(new { answer = "Proszę wpisać wiadomość." });
            }

            var response = await _chatBotService.GetAnswerAsync(userPrompt);
            return Json(new { answer = response });
        }
    }
}
