﻿using Assets.Scripts.Baby;
using Assets.Scripts.Baby.Need;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Item
{
    public bool isCrying = false;

    private Mother mother;
    private List<Need> needs = new List<Need>();
    [HideInInspector] public Collider2D m_collider;


    // Start is called before the first frame update
    void Start()
    {
        needs.Add(new Need(ENeedType.Hunger,10f));
        m_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isCrying = needsArentMet();
        updateNeeds();

        if (isCrying)
        {
            //Debug.Log("Baby is crying");
        }
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

    private void updateNeeds()
    {
        foreach (Need need in needs)
        {
            need.removeTime(Time.deltaTime);
        }
    }


    public Mother GetMother()
    {
        return mother;
    }

    public override Item PickItem()
    {
        m_collider.enabled = false;
        return this;
    }

    public override void DropItem()
    {
        m_collider.enabled = true;
    }
}
