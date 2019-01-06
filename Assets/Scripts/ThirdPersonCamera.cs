using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour {

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .125f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;
    
    //    Use this for initialization
	void Start () {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        //Only move the camera based on mouse axis if the right mouse button is down. 
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);


        }
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        //Vector3 currentRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }

    public void SetMouseLookSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }
}
