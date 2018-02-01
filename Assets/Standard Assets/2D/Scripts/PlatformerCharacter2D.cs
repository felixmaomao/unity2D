using Assets.Standard_Assets._2D.Scripts;
using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	

        [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;
                                                     // Amount of maxSpeed applied to crouching movement. 1 = 100%

        [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
        private Transform ceilingCheck; // A position marking where to check for ceilings
        private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator anim; // Reference to the player's animator component.
		private Rigidbody2D playerRigidbody2D;
        private Transform playerGraphics;


        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            ceilingCheck = transform.Find("CeilingCheck");
            anim = GetComponent<Animator>();
			playerRigidbody2D = GetComponent<Rigidbody2D> ();
            playerGraphics = transform.Find("Graphics");
        }


        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            // Set the vertical animation
			anim.SetFloat("vSpeed", playerRigidbody2D.velocity.y);
        }

        //角色移动
        public void Move(float move, bool crouch, bool jump)
        {


            // If crouching, check to see if the character can stand up
            if (!crouch && anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
                    crouch = true;
            }

            // Set whether or not the character is crouching in the animator
            anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (grounded || airControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*crouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
				playerRigidbody2D.velocity = new Vector2(move*maxSpeed, playerRigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }
            // If the player should jump...           
            if (grounded && jump && anim.GetBool("Ground"))
            {
                Debug.Log("-----reached jump condition");
                // Add a vertical force to the player.
                grounded = false;
                anim.SetBool("Ground", false);
				playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }

        /*
          *Author:Felix
          *Description: 角色反转
        */   
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1;
            playerGraphics.localScale = theScale;           
        }


        /*
          *Author:Felix
          *Description: 更换武器                
        */
        public void ChangeWeapon()
        {
            Transform nowWeapon = gameObject.transform.Find("Pistol");
            if (nowWeapon == null)
            {
                Debug.Log("no weapon now");
            }
            else
            {
                Destroy(nowWeapon);
            }
            GameObject weaponPrefab = (GameObject)Resources.Load("m16");
            if (weaponPrefab == null)
            {
                Debug.LogError("prefab not found");
            }
            GameObject weapon= Instantiate(weaponPrefab, gameObject.transform.position, gameObject.transform.rotation);
            weapon.transform.parent = gameObject.transform.FindChild("Arm");
            //reset weapon position

        }

    }
} 