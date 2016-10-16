using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public int scoreValue;
    private SpawnManager gameController;
    public AudioSource sound;
    private static int StarCount = 0;
    public GUIText starText;
    public GameObject exp;
    public GameObject leaderBoard;
    //public Image starOne;
    // public Image starTwo;
    //public GameObject ally;
    // Use this for initialization
    void Start()
    {
        //starOne.enabled = false;
        // starTwo.enabled = false;
        scoreValue = 1;
        //Gets a reference to GameController so the score can be updated and gameover can be called
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
       // transform.Rotate(0, 1, 0);
        /*
                if (StarCount == 1)
                {
                    starOne.enabled = true;
                }

                if (StarCount == 2)
                {
                    starTwo.enabled = true;
                }*/
    }

    void OnCollisionEnter(Collision other)
    {

        
        if (other.gameObject.CompareTag("AllyBird"))
        {
            GameObject audio = GameObject.FindWithTag("RockExpl");
            sound = audio.GetComponent<AudioSource>();
            sound.Play();
            // Destroy(other.gameObject);

            Destroy(gameObject);
            Vector3 expPos = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, 0);

            Instantiate(exp, expPos, Quaternion.identity);
        }


        if (other.gameObject.CompareTag("Player"))
        {

            GameObject audio = GameObject.FindWithTag("Squack");
            sound = audio.GetComponent<AudioSource>();
            sound.Play();
            other.transform.Rotate(new Vector3(0,180,0));
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -540f);
            other.gameObject.GetComponent<Animator>().speed=0;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Application.CaptureScreenshot("Screenshot.png");
            Debug.Log("Application.persistentDataPath" + Application.persistentDataPath.ToString());
            Destroy(other.gameObject,2);
            Vector3 expPos = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, 0);

            Instantiate(exp, expPos, Quaternion.identity);
            gameController.GameOver();
        }
        else {
            Debug.Log("Collision");
            /*
            sound.Play();
            if (other.gameObject.CompareTag("Robot"))
            {


                if (gameObject.CompareTag("Star"))
                {
                    StarCount++;


                    gameController.setStarCount(StarCount);
                    if (StarCount % 2 == 0)
                    {

                        Instantiate(ally, new Vector2(0, 0), Quaternion.identity);
                    }
                }
                if (gameObject.CompareTag("Heart"))
                {

                    gameController.lives++;
                    gameController.UpdateLives();
                }
    */
            Debug.Log(other.collider);
            //Destroy(other.gameObject);
         //   Destroy(gameObject);

            //  gameController.AddScore(scoreValue);



        }

        
    }
}

