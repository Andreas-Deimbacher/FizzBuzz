using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FizzBuzz.Models
{
    public class IndexModels
    {
        /* Input Value */
        [Required(ErrorMessage ="Error")]
        public int? Input { get; set; }

        /* Output Value */
        public string Output { get; set; }

        /* Tweet succesfull send */
        public bool tweetSend { get; set; }

        /* The Tweet Message */
        public string TweetMessage { get; set; }


    }
}