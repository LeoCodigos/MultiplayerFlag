using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool pause = false, isGround, isRestore;
    public float vel, velRot, pulo;

    [SerializeField] private Rigidbody rigid;
    private Animator anim;
    public PlayerInputActions input;
    public LayerMask chao;
    private InputAction mover, pular;



    void Movimentar()
    {
        //Movimentos Básicos
        
        Vector3 velFis = Vector3.zero;

        //Andar para frente e andar para trás
        if(mover.ReadValue<Vector2>().y > 0)
        {
            velFis = transform.forward * mover.ReadValue<Vector2>().y * vel * Time.fixedDeltaTime;
        }else
        {
            //Rotacionar
            transform.Rotate(0, mover.ReadValue<Vector2>().y * velRot, 0);
        }

        velFis.y = (rigid.velocity.y < 0) ? rigid.velocity.y * 1.05f : rigid.velocity.y;

        rigid.velocity = velFis;

        //Rotacionar
        transform.Rotate(0, mover.ReadValue<Vector2>().x * velRot, 0);

        //Atualiza a Máquina de Estados (FSM)
        AttFSM(false);

        //Descongela a velocidade das animações
        isRestore = Physics.CheckSphere(transform.GetChild(1).position, 0.5f, chao, QueryTriggerInteraction.Ignore);
        if(isRestore)
        {
            Descongelar();
        }
    }

    public void Pular(InputAction.CallbackContext context)
    {
        //Debug.Log("Pular");
        isGround = Physics.CheckSphere(transform.GetChild(0).position, 0.1f, chao, QueryTriggerInteraction.Ignore);
        
        if(isGround)
        {
            rigid.AddForce(Vector3.up * pulo, ForceMode.VelocityChange);
            Debug.Log("Passei Aqui!");
        }

        //Atualiza a Máquina de Estados (FSM)
        AttFSM(isGround);
    }

    //Congela a animação do pulo no apice do salto
    public void Congelar()
    {
        Debug.Log("Cogelando");
        anim.speed = 0.0f;
    }

    void Descongelar()
    {
        Debug.Log("Descongelou");
        anim.speed = 1.0f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.GetChild(0).transform.position, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.GetChild(1).transform.position, 0.5f);
    }

    void AttFSM(bool isGround){
        if(mover.ReadValue<Vector2>().y != 0)
        {
            anim.SetBool("movimentando", true);
        }else
        {
            anim.SetBool("movimentando", false);
        }

        if(isGround)
        {
             anim.SetTrigger("pular");
        }
    }

    void AttPulo()
    {
        if(isGround)
        {
            anim.SetTrigger("pular");
        }
    }

    void OnEnable()
    {
        mover = input.Player.Move;
        mover.Enable();
        

        pular = input.Player.Jump;
        pular.Enable();
        pular.performed += Pular;
    }

    void OnDisable()
    {

    }

    void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Move.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(pause == false){
            Movimentar();
        }
    }

    void Update()
    {

    }
}
