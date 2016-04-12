using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.DDDBase
{
    public interface IEntity
    {
        Guid Id
        {
            get;
        }

        DateTime CreatedAt
        {
            get;
        }
    }
}
