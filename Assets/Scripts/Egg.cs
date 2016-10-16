using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Egg : MonoBehaviour {

    public float speed;
    private SpawnManager gameController;
   public float tumble = 45f;

 
    void Start()


    {
        getGameController();

        //Allows the coins and hazards to fall
        GetComponent<Rigidbody>().velocity = transform.forward * speed*gameController.timeScale;
       
    }

    void Update()
    {
     //   transform.Rotate(new Vector3(1f, 0f, 0f), 1);
        if (gameController.pause==false)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed * gameController.timeScale;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 0;
        }
        /*
        GameObject Player = GameObject.FindWithTag("Player");
        float step = speed * Time.deltaTime*gameController.timeScale;
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        */

        if (GetComponent<Rigidbody>().position.z < Camera.main.GetComponent<Transform>().position.z) {
            Destroy(gameObject);
        }

        
        
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
