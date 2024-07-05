using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace OutOfOfficeWebApp.Utils
{
    public static class Extensions
    {
        public static EntityEntry<T>? AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null!) where T : class
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }

        public static void SeedEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter) 
            where T : EnumTable<TEnum>
            where TEnum : Enum
        { 
            Enum.GetValues(typeof(TEnum))
                .Cast<object>()
                .Select(value => converter((TEnum)value))
                .ToList()
                .ForEach(instance => dbSet.AddIfNotExists(instance));
        }

    }
}
