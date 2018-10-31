using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    [SerializeField] private bool m_DoubleJumpEnabled;
    [Range(1,1000)][SerializeField] private float m_DoubleJumpForce; // A variable that stores the force of the double-jump
	[Range(1,50)] [SerializeField] private float m_GlideDrag;  // A variable that stores the drag from the glide function
	[SerializeField] private bool m_EnableGlide; // A variable that stores if the glide is enabled/not
	[Range(1,10)] [SerializeField] private float m_GlideSpeed; // A variable that stores how fast the character is moving whilst gliding

	const float k_GroundedRadius = 0.3f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero; 
    private bool already_jumped = false; // For determining if the character has already jumped
	public Rigidbody2D rb; // Rigidbody to reference the character rigidbody for glide fumction
    private Animator anim; //Adds Animator component
    public PlayerMovement player_m; //Adds PlayerMovement Scripe


    public bool shotDelay; //Disable or Enable Fire Buttons
    public bool shooting; //Shooting Animation to play or not
    public GameObject projectile; //What projectile is firing
    public Transform firePoint; //Where the projectile is firing from(in this case the side)
    public Transform firePointSideUp; //Same as above but the projectile will more diagonally and needs to position the firing point
    private bool dirKeys; //Tells us we are hitting Direction Keys for Left and Right
    public bool flipped; // A bool to tell if it has flipped and therefore need to change motion for shot to go other way
    public float shotSpeed; //How fast is the shot
    public bool noShootingDash; //Disables firing if Dashing
    public bool DeadAnimation = false;
    
    
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

 	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player_m.GetComponent<PlayerMovement>();
       

        flipped = false; // Starts the code off left so no flipping at this stage
        shooting = false; // Tells Shooting Animation not to play yet
        shotDelay = false; // Is a bool to delay the shot so that it dosen't fire rapidly
    }


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Update()
	{
        anim.SetBool("isGrounded", m_Grounded);
        anim.SetBool("isShootingSide", shooting);
        anim.SetBool("Dead", DeadAnimation);

        if (Input.GetAxis("Horizontal") > 0)
        {
            // If Key is hitting left
            flipped = false; // It won't flip
            dirKeys = true; //Tells us we are hitting Direction Keys for Left and Right
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            flipped = true; //It has flipped and therefore need to change motion for shot to go other way
            dirKeys = true; //Tells us we are hitting Direction Keys for Left and Right
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
        else
        {
            dirKeys = false; // If no Direction Key is pressed then this bool is false
        }

        if(Input.GetButtonUp("Fire1"))
        {
            shooting = false; //If shooting button is let go, shooting animation will not play
        }

        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  == 0 && !dirKeys && flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideLeftStill ()); //Starts a Timer when pressed to start ShootingSideLeftStill Function
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  == 0 && !dirKeys && !flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideRightStill ());
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  == 0 && dirKeys && !flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideRightRun ());
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  == 0 && dirKeys && flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideLeftRun ());
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
    
        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  > 0 && dirKeys && !flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideUpRightRun ());
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
        if(Input.GetButton("Fire1") && Input.GetAxis("Vertical")  > 0 && dirKeys && flipped && !shotDelay && !noShootingDash){
            StartCoroutine (ShootingSideUpLeftRun ());
            shooting = true; //Tells Shooting animation to play
            player_m.isPressed = true; //A button is pressed with attaches to Idle animation
        }
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
        m_Grounded = false;
        player_m.glidingButton = true;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
                player_m.glidingButton = false;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool glide)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		// Allow the player to glide only if in air and not jumping at the same moment
		if(glide && !m_Grounded && !jump && m_EnableGlide) 
		{
			rb.drag = m_GlideDrag;
			move *= m_GlideSpeed;
		} else if(!glide || m_Grounded) 
		{
			rb.drag = 0;
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
        // If the player should jump...
        if (m_Grounded && jump)
        { 
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
        else if (!m_Grounded && jump && !already_jumped && m_DoubleJumpEnabled)
        {
            already_jumped = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_DoubleJumpForce)); //Add vertical force to the player
        }
        if (m_Grounded)
        {
            already_jumped = false;
        }
	}


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    IEnumerator ShootingSideLeftRun()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePoint.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = -shotSpeed; // Shoots Left while running
        Vector3 newScale = go.transform.localScale; //These 3 bits of code tell the projectile to flipped with the Character, making the projectile go in the right direction
        newScale.x *= -1;
        go.transform.localScale = newScale;
        shotDelay = true; //Disables fire buttons
        yield return new WaitForSeconds(0.3f); //Wait 0.3 seconds
        shotDelay = false; //Enable fire buttons
    }

    IEnumerator ShootingSideRightRun()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePoint.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = shotSpeed; // Shoots Left while running
        shotDelay = true;
        Vector3 newScale = go.transform.localScale;
        newScale.x *= +1;
        go.transform.localScale = newScale;
        yield return new WaitForSeconds(0.3f);
        shotDelay = false;
    }

    IEnumerator ShootingSideRightStill()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePoint.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = shotSpeed; // Shoot Right with no movement while facing right
        shotDelay = true;
        Vector3 newScale = go.transform.localScale;
        newScale.x *= +1;
        go.transform.localScale = newScale;
        yield return new WaitForSeconds(0.3f);
        shotDelay = false;
    }

    IEnumerator ShootingSideLeftStill()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePoint.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = -shotSpeed; // Shoot Left with no movement while facing left
        Vector3 newScale = go.transform.localScale;
        newScale.x *= -1;
        go.transform.localScale = newScale;
        shotDelay = true;
        yield return new WaitForSeconds(0.3f);
        shotDelay = false;
    }

    IEnumerator ShootingSideUpRightRun()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePointSideUp.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = shotSpeed; // Shoots Right and Up while Running
        go.GetComponent<ProjectileController>().yspeed = shotSpeed;
        shotDelay = true;
        yield return new WaitForSeconds(0.3f);
        shotDelay = false;
    }

    IEnumerator ShootingSideUpLeftRun()
    {
        GameObject go = (GameObject)Instantiate(projectile, firePointSideUp.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().xspeed = -shotSpeed; // Shoots Left and Up while Running
        go.GetComponent<ProjectileController>().yspeed = shotSpeed;
        shotDelay = true;
        yield return new WaitForSeconds(0.3f);
        shotDelay = false;
    }
}
