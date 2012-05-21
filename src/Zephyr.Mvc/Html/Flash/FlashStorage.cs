using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Zephyr.DesignByContract;
using System.Linq;

namespace Zephyr.Web.Mvc.Html.Flash
{
    internal class FlashStorage
    {
        private static readonly string Key = typeof (FlashStorage).FullName;

        private readonly TempDataDictionary _backingStore;

        public FlashStorage(TempDataDictionary backingStore)
        {
            Check.Require(backingStore!=null, "backing store for Flash storage cannot be null", new ArgumentNullException("backingStore"));

            _backingStore = backingStore;
        }

        public IEnumerable<KeyValuePair<string, string>> Messages
        {
            get
            {
                
                object value;

                if (!_backingStore.TryGetValue(Key, out value))
                {
                    return new List<KeyValuePair<string, string>>();
                }

                return (IEnumerable<KeyValuePair<string, string>>)value; 
            }
        }

        public void Add(string type, string message)
        {
            if(String.IsNullOrEmpty(message))
                return;

            //as the type would come from a anonymous object property name, 
            //we assume the underscore means the hyphen as that is the usual convention for css class name 
            type = type.Replace("_", "-");

            IList<KeyValuePair<string, string>> messages;
            object tmp;
            if(!_backingStore.TryGetValue(Key, out tmp))
            {
                messages=new List<KeyValuePair<string, string>>();
                _backingStore.Add(Key, messages);
            }
            else
            {
                messages = (IList<KeyValuePair<string, string>>) tmp;
            }

            var item = messages.SingleOrDefault(e => e.Key.Equals(type, StringComparison.OrdinalIgnoreCase));

            if(!String.IsNullOrWhiteSpace(item.Value))
            {
                messages.Remove(item);
            }

            messages.Add(new KeyValuePair<string, string>(type, message));

            //_backingStore.Keep(Key);
        }
    }
}