namespace generic_Common;

public class MyStack<T>
{
    private readonly List<T> _items = new List<T>();
    private object _item;
    
    // Them phan tu vao dinh stack
    public void Push(T item)
    {
        _items.Add(item);
    }
    
    //Lay va xoa phan tu o dinh stack
    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack rỗng!");
        
        T top = _items[_items.Count - 1]; //Lay phan tu cuoi cung
        _items.RemoveAt(_items.Count - 1); // xoa phan tu cuoi
        return top;
    }
    
    //Lay phan tu o dinh stack nhung k xoa
    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack rỗng!");
        return _items[_items.Count - 1];
    }

    //kiem tra stack co rong khong
    public bool IsEmpty()
    {
        return _items.Count == 0;
    }
}