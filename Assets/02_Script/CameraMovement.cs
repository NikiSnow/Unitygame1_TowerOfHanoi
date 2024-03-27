using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetDistanceFromCamera;
    private float X, Y;

    public float sensitivity = 6; // чувствительность мышки
	//[HideInInspector]
	public float limit_y = 1f; // ограничение вращения по Y
	[HideInInspector]
	public float limit_x_left = 20f;
	[HideInInspector]
	public float limit_x_right = 20f;
	[HideInInspector]
	public float zoomSpeed = 1f; // чувствительность при увеличении, колесиком мышки
    [HideInInspector]
	public float zoomMax = 20; // макс. увеличение
	[HideInInspector]
	public float zoomMin = 2; // мин. увеличение
    public bool activate_X_LimitAngle = false;
	private bool startPositionDone = false;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        RotateAroundTarget();
    }

    private	void RotateAroundTarget()
	{
		if(!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
		{			
			X += Input.GetAxis("Mouse X") * sensitivity;
			if(activate_X_LimitAngle)X = Mathf.Clamp (X, -limit_x_left, limit_x_right);	
			Y += Input.GetAxis("Mouse Y") * sensitivity;
            if(activate_X_LimitAngle)Y = Mathf.Clamp (Y, -limit_y, limit_y);				
		}	
		transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offsetDistanceFromCamera + target.position;

        transform.LookAt(target);
	}
}
