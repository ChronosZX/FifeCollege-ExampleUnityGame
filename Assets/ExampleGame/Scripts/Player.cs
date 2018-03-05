using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Public variables visible in the editor (Unity)
    public string logText = "Hello world again";
    public float speed = 2;
    public float jumpSpeed = 50;
    public float health = 10;
	public int allowedAirJumps = 0;

	// Private variables not visible or accessible outside this script
	private int numAirJumps = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log(logText);
        //ApplyDamage(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Getting the rigidbody from the game object we are attached to
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        // Number between -1 and 1 based on player pressing left or right
        float horizontal = Input.GetAxis("Horizontal");

        // Boolean (true or false) based on player pressing space bar
        bool jump = Input.GetButtonDown("Jump");

        // Find out if we are touching the ground

        // Get the collider component attached to this object
        Collider2D collider = GetComponent<Collider2D>();

        // Find out if we are colliding with the ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        bool touchingGround = collider.IsTouchingLayers(groundLayer);

		// If we are touching the ground,
		//   we can reset our air jumps to 0
		if (touchingGround)
			numAirJumps = 0;

		// Normally we are only allowed to jump if we are touching the ground
		bool allowedToJump = touchingGround;

        // However, if our allowed air jumps are
		//	higher than our current air jump count
		//	(meaning we have at least 1 jump left)
		// we are allowed to jump.
		// Even if we aren't touching the ground!
		if (allowedAirJumps > numAirJumps) 
		{
			allowedToJump = true;
		}
		
        // Cache a local copy of our rigidbody's velocity
        Vector2 velocity = rigidBody.velocity;

        // Set the x (left/right) component of the velocity based on our input
        velocity.x = horizontal * speed;

        // Set the y (up/down) component of the velocity based on jump
		if (jump == true && allowedToJump == true)
        {
            velocity.y = jumpSpeed;

			if (touchingGround != true) { // or touchingGround == false
				numAirJumps = numAirJumps + 1;
			}
        }

        // Set our rigidbody's velocity based on our local copy
        rigidBody.velocity = velocity;

        // Print a log when the mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse button down!");
        }

        // Print a log of the mouse position
        Vector2 mousePosition = Input.mousePosition;
        Debug.Log("Mouse position is "+mousePosition);
    }

    public void ApplyDamage (float damageToDeal)
    {
        health = health - damageToDeal;
    }
}

// NO CODE HERE
