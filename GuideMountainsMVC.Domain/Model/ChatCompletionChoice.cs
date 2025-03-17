using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class ChatCompletionChoice
    {
        public int Index { get; set; }
        public ChatCompletionMessage Message { get; set; }
        public string FinishReason { get; set; }
    }
}
