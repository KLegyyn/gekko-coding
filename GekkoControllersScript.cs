using UnityEngine;
using System.Collections;

public class GekkoControllersScript : MonoBehaviour 
{
	public float maxSpeed = 3f; //Maximum Speed
	bool facingRight = true; //Character is facing right
	
	Animator anim; //Ref for the Animator
	
	bool grounded = false; //Tells if the Character is grounded
	public Transform groundCheck; //Ask where the ground should be
	float groundRadius=0.03f; //How big the sphere is gonna be when we check the ground
	public LayerMask whatIsGround; //Tells the Player what is considered ground
	public float jumpForce = 235;
	float jumpForceMax = 35;
	bool lookingUp = false;
	bool crouching = false;
	bool skidding = false;
	float move;
	bool jumpPress, jumpHeld;
	
	//public float timeLeft = 60f;

	public GekkoSlideControllerScript slideScript;
	public GekkoControllersScript originalScript;
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>(); //Gets the input 'Animator'
		//slideScript.enabled = false;
		originalScript.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		anim = GetComponent<Animator>(); //Gets the input 'Animator'
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//ground = overlaps (where the circle is generated, the radius, all collision)
		//Is there a collider or not
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y); //Every frame, tell me what my Vertical Speed

		anim.SetBool ("Crouch", crouching);
		anim.SetBool ("Up", lookingUp);
		anim.SetBool ("Skid", skidding);

		//rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); //Causes movement of Character
		anim.SetFloat ("Speed", Mathf.Abs (move)); //This controls the animation
		Move();
		//if (move > 0 && !facingRight) 
		//	Flip ();
		// else if (move < 0 && facingRight) 
		//	Flip ();

		if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") >0 && !facingRight) {
			Flip();
				}
		else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") <0 && facingRight) {
			Flip();
		}

		if (grounded && move > 0 && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") <0 && move != 0) {
			anim.SetBool ("Skid", true);
				}

		else if (grounded && move < 0 && Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") >0 && move != 0) {
			anim.SetBool ("Skid", true);
		}

		if (move == 0 && grounded && Input.GetKey(KeyCode.UpArrow)) {
			anim.SetBool("Up",true);
			print ("Looking Up");
		}
		if (move == 0 && grounded && Input.GetKey(KeyCode.DownArrow)) {
			anim.SetBool("Crouch",true);
			print ("Crouch");
		}

		//if (grounded)  //if grounded and pressing Z
		//{
			//GetKeyDown Not the best idea, best to go to input manager, create a jump access or button and say jump so remappable for players
			//anim.SetBool ("Ground",false); //No longer grounded
			//rigidbody2D.AddForce(new Vector2(0,jump * jumpForce)); //Jump! Also add some force!
			//rigidbody2D.AddForce(new Vector2(0,jumpForce));	
			//print("Small Jump");
				
		//}

	}
	
	void Update()
	{
		Jumping();	
	}
	
	void Flip() //Turns the Character Around
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void Move()
	{
		move = Input.GetAxis ("Horizontal");
		maxSpeed = 3f;
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); //Causes movement of Character
		print ("Move");
	}
		//rigidbody2D.AddForce(new Vector2(0,jumpForce)); //Jump! Also add some force!
		//rigidbody2D.velocity = new Vector2(0,jumpForce);
		//rigidbody2D.gravityScale = jumpForce*Time.deltaTime;
		//rigidbody2D.AddForce(Vector2.up *jumpForce, ForceMode2D.Impulse);				

	void Jumping() //This needs to be fixed
	{
		jumpPress = Input.GetButtonDown("Jump");
		jumpHeld = Input.GetButton("Jump");

		if (jumpPress && grounded) {
			rigidbody2D.AddForce(new Vector2(0,jumpForce/2f));
			print ("Jumping...");
				}
		else if (jumpHeld && grounded) {
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			print ("Jumping HIGH!");
				}

	}
	
	
}
