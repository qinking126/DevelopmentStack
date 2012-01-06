using DevelopmentStack.Domain.Entities;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SharpLite.NHibernateProvider;
using DevelopmentStack.Domain;

namespace DevelopmentStack.NHibernateProvider
{
    public class NHibernateInitializer
    {
        public static Configuration Initialize()
        {
            var configuration = new Configuration();

            configuration
                .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                .DataBaseIntegration(db =>
                                         {
                                             db.ConnectionStringName = "DevelopmentStack";
                                             db.Dialect<MsSql2008Dialect>();
                                             db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                                         })
                .AddAssembly(typeof (Stack).Assembly)
                .CurrentSessionContext<LazySessionContext>();

            ConventionModelMapper mapper = new ConventionModelMapper();
            mapper.WithConventions(configuration);

            return configuration;
        }
    }
}
