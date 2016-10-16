using UnityEngine;
using System.Collections;
/// <summary>
/// This class is one of two classes that work together to scroll the blackground
/// </summary>
public class Scroll : MonoBehaviour {

    public float scrSpeed;
    public float tileSizeZ;

    private Vector3 startPos;
    private SpawnManager gameController;



    // Use this for initialization
    void Start () {
        startPos = transform.position;
        getGameController();
    }
	
	// Update is called once per frame
	void Update () {
        float newPos = Mathf.Repeat(Time.time * scrSpeed*gameController.timeScale, tileSizeZ);
        transform.position = startPos + Vector3.back * newPos;
       


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
