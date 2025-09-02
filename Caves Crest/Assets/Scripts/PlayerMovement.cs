using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES LINE 7-50

    // Default Movement & Player Flip & Animations
    private float horizontal = 0f; // To move left and right
    private float speed = 4f; // The speed of movement
    private float jumpingPower = 13.4f; // The power of jump
    private bool isFacingRight = true; // To check if Facing Right is false to flip character to left.
    private Animator anim; // To call animations to player
    private bool doubleJump; // To jump again in midair after jumping once. (Double Jump)

    // Private Variables for Dashing in Movement.
    private bool canDash = true; // To control dashing availability
    private bool isDashing; // Flag indicating if the player is currently dashing
    private float dashingPower = 24f; // The power of dash
    private float dashingTime = 0.2f; // Duration of dash
    private float dashingCooldown = 2f; // Cooldown time between dashes

    // Private Variables for WallSliding & WallJumping in Movement.
    private bool isWallSliding; // Flag indicating if the player is currently sliding on a wall
    private float wallSlidingSpeed = 2f; // Speed of sliding on a wall

    private bool isWallJumping;// Flag indicating if the player is currently performing a wall jump
    private float wallJumpingDirection; // Direction of wall jump
    private float wallJumpingTime = 0.2f; // Time during which wall jump is possible
    private float wallJumpingCounter; // Counter for wall jumping time
    private float wallJumpingDuration = 0.4f; // Duration of wall jump animation
    private Vector2 wallJumpingPower = new Vector2(8f, 16f); // Power of wall jump

    // To reference gameObjects/assets from Unity to code.
    [SerializeField] private Rigidbody2D rb; // Rigidbody component of the player
    [SerializeField] private Transform groundCheck; // Check for grounding
    [SerializeField] private LayerMask groundLayer; // Layer for detecting ground
    [SerializeField] private TrailRenderer tr; // Trail renderer for dashing effect
    [SerializeField] private Transform wallCheck; // Check for a wall
    [SerializeField] private LayerMask wallLayer; // Layer for detecting a wall
    [SerializeField] private AudioSource jumpSoundEffect; // Sound effect for jumping
    [SerializeField] private AudioSource dashSoundEffect; // Sound effect for dashing
    [SerializeField] private AudioSource wallJumpSoundEffect; // Sound effect for wall jumping

    // List of animation states, used in if/else and true/false statements.
    private enum MovementState { idle, running, jumping, falling}

    // For dust trail effect in movement.
    public ParticleSystem dust;

    private void Start() // Private Start is only used for getting the animations for the Player
    {
        anim = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame, handling player input, movement, and state changes.
    // All methods such as dashing are functional when put inside the update method.
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input axis

        if (IsGrounded() && !Input.GetButton("Jump")) // ! is a NOT operation, so basically it just means false.
        {
            doubleJump = false; // Reset double jump if not pressing jump and grounded
        }

        // Check for jump input and perform jump logic
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                jumpSoundEffect.Play(); // Play jump sound effect

                rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Apply jump velocity

                doubleJump = !doubleJump; // Toggle double jump
            }
        }


        // Reduce upward velocity on jump release
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            CreateDust(); // Create dust effect
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        }

        // Check for dash input and initiate dash coroutine
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash()); // Start dashing coroutine
        }

        WallSlide(); // Check and handle wall sliding

        WallJump(); // Check and handle wall jumping

        if (!isWallJumping)
        {
            Flip(); // Flip character if not wall jumping
        }

        // Update player animation state
        UpdateAnimationUpdate();

    } // void Update() ends here.

//------------------------------------------------------------------------------------------\\


    // Animation State of Player
    private void UpdateAnimationUpdate()
    {
        MovementState state;

        // Player animation states switch from idle to running or jumping,
        // if player axis is less or greater than 0.

        if (horizontal > 0f)
        {
            state = MovementState.running;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

 //------------------------------------------------------------------------------------------\\

    private void FixedUpdate()
    {

        if (isDashing)
        {
            return; // Skip further updates if dashing
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Apply movement velocity
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // Check if player is grounded
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer); // Check if player is against a wall
    }

    private void WallSlide()
    {
        // Check if the player is against a wall, not grounded, and moving horizontally.
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;

            // Limit vertical velocity to create a smooth wall sliding effect.
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        // If the player is currently wall sliding, initiate wall jump setup.
        if (isWallSliding)
        {
            CreateDust();
            isWallJumping = false;

            // Determine the direction of the wall jump based on the player's scale/direction.
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            // Cancel any pending wall jump stop invocations.
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            // If not wall sliding, decrement the wall jump counter.
            wallJumpingCounter -= Time.deltaTime;
        }

        // If the Jump button is pressed and the wall jump counter is still positive, perform wall jump.
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            wallJumpSoundEffect.Play(); // Play wall jump sound effect
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y); // Apply the wall jump velocity.
            wallJumpingCounter = 0f; // Reset the wall jump counter to prevent multiple wall jumps in quick succession.

            // If the player is facing a different direction than the wall, flip the character.
            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            // Schedule the method to stop wall jumping after the specified duration.
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        // Reset the wall jumping flag after the wall jump duration has passed.
        isWallJumping = false;
    }
//------------------------------------------------------------------------------------------\\

    private void Flip() // Method to flip character when moving left or right.
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            CreateDust(); // Create dust effect
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    private IEnumerator Dash()
    {
        dashSoundEffect.Play(); // Play dash sound effect
        CreateDust(); // Create dust effect

        canDash = false; // Prevent dashing during the dash coroutine
        isDashing = true; // Set the dashing flag to true

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Disable gravity during the dash

        // Apply a velocity based on the player's direction and the dashing power.
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true; // Enable trail renderer during the dash
        yield return new WaitForSeconds(dashingTime); // Wait for the dash duration
        tr.emitting = false; // Disable trail renderer after the dash

        rb.gravityScale = originalGravity; // Restore original gravity
        isDashing = false; // Reset the dashing flag
        yield return new WaitForSeconds(dashingCooldown); // Apply cooldown before allowing another dash
        canDash = true; // Allow dashing again after cooldown
    }

    // A method to get the particle effect component from Unity to be recalled in other methods.
    void CreateDust()
    {
        dust.Play(); // Play dust particle effect
    }
}