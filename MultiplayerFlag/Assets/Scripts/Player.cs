using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.Netcode.Components;


public class Player : NetworkBehaviour
{
    #region Atributes
    public bool pause = false, isGround, isRestore;
    public float vel, velRot, pulo;
    [SerializeField] private bool movePress, jumpPress;
    [SerializeField] private Vector2 currentMove;
    [SerializeField] private Vector3 dir;
    [SerializeField] private Transform back;
    [SerializeField] private Material blue,red;
    [SerializeField] private GameObject skin;

    private Gun gun;

    [SerializeField] private NetworkVariable<int>teamId=new NetworkVariable<int>(7,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);


    [SerializeField] private Rigidbody rigid;
    private Animator anim;
    private NetworkAnimator netAnim;
    public LayerMask chao;
    public PlayerInputActions input;
    private InputAction mover, pular, teamSwap, fire;

    #endregion

    #region Gets
    public Transform GetBack(){
        return back;
    }

    #endregion

    #region Methods
    public void SwapTeam(){
        if(!IsOwner)return;
        if(teamId.Value==7){
            teamId.Value=6;
        }else{
            teamId.Value=7;
        } 
    }

    public override void OnNetworkSpawn()
    {
        teamId.OnValueChanged+=(int pastValue,int newValue)=>{
            this.gameObject.layer=teamId.Value;
            if(this.gameObject.layer==6)skin.GetComponent<SkinnedMeshRenderer>().material=red;
            else skin.GetComponent<SkinnedMeshRenderer>().material=blue;
            Debug.Log("Troquei de Time");
        };
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if(IsOwner)
        gun.Fire();
    }

    void Movimentar(Vector2 dir)
    {
        Vector3 velFis = Vector3.zero;

        //Andar para frente e andar para trás
        if(dir != Vector2.zero)
        {
            velFis = new Vector3(dir.x * vel, rigid.velocity.y, dir.y * vel) * Time.fixedDeltaTime;
        }

        velFis.y = (rigid.velocity.y < 0) ? rigid.velocity.y * 1.05f : rigid.velocity.y;

        rigid.velocity = velFis;

        //Atualiza a Máquina de Estados (FSM)
        AttFSM(false);

        if(dir != Vector2.zero)
        {
            Rotacionar();
        }

        //Descongela a velocidade das animações
        isRestore = Physics.CheckSphere(transform.GetChild(1).position, 0.5f, chao, QueryTriggerInteraction.Ignore);
        if(isRestore)
        {
            Descongelar();
        }        
    }

    void Rotacionar()
    {
        dir = new Vector3(currentMove.x, 0, currentMove.y);

        Quaternion targetRot = Quaternion.LookRotation(dir);
        Quaternion playerRot = Quaternion.Slerp(transform.rotation, targetRot, velRot * Time.fixedDeltaTime);

        transform.rotation = playerRot;
    }

    public void Pular(InputAction.CallbackContext context)
    {

        //Debug.Log("Pular");
        isGround = Physics.CheckSphere(transform.GetChild(0).position, 0.1f, chao, QueryTriggerInteraction.Ignore);
        
        if(IsOwner){
        if(isGround)
        {
            rigid.AddForce(Vector3.up * pulo, ForceMode.VelocityChange);
            Debug.Log("Passei Aqui!");
        }

        //Atualiza a Máquina de Estados (FSM)
        AttFSM(isGround);
        }
    }

    public void TeamSwap(InputAction.CallbackContext context){
        SwapTeam();
    }
  
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.GetChild(0).transform.position, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.GetChild(1).transform.position, 0.5f);
    }
      #endregion

    #region StatesFSM
    void AttFSM(bool isGround){

        if(currentMove != Vector2.zero)
        {
            anim.SetBool("movimentando", true);
            
        }else
        {
            anim.SetBool("movimentando", false);
        }

        if(isGround)
        {
            anim.SetTrigger("pular");
            netAnim.SetTrigger("pular");
        }
    }

    void AttPulo()
    {
        if(isGround)
        {
            anim.SetTrigger("pular");
        }
    }

       //Congela a animação do pulo no apice do salto
    public void Congelar()
    {
        Debug.Log("Cogelando");
        anim.speed = 0.2f;
    }

    void Descongelar()
    {
        Debug.Log("Descongelou");
        anim.speed = 1.0f;
    }

    #endregion

    void OnEnable()
    {

            mover.Enable();
                
            pular.Enable();
            pular.performed += Pular;

            teamSwap.Enable();
            teamSwap.performed += TeamSwap;

            fire.Enable();
            fire.performed += Fire;

    }

    void OnDisable()
    {
        pular.Disable();
        teamSwap.Disable();
        fire.Disable();
    }

    void Awake()
    {
        input = new PlayerInputActions();
        mover = input.Player.Move;
        pular = input.Player.Jump;
        teamSwap = input.Player.TeamSwap;
        fire = input.Player.Fire;

        mover.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
               
        pular.performed += ctx => Debug.Log(ctx.ReadValueAsObject());

        teamSwap.performed += ctx => Debug.Log(ctx.ReadValueAsObject());

        fire.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
    }

    void Start()
    {
        gun= GetComponentInChildren<Gun>();
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        anim = gameObject.GetComponent<Animator>();
        netAnim=this.GetComponent<NetworkAnimator>();
    }

    void FixedUpdate()
    {
        currentMove = mover.ReadValue<Vector2>();

        if(pause == false)
        {
            if(IsOwner == true)
            Movimentar(currentMove);
        }
    }
}
