namespace Dsa;

public class MyArray<T>
{
    private T[] _data;
    private int _size;
    private int _capacity;

    public MyArray(int capacity = 2)
    {
        _data = new T[2];
        _size = 0;
        _capacity = capacity;
    }

    public bool Resize(int newCapacity)
    {
        T[] newData = new T[newCapacity];
        for (int i = 0; i < _size; i++)
        {
            newData[i] = _data[i];
        }

        _data = newData;
        _capacity = newCapacity;
        return true;
    }

    public bool Add(T value)
    {
        if (_size >= _capacity)
        {
            Resize(_capacity * 2);
        }
        _data[_size] = value;
        _size++;
        return true;
    }

    public bool InsertAt(int index, T value)
    {
        if (_size >= _capacity)
        {
            Resize(_capacity * 2);
        }

        for (int i = _size; i > index; i--)
        {
            _data[i] = _data[i - 1];
        }

        _data[index] = value;
        _size++;
        return true;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 && index >= _size)
        {
            throw new ArgumentException("Invalid index");
        }

        if (_size == 0)
        {
            throw new InvalidOperationException("Array is empty");
        }

        T value = _data[index];

        for (int i = index; i < _size; i++)
        {
            _data[i] = _data[i + 1];
        }

        _size--;
        return value;
    }

    public void PrintArray()
    {
        Console.Write("[");
        for (int i = 0; i < _size; i++)
        {
            Console.Write(_data[i]);
            if (i < _size - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }
}