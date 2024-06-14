// Copyright 2013-2019 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers 
{
    /// <summary>
    /// Enriches log events with a ThreadName property containing the <see cref="Thread.CurrentThread"/> <see cref="Thread.Name"/>.
    /// </summary>
    sealed class ThreadNameEnricher : ILogEventEnricher 
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        const string ThreadNamePropertyName = "ThreadName";

        /// <summary>
        /// The cached last created "ThreadName" property with some thread name. It is likely to be reused frequently so avoiding heap allocations.
        /// </summary>
        private LogEventProperty? _lastValue;

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) 
        {
            var threadName = Thread.CurrentThread.Name;
            if (threadName is not null) 
            {
                var last = _lastValue;
                if (last is null || (string)((ScalarValue)last.Value).Value! != threadName)
                    // no need to synchronize threads on write - just some of them will win
                    _lastValue = last = new LogEventProperty(ThreadNamePropertyName, new ScalarValue(threadName));

                logEvent.AddPropertyIfAbsent(last);
            }
        }
    }
}
