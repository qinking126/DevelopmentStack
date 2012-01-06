using System.Configuration;

namespace DevelopmentStack.Web.Infrastructure.Facebook
{
    public class FacebookSettings : ConfigurationSection
    {
        private static FacebookSettings settings = ConfigurationManager.GetSection("Facebook") as FacebookSettings;
        public static FacebookSettings Settings { get { return settings; } }

        [ConfigurationProperty("ClientId", IsRequired = true)]
        public string ClientId
        {
            get { return (string)this["ClientId"]; }
            set { this["ClientId"] = value; }
        }

        [ConfigurationProperty("ClientSecret", IsRequired = true)]
        public string ClientSecret
        {
            get { return (string)this["ClientSecret"]; }
            set { this["ClientSecret"] = value; }
        }

        [ConfigurationProperty("RedirectUri", IsRequired = true)]
        public string RedirectUri
        {
            get { return (string)this["RedirectUri"]; }
            set { this["RedirectUri"] = value; }
        }
    }
}