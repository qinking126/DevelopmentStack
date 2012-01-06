using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;

namespace DevelopmentStack.NHibernateProvider.Override
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
