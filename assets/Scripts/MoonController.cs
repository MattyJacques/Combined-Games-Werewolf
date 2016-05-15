using UnityEngine;
using System.Collections;

public class MoonController : MonoBehaviour
{
	public Vector3 startPos;
	public Vector3 endPos;

	private float speed = 50f;
	private Vector3 nextPos;
    private Transform currentPos;
    // Use this for initialization
    void Start ()
	{
		startPos = transform.position;
		//endPos = new Vector3 (20f, 4.5f, 80f);
		currentPos = gameObject.GetComponent<Transform> ();
		currentPos.position = startPos;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
        nextPos = (endPos - transform.position) / speed * Time.deltaTime;
        nextPos = new Vector3(currentPos.position.x + nextPos.x, currentPos.position.y + nextPos.y, currentPos.position.z);
		currentPos.position = nextPos;

		if (currentPos.position.x > endPos.x) {
			currentPos.position = startPos;
		}
	}
}
