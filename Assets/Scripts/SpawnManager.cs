using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

/// <summary>
/// This class spawns GameObjects for the player to collect and avoid respectively
/// </summary>

public class SpawnManager : MonoBehaviour
{
    private bool spawn = true;
    public int maxPlatforms = 100;
    public GameObject platform;
    public GameObject objplatform;
    public GameObject collectible;
    public GameObject ship;
    public float horizontalMin;
    public float horizontalMax;
    public float verticalMin;
    public float verticalMax;
    public float objhorizontalMin;
    public float objhorizontalMax;
    public float objverticalMin;
    public float objverticalMax;
    public GameObject Obstacle;

    public GameObject Obstacle1;
    /*public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Obstacle4;
    public GameObject Obstacle5;
    public GameObject Obstacle6;
    */

    public GameObject diamond;
    public GameObject egg;
    public GameObject exitbtn;

    public GameObject Bossobj;
    public static bool BossActive = false;
    //public GameObject Boss;
    public GameObject Building1;
    public GameObject Building2;
    //  public GameObject Bird4;
    //   public GameObject Bat;
    public Transform enemySpawn;
    public Vector3 spawnValues;
    public Vector3 spawnValuesBuilding1;
    public Vector3 spawnValuesBuilding2;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text starText;
    public TextMesh scoreText;
    public TextMesh targetText;
    public TextMesh percentText;
    public TextMesh restartText;
    public TextMesh livesText;
    public TextMesh highScoreText;
    public TextMesh Timer;

    public int time;

    private bool gameOver;
    private bool restart;
    public static int score;
    public static int target;
    private double percent;
    private double x;
    private double y;
    private double z;
    public int lives = 3;
    public int gameSpeed;
    public float timeScale = Time.timeScale;
    private bool bird = false;
    private Animator anim;

    private Vector2 originPosition;
    private Vector2 randomPosition;
    GameObject[] obstacles;

    public GameObject exp;
    public static bool launch = false;
    public static int birdCount = 0;
    public bool touchMode = false;
    public bool pause = false;
    Toggle t;
    Toggle p;

    public static int playCount = 0;
    public static int tMode = 0;
    public static int dMode = 0;
    public static int sMode = 0;

    public static int mod = 10;
    //public static float limit = 0;
    public static float exponent = 1.05f;

    private LeaderBoard SubmitBtn;
    // Use this for initialization

    private bool tenPtAchieveBool = false;
    private bool hundredPercentAchieveBool = false;
    private bool hundredPtAchieveBool = false;
    private bool cornerCatchAchieveBool = false;

    public bool hideText=false;

    Vector3 togglePos ;
    Vector3 pogglePos ;
    Vector3 diamondPos ;
    Vector3 eggPos;
    Vector3 exitbtnPos;
    Vector3 percentPos;
    Vector3 targetTextPos;
    Vector3 highscorePos;

    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        scoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0);

        GameObject toggle = GameObject.FindWithTag("Toggle");
        GameObject poggle = GameObject.FindWithTag("Pause");
        GameObject Submit = GameObject.FindWithTag("Submit Score");
        SubmitBtn = Submit.GetComponent<LeaderBoard>();

        t = toggle.GetComponent<Toggle>();
        p = poggle.GetComponent<Toggle>();
        anim = GetComponent<Animator>();
        diamond = GameObject.FindWithTag("DiamondUI");
        egg = GameObject.FindWithTag("Egg");
        exitbtn = GameObject.FindWithTag("ExitBtn");


         togglePos = t.GetComponent<Transform>().position;
        pogglePos = p.GetComponent<Transform>().position;
         diamondPos = diamond.GetComponent<Transform>().position;
         eggPos = egg.GetComponent<Transform>().position;
         exitbtnPos = exitbtn.GetComponent<Transform>().position;
        targetTextPos = targetText.GetComponent<Transform>().position;
        highscorePos = scoreText.GetComponent<Transform>().position;
        percentPos = percentText.GetComponent<Transform>().position;


       
        playCount = PlayerPrefs.GetInt("playCount", 0);
        playCount++;
        PlayerPrefs.SetInt("playCount", playCount);
        tMode = PlayerPrefs.GetInt("Touches", 0);

        if (PlayerPrefs.GetInt("Difficulty",1) == 0)
        {
            mod = 10;
            exponent = 1.05f;
            timeScale = Time.timeScale * 1.2f;
            restartText.text = "         Hard Mode";
            restartText.color = Color.red;
            Invoke("clearRestartText",1);
        }
        else {

            mod = 5;
            exponent = 1.1f;
            timeScale = Time.timeScale * .8f;
            restartText.text = "         Easy Mode";
            restartText.color = Color.green;
            Invoke("clearRestartText", 1);
        }


        if (tMode == 0)
        {
            t.isOn = false;
        }
        else
        {
            t.isOn = true;
        }

        clearText();
        score = 0;
        target = 0;
        percent = 0f;
        //  obstacles = new GameObject[7] { Obstacle, Obstacle1, Obstacle2, Obstacle3, Obstacle4, Obstacle5, Obstacle6 };
        originPosition = new Vector2(-15, -4);
        // Spawn();
        StartCoroutine(SpawnWaves());
    }



    void Spawn()
    {
        for (int i = 0; i < maxPlatforms; i++)
        //if(spawn)
        {
            randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            Instantiate(platform, randomPosition, Quaternion.identity);


            if (Random.Range(1, 18) % 3 == 0)
            {
                Vector2 randomPosition2 = originPosition + new Vector2(Random.Range(objhorizontalMin, objhorizontalMax), Random.Range(objverticalMin, objverticalMax));
                Vector2 randomPosition3 = randomPosition2 + new Vector2(0, +5);
                Instantiate(objplatform, randomPosition2, Quaternion.identity);
                Instantiate(collectible, randomPosition3, Quaternion.identity);
            }



            //    Instantiate(Bird1, randomPosition, Quaternion.identity);
            //  Instantiate(Bird2, randomPosition, Quaternion.identity);
            //  Instantiate(Bird3, randomPosition, Quaternion.identity);
            //   Instantiate(Bird4, randomPosition, Quaternion.identity);
            originPosition = randomPosition;
            if (i == maxPlatforms - 1 && Application.loadedLevel == 1)
            {
                Instantiate(Bossobj, randomPosition + new Vector2(-5, 4), Quaternion.identity);
                //   Instantiate(ship, randomPosition, Quaternion.identity);
            }
            if (i == maxPlatforms - 1 && Application.loadedLevel == 2)
            {
                // Instantiate(Bossobj, randomPosition + new Vector2(-5, 5), Quaternion.identity);
                Instantiate(ship, randomPosition + new Vector2(0, +5), Quaternion.identity);
                Instantiate(collectible, randomPosition + new Vector2(0, +5), Quaternion.identity);
            }
        }

    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (BossActive == false)
        {
            /*
            if (bird == false)
            {

                temphazard = hazard;
                bird = true;
            }
            else if (bird == true)
            {

                temphazard = hazard3;
                bird = false;
            }
            */

            Quaternion spawnRotation = Quaternion.identity;
            Quaternion spawnRotationObj = Quaternion.Euler(0, 180, 0);


            for (int i = 0; i < hazardCount; i++)
            {
                if (p.isOn == false)
                {

                    float distance = (transform.position - Camera.main.transform.position).z;
                    // float distance = 1f;

                    float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
                    float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

                    float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
                    float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y;

                    Vector3 spawnPosition1 = new Vector3(Random.RandomRange(leftBorder, rightBorder), Random.RandomRange(topBorder, bottomBorder), distance);
                    Vector3 spawnPosition2 = new Vector3(Random.RandomRange(leftBorder, rightBorder), Random.RandomRange(topBorder, bottomBorder), distance);
                    Vector3 spawnPosition5 = new Vector3(Random.RandomRange(leftBorder, rightBorder), Random.RandomRange(topBorder, bottomBorder), distance);
                    /*
                    spawnPosition1 = Camera.main.ViewportToWorldPoint(spawnPosition1);
                    Debug.Log("Spawn1"+spawnPosition1);
                    spawnPosition1.z = spawnValues.z;
                    if (spawnPosition1.x > 0)
                    {

                    }
                    else { spawnPosition1.x -= 5; }


                    spawnPosition2 = Camera.main.ViewportToWorldPoint(spawnPosition2);
                    Debug.Log("Spawn2" + spawnPosition2);
                    spawnPosition2.z = spawnValues.z;
                    */

                    //   Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    //  Vector3 spawnPosition2 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                    // Vector3 spawnPosition4 = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                    AddTarget(1);

                    Vector3 spawnPosition3 = new Vector3(Random.Range(spawnValuesBuilding1.x - 5, spawnValuesBuilding1.x + 5), spawnValuesBuilding1.y, spawnValuesBuilding1.z);
                    Vector3 spawnPosition4 = new Vector3(Random.Range(spawnValuesBuilding2.x - 5, spawnValuesBuilding2.x + 5), spawnValuesBuilding1.y, spawnValuesBuilding1.z);
                    // Instantiate(Building1, spawnPosition3, spawnRotation);
                    // Instantiate(Building2, spawnPosition4, spawnRotation);


                    //Generates birds on screen

                    Instantiate(Obstacle, spawnPosition1, spawnRotationObj);

                    if (PlayerPrefs.GetInt("Difficulty", 1) == 1)
                    {

                        if (percent < 80)
                            Instantiate(Obstacle1, spawnPosition2, spawnRotationObj);

                        /*
                                                if (percent < 70)
                                                    Instantiate(Obstacle1, spawnPosition3, spawnRotationObj);

                                                if (percent < 60)
                                                    Instantiate(Obstacle1, spawnPosition4, spawnRotationObj);

                                                if (percent < 50)
                                                    Instantiate(Obstacle1, spawnPosition5, spawnRotationObj);
                                                */
                    }

                    else {

                        Instantiate(Obstacle1, spawnPosition5, spawnRotationObj);

                        if (percent < 80)
                            Instantiate(Obstacle1, spawnPosition2, spawnRotationObj);

                        if (percent < 70)
                            Instantiate(Obstacle1, spawnPosition3, spawnRotationObj);

                        if (percent < 60)
                            Instantiate(Obstacle1, spawnPosition4, spawnRotationObj);

                        if (percent < 50)
                            Instantiate(Obstacle1, spawnPosition5, spawnRotationObj);
                    }
                    Vector3 expPos = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, 5);

                }
                //   Instantiate(exp, spawnPosition1, Quaternion.identity);

                //Instantiate(obstacles[Random.Range(0, 6)], spawnPosition1, spawnRotationObj);

                // Instantiate(Bird2, spawnPosition2, spawnRotation);
                // Instantiate(Bird3, spawnPosition3, spawnRotation);
                // Instantiate(Bird4, spawnPosition4, spawnRotation);

                //Randomly generate bats on screen

                if (Random.Range(1, 30) % 3 == 0)
                {
                    //      Instantiate(diamond, spawnPosition5, spawnRotationObj);
                    // Vector3 spawnPosition5 = new Vector3(spawnValuesbat.x, Random.Range(-spawnValuesbat.y, spawnValuesbat.y), spawnValuesbat.z);
                    //Instantiate(Obstacle1, spawnPosition2, spawnRotationObj);
                    //Instantiate(Bat, enemySpawn, spawnRotation)
                    //   Instantiate(Bat, spawnPosition5, spawnRotation);

                }


                if (gameOver)
                {

                    revealUiText();
                   // if (GlobalVariables.loggedIn)
                   // {
                        SubmitBtn.OnAddScoreToLeaderBorad(score);
                   // }
                    restartText.text = "         Game Over!\n\nTap Screen to Restart";
                    // restartText.text = "    Game Over!\n\nPress 'R' for Restart\n\nPress 'X' to Exit";

                    if (score > PlayerPrefs.GetInt("highscore", 0))
                    {
                        highScoreText.text = "High Score: " + score;

                        resetAchievements();

                        PlayerPrefs.SetInt("highscore", score);

                        new WaitForSeconds(3);
                     //   if (GlobalVariables.loggedIn)
                       // {
                            Invoke("showLeaderBoard", 1);
                      //  }
                        // Instantiate();
                       
                    }

                    restart = true;
                    break;
                }
                yield return new WaitForSeconds(spawnWait);
            }
            if (gameOver)
            {
      revealUiText();

               // if (GlobalVariables.loggedIn)
              //  {

                    SubmitBtn.OnAddScoreToLeaderBorad(score);
             //   }

                restartText.text = "         Game Over!\n\nTap Screen to Restart";
                // restartText.text = "    Game Over!\n\nPress 'R' for Restart\n\nPress 'X' to Exit";

                if (score > PlayerPrefs.GetInt("highscore", 0))
                {
                    highScoreText.text = "High Score: " + score;

                    resetAchievements();

                    PlayerPrefs.SetInt("highscore", score);
                    new WaitForSeconds(3);
                  //  if (GlobalVariables.loggedIn)
                 //   {
                        Invoke("showLeaderBoard", 1);
                  //  }
                    }
                
                restart = true;
                break;
            }
            yield return new WaitForSeconds(waveWait);


        }
    }



    void showLeaderBoard() {
        SubmitBtn.OnShowLeaderBoard();
    }
    public void ShowAd()
    {
        if (playCount > 3)
        {
            if (score > PlayerPrefs.GetInt("highscore", 0))
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
            }
            else
            if (score <= PlayerPrefs.GetInt("highscore", 0) && playCount % 3 == 0)
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
        }

    }


    // Update is called once per frame
    void Update()
    {
        //   Spawn();

       // checkAchievement();
       checkHideUI();

        if (p.isOn)
        {
            pause = true;
        }
        else if (!p.isOn)
        {
            pause = false;
        }

        if (t.isOn)
        {
            touchMode = true;
            PlayerPrefs.SetInt("Touches", 1);
        }
        else if (!t.isOn)
        {
            touchMode = false;
            PlayerPrefs.SetInt("Touches", 0);
        }


        CalcPercent();

        if (restart)
        {
            if (Input.touchCount > 0)
            {
                Invoke("ShowAd",1);
                Application.LoadLevel(Application.loadedLevel);
            }

            /*
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Application.LoadLevel(5);
            }
            */
        }
    }


     public void  checkHideUI()
    {
       // Color color = scoreText.color;
      //  color.a = 0.1f;
        if (hideText)
        {

            hideUiText();
        }
        else {
            revealUiText();
        }

    }

   public void  hideUiText()
    {



        //   scoreText.color = color;
        //   percentText.color = color;
        //   targetText.color = color;
        t.GetComponent<Transform>().transform.Translate(-25, 0, 0);
        p.GetComponent<Transform>().transform.Translate(-25, 0, 0);
        diamond.GetComponent<Transform>().transform.Translate(0, 25, 0);
        egg.GetComponent<Transform>().transform.Translate(0, 25, 0);
        exitbtn.GetComponent<Transform>().transform.Translate(25, 0, 0);

        targetText.GetComponent<Transform>().transform.Translate(0, 25, 0);
        scoreText.GetComponent<Transform>().transform.Translate(0, 25, 0);
        percentText.GetComponent<Transform>().transform.Translate(0, 25, 0);

    }

    public void revealUiText()
    {
        //  scoreText.color = Color.white;
        //   percentText.color = Color.white;
        //   targetText.color = Color.white;
        t.GetComponent<Transform>().transform.position = Vector3.MoveTowards(t.GetComponent<Transform>().transform.position, togglePos, 100f);
        p.GetComponent<Transform>().transform.position = Vector3.MoveTowards(p.GetComponent<Transform>().transform.position, pogglePos, 100f);
        diamond.GetComponent<Transform>().position = Vector3.MoveTowards(diamond.GetComponent<Transform>().transform.position, diamondPos, 100f);
        egg.GetComponent<Transform>().position = Vector3.MoveTowards(egg.GetComponent<Transform>().transform.position, eggPos, 100f);
        exitbtn.GetComponent<Transform>().position = Vector3.MoveTowards(exitbtn.GetComponent<Transform>().transform.position, exitbtnPos, 100f);

        targetText.GetComponent<Transform>().position = Vector3.MoveTowards(targetText.GetComponent<Transform>().transform.position, targetTextPos, 100f);
        scoreText.GetComponent<Transform>().position = Vector3.MoveTowards(scoreText.GetComponent<Transform>().transform.position, highscorePos, 100f);
        percentText.GetComponent<Transform>().position = Vector3.MoveTowards(percentText.GetComponent<Transform>().transform.position, percentPos, 100f);

    }

    //updates the score
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        if (score % mod == 0 && timeScale < 5.5)
        {
            timeScale *= exponent;
        }
        UpdateScore();
    }

    //updates the score
    public void AddTarget(int newScoreValue)
    {
        target += newScoreValue;

        UpdateTargetText();
    }

    //updates the score
    public void CalcPercent()
    {
        UpdateTargetText();

        if (score > 0)
        {
            percent = (double)score / target * 100d;
        }
        Debug.Log("Score: " + score + " Target: " + target + " Percent: " + percent);
        UpdatePercentText();
    }

    public void setBossActive()
    {

        BossActive = true;
    }

    public void setBossInActive()
    {

        BossActive = false;
    }
    public bool getBossActive()
    {

        return BossActive;
    }

    //Displays the new score to the UI
    void UpdateScore()
    {

        // scoreText.text = "Score: " + score;
        CalcPercent();

    }



    void UpdateTargetText()
    {

        targetText.text = score + "/" + target;
    }

    void UpdatePercentText()
    {


        if (percent >= 80)
        {
            percentText.color = Color.green;
            diamond.GetComponent<RotateBehaviour>().RotationAmount.y = 300;
            diamond.GetComponent<Renderer>().material.color = Color.green;
        }
        if (percent < 80 && percent > 69)
        {
            percentText.color = Color.yellow;
            diamond.GetComponent<RotateBehaviour>().RotationAmount.y = 200;
            diamond.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (percent < 60)
        {
            percentText.color = Color.red;
            diamond.GetComponent<RotateBehaviour>().RotationAmount.y = 100;
            diamond.GetComponent<Renderer>().material.color = Color.red;
        }


        percentText.text = Mathf.Round((float)percent) + " %";

        /*
        if (hideText) {
            Color color = percentText.color;
            color.a = 0.1f;
            percentText.color = color;
        }
        */
    }

    //Displays the new life count to the UI
    public void UpdateLives()
    {
        if (lives < 0)
        {
            lives = 0;
        }
        // livesText.text = "Lives: " + lives;
        livesText.text = lives.ToString();
    }

    //Set StarCount
    public void setStarCount(int starCount)
    {
        starText.text = starCount.ToString();


    }

    //Sets game as ended
    public void GameOver()
    {

        // livesText.text = "Game Over! \nPress R to restart";
        restartText.text = "         Game Over!\n\nTap Screen to Restart";
        gameOver = true;
        BossActive = false;

    }




    public void setLevelComplete()

    {
        restartText.GetComponent<RectTransform>().rect.Set(0, 290, 0, 30);

        restartText.text = "LEVEL COMPLETED";
        // restart = true;
        for (int i = 0; i < 260; i++)
        {
            restartText.GetComponent<RectTransform>().rect.Set(0, 290, i, 30);
            new WaitForSeconds(1);
        }

        new WaitForSeconds(3);

        if (Application.loadedLevel == 1)
        {
            Application.LoadLevel(2);



        }
        else if (Application.loadedLevel == 2)
        {
            Application.LoadLevel(3);


        }


    }

    public void incrementBirdCount()
    {

        birdCount++;
    }

    public int getBirdCount()
    {

        return birdCount;
    }
    
        public void showSwipeModeToast()
        {
        if (!touchMode)
        {
            highScoreText.text = "Swipe Mode On";
            Invoke("clearText", 1);
        }

       else if (touchMode)
        {
            highScoreText.text = "Swipe Mode Off";
            Invoke("clearText", 1);
        }
    }
  


    public void checkAchievement() {

        if (score!=0&&score%10==0&&!tenPtAchieveBool) {
            highScoreText.text = "10pt Achievement";
            Invoke("clearText", 1);
            tenPtAchieveBool = true;
        }

        if (percent>99.9 && !hundredPercentAchieveBool)
        {
            highScoreText.text = "100% Achievement";
            Invoke("clearText", 1);
            hundredPercentAchieveBool = true;
        }

        if (score != 0 && score % 100 == 0 && !hundredPtAchieveBool)
        {
            highScoreText.text = "100pt Achievement";
            Invoke("clearText", 1);
            hundredPtAchieveBool = true;

            if (cornerCatchAchieveBool)
            {
                highScoreText.text = "Corner Catch";
                Invoke("clearText", 1);
                cornerCatchAchieveBool = true;
            }
        }

    }


    void clearText()
    {
        highScoreText.text = "";
        resetAchievements();
    }

    void clearRestartText()
    {
        restartText.text = "";
        restartText.color = Color.white;
    }

    void resetAchievements() {
        tenPtAchieveBool = false;
        hundredPtAchieveBool = false;
        hundredPercentAchieveBool = false;
        cornerCatchAchieveBool = false;
    }
}
