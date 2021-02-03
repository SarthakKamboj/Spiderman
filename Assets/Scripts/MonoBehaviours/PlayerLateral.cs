using UnityEngine;
using Move;

public class PlayerLateral : MonoBehaviour
{

    public Transform camTransform;

    private float horizontal, vertical;
    [SerializeField]
    private CharacterController cc;
    private Vector3 direction;
    private float turnSmoothVel;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float maxVel = 20f;
    // private float curMouseMag = 0f;
    // private float prevMouseMag = 0f;


    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void FixedUpdate()
    {

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = GetTargetYRot();
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

            // TODO: get point w/ derivative for prevMouse - curMouseMag and find that delta to get speed

            Vector3 moveDir = Movement.GetDir(targetAngle).normalized;
            cc.Move(moveDir * maxVel * Time.deltaTime);

        }
    }

    private float GetTargetYRot()
    {
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
    }

}
