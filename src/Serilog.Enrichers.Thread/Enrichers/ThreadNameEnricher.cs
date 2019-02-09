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

namespace Serilog.Enrichers {
#if NET45 || NETSTANDARD2_0
    /// <summary>
    /// Enriches log events with a ThreadName property containing the 
    /// </summary>
    public class ThreadNameEnricher : ILogEventEnricher 
    {

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string ThreadNamePropertyName = "ThreadName";


        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) 
        {
            logEvent.AddPropertyIfAbsent(new LogEventProperty(ThreadNamePropertyName, new ScalarValue(Thread.CurrentThread.Name)));
        }
    }
#endif
}