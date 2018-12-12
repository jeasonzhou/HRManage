using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Database.Interfaces
{
    interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
