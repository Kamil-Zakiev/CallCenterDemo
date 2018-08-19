using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace NHibernateConfigs.Maps
{
    public class CategoryMap : PersistingObjectMap<Category>
    {
        public CategoryMap()
        {
            Property(cat => cat.Name, m => m.Column("name"));
        }
    }
}
