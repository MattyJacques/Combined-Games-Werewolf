using UnityEngine;
using System.Collections;

public class MoonController : MonoBehaviour
{
    //Set the start position
	public Vector3 startPos;
    //Set the end position
	public Vector3 endPos;

    //Set the speed
	private float speed = 50f;
    //Next position variable
	private Vector3 nextPos;
    //Current positon
    private Transform currentPos;

    void Awake()
	{
        //Store the starting position
		startPos = transform.position;
        //Store the current position
		currentPos = gameObject.GetComponent<Transform> ();
        //Set the current postion
		currentPos.position = startPos;
	}

	void FixedUpdate ()
	{
        //Set the next position difference
        nextPos = (endPos - transform.position) / speed * Time.deltaTime;
        //Set the next position
        nextPos = new Vector3(currentPos.position.x + nextPos.x, 
            currentPos.position.y + nextPos.y, currentPos.position.z);
        //Update the current position
		currentPos.position = nextPos;
        //If we are past the end position, reset the position
		if (currentPos.position.x > endPos.x) {
			currentPos.position = startPos;
		}
	}
}
