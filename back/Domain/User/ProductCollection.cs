using System.Collections;
using lab_1_frog_aunyuh_team.back.Domain.User;

public class ProductCollection : IEnumerable<Product>
{
    private Product[] _items = new Product[Size];
    private int _count;
    static readonly int Size = 100;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public void Add(Product productToAdd)
    {
        if (_count >= Size)
        {
            return;
        }
        
        _items[_count] = productToAdd;
        _count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            return;
        }

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _items[_count - 1] = null;
        _count--;
    }

    public Product? GetAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            return null;
        }
        
        return _items[index];
    }

    public void SetAt(int index, Product product)
    {
        if (index < 0 || index >= _count)
        {
            return;
        }
        
        _items[index] = product;
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return new ProductEnumerator(_items, _count);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class ProductEnumerator : IEnumerator<Product>
{
    private Product[] _items;
    private int _count;
    private int _position = -1;

    public ProductEnumerator(Product[] items, int count)
    {
        _items = items;
        _count = count;
    }

    public Product Current
    {
        get
        {
            if (_position < 0 || _position >= _count)
            {
                throw new InvalidOperationException();
            }
            
            return _items[_position];
        }
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public bool MoveNext()
    {
        _position++;
        return _position < _count;
    }

    public void Reset()
    {
        _position = -1;
    }

    public void Dispose()
    {
    }
}
