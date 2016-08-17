using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

namespace DomainObjects
{
    public partial class BatchFileDescription
    {
        public static BatchFileDescription[] FindByBatchFileId(int batchFileId)
        {
            string queryText =
                @"  from BatchFileDescription b
                    where 
                    b.BatchFile.Id = :batchFileId";

            SimpleQuery<BatchFileDescription> query = new SimpleQuery<BatchFileDescription>(queryText);
            query.SetParameter("batchFileId", batchFileId);

            BatchFileDescription[] results = query.Execute();
            return results;
        }

    }
}
