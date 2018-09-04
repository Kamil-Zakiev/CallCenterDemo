using System;
using System.Linq.Expressions;
using Domain.Entities;
using NHibernate.Mapping.ByCode;

namespace NHibernateConfigs.Maps
{
    public class RequestMap:PersistingObjectMap<Request>
    {
        public RequestMap()
        {
            Property(req => req.ConsumerName, m => m.Column("consumer_name"));
            Property(req => req.Phone, m => m.Column("phone"));
            Property(req => req.Comment, m => m.Column("comment"));
            Property(req => req.Date, m => m.Column("date"));
            Property(req => req.State, m => m.Column("state"));

            LazyReferenceTo(req => req.Category, "cat_id");
            LazyReferenceTo(req => req.Author, "user_id");
            LazyReferenceTo(req => req.Executor, "executor_id", notNull: false);
            Property(req => req.WorkerComment, m => m.Column("worker_comment"));
        }
    }
}