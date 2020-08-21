using Assets.Scripts.Baby.Need;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaby : MonoBehaviour
{
    public float needCryingmMultiplier = 2.5f;
    public float needDefaulMultipliert = 1f;
    private float needSpeedMultiplier = 1f;
    public bool isCrying = false;

    private Mother mother;
    private List<Need> needs = new List<Need>();

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
}
