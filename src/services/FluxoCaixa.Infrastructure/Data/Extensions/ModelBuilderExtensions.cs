using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluxoCaixa.Infrastructure.Data.Extensions;

internal static class ModelBuilderExtensions
{
	public static void UseDatabasePropertiesAsUppperCase(this ModelBuilder modelBuilder)
	{
		foreach (var entity in modelBuilder.Model.GetEntityTypes())
		{
			SetTableNameAsToUpper(entity);
			SetPropertiesAndKeysAsToUpper(entity);
		}
	}

	private static void SetTableNameAsToUpper(IMutableEntityType entity)
		=> entity.SetTableName(GetTableName(entity));

	private static void SetPropertiesAndKeysAsToUpper(IMutableEntityType entity)
	{
		foreach (var property in entity.GetProperties())
		{
			var column = property.GetColumnName(StoreObjectIdentifier.Table(GetTableName(entity), entity.GetSchema()));
			if (column != null)
				property.SetColumnName(column.ToUpper());
		}

		foreach (var key in entity.GetKeys())
		{
			var newKey = key.GetName();
			if (newKey != null)
				key.SetName(newKey.ToUpper());
		}

		foreach (var key in entity.GetForeignKeys())
		{
			var newKey = key.GetConstraintName();
			if (newKey != null)
				key.SetConstraintName(newKey.ToUpper());
		}
	}

	private static string GetTableName(IMutableEntityType entity)
	{
		var entityName = entity.GetTableName();
		ArgumentNullException.ThrowIfNull(entityName);
		return entityName.ToUpper();
	}
}
