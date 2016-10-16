using UnityEngine;
using System.Collections;

/// <summary>
/// This class loops the background around the quad
/// </summary>

public class BackGroundScrollScript : MonoBehaviour {

    public float speed;
    public float x;
    public float y;
    private SpawnManager gameController;
    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
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
	void Update () {
        


            x = Mathf.Repeat(Time.time * speed, 1);
        y = Mathf.Repeat(Time.time * speed, 1);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0,-y));

        
        
        /*
        x = Mathf.Repeat(Time.time * speed, 1);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(x, 0));
*/
    }
}
