using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity.Core
{
    /// <summary>
    /// Manages type overrides for entities
    /// </summary>
    public class TypeMapperOverrideProvider
    {

        private static object _lock = new object();
        private static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        /// <summary>
        /// Adds a mapping for a specific entity type
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void AddMap(Type from, Type to)
        {
            lock (_lock) {
                if (!_mappings.ContainsKey(from))
                    _mappings.Add(from, to);
            }
        }

        /// <summary>
        /// Adds a mapping for a specific entity type
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        public static void AddMap<TSource, TDestination>() where TDestination : TSource
        {
            Type from = typeof(TSource);
            Type to = typeof(TDestination);
            AddMap(from, to);
        }

        /// <summary>
        /// Removes a previously defined mapping
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void RemoveMap(Type from, Type to)
        {
            lock (_lock) {
                if (_mappings.ContainsKey(from))
                    _mappings.Remove(from);
            }
        }

        /// <summary>
        /// Tries to get the mapping for the specified entity type
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool TryGetMap(Type from, out Type to)
        {
            return _mappings.TryGetValue(from, out to);
        }

    }
}
