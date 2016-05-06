using UnityEngine;
using System.Collections;

public class MoonController : MonoBehaviour
{
	public Vector3 startPos;
	public Vector3 endPos;

	private float speed = 1.3f;
	private Vector3 nextPos;
    private Transform currentPos;
    // Use this for initialization
    void Start ()
	{
		startPos = transform.position;
		endPos = new Vector3 (20f, 4.5f, 80f);
		currentPos = gameObject.GetComponent<Transform> ();
		currentPos.position = startPos;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
        nextPos = new Vector3(currentPos.position.x + (speed * Time.deltaTime), currentPos.position.y, currentPos.position.z);
		currentPos.position = nextPos;

		if (currentPos.position.x > endPos.x) {
			currentPos.position = startPos;
		}
	}
}
