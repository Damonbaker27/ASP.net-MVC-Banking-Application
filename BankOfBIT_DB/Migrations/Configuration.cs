namespace BankOfBIT_DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BankOfBIT_DB.Data.BankOfBIT_DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BankOfBIT_DB.Data.BankOfBIT_DBContext";
        }

        protected override void Seed(BankOfBIT_DB.Data.BankOfBIT_DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
