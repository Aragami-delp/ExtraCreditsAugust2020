using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageZone : MonoBehaviour
{

    private Item item = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(Item item)
    {
        this.item = item;
    }

    public Item getItem()
    {
        return item;
    }

    public bool isFull()
    {
        return item != null;
    }
}
