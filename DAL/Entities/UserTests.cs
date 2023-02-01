using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserTests: BaseEntity
    {
        public string UserId { get; set; }
        public string TestId { get; set; }
        public Test? Test { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
