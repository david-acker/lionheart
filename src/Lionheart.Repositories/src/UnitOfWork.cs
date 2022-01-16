using Lionheart.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lionheart.Repositories;

public class UnitOfWork : IUnitOfWork
{
    // TODO: Create some sort of connection service to be injected here.
    public UnitOfWork()
    {
    }

    public IDbTransaction StartTransaction()
    {
        throw new NotImplementedException();
    }

    public void CommitTransaction()
    {
        throw new NotImplementedException();
    }
}
