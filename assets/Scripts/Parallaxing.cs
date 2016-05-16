using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

    //Store Parents of the background
    public Transform[] backgrounds;
    //Store the amount to parallax
    private float[] parallaxScales;
    //Store the smoothing value
    public float smoothing = 1f;
    //Reference the main camera
    private Transform cam;  
    //Store the previous camera position              
    private Vector3 previousCamPos;      

    void Awake()
    {
        //Set the camera reference
        cam = Camera.main.transform;
    }


    void Start()
    {
        //Set the previous camera position
        previousCamPos = cam.position;
        //Set the scale of parallax
        parallaxScales = new float[backgrounds.Length];

        //Loop through all background layers
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //Set the scale according to how far back the background is
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        //For all the backgrounds
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //Parallax relative to the camera movement
            float parallaxX = (previousCamPos.x - cam.position.x) * 
                parallaxScales[i];
            float parallaxY = (previousCamPos.y - cam.position.y) * 
                parallaxScales[i];

            //Offset the background according to the the parallax scale
            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;


            //Create a vector for the desired position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, 
                backgroundTargetPosY, backgrounds[i].position.z);

            //Lerp the background to the desired position 
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, 
                backgroundTargetPos,smoothing * Time.deltaTime);
        }
        //Set previous cam position
        previousCamPos = cam.position;
    }
}
