using UnityEngine;
using System.Collections;

public class DeplacementPlayer : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float multiply = 1.0f;
    public float stamina = 20.0f;
    public float staminaMax = 20.0f;
    public float staminaCount = 0.5f;
    public float jumpCount = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        speed = 4.0F;
        jumpSpeed = 8.0F;
        gravity = 20.0F;
        multiply = 1.0f;
        staminaMax = 20.0f;
        stamina = staminaMax;
        staminaCount = 0.5f;
        jumpCount = 5.0f;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded && GetComponent<Player>().GetState() == Player.State.alive)
        {   
            if (Input.GetButton("Sprint") && stamina > 0)
            {
                RemoveStamina();
                multiply = 1.5f;
            }
            else
            {
                AddStamina();
                multiply = 1.0f;
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed * multiply;

            if (Input.GetButton("Jump"))
            {
                if (jumpCount < stamina)
                {
                    moveDirection.y = jumpSpeed;
                    RemoveStamina(jumpCount);
                }
                else
                {
                    moveDirection.y = jumpSpeed * (stamina / jumpCount);
                    RemoveStamina(stamina / jumpCount);
                }
            }

        }
        else if (GetComponent<Player>().GetState() == Player.State.menu)
        {
            moveDirection = new Vector3(0, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed * multiply;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void AddStamina()
    {
        stamina += Time.deltaTime;
        if(stamina >= staminaMax)
        {
            stamina = staminaMax;
        }
    }

    void RemoveStamina(float amount = 0)
    {
        if (amount == 0)
            stamina -= Time.deltaTime;
        else
            stamina -= amount;

        if (stamina <= 0)
        {
            stamina = 0;
        }
    }
}
