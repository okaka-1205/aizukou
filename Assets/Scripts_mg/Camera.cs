using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;

    [SerializeField] private float minX, maxX, minY, maxY;

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        if (target != null)
        {float x = Mathf.Clamp(target.position.x, minX, maxX);
         float y = Mathf.Clamp(target.position.y, minY, maxY);
         transform.position = new Vector3(x, y, transform.position.z);

        }
    }
}
