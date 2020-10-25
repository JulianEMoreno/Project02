﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    Rigidbody rb;

    [SerializeField] float speed = 250f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player taking damage");
            PlayerHealth currentHealth = collision.transform.GetComponent<PlayerHealth>();
            PlayerHealth.singleton.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
