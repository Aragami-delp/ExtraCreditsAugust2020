using Assets.Scripts.Baby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStation : StorageZone
{
    public float timeToRefreshNeed = 5f;
    public float timeLeft;

    private bool staionLocked = false;

    private BabyBottle babbyBottle = null;
    private AbstractBaby baby = null;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToRefreshNeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (babbyBottle != null && baby != null)
        {
            if (baby.hasNeed(ENeedType.Hunger))
            {
                staionLocked = true;
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                    baby.satisfyNeed(ENeedType.Hunger);
                    timeToRefreshNeed = 5f;
                    staionLocked = false;
                    Destroy(babbyBottle.gameObject);
                    babbyBottle = null;
                }
            }

        }
    }


    public override Item pickItem()
    {
        if (!staionLocked)
        {
            if (baby != null)
            {
                return baby;
            }
            else if (babbyBottle != null)
            {
                return babbyBottle;
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
        else if (item is BabyBottle && babbyBottle == null)
        {
            babbyBottle = (BabyBottle) item;
            return true;
        }
        return false;
    }
}
