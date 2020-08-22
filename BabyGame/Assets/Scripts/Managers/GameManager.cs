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
        mockMove();
        updateMotherTime();
        pickUpReturnZone();
    }

    private void mockMove()
    {
        if (starterZone.isFull() && !returnZone.isFull())
        {
            GameObject baby = starterZone.getItem(0);

            returnZone.addItem(baby);
            starterZone.removeItem(baby);

            Debug.Log("Moved baby to return zone");
        }
    }

    private void generateNewMother()
    {
        if (timeUntilNextMother < 0)
        {
            mothers.Add(new Mother());
            timeUntilNextMother = Random.Range(minTimeBetweenMothers, maxTimeBetweenMothers);
            Debug.Log("Generated new Mother");
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
                    GameObject baby = generateBaby();
                    mother.setBaby(baby);
                    mother.setState(EMotherState.AWAY);
                    starterZone.addItem(baby);

                    Debug.Log(baby);
                    Debug.Log("Set Baby on starter Zone");
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
            if (mothers[i].getState().Equals(EMotherState.RETURN_LINE))
            {
                if (returnZone.isFull())
                {
                    if (mothers[i].isMyBaby(returnZone.getItem(0)))
                    {
                        //Here should be a point System that adds points if the baby isn't crying
                        mothers.RemoveAt(i);
                        GameObject baby = returnZone.getItem(0);

                        returnZone.removeItem(baby);
                        Destroy(baby);
                        Debug.Log("Picked up baby");
                    }
                }
                break;
            }
        }
    }


    private GameObject generateBaby()
    {
        //Later with prefabs
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<DefaultBaby>();

        return gameObject;
    }
}