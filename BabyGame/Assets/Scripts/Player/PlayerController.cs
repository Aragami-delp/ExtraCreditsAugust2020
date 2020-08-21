using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private Rigidbody2D m_rig;

    [SerializeField] private Transform handTransform;
    private Baby m_babyInHand; // For example a Baby
    private bool m_somethingInHand;
    private bool m_canPickUp = false; // Pickup is needed in collision, but OnCollisionStay2D isn't called each frame so input will also be asked in Update
    private Collider2D m_thingInHandCollider;

    private void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_canPickUp = Input.GetKey(KeyCode.E);
        if (m_somethingInHand && Input.GetKeyDown(KeyCode.Q))
        {
            this.DropBaby();
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_rig.MovePosition(m_rig.position + nextMove * (speed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Picks up the Baby
    /// </summary>
    /// <param name="_other">Collider of the Baby</param>
    public void PickUpBaby(Collider2D _other)
    {
        Debug.Log("PickUp");
        if (!m_somethingInHand)
        {
            m_somethingInHand = true;
            m_babyInHand = _other.GetComponent<Baby>();
            m_babyInHand.transform.parent = handTransform;
            // Disable collider
            m_thingInHandCollider = _other;
            // Disabled collider cant be found with GetComponent() ? therefor it needs to be stored
            m_thingInHandCollider.enabled = false;
            m_babyInHand.transform.position = handTransform.position;
        }
        else
        {
            Debug.LogError("Already " + m_babyInHand.name + " in Hand");
        }
    }

    /// <summary>
    /// Drops the baby out of the players Hand
    /// </summary>
    /// <param name="_enableCollider">Should the collider be enabled after drop</param>
    private void DropBaby(bool _enableCollider = true)
    {
        Debug.Log("Drop");
        if (m_somethingInHand)
        {
            m_somethingInHand = false;
            m_babyInHand.transform.parent = null;
            m_babyInHand = null;
            // Enable Collider
            if (_enableCollider)
                m_thingInHandCollider.enabled = true;
            m_thingInHandCollider = null;
        }
        else
        {
            Debug.LogWarning("Nothing to Drop");
        }
    }

    /// <summary>
    /// Returns the baby and removes it from the players hand
    /// </summary>
    /// <returns>Baby that just happend to be in the Players Hand</returns>
    public Baby GiveBaby()
    {
        Baby retVal = m_babyInHand;
        DropBaby(false);
        return retVal;
    }

    private void OnCollisionStay2D(Collision2D _other)
    {
        if (_other.collider.CompareTag("Baby") && m_canPickUp)
        {
            PickUpBaby(_other.collider);
        }
    }
}
