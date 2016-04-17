using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.DDDBase
{
    public interface IUnitOfWork
    {

        int Commit();

        void CommitAndRefreshChanges();

        void RollbackChanges();
    }
}
