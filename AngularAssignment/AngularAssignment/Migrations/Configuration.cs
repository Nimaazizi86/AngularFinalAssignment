namespace AngularAssignment.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularAssignment.PeopleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AngularAssignment.PeopleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.People.AddOrUpdate(
              p => p.Firstname,
              new Person { Firstname = "Andrew", Lastname = "Peters" },
              new Person { Firstname = "Brice", Lastname = "Lambson" },
              new Person { Firstname = "Rowan", Lastname = "Miller" }

            );
            //
        }
    }
}
