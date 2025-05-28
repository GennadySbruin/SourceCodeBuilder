using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public interface ICodeWriter<T> where T : class
    {
        string WriteCode(T o);
        void Clear();
    }
}
