using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformPath : MonoBehaviour {

 
  public Transform[] platformPoints;  //points for the platform


  public IEnumerator<Transform> GetPathEnumerator()
  {
    //if path is empty or too short, break
    if(platformPoints == null || platformPoints.Length < 1)
    {
      yield break;
    }

    // set direction and index
    int direction = 1;
    int index = 0;

    //always looping
    while(true)
    {
      //return index of points
      yield return platformPoints[index];

      // if length == 1 continue
      if(platformPoints.Length == 1)
      {
        continue;
      }

      // if index < 0 change direction
      if(index <= 0)
      {
        direction = 1;
      }
      //if index greater than length - 1 change direction
      else if(index >= platformPoints.Length - 1)
      {
        direction = -1;
      }
      //set index
      index = index + direction;
    }
  }

  public void OnDrawGizmos()
  {
    //if platform points is null or less than minimum return
    if(platformPoints == null || platformPoints.Length < 2)
    {
      return;
    }
    //draw lines between the points, for easier setup
    for(int i = 1; i< platformPoints.Length; i++)
    {
      Gizmos.DrawLine(platformPoints[i - 1].position, platformPoints[i].position);
    }
  }
}
