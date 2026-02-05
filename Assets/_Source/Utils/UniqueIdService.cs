using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utils
{
    public class UniqueIdService
    {
        private readonly Dictionary<Type, int> _uniqueId = new();

        public int GenerateId<T>()
        {
            var type = typeof(T);

            if (!_uniqueId.TryAdd(type, 0))
                _uniqueId[type]++;

            return _uniqueId[type];
        }
    }
}