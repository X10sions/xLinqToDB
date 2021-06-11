﻿using Remotion.Linq.Clauses;

namespace NHibernate_v5_2_6.Linq.Visitors.ResultOperatorProcessors
{
    public abstract class ResultOperatorProcessorBase
    {
        public abstract void Process(ResultOperatorBase resultOperator, QueryModelVisitor queryModel, IntermediateHqlTree tree);
    }
}