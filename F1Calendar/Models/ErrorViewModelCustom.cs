using System.Collections.Generic;

namespace F1Manager.Models
{
    public class ErrorViewModelCustom
    {
        public string MainMessage { get; set; }

        public List<string> Errors { get; set; }
    }
}