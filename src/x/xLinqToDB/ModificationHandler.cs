﻿using LinqToDB.Linq;
using System.Linq.Expressions;

namespace LinqToDB;
public class ModificationHandler<T> : IModificationHandler<T> where T : class {
  ITable<T> Table { get; }
  Expression<Func<T, bool>> Predicate { get; set; }


  public Type EntityType => typeof(T);
  //public IQueryable<T> Queryable => Table.Where(Predicate);
  public IValueInsertable<T> Insertable { get; }
  public IUpdatable<T> Updatable { get; }

  public ModificationHandler(IDataContext dataContext, Expression<Func<T, bool>> predicate) : this(dataContext.GetTable<T>(), predicate) { }

  public ModificationHandler(ITable<T> table, Expression<Func<T, bool>> predicate) {
    Table = table;
    Predicate = predicate;
    //Queryable = table.Where(predicate);
    //Deletable = table.Where(wherePredicate);
    Updatable = table.Where(predicate).AsUpdatable();
    Insertable = table.AsValueInsertable();//  table.DataContext.Into(table);
  }

}