using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }

    public interface IAuditable
    {
        bool IsNew { get; }
        Guid CreateUserId { get; set; }
        DateTime? CreateDateTime { get; set; }
        Guid? ChangeUserId { get; set; }
        DateTime? ChangeDateTime { get; set; }
    }
}
