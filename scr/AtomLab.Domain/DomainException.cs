using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomLab.Domain
{
  public  class DomainException:Exception
  {
      public DomainException(string message) : base(message)
      {
          
      }
  }
}
