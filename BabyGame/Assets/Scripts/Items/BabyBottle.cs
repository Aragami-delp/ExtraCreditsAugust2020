using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBottle : Item
{
    [HideInInspector] public Collider2D m_collider;

    public virtual void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    public override Item PickItem()
    {
        m_collider.enabled = false;
        return this;
    }

    public override void DropItem()
    {
        m_collider.enabled = true;
    }
}
