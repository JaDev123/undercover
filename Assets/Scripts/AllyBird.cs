using UnityEngine;
using System.Collections;

public class AllyBird : MonoBehaviour {

    public float birdspeed=450f;
    private SpawnManager gameController;
   public float tumble = 0f;

    void Start()


    {
        getGameController();

        //Allows the coins and hazards to fall
        GetComponent<Rigidbody>().velocity = transform.forward * birdspeed * gameController.timeScale;
       
    }

    void Update()
    {

   //     if((gameController.getBirdCount()%3==0)&& (gameController.getBirdCount() >= 3))
   //     {

           // GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().velocity = transform.forward * birdspeed * gameController.timeScale;

            if (GetComponent<Rigidbody>().position.z < Camera.main.GetComponent<Transform>().position.z)
            {
                Destroy(gameObject);
            }

        if (GetComponent<Rigidbody>().position.z >0)
        {
            Destroy(gameObject);
        }


        // }

        //  transform.Rotate(0, 1, 0);
        /*
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        //pos.z = Mathf.Clamp01(205);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        */
        //      GetComponent<Rigidbody>().MoveRotation( Quaternion.Euler(0, 180, 0));

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
