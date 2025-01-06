using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstraction
{
    public record Error (string code, string name)
    {
        public static Error None = new(string.Empty, string.Empty);
    }
}
