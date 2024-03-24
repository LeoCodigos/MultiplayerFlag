using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Vector2 currentMove;
    [SerializeField] private Vector2 cameraLook;


    private PlayerInputActions inputs;
    private InputAction move, jump, teamSwap, fire, look; 

    void OnEnable()
    {
        inputs.Enable();

        jump.performed += characterMovement.Jump;
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Awake()
    {
        inputs = new PlayerInputActions();

        move = inputs.Player.Move;
        jump = inputs.Player.Jump;
        teamSwap = inputs.Player.TeamSwap;
        look = inputs.Player.Look;
    }

    void FixedUpdate()
    {
        currentMove = move.ReadValue<Vector2>();

        characterMovement.SetInput(new CharacterMovementInput()
        {
            MoveInput = currentMove
        });
    }

    void Update()
    {
        cameraLook = look.ReadValue<Vector2>();
        cameraController.CameraRotation(new Vector2(cameraLook.y, cameraLook.x));
    }

}
