using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6.0f; // Normal y�r�me h�z�
    public float sprintSpeed = 12.0f; // Ko�ma h�z�
    public float slowWalkSpeed = 3.0f; // Yava� y�r�me h�z�
    public float jumpSpeed = 8.0f; // Z�plama h�z�
    public float gravity = 20.0f; // Yer �ekimi

    public Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Y�n tu�lar�yla hareketi al
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            // Ko�ma tu�u (Shift) bas�l�ysa h�z artt�r
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= sprintSpeed;
            }
            // Yava� y�r�me tu�u (Ctrl) bas�l�ysa h�z azalt
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                moveDirection *= slowWalkSpeed;
            }
            // Normal y�r�me
            else
            {
                moveDirection *= walkSpeed;
            }

            // Z�plama
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Yer �ekimi uygulama
        moveDirection.y -= gravity * Time.deltaTime;

        // Hareketi uygula
        controller.Move(moveDirection * Time.deltaTime);
    }
}
