using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageZone : MonoBehaviour
{
    public int maxSize = 1;
    public List<Item> itemList = new List<Item>(0);

    [SerializeField] protected float timeToRefreshNeed = 5f;
    protected float timeLeft;

    protected bool stationLocked = false;

    public bool addItem(Item item)
    {
        if (item.GetComponent<Item>() != null && !isFull)
        {
            this.itemList.Add(item);
            return true;
        }
        else
        {
            Debug.Log("NOT SAME");
            return false;
        }
    }

    public void removeItem(Item item)
    {
        itemList.Remove(item);
    }

    public Item getItem(int index)
    {
        if (index >= 0 && index < maxSize)
        {
            return itemList[index];
        }
        else
        {
            throw new System.ArgumentException();
        }
    }

    public virtual bool isFull
    {
        get { return itemList.Count >= maxSize; }
    }

    public virtual Item pickItem()
    {
        return null;
    }

    public virtual bool dropItem(Item item)
    {
        return true;
    }
}
