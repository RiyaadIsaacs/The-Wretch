using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerControl : MonoBehaviour
{
    //Movement Fields 
    [SerializeField] private float speed = 20f, baseSpeed = 20f;

    //Health Fields - Exposed for ease of use
    [SerializeField] public float playerHealthMax = 100f;
    public float currentPlayerHealth;

    //Attack Field - Exposed for ease of use
    [SerializeField] public int playerAttack = 20;

    //Respawn Fields 
    //Field to store empty of respawn point, can be an array if we need checkpoints?
    [SerializeField] public GameObject respawnPosition;

    //Singleton for easy access to player
    public static PlayerControl instance;

    // bool to check if the player has found the wheel
    public bool hasWheel = false;

    //Fields for checking attack range
    EnemyBehaviour enemyInRange;

    // Apply the force to the rigidbody of the player
    private Rigidbody rb;

    //Animator for the player
    private Animator animator;

    //Bools for game management
    private bool checkpointAchieved = false;
    private bool hasCog = false;
    private GameObject wheelCog;
    private bool bridgeOpened = false;
    
    //Draw bridge implementation 
    // Add these variables at the top of your PlayerControl class
    [Header("Bridge Control")]
    [SerializeField] private Transform drawbridgeTransform;
    [SerializeField] private Transform cogPlacementSpot;
    [SerializeField] private float bridgeRotationAngle = 90.0f;
    [SerializeField] private float rotationDuration = 3.0f;

    // Assigning animator 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerInitialize();
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Read the AD keys to move left and right
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -0.5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 0.5f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1.5f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1.5f;
        }

        // Read the shift key to move faster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = baseSpeed * 2;
        }
        else
        {
            speed = baseSpeed;
        }

        // Create a movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Apply movement to the rigidbody of the player
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && enemyInRange != null)
        {
            enemyInRange.EnemyTakeDamage(playerAttack);
            Debug.Log("Player attacked: " + enemyInRange.name);
        }
    }

    //Initialize the Player fields 
    private void playerInitialize()
    {
        //Reset health and attack
        currentPlayerHealth = playerHealthMax;
        playerAttack = 20;
    }

    //Respawn player method - if we keep scene as is
    public void respawnPlayer()
    {
        //Check if checkpoint has been reached 
        if (checkpointAchieved == true)
        {
            //Reset player to the position of the checkpoint
            this.transform.position = respawnPosition.transform.position;
            //Reset player stats 
            playerInitialize();
        }
        else
        {
            //Restart the Game 
            restartGame();
        }
        
    }

    //Restart Game method 
    private void restartGame()
    {
        //Reload current active scene 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Trigger for attack range
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        if (other.tag == "Enemy")
        {
            enemyInRange = other.GetComponent<EnemyBehaviour>();
            Debug.Log("Enemy in range: " + enemyInRange.name);
        }
        
        //Trigger for Checpoint 1
        if (other.tag == "Checkpoint")
        {
            //Mark that checkpoint has been achieved 
            checkpointAchieved = true;
            //Log message that we have achieved checkpoint
            Debug.Log("Checkpoint achieved");
        }
        
        //Trigger for the Wheel Pickup
        if (other.name == "DrawBridgeCog")
        {
            //Set bool to activate that we have gotten the cog 
            hasCog = true; 
            //Log that we have the cog 
            Debug.Log("Draw bridge cog obtained");
            //Hide object and save it to game object variable for future
            wheelCog = other.gameObject;
            wheelCog.SetActive(false);
        }
        
        //Trigger Check to place Cog by bridge and trigger bridge opening
        if (other.name == "DrawBridgeRockl")
        {
            // Check if the player has the cog and the bridge hasn't been opened yet.
            if (hasCog == true && bridgeOpened == false)
            {
                Debug.Log("Player has the cog. Placing it and opening the bridge.");

                // 1. Move and show the Cog
                // Access the wheelCog GameObject you stored earlier
                if (wheelCog != null)
                {
                    // Set the cog's parent to the placement spot (optional but good practice)
                    wheelCog.transform.SetParent(cogPlacementSpot); 
                    // Move the cog to the exact position and rotation of the placement spot
                    wheelCog.transform.position = cogPlacementSpot.position;
                    wheelCog.transform.rotation = cogPlacementSpot.rotation;
                    // Make the cog visible again
                    wheelCog.SetActive(true);
                }

                // 2. Open the Bridge
                // Start the coroutine that handles the rotation animation
                StartCoroutine(RotateBridge());
        
                // 3. Clean up
                // Set hasCog to false so this trigger doesn't run again
                hasCog = false;
                bridgeOpened = true;
                
                //Disable collider on cog so not pickable 
                Collider cogCollider = wheelCog.GetComponent<Collider>();
                if (cogCollider != null)
                {
                    cogCollider.enabled = false;
                }
            }
            else
            {
                Debug.Log("Player does not have the cog yet.");
            }
        }
    }

    // Trigger for leaving attack range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy out of range: " + other.name);
            enemyInRange = null;
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        currentPlayerHealth -= damage;
        if (currentPlayerHealth <= 0)
        {
            respawnPlayer();
        }
    }
    
    
    // Coroutine to smoothly rotate the bridge around a pivot point over a set duration.
    private IEnumerator RotateBridge()
    {
        float timeElapsed = 0;
        float totalRotation = 0;

        // The axis to rotate on, relative to the bridge itself. 
        // Vector3.right is the local X-axis (red arrow).
        Vector3 rotationAxis = Vector3.right;

        while (timeElapsed < rotationDuration)
        {
            // Calculate how much to rotate in this single frame
            float rotationThisFrame = (bridgeRotationAngle / rotationDuration) * Time.deltaTime;

            // Make sure we don't overshoot the target angle
            if (totalRotation + rotationThisFrame > bridgeRotationAngle)
            {
                rotationThisFrame = bridgeRotationAngle - totalRotation;
            }

            // Rotate the bridge around its own pivot point
            drawbridgeTransform.Rotate(rotationAxis, rotationThisFrame);

            totalRotation += rotationThisFrame;
            timeElapsed += Time.deltaTime;

            // Wait until the next frame to continue the loop
            yield return null;
        }
    }
}