using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour
{
    private float timer;
    private int rot = 10;
    public bool stopRotation = false;

    public GameObject startText;
    public int CameraZoom;

    public Camera camera;
    

    // Start is called before the first frame update
    void Start()
    {
        camera.fieldOfView = 150;
        transform.Rotate(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rot * Time.deltaTime, 0);
        
        
        
        if (Input.GetKeyDown(KeyCode.Space) && stopRotation == false)
        {
            rot = 100;
            stopRotation = true;
            Destroy(startText);

        }
        
        
        if (stopRotation)
        {
            if (transform.eulerAngles.y > -10 && transform.eulerAngles.y < 10)
            {
                Debug.Log("PARE");
                rot = 0;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        
        
        
        if (stopRotation && camera.fieldOfView >= CameraZoom)
        {
            camera.fieldOfView -= 40 * Time.deltaTime;
        }
        if (camera.fieldOfView < CameraZoom)
        {
            camera.fieldOfView = CameraZoom;
        }
        
        
    }

    
}


