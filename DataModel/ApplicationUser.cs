using Microsoft.AspNetCore.Identity;

namespace RadMock2021.DataModel
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string SecondName { get; set; }

    }

}
