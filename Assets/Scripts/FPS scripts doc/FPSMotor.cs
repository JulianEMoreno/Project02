
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    public event Action Land = delegate { };

    [SerializeField] Camera _camera = null;
    [SerializeField] float _cameraAngleLimit = 70f;
    [SerializeField] GroundDetector  _groundDetector = null;


    //tracking our own camera angle, to avoid weird 0-360 angle conversions
    bool _isGrounded = false;
    private float _currentCameraRotationX = 0;
   
    Rigidbody _rigidbody = null;

   
    Vector3 _movementThisFrame = Vector3.zero;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;

    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

    void OnGroundDetected()
    {
        _isGrounded = true;
        //notify others that we have landed (animatons...)
        Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }
    public void Move(Vector3 requestMovement)
    {
        //store this movement for next FixedUpdate tick
        _movementThisFrame = requestMovement;
       // Debug.Log("Move: " + requestMovement);
    }

    public void Turn(float turnAmount)
    {
        //store this rotation for next FixedUpdate tick
        _turnAmountThisFrame = turnAmount;
       // Debug.Log("Turn: " + turnAmount);
    }

    public void Look(float lookAmount)
    {
        //store this rotation for next FixedUpdate tick
        _lookAmountThisFrame = lookAmount;
        //Debug.Log("Look: " + lookAmount);
    }

    public void Jump(float jumpForce)
    {
        //only allow us to jump if we're on the ground
        if (_isGrounded == false)
            return;

        _rigidbody.AddForce(Vector3.up * jumpForce);
        Debug.Log("Jump!");
    }
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
    }

    void ApplyMovement(Vector3 moveVector)
    {
        //confirm that we actually have movement, exit early if we dont
        if (moveVector == Vector3.zero)
            return;

        //move the rigidbody
        _rigidbody.MovePosition(_rigidbody.position + moveVector);

        //clear out movement, until we get a new move request 
        _movementThisFrame = Vector3.zero;
    }

    void ApplyTurn(float rotateAmount)
    {
        //confirm that we actually have rotation, exit early if we dont
        if (rotateAmount == 0)
            return;

        //move the body. convert x,y,z to quaternion for moverotation()
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);

        //clear out turn amount, until  we get a new turn request
        _turnAmountThisFrame = 0;

    }

    void ApplyLook(float lookAmount)
    {
        //confirm that we actually have rotation, exit early if we dont
        if (lookAmount == 0)
            return;

        //calculate and clamp our new camera rotation, before we apply it
        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp
                (_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);

        //clear out x movement, until we get a new move request
        _lookAmountThisFrame = 0;
    }
}
