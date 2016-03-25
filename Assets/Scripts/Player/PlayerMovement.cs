using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Rigidbody playerRigidbody;

    Animator anim;
#if !MOBILE_INPUT
	int floorMask;
	float camRayLength = 100f;
#endif
    

    void Awake()
    {
#if !MOBILE_INPUT
		floorMask = LayerMask.GetMask("Floor");
#endif
        
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
//        float h = Input.GetAxisRaw("Horizontal");
//        float v = Input.GetAxisRaw("Vertical");
		float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
		print ("H:" + h + "  V:" + v);
        Move(h, v);
        Animating(h, v);
        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;//判断角色是否在移动
        anim.SetBool("IsWalking", walking);
        


    }

    void Turning()
    {
#if !MOBILE_INPUT
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//根据当前鼠标的位置，从摄像机发射一条射线，返回射线
		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
			
			
		}

#else
		Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X"),0f,CrossPlatformInputManager.GetAxisRaw("Mouse Y"));
		if(turnDir != Vector3.zero)
		{
			Vector3 playerToMouse = (transform.position + turnDir) - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
#endif
        
    }
}
