using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public struct CharacterMovementInput
{
    public Vector2 MoveInput;
}

public class CharacterMovement : NetworkBehaviour
{
    private Vector3 moveInput;
    private LayerMask ground;
    [SerializeField] private bool isGround;
    [SerializeField] private Rigidbody rg;
    [SerializeField] private float raio = 0.1f;
    [SerializeField] private Transform back;
    [SerializeField] private Material blue,red;
    [SerializeField] private SkinnedMeshRenderer skin;

    [Header("GroundMove")]
    [SerializeField] private float  maxSpeed = 5.0f;
    [SerializeField] private float acceleration = 40.0f;
    [SerializeField] private float rotationSpeed = 6.5f; 
    [SerializeField] private float gravity = 20;

    [Header("AirMove")]
    [SerializeField] private float jumpHeight = 1.0f;
    private float jumpSpeed => Mathf.Sqrt(2.0f * -Physics.gravity.y * jumpHeight);
    [SerializeField] private float airMaxSpeed = 4.0f; 
    [SerializeField] private float airAcceleration = 15.0f;
    [SerializeField] private float drag = 0.4f;
    
    [Header("NetworkVariables")]
    [SerializeField] private NetworkVariable<int>teamId=new NetworkVariable<int>(7,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);

    #region Gets
    
    public Transform GetBack(){
        return back;
    }

    #endregion
    
    public void SetInput(in CharacterMovementInput input)
    {
        moveInput = Vector3.zero;

        if(input.MoveInput != Vector2.zero)
        {
            moveInput = new Vector3(input.MoveInput.x, 0, input.MoveInput.y).normalized;
        }

        if(IsOwner == true)
        {
            Movement();
            Rotation();
        }
        
    }

    public override void OnNetworkSpawn()
    {
        teamId.OnValueChanged+=(int pastValue,int newValue)=>{
            this.gameObject.layer=teamId.Value;
            if(this.gameObject.layer==6) skin.material=red;
            else skin.material=blue;
            Debug.Log("Troquei de Time");
        };
    }

    void Movement(){
        isGround = Physics.Raycast(this.transform.position, Vector3.down, raio, ground, QueryTriggerInteraction.Ignore);
        Debug.DrawRay(this.transform.position, Vector3.down * raio, Color.green, 0);
        
        if(isGround)
        {
            Vector3 targetSpeed = moveInput * maxSpeed;
            //rg.velocity = Vector3.Lerp(rg.velocity, targetSpeed, acceleration * Time.fixedDeltaTime);
            rg.velocity = Vector3.MoveTowards(rg.velocity, targetSpeed, acceleration * Time.fixedDeltaTime);
            //Debug.Log(rg.velocity);
        }else
        {
            Vector2 targetAirSpeed = new Vector2(moveInput.x, moveInput.z) * airMaxSpeed;
            Vector2 currentAirSpeed = new Vector2(rg.velocity.x, rg.velocity.z);
            currentAirSpeed = Vector3.MoveTowards(currentAirSpeed, targetAirSpeed, airAcceleration * Time.fixedDeltaTime);
            rg.velocity = new Vector3(ApplyDrag(currentAirSpeed.x, drag, Time.fixedDeltaTime), rg.velocity.y , ApplyDrag(currentAirSpeed.y, drag, Time.fixedDeltaTime));        
        }               
    }

    void Rotation()
    {
        if(moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            Quaternion playerRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            this.transform.rotation = playerRotation;
        }
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isGround)
        {
            rg.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }
    }

    private static float ApplyDrag(float velocity, float drag, float time)
    {
        return velocity / (1 + drag * time);
    }

    public void TeamSwap(InputAction.CallbackContext context)
    {
        if(!IsOwner)return;
        if(teamId.Value==7)
        {
            teamId.Value=6;
        }else
        {
            teamId.Value=7;
        } 
    }

    void Awake()
    {
        ground = LayerMask.GetMask("Ground");
    }

    void Start()
    {
        rg = this.transform.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, Physics.gravity.y - (rg.mass / gravity), 0);        
    }
    
}
