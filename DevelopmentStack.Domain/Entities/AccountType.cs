using SharpLite.Domain;


namespace DevelopmentStack.Domain.Entities
{
    public class AccountType : Entity
    {
        [DomainSignature]
        public virtual string Name { get; set; }

        public AccountType()
        {
        }
    }
}
