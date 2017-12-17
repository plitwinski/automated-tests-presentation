using System;
using System.Collections.Generic;
using System.Text;

namespace Example.CoarseGrainedUTest.Messages
{
    public class MovieAddedMessage
    {
        public string CinemaName { get; set; }
        public string MovieName { get; set; }
    }
}
