﻿using System.Reflection;

namespace System.Data.Linq;

static class SubqueryRules {
  /// <summary>
  /// This list of top-level methods that are supported in subqueries.
  /// </summary>
  /// <param name="mi"></param>
  /// <returns></returns>
  static internal bool IsSupportedTopLevelMethod(MethodInfo mi) {
    if (!IsSequenceOperatorCall(mi))
      return false;
    switch (mi.Name) {
      case "Where":
      case "OrderBy":
      case "OrderByDescending":
      case "ThenBy":
      case "ThenByDescending":
      case "Take":
        return true;
    }
    return false;
  }
  private static bool IsSequenceOperatorCall(MethodInfo mi) {
    Type declType = mi.DeclaringType;
    if (declType == typeof(System.Linq.Enumerable) ||
        declType == typeof(System.Linq.Queryable)) {
      return true;
    }
    return false;
  }
}