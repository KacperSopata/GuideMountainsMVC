using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IChatBotService
    {
        Task<string> GetAnswerAsync(string userPrompt);
    }
}
