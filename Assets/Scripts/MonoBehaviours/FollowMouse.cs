using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    [SerializeField] RectTransform rectTransform;

    void Start()
    {

    }

    void Update()
    {
        rectTransform.anchoredPosition3D = Input.mousePosition + new Vector3(-rectTransform.rect.width / 2, -rectTransform.rect.height / 2, 0f);
    }
}
