namespace AppiSimo.Data.ContextExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public static class UpdateManyToManyExtension
    {
        public static async Task TryUpdateManyToMany<T, TKey>(this DbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
        {
            currentItems = currentItems.ToList();
            newItems = newItems.ToList();
            
            db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
            await db.Set<T>().AddRangeAsync(newItems.Except(currentItems, getKey));
        }

        static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(objA: null, objB: t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }
    }
}