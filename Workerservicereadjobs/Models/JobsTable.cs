using System;
using System.Collections.Generic;
using System.Text;

namespace Workerservicereadjobs.Models
{
  public  class JobsTable
    {
        public int Idtache { get; set; }
        public string themessage { get; set; }
        public int frequence { get; set; }
        public DateTime startat { get; set; }
        public DateTime endat { get; set; }
    }
}
