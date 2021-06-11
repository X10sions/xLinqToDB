// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore_v2_2_7.Diagnostics
{
    /// <summary>
    ///     A <see cref="DiagnosticSource" /> event payload class for events that have
    ///     a navigation property.
    /// </summary>
    public class NavigationPathEventData : EventData
    {
        /// <summary>
        ///     Constructs the event payload.
        /// </summary>
        /// <param name="eventDefinition"> The event definition. </param>
        /// <param name="messageGenerator"> A delegate that generates a log message for this event. </param>
        /// <param name="navigationPath"> The navigation property. </param>
        public NavigationPathEventData(
            [NotNull] EventDefinitionBase eventDefinition,
            [NotNull] Func<EventDefinitionBase, EventData, string> messageGenerator,
            [NotNull] IReadOnlyCollection<IPropertyBase> navigationPath)
            : base(eventDefinition, messageGenerator)
        {
            NavigationPath = navigationPath;
        }

        /// <summary>
        ///     The navigation property.
        /// </summary>
        public virtual IReadOnlyCollection<IPropertyBase> NavigationPath { get; }
    }
}
