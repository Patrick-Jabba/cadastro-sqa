using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastroSqa.Interfaces
{
    public interface IUpdate<in T, out A>
    {
        A Update(int id, T obj);
    }
}