using System;
using System.Linq.Expressions;
using Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateConfigs.Maps
{
    public abstract class PersistingObjectMap<T> : ClassMapping<T> where T : PersistingObject
    {
        protected PersistingObjectMap()
        {
            Table($"kz_{typeof(T).Name.ToLower()}");
            Id(po => po.Id, m =>
            {
                m.Column("id");
                m.Generator(Generators.Sequence, g => g.Params(new {sequence = "throughout_app_id_seq"}));
            });
        }

        protected void LazyReferenceTo<TRef>(Expression<Func<T, TRef>> propSelector, string colName) where TRef : class
        {
            ManyToOne(propSelector, m =>
            {
                m.Column(colName);
                m.NotNullable(true);
                m.Cascade(Cascade.None);
                m.Fetch(FetchKind.Select);
            });
        }
    }
}