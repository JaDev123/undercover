using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls player movement
/// </summary>

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector]
    public static bool facingRight = true;
    [HideInInspector]
    public bool jump = true;

    public float gameSpeed = 100f;
    public float moveForce = 5f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    //  public GameObject Camera;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody rb;
    private SpawnManager gameController;
    public GameObject bomb;
    public GameObject bombpos;
    private Vector2 touchOrigin = -Vector2.one;
    private Vector3 initialPos;
    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float bottomBorder;
    private float dist;
    // Use this for initialization
    public TextMesh screenPos;


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 1.5f;
    private float maxSwipeTime = 5f;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;

        // Screen.autorotateToLandscapeLeft=true;
        //  dist = initialPos - Screen.width;

        // dist = (transform.position - Camera.main.transform.position).z;
        //  leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        //  rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //     topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        //     bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).y;

        Debug.Log("Initial Position " + initialPos);
        //     Debug.Log("leftBorder" + leftBorder);
        //      Debug.Log("rightBorder " + rightBorder);
        //      Debug.Log("topBorder " + topBorder);
        //      Debug.Log("bottomBorder " + bottomBorder);
        Debug.Log("Screen Width " + Screen.width);
        Debug.Log("Screen Height " + Screen.height);


    }

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("SpawnManager");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SpawnManager>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' Script");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // getTouchInput();


        //  transform.Translate(Input.acceleration.x, 0, 0);

        //  CameraFollow();
        //Check if player is on the ground	
        // grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));



        //If player already jumped then they cannot jump again
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        //Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bomb, bombpos.GetComponent<Transform>().position, Quaternion.identity);

        }


        /*
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }
        */

        /*
        transform.position = new Vector2(

Mathf.Clamp(transform.position.x, -7, 3),
transform.position.y


);
        */
        /*
            rb2d.position = new Vector2(

    Mathf.Clamp(rb2d.position.x, -5, 0),
    rb2d.position.y


    );
        */



    }

    void FixedUpdate()
    {
        if (gameController.touchMode)
        {
            getMobileTouchInput();
        }
        else
        {
            getMobileInput();
        }
        //Get direction of movement



        //Get direction of movement
        //  float v = Input.GetAxis("Vertical");
        //Debug.Log(v);
        // rb2d.AddForce(new Vector2(0f, jumpForce));


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * h * moveForce * gameController.timeScale);
        transform.Translate(Vector3.up * v * moveForce * gameController.timeScale);
        if (h > 0)
        {
            // transform.Rotate(new Vector3(0, 0, 45));
        }
        transform.Rotate(new Vector3(0, 0, 0));
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        //pos.z = Mathf.Clamp01(205);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        if (transform.position.z != -500f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -520f);

        if (transform.position.y > 195f)
            transform.position = new Vector3(transform.position.x, 195, -520f);
        
        if (transform.position.y > 175f)
            gameController.hideText=true;
        else
            gameController.hideText = false;


        //When Players reaches desired (L/R)possition make him stop
        /*
        if (transform.position.x <= -60f)
            transform.position = new Vector3(-60f, transform.position.y, transform.position.z);
        else if (transform.position.x >= 60f)
            transform.position = new Vector3(60f, transform.position.y, transform.position.z);
        */
        /*
        if (transform.position.y <= 150f)

            transform.position = new Vector3(transform.position.x, 150f, transform.position.z);
        else if (transform.position.y>= 190f)
            transform.position = new Vector3(transform.position.x, 190f, transform.position.z);
        */
        Debug.Log("Position " + transform.position);
        Debug.Log("Initial Position " + initialPos);

        UpdateScreenPos();
        // Mathf.Clamp(transform.position.y, 150f, 190f);
        //Mathf.Clamp(transform.position.x, initialPos + 5f, initialPos - 5f);
        //Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        // Mathf.Clamp(transform.position.y, topBorder, bottomBorder);

        // Mathf.Clamp(transform.position.x, 0 - Screen.width / 2, 0 + Screen.width / 2);
        //Mathf.Clamp(transform.position.z, 0 - Screen.height / 2, 0 + Screen.height / 2);

        Debug.Log(h);
        //bombpos.GetComponent<Transform>().position = GetComponent<Transform>().position-new Vector3(-1f,30f,5f);
        Debug.Log("Bomb Position " + bombpos.GetComponent<Transform>().position);

        //Set animation speed to absolute(positive) direction of movement
        // anim.SetFloat("Speed",Mathf.Abs(h));


        //If direction of movement times speed is less than the maximum speed of the player then continue to add force to the player
        /*
        (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector3.right * h * moveForce);
        }
        */
        /*
        //If direction of movement times speed is more than the maximum speed of the player then limit the velocity at maximum speed		
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        if (h != 0)//Moving
        {
            anim.SetInteger("animState",1);//Play Run Anim



            //If moving right but not facing right then flip player to face right
            if (h > 0 && !facingRight)
            {

                Flip();
            }

            //If moving left but not facing left then flip player to face left
            else if (h < 0 && facingRight)
            {

                Flip();
            }
        }
        else if (h == 0)//Not Moving
        {
            anim.SetInteger("animState",0);//idle anim
        }

        //	If player pressed jump then play jump animation and addforce to the y axis (up) then set jump to false
        if (jump)   
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        */

        // transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 1f);
        //  transform.Rotate(new Vector3(0, 0, -45));
    }

    //Displays the position to the UI
    public void UpdateScreenPos()
    {
        screenPos.text = " " + transform.position;


    }

    //Change the direction the player is facing
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CameraFollow()
    {
        // Camera.GetComponent<Transform>().position= transform.position;

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collision");
            gameController.lives--;
            if (gameController.lives > 0)
            {

                GameObject player = gameObject;

                player.GetComponent<Transform>().position = new Vector2(-5, 0);

            }
            else if (gameController.lives <= 0)
            {
                gameController.lives = 0;
                gameController.UpdateLives();
                gameController.GameOver();

                anim.SetTrigger("Death");
            }
            Debug.Log(gameController.lives);
            gameController.UpdateLives();
            /*
                       if (gameController.lives > 0)
                       {

                           GameObject player = gameObject;

                           player.GetComponent<Transform>().position = new Vector2(-5, 0);

                       }
                       else if (gameController.lives <= 0)
                       {
                           gameController.lives = 0;
                           gameController.UpdateLives();
                           gameController.GameOver();

                           anim.SetTrigger("Death");
                       }
                       */
            // sound.Play();
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            gameController.lives--;
            gameController.UpdateLives();


            if (gameController.lives > 0)
            {

                GameObject player = gameObject;

                player.GetComponent<Transform>().position = new Vector2(-5, 0);

            }
            else if (gameController.lives <= 0)
            {
                gameController.lives = 0;
                gameController.UpdateLives();
                gameController.GameOver();
                anim.SetTrigger("Death");

            }
            // sound.Play();
        }
        if (other.gameObject.CompareTag("ScifiFighter"))
        {

            gameController.setLevelComplete();

            Destroy(gameObject);

        }
    }

    public void getMobileInput()
    {

        //If it's not the player's turn, exit the function.
        //  if (!GameManager.instance.playersTurn) return;
        rb.isKinematic = true;
        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
       
        /*if (horizontal != 0)
        {
            vertical = 0;
        }
        */
        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        /* 
            //Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {
            //Vector3 touchPosition = Input.touches[0].position;
//touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
            //Drop Bomb
            //Instantiate(bomb, bombpos.GetComponent<Transform>().position, Quaternion.identity);


            // rb.AddForce(Vector3.up * moveForce);
            //   transform.Translate(0, 1f, 0);

            //Store the first touch detected.
                Touch myTouch = Input.touches[0];


            

              //Check if the phase of that touch equals Began
              if (myTouch.phase == TouchPhase.Began)
              {
                  //If so, set touchOrigin to the position of that touch
                  touchOrigin = myTouch.position;
              }

              //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
              else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
              {
                  //Set touchEnd to equal the position of this touch
                  Vector2 touchEnd = myTouch.position;

                  //Calculate the difference between the beginning and end of the touch on the x axis.
                  float x = touchEnd.x - touchOrigin.x;

                  //Calculate the difference between the beginning and end of the touch on the y axis.
                  float y = touchEnd.y - touchOrigin.y;

                  //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                  touchOrigin.x = -1;

                  //Check if the difference along the x axis is greater than the difference along the y axis.
                  if (Mathf.Abs(x) > Mathf.Abs(y))
                      //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                      horizontal = x > 0 ? 1 : -1;
                  else
                      //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                      vertical = y > 0 ? 1 : -1;
              }
              
        }
            */
#endif //End of mobile platform dependendent compilation section started above with #elif
        //Check if we have a non-zero value for horizontal or vertical
        //transform.Translate(Input.acceleration.x * moveForce, 0, 0);
        //transform.Translate(Input.acceleration.x * moveForce, Input.acceleration.y * moveForce, 0);

        transform.Translate(Input.acceleration.x * moveForce * gameController.timeScale, Input.acceleration.y * moveForce * gameController.timeScale, 0);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // pos.z = Mathf.Clamp01(205);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        if (transform.position.z != -500f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -520f);
        // transform.Translate(Input.acceleration.x*moveForce, Input.acceleration.y * moveForce, 0);
        // Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        //Mathf.Clamp(transform.position.y, topBorder, bottomBorder);
        Debug.Log("Position " + transform.position.y);
        // Mathf.Clamp(rb.transform.position.x, 0-Screen.width/2, 0 + Screen.width / 2);
        //Mathf.Clamp(rb.transform.position.y, 0 - Screen.height / 2, 0 + Screen.height / 2);
        //float h = Input.GetAxis("Horizontal");
        // rb.transform.Translate(Vector3.right * h * moveForce);

        // Debug.Log(h);
        // bombpos.GetComponent<Transform>().position = GetComponent<Transform>().position - new Vector3(0f, 50f, 0f);


        /*
        if (horizontal != 0 || vertical != 0)
        {
            //Drop Bomb
            Instantiate(bomb, bombpos.GetComponent<Transform>().position, Quaternion.identity);

        }*/
    }


    public void getMobileTouchInput()
    {
        //If it's not the player's turn, exit the function.
        //  if (!GameManager.instance.playersTurn) return;

        float horizontal = 0;     //Used to store the horizontal move direction.
        float vertical = 0;       //Used to store the vertical move direction.
        bool ended = false;

        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
       
        /*if (horizontal != 0)
        {
            vertical = 0;
        }
        */
        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Vector3 touchPosition = Input.touches[0].position;
            //touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            //Drop Bomb
            //Instantiate(bomb, bombpos.GetComponent<Transform>().position, Quaternion.identity);


            // rb.AddForce(Vector3.up * moveForce);
            //   transform.Translate(0, 1f, 0);

            //Store the first touch detected.
            Touch myTouch = Input.touches[0];




            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            //  if (myTouch.phase == TouchPhase.Began || myTouch.phase == TouchPhase.Stationary)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Moved)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                //touchOrigin.x = -1;


                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontal = x > 0 ? x : 0;
                else
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    vertical = y > 0 ? y : 0;

                horizontal = x;
                vertical = y;

            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Stationary)
            {
                horizontal = 0;
                vertical = 0;
                /*
                transform.Translate(horizontal * moveForce / 200 * gameController.timeScale, vertical * moveForce / 200 * gameController.timeScale, 0);
                     horizontal = horizontal>0?horizontal-0.05f:0;
                     vertical = vertical > 0 ? vertical-0.05f : 0;
                */
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended)
            {
                horizontal = 0;
                vertical = 0;
                ended = true;
                /*
                transform.Translate(horizontal * moveForce / 200 * gameController.timeScale, vertical * moveForce / 200 * gameController.timeScale, 0);
                     horizontal = horizontal>0?horizontal-0.05f:0;
                     vertical = vertical > 0 ? vertical-0.05f : 0;
                */
            }

        }

#endif //End of mobile platform dependendent compilation section started above with #elif
        //Check if we have a non-zero value for horizontal or vertical
        //transform.Translate(Input.acceleration.x * moveForce, 0, 0);
        //transform.Translate(Input.acceleration.x * moveForce, Input.acceleration.y * moveForce, 0);
        //  rb.AddForce(Vector3.right * horizontal * moveForce * gameController.timeScale);
        //rb.AddForce(Vector3.up * vertical * moveForce * gameController.timeScale);
        //transform.Translate(Vector3.right * horizontal * moveForce/2 * gameController.timeScale);
        //transform.Translate(Vector3.up * vertical * moveForce/2 * gameController.timeScale);

        transform.Translate(horizontal * moveForce / 200 * gameController.timeScale, vertical * moveForce / 200 * gameController.timeScale, 0);

        if (ended)
        {
            rb.AddForce(10, 10, 0);
            ended = false;
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // pos.z = Mathf.Clamp01(205);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        if (transform.position.z != -500f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -520f);
        // transform.Translate(Input.acceleration.x*moveForce, Input.acceleration.y * moveForce, 0);
        // Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        //Mathf.Clamp(transform.position.y, topBorder, bottomBorder);
        Debug.Log("Position " + transform.position.y);
        // Mathf.Clamp(rb.transform.position.x, 0-Screen.width/2, 0 + Screen.width / 2);
        //Mathf.Clamp(rb.transform.position.y, 0 - Screen.height / 2, 0 + Screen.height / 2);
        //float h = Input.GetAxis("Horizontal");
        // rb.transform.Translate(Vector3.right * h * moveForce);

        // Debug.Log(h);
        // bombpos.GetComponent<Transform>().position = GetComponent<Transform>().position - new Vector3(0f, 50f, 0f);


        /*
        if (horizontal != 0 || vertical != 0)
        {
            //Drop Bomb
            Instantiate(bomb, bombpos.GetComponent<Transform>().position, Quaternion.identity);

        }*/
    }
}


