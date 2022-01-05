using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	public Transform Target;
	
	[SerializeField]
	public Transform camTransform;

	// offset between camera and target
	[SerializeField]
	public Vector3 Offset;

	// change this value to get desired smoothness

	[SerializeField]
	private float SmoothTime = 0.3f; 
    	// This value will change at the runtime depending on target movement. Initialize with zero vector.

	private Vector3 velocity = Vector3.zero;
 
	private void Start()
	{
		Offset = camTransform.position - Target.position;
	}
 
	private void LateUpdate()
	{
		// update position
		Vector3 targetPosition = Target.position + Offset;
		camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
		// update rotation
		transform.LookAt(Target);
    }
}