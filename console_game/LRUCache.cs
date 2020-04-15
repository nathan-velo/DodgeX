using System;
using System.Collections.Generic;
using System.Text;

namespace console_game {
    class LRUCache<T> {
        private const int NotFound = -1;
        private int _count;
        private readonly int _capacity;
        private readonly LinkedList<CacheItem<T>> _items;
        private readonly Dictionary<string, LinkedListNode<CacheItem<T>>> _keys;

        public LRUCache(int capacity) {
            _items = new LinkedList<CacheItem<T>>();
            _keys = new Dictionary<string, LinkedListNode<CacheItem<T>>>(capacity);
            _count = 0;
            _capacity = capacity;
        }

        public T Get(string key) {
            if (!_keys.ContainsKey(key)) {
                return default;
            }

            var cacheItem = _keys[key];

            if (cacheItem != _items.First) {
                // promote to most recently used
                _items.Remove(cacheItem);
                _items.AddFirst(cacheItem);
            }

            return cacheItem.Value.Value;
        }

        public void Put(string key, T value) {
            if (!_keys.ContainsKey(key)) {
                // add
                _keys[key] = _items.AddFirst(new CacheItem<T>(key, value));

                if (_count == _capacity) {
                    // remove least recently used
                    var last = _items.Last;
                    _keys.Remove(last.Value.Key);
                    _items.RemoveLast();
                }
                else {
                    _count++;
                }
            }
            else {
                // update
                var cacheItem = _keys[key];
                cacheItem.Value.Value = value;

                if (cacheItem != _items.First) {
                    // promote to most recently used
                    _items.Remove(cacheItem);
                    _items.AddFirst(cacheItem);
                }
            }
        }

        private class CacheItem<T> {
            public CacheItem (string key, T value) {
                Key = key;
                Value = value;
            }

            public string Key { get; }

            public T Value { get; set; }
        }
    }
}
