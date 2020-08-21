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
    private Transform m_currentBaby;

    private void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
        m_rig.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        Vector2 nextMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_rig.MovePosition(m_rig.position + nextMove * (speed * Time.fixedDeltaTime));
    }

    public void PickUp(Transform _other)
    {
        m_currentBaby = _other;
        m_currentBaby.parent = handTransform;
    }
}
