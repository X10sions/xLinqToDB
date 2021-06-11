﻿using NHibernate.Engine;
using System.Linq.Expressions;

namespace NHibernate_v5_2.Linq {
  public class NhLinqDmlExpression<T> : NhLinqExpression {
    protected override QueryMode QueryMode { get; }

    /// <summary>
    /// Entity type to insert or update when the expression is a DML.
    /// </summary>
    protected override System.Type TargetType => typeof(T);

    public NhLinqDmlExpression(QueryMode queryMode, Expression expression, ISessionFactoryImplementor sessionFactory)
      : base(expression, sessionFactory) {
      Key = $"{queryMode.ToString().ToUpperInvariant()} {Key}";
      QueryMode = queryMode;
    }
  }
}