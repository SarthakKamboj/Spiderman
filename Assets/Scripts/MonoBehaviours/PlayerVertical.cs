using UnityEngine;

public class PlayerVertical : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask groundLayerMask;
    [SerializeField]
    CharacterController cc;

    float yVel;
    bool jumping = false;

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        if (isGrounded())
        {
            if (!jumping)
            {
                yVel = -2f;
            }
            else if (yVel < 0f)
            {
                jumping = false;
            }
        }
        yVel += Physics.gravity.y * Time.fixedDeltaTime;
        cc.Move(new Vector3(0f, yVel * Time.fixedDeltaTime, 0f));
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jumping = true;
            yVel += 10f;
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, radius, groundLayerMask);

    }

}
