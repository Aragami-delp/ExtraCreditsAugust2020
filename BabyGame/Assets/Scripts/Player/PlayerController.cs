using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, InspectorName("Movement Speed"), Tooltip("The movement speed multiplier of the player")]
    private float speed = 1f;

    private Rigidbody2D m_rig;

#pragma warning disable CS0649
    [SerializeField, Tooltip("Position where items should be held at")] private Transform handTransform;
    public Item m_itemInHand { get; private set; } // For example a Baby
    private bool m_canPickUp = false; // Pickup is needed in collision, but OnCollisionStay2D isn't called each frame so input will also be asked in Update

    private void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_canPickUp = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            m_canPickUp = false;
        }


        if (m_itemInHand != null && Input.GetKeyDown(KeyCode.Q))
        {
            this.DropItem();
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        m_rig.MovePosition(m_rig.position + nextMove * (speed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Picks up an Item
    /// </summary>
    /// <param name="_other">Collider of the Item</param>
    public void PickUpItem(Item _other)
    {
        Debug.Log("PickUp");
        if (m_itemInHand == null)
        {
            m_itemInHand = _other;
            m_itemInHand.transform.parent = handTransform;
            m_itemInHand.transform.position = handTransform.position;
        }
        else
        {
            Debug.LogError("Already " + m_itemInHand.name + " in Hand");
        }
    }

    /// <summary>
    /// Returns the Item from the players hand and removes them from the player
    /// </summary>
    /// <returns>Item the player is currently holding</returns>
    public Item DropItem()
    {
        Debug.Log("Drop");
        if (m_itemInHand != null)
        {
            Item retVal = m_itemInHand;
            m_itemInHand.transform.parent = null;
            m_itemInHand = null;
            retVal.DropItem();
            return retVal;
        }
        else
        {
            throw new NullReferenceException("Player doesn't have an Item!");
        }
    }

    private void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.CompareTag("Item") && m_canPickUp)
        {
            m_canPickUp = false;
            PickUpItem(_other.GetComponent<Item>().PickItem());
        }
    }
}
