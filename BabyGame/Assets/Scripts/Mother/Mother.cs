using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    private EMotherState state = EMotherState.GIVE_UP_LINE;
    private GameObject baby = null;
    private float maxTimeUntilReturn = 5f; // change to random value
    private float timeLeftUntilReturn;

    public Mother()
    {
        this.timeLeftUntilReturn = maxTimeUntilReturn;
    }

    public void removeTime(float timeToRemove)
    {
        timeLeftUntilReturn -= timeToRemove;

        if (timeLeftUntilReturn < 0)
        {
            state = EMotherState.RETURN_LINE;
            this.transform.position = GameManager.Instance.returnZone.transform.position;
            Debug.Log("Mother State PickUp");
        }
    }

    public void setBaby(GameObject baby)
    {
        this.baby = baby;
    }
    
    public GameObject getBaby()
    {
        return baby;
    }

    public bool isMyBaby(GameObject gameObject)
    {
        return gameObject.Equals(baby);
    }

    public void setState(EMotherState state)
    {
        this.state = state;
    }

    public EMotherState getState()
    {
        return state;
    }

}