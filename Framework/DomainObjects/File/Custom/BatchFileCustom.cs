using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

namespace DomainObjects
{
    public partial class BatchFile
    {
        public static BatchFile[] FindByClientId(int clientId)
        {
            string queryText =
                @"  from BatchFile b
                    where 
                    b.ClientId = :clientId";

            SimpleQuery<BatchFile> query = new SimpleQuery<BatchFile>(queryText);
            query.SetParameter("clientId", clientId);

            BatchFile[] results = query.Execute();
            return results;
        }

    }
}
