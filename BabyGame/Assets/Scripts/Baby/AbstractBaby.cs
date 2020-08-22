using Assets.Scripts.Baby.Need;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaby : Item
{
    public float needCryingmMultiplier = 2.5f;
    public float needDefaulMultipliert = 1f;
    private float needSpeedMultiplier = 1f;
    public bool isCrying = false;

    private Mother mother;
    private List<Need> needs = new List<Need>();
    [HideInInspector] public Collider2D m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (needsArentMet())
        {
            isCrying = true;
            needSpeedMultiplier = needCryingmMultiplier;
        }
        else
        {
            isCrying = false;
            needSpeedMultiplier = needDefaulMultipliert;
        }
        updateNeeds(needSpeedMultiplier);
    }


    public bool needsArentMet()
    {
        foreach (Need need in needs)
        {
            if (!need.isMet()) {
                return true;
            }
        }
        return false;
    }

    public bool hasNeed(string type)
    {
        foreach (Need need in needs)
        {
            if (need.getType().Equals(type))
            {
                return true;
            }
        }
        return false;
    }

    public void satisfyNeed(string type)
    {
        foreach (Need need in needs)
        {
            if (need.getType().Equals(type))
            {
                need.refreshTime();
                break;
            }
        }
    }

    private void updateNeeds(float multiplier)
    {
        foreach (Need need in needs)
        {
            need.removeTime(Time.deltaTime*multiplier);
        }
    }

    protected void addNeed(Need needToAdd)
    {
        needs.Add(needToAdd);
    }

    public Mother getMother()
    {
        return mother;
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
