using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Baby;
using UnityEngine;

[Serializable]
public class BabySpots
{
    //[SerializeField] private Transform babyTransform;
    public AbstractBaby baby;
    public bool occupied; // cheaper and more convenient than check for null

    /// <summary>
    /// Adds a new Baby
    /// </summary>
    /// <param name="_newBaby">Baby to be added</param>
    public void AddBaby(AbstractBaby _newBaby)
    {
        baby = _newBaby;
        occupied = true;
        Debug.Log("Baby joined RelaxZone");
        //baby.transform.position = babyTransform.position;
    }

    /// <summary>
    /// Removes the Baby from the RelaxStation
    /// </summary>
    /// <returns>Removed Baby</returns>
    public AbstractBaby RemoveBaby()
    {
        AbstractBaby retVal = baby;
        baby = null;
        occupied = false;
        Debug.Log("Baby left RelaxZone");
        return retVal;
    }
}

public class RelaxStation : StorageZone
{
    [SerializeField] private List<BabySpots> babySpots;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_other.isTrigger && _other.CompareTag("Item"))
        {
            AbstractBaby newBaby = _other.GetComponent<AbstractBaby>();
            if (newBaby != null)
            {
                if (BabySpotAvailable)
                {
                    babySpots[EmptyBabySpotIndex].AddBaby(newBaby);
                }
            }
        }
    }

    private void Update()
    {
        foreach (BabySpots babySpot in babySpots)
        {
            if (babySpot.occupied && babySpot.baby.hasNeed(ENeedType.Sleep))
            {
                stationLocked = true;
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                    babySpot.baby.satisfyNeed(ENeedType.Sleep);
                    timeLeft = timeToRefreshNeed;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Item"))
        {
            AbstractBaby oldBaby = _other.GetComponent<AbstractBaby>();
            if (oldBaby != null)
            {
                _ = babySpots[GetBabySpotIndexByBaby(oldBaby)].RemoveBaby();
            }
        }
    }

    /// <summary>
    /// Returns true if a baby spot is available, false if not
    /// </summary>
    private bool BabySpotAvailable
    {
        get
        {
            foreach (BabySpots babySpot in babySpots)
            {
                if (babySpot.baby == null)
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Returns the index of the empty babyspot, throws NullReferenceException if not
    /// </summary>
    private int EmptyBabySpotIndex
    {
        get
        {
            for (int i = 0; i < babySpots.Count; i++)
            {
                if (babySpots[i].baby == null)
                    return i;
            }

            throw new NullReferenceException();
        }
    }

    /// <summary>
    /// Finds the index in which the baby is stored in the babySpots Array
    /// </summary>
    /// <param name="_oldBaby">Baby to be found</param>
    /// <returns>index of Baby</returns>
    private int GetBabySpotIndexByBaby(AbstractBaby _oldBaby)
    {
        for (int i = 0; i < babySpots.Count; i++)
        {
            if (babySpots[i].baby == _oldBaby)
            {
                return i;
            }
        }

        throw new NullReferenceException();
    }
}
