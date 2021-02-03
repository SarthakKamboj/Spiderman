using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] GameObject player;
    Vector3 offset;

    void OnEnable()
    {
        offset = Camera.main.transform.position - player.transform.position;
        Debug.Log(offset);
    }

    void Update()
    {
        Camera.main.transform.position = player.transform.position + offset;
    }
}
