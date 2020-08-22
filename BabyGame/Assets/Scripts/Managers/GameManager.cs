using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float maxTimeBetweenMothers = 10f;
    public float minTimeBetweenMothers = 5f;
    public StorageZone starterZone;
    public StorageZone returnZone;

    private float timeUntilNextMother = 0f;
    private List<Mother> mothers = new List<Mother>();

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // set singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        generateNewMother();
        fillUpStarterZone();
        updateMotherTime();
        pickUpReturnZone();
    }

    private void generateNewMother()
    {
        if (timeUntilNextMother < 0)
        {
            mothers.Add(new Mother());
            timeUntilNextMother = Random.Range(minTimeBetweenMothers, maxTimeBetweenMothers);
        }
        else
        {
            timeUntilNextMother -= Time.deltaTime;
        }
    }

    private void fillUpStarterZone()
    {
        if (!starterZone.isFull()) {
            foreach (Mother mother in mothers)
            {
                if (mother.getState().Equals(EMotherState.GIVE_UP_LINE))
                {
                    Baby baby = generateBaby();
                    mother.setBaby(baby);
                    mother.setState(EMotherState.AWAY);

                    starterZone.setItem(baby);
                break;
                }
            }

        }
    }   

    private void updateMotherTime ()
    {
        foreach (Mother mother in mothers)
        {
            if (mother.getState().Equals(EMotherState.AWAY))
            {
                mother.removeTime(Time.deltaTime);
            }
        }
    }

    private void pickUpReturnZone()
    {
        for (int i = 0; i < mothers.Count; i++)
        {
            if (mothers[i].getState().Equals(EMotherState.GIVE_UP_LINE))
            {
                if (returnZone.isFull())
                {
                    if (mothers[i].getBaby().Equals(returnZone.getItem()))
                    {
                        //Here should be a point System that adds points if the baby isn't crying
                        mothers.RemoveAt(i);
                        returnZone.setItem(null);
                    }
                }
                break;
            }
        }
    }


    private Baby generateBaby()
    {
        return null;
    }
}