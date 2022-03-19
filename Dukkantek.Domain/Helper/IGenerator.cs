using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Helper
{
    public interface IGenerator
    {
        string GenerateNumericalCode(int DigitsNumber);
    }
}
