using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    class Column
    {
        public readonly int limit;
        public readonly string type;

        internal Column(string type,int limit)
        {
            this.limit = limit;
            this.type = type;
        }

    }
}
