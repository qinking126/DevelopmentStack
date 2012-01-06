using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopmentStack.NHibernateProvider.Override;
using NHibernate.Mapping.ByCode;
using DevelopmentStack.Domain.Entities;

namespace DevelopmentStack.NHibernateProvider.Overrides
{
    internal class StackOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            
            mapper.Class<Stack>(map => 
                map.ManyToOne(
                            x => x.PostBy, 
                            manyToOne => 
                                        {
                                            manyToOne.Column("PostBy");
                                        }));
             
        }
    }
}
