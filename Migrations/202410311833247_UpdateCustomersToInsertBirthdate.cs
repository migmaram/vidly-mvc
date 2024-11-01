namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomersToInsertBirthdate : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET birthdate = '1822-12-27' WHERE Id = 1");
            Sql("UPDATE Customers SET birthdate = '1843-12-11' WHERE Id = 2");
            Sql("UPDATE Customers SET birthdate = '1797-08-30' WHERE Id = 3");
        }
        
        public override void Down()
        {
        }
    }
}
