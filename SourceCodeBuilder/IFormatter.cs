using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public interface IFormatter<T> where T : class
    {
        string ToString(T o);
        void Clear();
    }
}
