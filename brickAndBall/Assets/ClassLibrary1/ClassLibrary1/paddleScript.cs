using UnityEngine;
using System.Collections;

public class paddleScript : MonoBehaviour {

	// Use this for initialization
    public int maxLevel = 6;
    public float paddelSpeed = 12f;
    public GameObject attachedBall;
    public GameObject foodPre;
    //
    public Vector3 baseOffset = new Vector3(0, 1f, 0);
    public Vector3 launchForce = new Vector3(0, 700f, 0);
    public float bounceForce = 10f;
    //
    GameObject ball;
    GameObject paddleBall;
    public GUISkin guiSkin;
    public int level = 1;
    public float increaseSpeedPerLevel = 3.0f;
    
    //
    public int lives = 3;
    int scroes = 0;
    //
    int newBallHitCount = 0;
    int currentBallIndex = 0;
    //for ball
    public Material[] mats;
    public int[] hitPoints;
    public int[] nextBallHitCounts;


    public AudioClip brickBrokeSound;
    public AudioClip jumpSound;
    public AudioClip []pickCoinSounds;
   //
    public bool hasAccelerate = false;
	void Start () {
        if (mats.Length != hitPoints.Length || mats.Length != nextBallHitCounts.Length)
        {
            Debug.Log("mats.Length != hitPoints.Length || mats.Length != hitPoints.Length");
        }
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1;
	}
    void playSound(AudioClip a)
    {
        AudioSource.PlayClipAtPoint(a, Vector3.zero);
    }
    public void exitPlay()
    {
        if(gameObject)Destroy(gameObject);
        if (ball) Destroy(ball);
    }
    public void PlayBrcikBrokeSound(){
        playSound(brickBrokeSound);
    }
    void changeBall()
    {
        newBallHitCount = 0;
        if (currentBallIndex >= mats.Length) currentBallIndex = mats.Length - 1;
        int i = currentBallIndex;
        if (0 <= i && i < mats.Length)
        {
           
          
            Debug.Log(ball);
            Debug.Log(attachedBall);
            if (ball)
            {
                MeshRenderer mr=ball.GetComponent<MeshRenderer>();
                Debug.Log(mr);
                if (mr)
                {
                    mr.material = mats[i];
                }
                ball.GetComponent<BallScript>().hitPower = hitPoints[i];
                
            }
            //
            Debug.Log("using ball " + i);
        }
        
       
    }
    public void addScore(int v)
    {
        scroes += v;
        //
        newBallHitCount++;
        if (newBallHitCount >= nextBallHitCounts[currentBallIndex])
        {
            Debug.Log("change to next ball");
            newBallHitCount = 0;
            currentBallIndex++;
            changeBall();
        }
    }
    public void EatFood()
    {
        playSound(pickCoinSounds[Random.Range(0,pickCoinSounds.Length-1)]);
        Debug.Log("eat a food");
        this.scroes++;
        
    }
    void spawnBall()
    {
        if (attachedBall == null)
        {
            Debug.Log("you forget to place a ball in inspector panel");
            return;
        }
        paddleBall=ball = (GameObject)Instantiate(attachedBall, transform.position + baseOffset, Quaternion.identity);
        Debug.Log("spawn a ball" + ball);
        changeBall();      
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 10, 100, 200), new GUIContent("Lives:" + lives),guiSkin.GetStyle("label"));
        GUI.Label(new Rect(100, 10, 200, 200), new GUIContent("Score:" + scroes), guiSkin.GetStyle("label"));
    }
	// Update is called once per frame
    
    void loadNextLevel()
    {
        if (level < maxLevel)
        {
            level++;
            Application.LoadLevel("level" + level);
            paddelSpeed += increaseSpeedPerLevel;
            Debug.Log("paddelSpeed=" + paddelSpeed);
        }
        else
        {
            YouWin();
        }

    }
    void YouWin()
    {
        exitPlay();
        Application.LoadLevel("youWin");
    }
     void OnLevelWasLoaded (int level  ) {
         spawnBall();
         Debug.Log("has load level index=" + level);
       
    }

    void checkForNextLevel()
    {
      
        if (GameObject.FindGameObjectsWithTag("BrickTag").Length <= 0)
        {
            loadNextLevel();
        }


    }

    /*static float moveDirOldTime = Time.time;
    public void MoveDir(float dir){
        transform.Translate(paddelSpeed * dir * (Time.time - moveDirOldTime), 0, 0);
        moveDirOldTime = Time.time;
    }*/
    bool isClickMoveLeft=false;
        bool isClickMoveRight=false;
    public void ClickMoveLeft()
    {
        isClickMoveLeft = true;
    }
    public void ClickMoveRight()
    {
        isClickMoveRight = true;
    }

    public void Die(){
        if (lives > 0)
        {
            lives--;
            currentBallIndex = 0;
            spawnBall();
        }else 
        if (lives <= 0)
        {
            //
            Debug.Log("game over");
            LoseGame();
        }
    }
    void LoseGame()
    {
        exitPlay();
        Application.LoadLevel("gameOver");
    }
	void Update () {

        //for button:
    
        float moveDir = 0;
        if (isClickMoveLeft)
        {
            isClickMoveLeft = false;
            moveDir = -1;

        }
        if (isClickMoveRight)
        {
            isClickMoveRight = false;
            moveDir = 1;

        }
        moveDir *= 1.2f;
        transform.Translate(paddelSpeed * moveDir * Time.deltaTime, 0, 0);
        //
        
        //for android:
        if (hasAccelerate)
        {
            Vector3 acc= Input.acceleration;
            if(acc.z>0){acc.x=-acc.x;acc.y=-acc.y;}
            float dir = 1;
            dir = acc.x * 7.0f;
            if (dir > 1.2f) dir = 1f;
            if (dir < -1.2f) dir = -1f;
            dir *= 1.6f;
            transform.Translate(paddelSpeed* dir* Time.deltaTime, 0, 0);
        }


        transform.Translate(paddelSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);

        const float rightBound = 7f;
        const float leftBound = -7f;
        if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < leftBound)
        {
           
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
         
        }
        if (paddleBall)
        {
            paddleBall.rigidbody.position = transform.position + baseOffset;
            if (Input.GetButtonDown("Jump"))
            {
                paddleBall.rigidbody.AddForce(launchForce);
                paddleBall.rigidbody.isKinematic = false;
                paddleBall = null;
            }
            //for android:
            if (hasAccelerate)
            {
                if (Input.touchCount > 0)
                {
                    Debug.Log("touching..");
                    paddleBall.rigidbody.AddForce(launchForce);
                    paddleBall.rigidbody.isKinematic = false;
                    paddleBall = null;
                }
                
            }
        }
        checkForNextLevel();

        //
        if (Input.GetAxis("Vertical") > 0)
        {
            loadNextLevel();
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (level > 2)
            {
                level -= 2;
            }
            loadNextLevel();
        }
	}

    void OnCollisionEnter(Collision col)
    {
       
 
        foreach (ContactPoint contact in col.contacts)
        {
            //
            if (contact.thisCollider == this.collider)
            {
                playSound(jumpSound);
                float err = contact.point.x - this.transform.position.x;
                contact.otherCollider.rigidbody.AddForce(bounceForce * err, 0, 0);
                
            }
        }
    }
}
