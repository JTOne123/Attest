﻿using System.Collections;
using System.Collections.Generic;
using Attest.Testing.Core;

namespace Attest.Testing.xUnit
{
    internal class Scenario : IScenario
    {
        public void Add(string key, object value)
        {
            Properties.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return Properties.Contains(key);
        }

        public void Clear()
        {
            Properties.Clear();
        }

        public object this[string key]
        {
            get => Properties[key];
            set => Properties[key] = value;
        }

        private IDictionary Properties { get; } = new Dictionary<string, object>();
    }
}