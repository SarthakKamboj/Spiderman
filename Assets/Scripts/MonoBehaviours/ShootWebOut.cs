using UnityEngine;

public class ShootWebOut : MonoBehaviour
{
    [SerializeField] GameObject webPrefab;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] float springVal = 10f;
    [SerializeField] float damperVal = 3f;
    [SerializeField] float initialForceX = 10f;
    [SerializeField] float initialForceY = 10f;
    [SerializeField] LayerMask layerMask;
    SpringJoint sj;
    [SerializeField] LineRenderer lr;
    [SerializeField] float playerCloseToWallThreshold = 2f;
    HAND hand = HAND.NONE;
    Rigidbody rb;
    float movementOffset = 0f;
    [SerializeField] float movementOffsetMultiplier = 10f;

    enum HAND
    {
        NONE = -1,
        LEFT = 0,
        RIGHT = 1
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        ApplyGlideForce();

        RenderWeb();
        HandleWebExistence();

        if (GetMouseButtonDown())
        {
            HAND h = HAND.LEFT;
            if (Input.GetMouseButtonDown(1))
            {
                h = HAND.RIGHT;
            }

            CreateWeb(h);
        }

    }

    void ApplyGlideForce()
    {
        if (sj)
        {
            rb.AddForce(new Vector3(0f, movementOffset * movementOffsetMultiplier * Time.deltaTime, 0f));
        }
    }


    void HandleWebExistence()
    {
        if (sj)
        {
            if (Vector3.Distance(lr.GetPosition(1), lr.GetPosition(0)) <= playerCloseToWallThreshold && rb.velocity.y > 0)
            {
                RemoveWeb();
            }
        }
    }


    void RenderWeb()
    {
        switch (hand)
        {
            case HAND.NONE:
                return;
            case HAND.LEFT:
                lr.SetPosition(1, leftHand.position);
                break;
            case HAND.RIGHT:
                lr.SetPosition(1, rightHand.position);
                break;
        }


    }

    void RemoveWeb()
    {
        if (sj)
        {
            Destroy(sj);
            lr.positionCount = 0;
            hand = HAND.NONE;
        }
    }

    void CreateWeb(HAND h)
    {

        // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, 0f, Screen.height / 2));
        // Vector3 camCenterWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            // if a web already exists, destroy it before creating the new web
            if (sj)
            {
                RemoveWeb();
            }

            // set which hand the web was shot from
            hand = h;

            // add spring to move closer to hit point/ building
            sj = gameObject.AddComponent<SpringJoint>();
            sj.autoConfigureConnectedAnchor = false;
            sj.connectedAnchor = hit.point;

            // set spring distance
            float distance = Vector3.Distance(transform.position, hit.point);
            sj.minDistance = distance * 0.1f;
            sj.maxDistance = distance * 0.8f;

            // set springiness and dampness
            sj.spring = springVal;
            sj.damper = damperVal;

            // add force up in direction of movement 
            // Vector3 initialForceDir = (hit.point - transform.position).normalized;
            // initialForceDir.x *= initialForceX;
            // initialForceDir.y *= initialForceY;
            // rb.AddForce(initialForceDir, ForceMode.Impulse);

            // Vector3 dir = hit.point - transform.position;
            // Vector3 right = Vector3.Cross(dir, Vector3.up);
            // rb.AddForce(right.normalized * 2, ForceMode.Impulse);

            lr.positionCount = 2;
            lr.SetPosition(0, hit.point);

            Invoke("RemoveWeb", 2f);
            movementOffset = (hit.point - transform.position).y;
            if (movementOffset > 0) movementOffset = 1;
            else movementOffset = -1;

            // RenderWeb();

        }
    }

    bool GetMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }

}
