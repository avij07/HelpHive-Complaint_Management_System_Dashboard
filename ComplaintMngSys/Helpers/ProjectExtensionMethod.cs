using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplaintMngSys.Helpers
{
    public static class ProjectExtensionMethod
    {
        public static IEnumerable<TO> Map<TI, TO>(this IEnumerable<TI> seznam, Func<TI, TO> mapper)
        {
            return seznam.Select(mapper);
        }
    }
}
