namespace SqlChangesTest.Schema.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

[Migration("20220211.001_MyTable.sql")]
public partial class MyTableMigration : BaseMigration
{
    #region Protected Methods

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        Up<MyTableMigration>(migrationBuilder);
    }

    #endregion Protected Methods
}