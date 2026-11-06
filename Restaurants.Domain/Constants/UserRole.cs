using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Domain.Constants
{
    public class UserRole
    {
        public const string Owner = "Owner";
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
