using Domain.Entities;

namespace NHibernateConfigs.Maps
{
    public class WorkDescriptionMap : PersistingObjectMap<WorkDescription>
    {
        public WorkDescriptionMap()
        {
            Property(wd => wd.Comment, m => m.Column("comment"));
            LazyReferenceTo(wd => wd.Request, "req_id");
            LazyReferenceTo(wd => wd.User, "user_id");
        }
    }
}