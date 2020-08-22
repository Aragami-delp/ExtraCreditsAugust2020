using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother
{
    private EMotherState state = EMotherState.GIVE_UP_LINE;
    private Baby baby;

    private float maxTimeUntilReturn = 20f;
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
        }
    }

    public void setBaby(Baby baby)
    {
        this.baby = baby;
    }
    
    public Baby getBaby()
    {
        return baby;
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