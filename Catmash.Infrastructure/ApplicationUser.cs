using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Catmash.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
    }

}
