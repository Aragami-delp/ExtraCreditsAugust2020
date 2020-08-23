using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float maxTimeBetweenMothers = 10f;
    public float minTimeBetweenMothers = 5f;
    public StorageZone starterZone;
    public ReturnZone returnZone;
#pragma warning disable CS0649
    [SerializeField] private GameObject babyPrefab;
#pragma warning disable CS0649
    [SerializeField] private GameObject motherPrefab;

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
        //mockMove();
        updateMotherTime();
        pickUpReturnZone();
    }

    private void mockMove()
    {
        if (starterZone.isFull && !returnZone.isFull)
        {
            Item baby = starterZone.getItem(0);

            starterZone.removeItem(baby);
            returnZone.addItem(baby);
            baby.transform.position = returnZone.transform.position;

            Debug.Log("Moved baby to return zone");
        }
    }

    private void generateNewMother()
    {
        if (timeUntilNextMother < 0)
        {
            Mother newMother = Instantiate(motherPrefab, starterZone.transform.position, new Quaternion()).GetComponent<Mother>();
            mothers.Add(newMother);
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
        if (!starterZone.isFull) {
            foreach (Mother mother in mothers)
            {
                if (mother.getState().Equals(EMotherState.GIVE_UP_LINE))
                {
                    GameObject baby = generateBaby();
                    mother.setBaby(baby.GetComponent<AbstractBaby>());
                    mother.setState(EMotherState.AWAY);
                    starterZone.addItem(baby.GetComponent<Item>());

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
                if (returnZone.isFull)
                {
                    if (mothers[i].isMyBaby(returnZone.GetBaby()))
                    {
                        //Here should be a point System that adds points if the baby isn't crying
                        Mother pickUpMother = mothers[i];
                        mothers.RemoveAt(i);
                        AbstractBaby baby = returnZone.GetBaby();

                        returnZone.RemoveBaby();
                        Destroy(baby.gameObject);
                        Debug.Log("Picked up baby");
                        Destroy(pickUpMother.gameObject);
                    }
                }
                break;
            }
        }
    }


    private GameObject generateBaby()
    {
        //Later with prefabs
        GameObject gameObject = Instantiate(babyPrefab, starterZone.transform.position, new Quaternion());

        return gameObject;
    }
}