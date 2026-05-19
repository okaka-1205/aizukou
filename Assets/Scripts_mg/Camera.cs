using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 [SerializeField] private Transform target;
 

    // Update is called once per frame
    void Update()
    {FollowTarget();
        
    }
    private void FollowTarget()
    {
        if (target != null)
        {
            transform.position = target.position;
            
        }
    }
}
