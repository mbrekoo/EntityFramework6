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
        /// Checks if a mapping exists for the specified base type
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static bool HasMap(Type from)
        {
            lock (_lock) {
                return _mappings.ContainsKey(from);
            }
        }

        /// <summary>
        /// Tries to get the mapping by the specified value type
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static bool TryGetByValue(Type to, out Type from)
        {
            lock (_lock) {

                foreach (var item in _mappings) {
                    if (item.Value == to) {
                        from = item.Key;
                        return true;
                    }
                }

                from = null;
                return false;
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
            lock (_lock) {
                return _mappings.TryGetValue(from, out to);
            }

        }

        /// <summary>
        /// Returns a dictionary of all current mappings
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Type,Type> GetAllMappings()
        {
            lock (_lock) {
                return _mappings.ToDictionary(x => x.Key, y => y.Value);
            }
        }

    }
}
