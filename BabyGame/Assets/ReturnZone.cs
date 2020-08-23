using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnZone : StorageZone
{
    private AbstractBaby m_baby = null;

    public override bool dropItem(Item _item)
    {
        if (_item is AbstractBaby && m_baby == null)
        {
            m_baby = (AbstractBaby)_item;
            m_baby.transform.position = this.transform.position;
            return true;
        }
        return false;
    }

    public override Item pickItem()
    {
        if (m_baby != null)
        {
            AbstractBaby retVal = m_baby;
            m_baby = null;
            return retVal;
        }
        return null;
    }

    public AbstractBaby GetBaby()
    {
        return m_baby;
    }

    public void RemoveBaby()
    {
        m_baby = null;
    }

    public override bool isFull
    {
        get { return m_baby != null; }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (isFull && _other.CompareTag("Item") && _other.GetComponent<AbstractBaby>() != null)
        {
            if (_other.GetComponent<AbstractBaby>().Equals(m_baby))
            {
                m_baby = null;
            }
        }
    }
}
