using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Asteroids.DI.Attributes;

namespace Asteroids.DI
{
    public class DiContainer
    {
        private readonly Dictionary<Type, object> _container;
        private readonly Dictionary<Type, List<InjectedField>> _injectedFields;

        public DiContainer()
        {
            _container = new Dictionary<Type, object>();
            _injectedFields = new Dictionary<Type, List<InjectedField>>();
        }

        public void Bind<T>(T obj)
        {
            var type = typeof(T);
            if (!_container.ContainsKey(type))
            {
                _container.Add(type, obj);
            }
            else
            {
                _container[type] = obj;
            }

            if (_injectedFields.ContainsKey(type))
            {
                foreach (var injectedField in _injectedFields[type])
                {
                    injectedField.FieldInfo?.SetValue(injectedField.Object, obj);
                }
            }
        }

        public void Inject(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.GetCustomAttribute<InjectAttribute>() != null)
                {
                    var value = fieldInfo.GetValue(obj);
                    if (_container.ContainsKey(fieldInfo.FieldType) && value == null)
                    {
                        fieldInfo.SetValue(obj, _container[fieldInfo.FieldType]);

                        if (!_injectedFields.ContainsKey(fieldInfo.FieldType)) 
                            _injectedFields.Add(fieldInfo.FieldType, new List<InjectedField>());

                        var field = _injectedFields[fieldInfo.FieldType].SingleOrDefault(x => x.FieldInfo.Equals(fieldInfo));
                        if (field == null)
                        {
                            field = new InjectedField(obj, fieldInfo);
                            _injectedFields[fieldInfo.FieldType].Add(field);
                        }
                    }
                }
            }
        }

        public void InjectAllBinded()
        {
            foreach (var obj in _container)
            {
                Inject(obj.Value);
            }
        }

        private class InjectedField
        {
            public object Object { get; }
            public FieldInfo FieldInfo { get; }

            public InjectedField(object obj, FieldInfo fieldInfo)
            {
                Object = obj;
                FieldInfo = fieldInfo;
            }
        }
    }
}