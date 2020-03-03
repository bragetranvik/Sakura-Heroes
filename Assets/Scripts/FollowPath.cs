using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards, JumpTowards
    }

    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;
    private IEnumerator<Transform> pointInPath;

    // Start is called before the first frame update
    void Start()
    {
        if(MyPath == null)
        {
            Debug.LogError("Movement path cannot be null", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if(pointInPath.Current == null)
        {
            Debug.LogError("The path must have points to follow", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
     if(pointInPath == null || pointInPath.Current == null)
        {
            return;
        }
     if (Type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position,
                Time.deltaTime * speed);
        }
     else if  (Type == MovementType.JumpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, 
                pointInPath.Current.position, Time.deltaTime * speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }
}
