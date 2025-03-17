using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class ChatCompletionResponse
    {
        public string Id { get; set; }  
        public string Object {  get; set; } 
        public long Created { get; set; }
        public List<ChatCompletionChoice> Choices { get; set; }
        public ChatCompletionUsage Usage { get; set; }
    }
}
