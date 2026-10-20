using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    float speed = 5f;
    float mouseSense = 0.4f;

    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MovePlayer()
    {

        //mouse movement
        Vector2 mouseDirection = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(0, mouseDirection.x * mouseSense, 0); 

        //WASD
        Vector2 direction = moveAction.action.ReadValue<Vector2>();
        Vector3 move = (transform.forward * direction.y) + (transform.right * direction.x);
        move.y = -1f;
        controller.Move(move * speed * Time.deltaTime);

        

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
