﻿using System.Linq.Expressions;

namespace System.Linq {
  public static class IQueryableExtensions {

    public static bool IsOrderedQueryable<T>(this IQueryable<T> queryable) {
      if (queryable == null) {
        throw new ArgumentNullException(nameof(queryable));
      }
      return queryable.Expression.Type == typeof(IOrderedQueryable<T>);
    }

    public static IOrderedQueryable<T> AddOrdering<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, bool descending) {
      if (source.Expression.Type != typeof(IOrderedQueryable<T>)) {
        return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
      }
      return descending ? ((IOrderedQueryable<T>)source).ThenByDescending(keySelector) : ((IOrderedQueryable<T>)source).ThenBy(keySelector);
    }

    //public static IOrderedQueryable<T> OrderByIf<T, TKey>(this IQueryable<T> source, bool condition, Expression<Func<T, TKey>> keySelector, IComparer<TKey> comparer) => condition ? source.OrderBy(keySelector, comparer) : source;
    //public static IOrderedQueryable<T> OrderByIf<T, TKey>(this IQueryable<T> source, bool condition, Expression<Func<T, TKey>> keySelector) => condition ? source.OrderBy(keySelector) : source;
    //public static IOrderedQueryable<T> OrderByDescendingIf<T, TKey>(this IQueryable<T> source, bool condition, Expression<Func<T, TKey>> keySelector, IComparer<TKey> comparer) => condition ? source.OrderByDescending(keySelector, comparer) : source;
    //public static IOrderedQueryable<T> OrderByDescendingIf<T, TKey>(this IQueryable<T> source, bool condition, Expression<Func<T, TKey>> keySelector) => condition ? source.OrderByDescending(keySelector) : source;

    public static IQueryable<T> Skip<T>(this IQueryable<T> query, int? skip)  => skip.HasValue ? query.Skip(skip.Value) : query;
    public static IQueryable<T> Take<T>(this IQueryable<T> query, int? take) => take.HasValue ? query.Take(take.Value) : query;

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate) => condition ? source.Where(truePredicate) : source.Where(falsePredicate);
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> truePredicate, Expression<Func<T, int, bool>> falsePredicate) => condition ? source.Where(truePredicate) : source.Where(falsePredicate);

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate) => condition ? source.Where(predicate) : source;
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate) => condition ? source.Where(predicate) : source;

    public static IQueryable<T> WhereIfNotNull<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate) => predicate != null ? source.Where(predicate) : source;
    public static IQueryable<T> WhereIfNotNull<T>(this IQueryable<T> source, Expression<Func<T, int, bool>>? predicate) => predicate != null ? source.Where(predicate) : source;

  }
}