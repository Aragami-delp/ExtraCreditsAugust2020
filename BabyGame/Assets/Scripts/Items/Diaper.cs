using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diaper : Item
{
    [HideInInspector] public Collider2D m_collider;

    public override Item PickItem()
    {
        m_collider.enabled = false;
        return this;
    }

    public override void DropItem()
    {
        m_collider.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
