using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _keySelector;

        public GenericEqualityComparer(Func<T, object> keySelector)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        }

        public bool Equals(T x, T y)
        {
            if (x == null || y == null) return false;
            return Equals(_keySelector(x), _keySelector(y));
        }

        public int GetHashCode(T obj)
        { 
            if (obj == null) return 0;
            var key = _keySelector(obj);
            return key?.GetHashCode() ?? 0;
        }

    }
}
