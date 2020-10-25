﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    /*
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar; 
    */
    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;
    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
    }
    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
    }

    void OnMove(Vector3 movement)
    {
        //incorporate our move speed
        _motor.Move(movement * _moveSpeed);
      //  Debug.Log("Move: " + movement);
    }
    void OnRotate(Vector3 rotation)
    {
        //camera looks vertical, body rotates left/right
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);

      //  Debug.Log("Rotate: " + rotation);
    }

    void OnJump()
    {
        //apply our jump force to our motor
        _motor.Jump(_jumpStrength);
      //  Debug.Log("Jump!");
    }
    // Start is called before the first frame update
    private void Start()
    {
        //currentHealth = maxHealth;
       // healthBar.SetMaxHealth(maxHealth);
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("sprinting");
            _moveSpeed = .4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("no sprinting");
                _moveSpeed = .1f;
            }
        /*
        if(Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }
        */
    }
    /*
    public void TakeDamage (float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    */
}
