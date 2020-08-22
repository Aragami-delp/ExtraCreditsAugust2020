using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageZone : MonoBehaviour
{
    public int maxSize = 1;
    public List<GameObject> itemList = new List<GameObject>(0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addItem(GameObject item)
    {
        if (item.GetComponent<Item>() != null && !isFull())
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

    public bool removeItem(GameObject item)
    {
        return itemList.Remove(item);
    }

    public GameObject getItem(int index)
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

    public List<GameObject> getItems
    {
        get
        {
            return itemList;
        }
    }

    public bool isFull()
    {
        return itemList.Count >= maxSize;
    }
}
