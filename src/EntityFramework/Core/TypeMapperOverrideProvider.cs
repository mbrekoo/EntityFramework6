﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity.Core
{
    public class TypeMapperOverrideProvider
    {

        private static object _lock = new object();
        private static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();


        public static void AddMap(Type from, Type to)
        {
            lock (_lock) {
                if (!_mappings.ContainsKey(from))
                    _mappings.Add(from, to);
            }
        }


        public static void AddMap<TSource, TDestination>() where TDestination : TSource
        {
            Type from = typeof(TSource);
            Type to = typeof(TDestination);
            AddMap(from, to);
        }

        public static void RemoveMap(Type from, Type to)
        {
            lock (_lock) {
                if (_mappings.ContainsKey(from))
                    _mappings.Remove(from);
            }
        }

        public static bool TryGetMap(Type from, out Type to)
        {
            return _mappings.TryGetValue(from, out to);
        }

    }
}
