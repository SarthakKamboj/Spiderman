using UnityEngine;

public class DrawGizmos : MonoBehaviour
{

    [SerializeField]
    Color color = Color.red;
    [SerializeField]
    float radius = 0.5f;


    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
