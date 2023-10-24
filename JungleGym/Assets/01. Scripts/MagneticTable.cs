using System;
using System.Collections.Generic;

public class MagneticTable<T, U> where T : IComparable<T>
{
	public struct PredicateTable
    {
        public T left;
        public T right;
        public U value;

        public PredicateTable(T left, T right, U value)
        {
            this.left = left;
            this.right = right;
            this.value = value;
        }
    }

    private Dictionary<int, PredicateTable> tables = new Dictionary<int, PredicateTable>();

    public void RegisterTable(int hash, T left, T right, U value)
    {
        if(left.CompareTo(right) > 0)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        if(tables.ContainsKey(hash) == false)
            tables.Add(hash, new PredicateTable(left, right, value));
    }

    public void DeleteTable(int hash)
    {
        if(tables.ContainsKey(hash))
            tables.Remove(hash);
    }

    public U GetFixedValue(T origin)
    {
        foreach(int hash in tables.Keys)
        {
            PredicateTable table = tables[hash];
            if(0 <= origin.CompareTo(table.left) && origin.CompareTo(table.right) <= 0)
                return table.value;
        }

        return default;
    }
}
