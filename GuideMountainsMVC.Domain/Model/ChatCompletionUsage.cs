using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class ChatCompletionUsage
    {
        public int PromptTokens { get; set; }   
        public int CompletionTokens { get; set; }   
        public int TotalTokens { get; set; }
    }
}
