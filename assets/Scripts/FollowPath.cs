using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

  //enumeration types
  public enum FollowType
  {
    MoveTowards, 
    Lerp
  }

  public FollowType type = FollowType.MoveTowards; // sets type to start
  public PlatformPath path;                        // path to follow
  public float speed = 1;                          // speed to move
  public float maxDistanceToGoal = 0.1f;           // 

  private IEnumerator<Transform> currentPoint;     // current point
    private Vector3 startPos;

    // Use this for initialization
    public void Start()
    {
        if (path == null) //if path is empty throw an error
        {
            Debug.LogError("path cannot be null", gameObject);
            return;
        }

        //get the path start
        currentPoint = path.GetPathEnumerator();
        currentPoint.MoveNext(); // strt moving

        if (currentPoint.Current == null)
        {
            return; // if current point is nothing break
        }

        path.atEnd = true;
        transform.position = currentPoint.Current.position;
        startPos = transform.position;
        //set the transform of the platform to the current point
        
    }
	
	// Update is called once per frame
	public void Update ()
  {
    //if current point is empty or bust
	  if(currentPoint == null || currentPoint.Current == null)
    {
      return;
    }

      if (path.atEnd)
        {
            transform.position = startPos;
            path.atEnd = false;
        }
    // if type of movement is movetowards, do that
    if(type == FollowType.MoveTowards)
    {
      transform.position = Vector3.MoveTowards(transform.position,
                                               currentPoint.Current.position,
                                               Time.deltaTime * speed);
    }
    //if type  is lerp, lerp
    else if( type == FollowType.Lerp)
    {
      transform.position = Vector3.Lerp(transform.position,
                                        currentPoint.Current.position,
                                        Time.deltaTime * speed);
    }

    // set distance squared variable 
    float distanceSquared = (transform.position - currentPoint.Current.position).sqrMagnitude;

    // if distance squared is less than mdtg squared
    //move to the next point
    if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
    {
      currentPoint.MoveNext();
    }
  }
}
