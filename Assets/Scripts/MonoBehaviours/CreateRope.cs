using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CreateRope : MonoBehaviour
{
    [SerializeField]
    GameObject ropePartPrefab;
    [SerializeField]
    int numParts = 30;

    void Start()
    {
        GameObject prevGO = gameObject;
        Vector3 pos = transform.position;
        float height = GetComponent<Collider>().bounds.size.y;
        for (int i = 0; i < numParts; i++)
        {
            GameObject ropePart = Instantiate(ropePartPrefab, pos + new Vector3(0f, -height * (i + 1), 0f), Quaternion.Euler(Vector3.up));
            ropePart.name = "RopePart" + (i + 1);
            ropePart.transform.SetParent(transform.parent);
            CharacterJoint[] prevGOJoints = prevGO.GetComponents<CharacterJoint>();
            if (prevGOJoints.Length > 1)
            {
                prevGOJoints[1].connectedBody = ropePart.GetComponent<Rigidbody>();
            }
            CharacterJoint[] characterJoints = ropePart.GetComponents<CharacterJoint>();
            characterJoints[0].connectedBody = prevGO.GetComponent<Rigidbody>();
            prevGO = ropePart;

            if (i == numParts - 1)
            {
                Destroy(characterJoints[1]);
                ropePart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }

}
