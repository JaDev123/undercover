using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public int scoreValue;
    private SpawnManager gameController;
    public AudioSource sound;
    private static int StarCount = 0;
    public GUIText starText;
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
        if (other.gameObject.CompareTag("Capsule"))
        {
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
            Destroy(gameObject);

            //  gameController.AddScore(scoreValue);



        }

        
    }
}

