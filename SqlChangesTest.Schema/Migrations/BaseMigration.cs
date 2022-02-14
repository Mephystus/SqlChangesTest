namespace SqlChangesTest.Schema.Migrations;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

[DbContext(typeof(TheContext))]
public abstract class BaseMigration : Migration
{
    #region Protected Methods

    protected static void Up<T>(MigrationBuilder builder, string path = "") where T : class
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            path = Path.Combine(AppContext.BaseDirectory, "Scripts", "Schema");
        }

        var attribute =
            Attribute.GetCustomAttribute(
                typeof(T),
                typeof(MigrationAttribute));

        var migrationAttribute = GetMigrationAttribute(attribute);

        var file = Path.Combine(path, migrationAttribute.Id);

        if (!File.Exists(file))
        {
            throw new FileNotFoundException("The file was not found.", file);
        }

        builder.Sql(File.ReadAllText(file));
    }

    #endregion Protected Methods

    #region Private Methods

    private static MigrationAttribute GetMigrationAttribute(Attribute? attribute)
    {
        if (attribute == null)
        {
            throw new ArgumentNullException(nameof(attribute));
        }

        return (MigrationAttribute)attribute;
    }

    #endregion Private Methods
}