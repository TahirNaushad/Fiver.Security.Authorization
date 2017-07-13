using Microsoft.AspNetCore.Authorization;

namespace Fiver.Security.Authorization.Auth
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public AgeRequirement(int age)
        {
            this.Age = age;
        }

        public int Age { get; }
    }
}
