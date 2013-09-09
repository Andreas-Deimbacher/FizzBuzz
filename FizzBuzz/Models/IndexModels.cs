using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FizzBuzz.Models
{
    public class IndexModels
    {
        [Required(ErrorMessage ="Error")]
        public int? Input { get; set; }

        public string Output { get; set; }

        public bool tweetSend { get; set; }

        public string TweetMessage { get; set; }


    }
}