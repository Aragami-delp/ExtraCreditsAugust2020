using Assets.Scripts.Baby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaperChangeStation : StorageZone
{
    private Diaper diaper = null;
    private AbstractBaby baby = null;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToRefreshNeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (diaper != null && baby != null)
        {
            if (baby.hasNeed(ENeedType.Poop))
            {
                stationLocked = true;
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                    baby.satisfyNeed(ENeedType.Poop);
                    timeLeft = timeToRefreshNeed;
                    stationLocked = false;
                    Destroy(diaper.gameObject);
                    diaper = null;
                }
            }

        }
    }


    public override Item pickItem()
    {
        if (!stationLocked)
        {
            if (baby != null)
            {
                return baby;
            }
            else if (diaper != null)
            {
                return diaper;
            }
        }
        return null;
    }

    public override bool dropItem(Item item)
    {
        if (item is AbstractBaby && baby == null)
        {
            baby = (AbstractBaby)item;
            return true;
        }
        else if (item is Diaper && diaper == null)
        {
            diaper = (Diaper)item;
            return true;
        }
        return false;
    }
}
