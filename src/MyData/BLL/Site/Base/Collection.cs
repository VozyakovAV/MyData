using System;
using System.Collections.Generic;
using System.Linq;

namespace MyData.BLL
{
    public class CollectionTypes
    {
        private Dictionary<Type, object> _list;

        Func<Type, object[], object> _create_object;

        public CollectionTypes(Func<Type, object[], object> create_object)
        {
            _create_object = create_object;
        }

        public T Get<T>(object[] parameters) where T : class
        {
            lock (this)
            {
                if (_list == null)
                    _list = new Dictionary<Type, object>();

                T res;
                var type = typeof(T);
                if (_list.ContainsKey(type))
                    res = (T)_list[type];
                else
                {
                    res = (T)_create_object.Invoke(type, parameters);
                    _list.Add(type, res);
                }
                return res;
            }
        }
    }
}