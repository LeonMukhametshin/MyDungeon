using System.Collections;

namespace MyListLib
{
    public class MyList<T> : IEnumerable<T>
    {
        //TODO: добавить методы Sort, Find, AddRange
        //TODO: написать Unit тесты

        private T[] _items;
        private int _count;

        private const int DefaultCapacity = 4;

        public int Count => _count;
        public int Capacity => _items.Length;

        public MyList()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        public MyList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException(nameof(capacity), "The capacity cannot be negative");
            }

            _items = capacity == 0 ? new T[DefaultCapacity] : new T[capacity];
            _count = 0;
        }

        public MyList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is ICollection<T> newCollection)
            {
                _items = new T[newCollection.Count];
                newCollection.CopyTo(this._items, 0);
                _count = newCollection.Count;
            }
            else
            {
                _items = new T[DefaultCapacity];
                _count = 0;
                foreach (var item in collection)
                {
                    Add(item);
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _items[index];
            }

            set
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                _items[index] = value;
            }
        }

        /// <summary>
        /// Добавляет элемент в конец списка.
        /// При заполнении внутреннего массива автоматически увеличивает его емкость.
        /// </summary>
        /// <param name="item">Элемент для добавления в список</param>
        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                EnsureCapacity(_count + 1);
            }

            _items[_count] = item;
            _count++;
        }

        /// <summary>
        /// Удаляет первое вхождение указанного элемента из списка.
        /// Выполняет поиск элемента и при его наличии удаляет через RemoveAt.
        /// </summary>
        /// <param name="item">Элемент для удаления</param>
        /// <returns>true если элемент был найден и удален; иначе false</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляет элемент по указанному индексу.
        /// Сдвигает все элементы после удаляемого на одну позицию влево.
        /// </summary>
        /// <param name="index">Отсчитываемый от нуля индекс удаляемого элемента</param>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается когда index меньше 0 или больше/равен Count</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            _count--;
            if (index < _count)
            {
                Array.Copy(_items, index + 1, _items, index, _count - index);
            }

            _items[_count] = default(T)!;
        }

        /// <summary>
        /// Вставляет элемент в список по указанному индексу.
        /// Сдвигает существующие элементы начиная с указанного индекса вправо.
        /// При необходимости увеличивает емкость списка.
        /// </summary>
        /// <param name="index">Отсчитываемый от нуля индекс, по которому должен быть вставлен элемент</param>
        /// <param name="item">Вставляемый элемент</param>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается когда index меньше 0 или больше Count</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (_count == _items.Length)
            {
                EnsureCapacity(_count + 1);
            }

            if (index < _count)
            {
                Array.Copy(_items, index, _items, index + 1, _count - index);
            }
            _items[index] = item;
            _count++;
        }

        /// <summary>
        /// Удаляет все элементы из списка.
        /// Обнуляет счетчик элементов, но сохраняет текущую емкость внутреннего массива.
        /// Не уменьшает размер внутреннего массива для оптимизации последующих добавлений.
        /// </summary>
        public void Clear()
        {
            if (_count > 0)
            {
                Array.Clear(_items, 0, _count);
                _count = 0;
            }
        }

        /// <summary>
        /// Гарантирует, что емкость внутреннего массива не меньше указанного значения.
        /// Увеличивает емкость по стратегии удвоения текущего размера.
        /// Вызывается автоматически при добавлении элементов когда массив заполнен.
        /// </summary>
        /// <param name="count">Минимальная требуемая емкость</param>
        private void EnsureCapacity(int count)
        {
            if (_items.Length >= count) return;

            int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;

            if (newCapacity < count)
            {
                newCapacity = count;
            }

            if ((uint)newCapacity > Array.MaxLength)
            {
                newCapacity = Array.MaxLength;
            }

            var newItems = new T[newCapacity];
            Array.Copy(_items, newItems, _count);
            _items = newItems;
        }

        public int IndexOf(T item) => Array.IndexOf(_items, item, 0, _count);

        public bool Contains(T item) => IndexOf(item) >= 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
