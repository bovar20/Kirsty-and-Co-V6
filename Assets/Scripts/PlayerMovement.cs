using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Controller; // References the CharacterController2D Script located in player
    private float HorizontalMove = 0f; // Variable to store the returned value from "Input.GetAxisRaw" which represents user input for horizontal movement
    public float runSpeed = 40f; // Variable to store the speed at which the character will move in-game
    private Animator anim; // Variable to refrence the animator component
    public bool playerjump = false; // Variable to store if the player has started a jump
    public bool playercrouch = false; // Variable to store if the player is currently crouching
    public bool isRunning; // Is the character running?
    //private Vector3 SpawnPosition; // Variable to store the starting position of the character
    [SerializeField] private bool m_EnableSprint; // Variable to store if the sprint is enabled
    [Range(1, 10)] [SerializeField] private float m_SprintDelay; // Variable to store the duration the sprint delay
    private bool currently_sprinting = false; // Variable to store if the character is currently sprinting
    private float original_speed; // Variable to store the original speed of the character
    private float sprint_timer = 0; // Variable to store the time that the character is able to sprint
    private float sprint_cooldown = 0; // Variable to store how long the cooldown has been in effect
    private bool on_cooldown = false; // Variable to store if the character is currently on a cooldown
    [Range(1,10)][SerializeField] private float m_SprintModifier; // Variable to change the modifier for the sprint
    [Range(1,10)][SerializeField] private float m_SprintTime; // Variable to determine how long the sprint will last 
    public bool is_gliding = false;
    private bool isCrouching = false; //Animation check for if the player is crouching
    [Range(0.01f, 1)] [SerializeField] private float m_DamageFlickerTime; // Variable to determine how long the sprint will last 
    private float hide_time = 0;
    public float idleTime; //The maximum amount of time for how long the player has to stand before going into the Idle Animation
    private float idleTimer; //The idle timer timing down to function Idle Animation
    public bool isPressed; //Check if a button is pressed to interupt Idle Animation countdown
    public bool glidingButton; //Check if character is in the air for glide to work with the jump button
    public bool player_hit = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        Controller = GetComponent<CharacterController2D>();
        //SpawnPosition = transform.position; // Save the initial position of the character
        original_speed = runSpeed; // Save the initial speed of the character
        idleTimer = idleTime; //IdleTimer start with the maximum amount of the value of idleTime

    }
    // Update is called once per frame
    void Update()
    {
        //All Animation for Kirsty to function
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("isDashing", currently_sprinting);
        anim.SetFloat("isIdle", idleTimer);


        // Get input from player
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Returns a value based on user input for horizontal movement e.g -1 = Left Arrow, 1 = Right Arrow, 0 = null

        idleTimer -= Time.deltaTime; //Times down idleTimer ready to play Idle Animation

        if (idleTimer < 0 && !isPressed)
        {
            idleTimer = 0; //If idle time is below 0 and nothing has been pressed. IdleTimer = 0;
        } else if (isPressed){
                idleTimer = idleTime; //idleTimer goes back to the maximum amount to wait of the value of idleTime
            isPressed = false; //A button is pressed with attaches to Idle animation
        }

        // Sprinting code
        if (Input.GetButtonUp("Sprint") && Input.GetAxis("Horizontal") > 0) // Check if the Right Control key is up to disable sprint
        {
            currently_sprinting = false;
            isPressed = true; //A button is pressed with attaches to Idle animation
            Controller.noShootingDash = true; //Disables fire button on Character Controller 2D
            if (!on_cooldown)
            {
                // If the character is not on cooldown already, it should be, hence enable cooldown
                on_cooldown = true;
            }
        }

        if (Input.GetButtonDown("Crouch")) {
            isCrouching = true; //Tells Crouching Animation to play
            isPressed = true; //A button is pressed with attaches to Idle animation
        } else if (Input.GetButtonUp("Crouch")){
            isCrouching = false; //Crouching Animation will not play if Key is up
        }
        

        if (Input.GetButtonUp("Sprint") && Input.GetAxis("Horizontal") < 0) // Check if the Right Control key is up to disable sprint
        {
            currently_sprinting = false;
            isPressed = true; //A button is pressed with attaches to Idle animation
            Controller.noShootingDash = true; //Disables fire button on Character Controller 2D
            if (!on_cooldown)
            {
                // If the character is not on cooldown already, it should be, hence enable cooldown
                on_cooldown = true;
            }
        }

        if (Input.GetButtonDown("Sprint") && Input.GetAxis("Horizontal") > 0 && isRunning && !currently_sprinting && m_EnableSprint && !on_cooldown)  
        {
            currently_sprinting = true;
            isPressed = true; //A button is pressed with attaches to Idle animation
            Controller.noShootingDash = true; //Disables fire button on Character Controller 2D
            runSpeed *= m_SprintModifier; // Increase the speed of the character by a factor of 1.5x
        }

        if (Input.GetButtonDown("Sprint") && Input.GetAxis("Horizontal") < 0 && isRunning && !currently_sprinting && m_EnableSprint && !on_cooldown)
        {
            currently_sprinting = true; 
            isPressed = true; //A button is pressed with attaches to Idle animation
            Controller.noShootingDash = true; //Disables fire button on Character Controller 2D
            runSpeed *= m_SprintModifier; // Increase the speed of the character by a factor of 1.5x
        }

        if(currently_sprinting)
        {
            if(sprint_timer > m_SprintTime) // Has the character been sprinting for over 2 seconds already?
            {
                // Disable the sprint and enable the cooldown
                currently_sprinting = false;
                on_cooldown = true;
            }
            sprint_timer += Time.deltaTime; // Increment the timer
        }
        else
        {
            sprint_timer = 0; // If the character is not sprinting, the sprint timer must be 0
        }

        if(!currently_sprinting) 
        {
            // Reset the character to status before the sprint was enabled
            runSpeed = original_speed;
            sprint_timer = 0;
            Controller.noShootingDash = false; //Enables fire button on Character Controller 2D
        }

        if(on_cooldown)
        {
            sprint_cooldown += Time.deltaTime; // Increment the timer
            if(sprint_cooldown > m_SprintDelay) // Has the cooldown exceeded the allocated delay?
            {
                on_cooldown = false; // The cooldown is no longer in effect.
                sprint_cooldown = 0;
            }
        }
        
        // Running code
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0) // Tell the animator that the character is running
        {
            isRunning = true;
            isPressed = true; //A button is pressed with attaches to Idle animation
        }
        else
        {
            isRunning = false;
        }

        // Jumping code
        if (Input.GetButtonDown("Jump"))  // Returns a value based on if the user has pressed the button bound to 'Jump'
        {
            playerjump = true; // Tells fixedupdate that the player wishes to jump
            isPressed = true; //A button is pressed with attaches to Idle animation
        }

        // Gliding code
        if(Input.GetButtonDown("Jump") && glidingButton)
        {
            is_gliding = true;
            isPressed = true; //A button is pressed with attaches to Idle animation
        } else 
        {
            is_gliding = false;
        }
        
        // Player hit code
       



        // Player re-position code
        //if(transform.position.y < -15) // If the player is out of bounds
        //{
           // transform.position = SpawnPosition; // Put them back to their initial position
           
        //}
	}

    // FixedUpdate is called a fixed number of times per second
    private void FixedUpdate()
    {
        // Move the character
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, playerjump, is_gliding); // Move the character based on input from the "Input.GetAxisRaw" for horizontal axis. Start crouching or jumping if applicable
        //Time.fixedDeltaTime references the time since the subroutine was last called to allow for consistent speed across platforms
        playerjump = false; // Automatically reset the value of 'playerjump' to prevent continuous jumping

        if(player_hit)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;


            hide_time += Time.deltaTime;
            Debug.Log(hide_time);
            if (hide_time >= m_DamageFlickerTime)
            {
                player_hit = false;
                hide_time = 0;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Output the Collider's GameObject's name
        if(collision.collider.tag == "Wall_Jump") 
        {
               
        }

        if(collision.collider.tag == "Enemy Projectile")
        {
            player_hit = true;
        }
        
    }


}
