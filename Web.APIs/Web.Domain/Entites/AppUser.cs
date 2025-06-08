﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.Entites
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public Gender gender {  get; set; }
        public string? ProfileImage { get; set; }
        public ICollection<Property> properties { get; set; }  = new HashSet<Property>();
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
        public ICollection<PropertyReview> PropertyReviews { get; set; } = new HashSet<PropertyReview>();
    }
}
