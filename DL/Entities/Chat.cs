﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public int? RepairOrderId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
