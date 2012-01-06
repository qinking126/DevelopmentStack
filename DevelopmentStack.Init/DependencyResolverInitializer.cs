using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DevelopmentStack.NHibernateProvider;
using NHibernate;
using SharpLite.Domain.DataInterfaces;
using SharpLite.NHibernateProvider;
using StructureMap;

namespace DevelopmentStack.Init
{
    public class DependencyResolverInitializer
    {
        public static void Initialize()
        {
            Container container = new Container(x =>
            {
                x.For<ISessionFactory>()
                    .Singleton()
                    .Use(() => NHibernateInitializer.Initialize().BuildSessionFactory());
                x.For<IEntityDuplicateChecker>().Use<EntityDuplicateChecker>();
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                x.For(typeof(IRepositoryWithTypedId<,>)).Use(typeof(RepositoryWithTypedId<,>));

                // This is a very exceptional case and you'd almost never be doing this; you'd
                // istead be using concreate query objects (e.g., MyStore.Domain.Queries.FindActiveCustomers)
                //x.For<IQueryForProductOrderSummaries>().Use<QueryForProductOrderSummaries>();
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        //public static object IQueryForProductOrderSummaries { get; set; }
    }
}
