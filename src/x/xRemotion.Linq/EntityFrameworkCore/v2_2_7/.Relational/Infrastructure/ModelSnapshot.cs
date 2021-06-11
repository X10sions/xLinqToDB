// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     Base class for the snapshot of the <see cref="IModel" /> state generated by Migrations.
    /// </summary>
    public abstract class ModelSnapshot
    {
        private readonly LazyRef<IModel> _model;

        /// <summary>
        ///     Constructs a new snapshot instance.
        /// </summary>
        protected ModelSnapshot()
        {
            _model = new LazyRef<IModel>(
                () =>
                {
                    var modelBuilder = new ModelBuilder(new ConventionSet());
                    BuildModel(modelBuilder);

                    return modelBuilder.Model;
                });
        }

        /// <summary>
        ///     The snapshot model.
        /// </summary>
        public virtual IModel Model => _model.Value;

        /// <summary>
        ///     Called lazily by <see cref="Model" /> to build the model snapshot
        ///     the first time it is requested.
        /// </summary>
        /// <param name="modelBuilder"> The <see cref="ModelBuilder" /> to use to build the model. </param>
        protected abstract void BuildModel([NotNull] ModelBuilder modelBuilder);
    }
}
