using Assets.Scripts.Baby;
using Assets.Scripts.Baby.Need;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public bool isCrying = false;

    private Mother mother;
    private List<INeed> needs = new List<INeed>();


    // Start is called before the first frame update
    void Start()
    {
        needs.Add(new NeedHunger(10f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isCrying = needsArentMet();
        updateNeeds();

        if (isCrying)
        {
            Debug.Log("Baby is crying");
        }
    }


    public bool needsArentMet()
    {
        foreach (INeed need in needs)
        {
            if (!need.isMet()) {
                return true;
            }
        }
        return false;
    }

    private void updateNeeds()
    {
        foreach (INeed need in needs)
        {
            need.removeTime(Time.deltaTime);
        }
    }


    public Mother GetMother()
    {
        return mother;
    }
}
