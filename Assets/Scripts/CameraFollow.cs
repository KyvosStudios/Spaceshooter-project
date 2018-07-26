using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Transform PivotPoint;

    public static  bool ZaxCam = true;
    public float smoothSpeed = 0.125f;
    public Vector3 Zaxoffset;
    public Vector3 AfterburnOffset;
    private Vector3 currentAngle;
    public Vector3 ZaxtargetAngle;
    public Vector3 AftertargetAngle;
    private void Start()
    {
        currentAngle = transform.eulerAngles;
    }

    void FixedUpdate()
    {
		//if (ZaxCam == true)
		//{

			Vector3 desiredPosition = target.position + Zaxoffset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;
			currentAngle = new Vector3(
			 Mathf.LerpAngle(currentAngle.x, ZaxtargetAngle.x, Time.deltaTime * 2),
			 Mathf.LerpAngle(currentAngle.y, ZaxtargetAngle.y, Time.deltaTime * 2),
			 Mathf.LerpAngle(currentAngle.z, ZaxtargetAngle.z, Time.deltaTime * 2));
			//transform.RotateAround(PivotPoint.position, transform.up, Controls.LeftJoystickXMovement * 30 * Time.deltaTime);

		//	transform.eulerAngles = currentAngle;

		//}
		//if (ZaxCam == false)
		//{
		//	Vector3 desiredPosition = target.position + AfterburnOffset;
		//	Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		//	transform.position = smoothedPosition;
		//	currentAngle = new Vector3(
		//	 Mathf.LerpAngle(currentAngle.x, AftertargetAngle.x, Time.deltaTime*2),
		//	 Mathf.LerpAngle(currentAngle.y, AftertargetAngle.y, Time.deltaTime*2),
		//	 Mathf.LerpAngle(currentAngle.z, AftertargetAngle.z, Time.deltaTime*2));

		//	transform.eulerAngles = currentAngle;
		//}

		transform.LookAt(target);
    }

}
