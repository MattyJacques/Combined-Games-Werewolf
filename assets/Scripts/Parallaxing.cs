using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

  public Transform[] backgrounds;       //backgrounds of the game
  private float[] parallaxScales;       //proportion for cameras movement
  public float smoothing = 1f;          //smoothing of movement

  private Transform cam;                //reference for cam
  private Vector3 previousCamPos;       // cam position in the previous frame

  void Awake()
  {
    cam = Camera.main.transform;        //set up camera reference
  } //Awake


	void Start ()
  {

    previousCamPos = cam.position;      // prev frame had camera position

    parallaxScales = new float[backgrounds.Length]; //assign parallax scales
    for(int i = 0; i < backgrounds.Length; i++)
    {
      parallaxScales[i] = backgrounds[i].position.z * -1;
    }
	}
	
	// Update is called once per frame
	void Update ()
  {
    //for each background
	  for(int i = 0; i < backgrounds.Length; i++)
    {
      //pallax is opposite of the camera movement
      float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

      //target x position = current x plus parallax
      float backgroundTargetPosX = backgrounds[i].position.x + parallax;

      //target pos = background current position with its target x pos
      Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,
                                                backgrounds[i].position.y,
                                                backgrounds[i].position.z);

      // fade between current pos and the target pos using lerp
      backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, 
                                             backgroundTargetPos, 
                                             smoothing * Time.deltaTime);
    }
    //set previous cam position
    previousCamPos = cam.position;
	}
}
