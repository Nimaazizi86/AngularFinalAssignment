using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace AngularAssignment
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("PeopleContext") { }

        public static PeopleContext Create()
        {
            return new PeopleContext();
        }

        public DbSet<Person> People { get; set; }

    }
}