using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HelpdeskDAL
{
    /* Interface class to force classes to "inherit" an ID and timer */
    public class HelpdeskEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timer { get; set; }

    }
}
