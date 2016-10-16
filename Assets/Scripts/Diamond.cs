using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour {

    public int scoreValue;
    private SpawnManager gameController;
    public AudioSource sound;
    private static int StarCount = 0;
    public GUIText starText;
    public GameObject ally;
    public GameObject ally2;
    public GameObject ally3;
    public GameObject exp;

    GameObject[] allys;
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
        getGameController();
        allys = new GameObject[] {ally2, ally3, ally };

    }   

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), 1f);
        /*
                if (StarCount == 1)
                {
                    starOne.enabled = true;
                }

                if (StarCount == 2)
                {
                    starTwo.enabled = true;
                }*/
        // Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 1f);
     //   Debug.Log("Target" +"X"+ transform.position.x +" Y"+ transform.position.y+" " + " Z" + transform.position.z);

    }

    void OnCollisionEnter(Collision other)
    {

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

        //Vector3 expPos = other.gameObject.GetComponent<Transform>().position + new Vector3(0, 0, 5);

        // Instantiate(exp, expPos, Quaternion.identity);

        
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //gameController.incrementBirdCount();
            //Destroy(gameObject);

            //  Instantiate(ally, gameObject.transform.position, Quaternion.identity);
            Vector3 expPos = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, 0);

            Instantiate(exp, expPos, Quaternion.identity);
            gameController.timeScale = 1.2f;
          //  Instantiate(allys[Random.Range(0, 3)], expPos, Quaternion.identity);
            gameController.AddScore(scoreValue);
            
            //Destroy(gameObject);
        }








    }


    public void Explode()
    {
        
        Instantiate(exp,gameObject.transform.position,Quaternion.identity);
      //  exp.Play();
        //Destroy(exp, exp.duration);
    }

    public void getGameController()
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


}

