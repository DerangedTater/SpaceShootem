using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

public GameObject AlienShip;
    public GameObject SpawnZone;
    public GameObject playerShip;
    public GameObject playerSpawn;
    public GameObject smallAsteroid;
    public GameObject mediumAsteroid;
    public GameObject largeAsteroid;


    public Text lives;
    public Text scoreBox;
    public Text Info;

    public ShipMover shipMover;
    public LaserMover laser;
    public Asteroid asteroid;

    private Transform spawnLocation;

    private int livesLeft;
    private int score;
    public float respawnDelay;
    public float AlienDelay;
    public int totalAsteroids;
    private float respawnTimer = 0;
    private float timePassed = 0;
    private int asteroidsLeft = 0;

    private bool hasStarted = false;
    private bool waitForRespawn = false;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAlienShip();
        SpawnAsteroids();
        //SpawnAlienShip();
        //SpawnAlienShip();

        livesLeft = 2;

        //shipMover.gameController = this;

    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Lives: " + livesLeft;
        scoreBox.text = "Score: " + score;
        if (laser != null)
        {
            laser.theGameController = this;
        }
        if (shipMover != null)
        {
            shipMover.gameController = this;
        }
        if(!hasStarted)
        {
            Info.text = "Use arrows to move and space to shoot. press space to begin";
            Time.timeScale = 0;
            GameStart();
        }


        if(gameOver)
        {
            GameRestart();
        }

        SpawnAlienOnDelay();
    }

    private void FixedUpdate()
    {
        if (waitForRespawn)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnDelay && IsCenterOpen())
            {
                //spawnLocation = Vector3(GetRandomLocation());
                Instantiate(playerShip, playerSpawn.transform);

                shipMover.gameController = this;
                waitForRespawn = false;
                respawnTimer = 0;
            }
        }
    }

    public void GameStart()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Info.text = "";
            Time.timeScale = 1;
            hasStarted = true;
            Instantiate(playerShip, playerSpawn.transform);
            shipMover.gameController = this;
        }
    }

    public void GameRestart()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void SpawnAlienShip()
    {
        Instantiate(AlienShip, GetRandomLocation(), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }

    private void SpawnAlienOnDelay()
    {
        timePassed += Time.deltaTime;

        if(timePassed >= AlienDelay)
        {
            SpawnAlienShip();
            timePassed = 0;
        }
    }

    //Generate a Vector3 location in the SpawnZone object
    public Vector3 GetRandomLocation()
    {
        float spawnWidth = SpawnZone.GetComponent<Renderer>().bounds.size.x;
        float spawnHeight = SpawnZone.GetComponent<Renderer>().bounds.size.y;

        return new Vector3(Random.Range(-spawnWidth / 2, spawnWidth / 2),
            Random.Range(-spawnHeight / 2, spawnHeight / 2));
    }


    public void OnDeath()
    {
        if (livesLeft > 0)
        {
            livesLeft--;
            waitForRespawn = true;
        }
        else
        {
            Info.text = "Game Over \n Press space to play again";
            gameOver = true;
        }
    }

    private void SpawnAsteroids()
    {
        for(int i = 0; i < totalAsteroids; i++)
        {
            int asteroidType = Random.Range(0, 3);
            Vector3[] spawnedAsteroids = new Vector3[totalAsteroids];
            Vector3 newLocation;
            GameObject currentAsteroid;
            switch (asteroidType)
            {
                case 0:

                    newLocation = GetRandomLocation();

                    for(int j = 0; j < i; j++)
                    {
                        if(newLocation == spawnedAsteroids[j])
                        {
                            newLocation = GetRandomLocation();
                            j = 0;
                        }
                    }

                    currentAsteroid = Instantiate(smallAsteroid, newLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    spawnedAsteroids[i] = currentAsteroid.transform.position;

                    asteroidsLeft += 1;

                    break;
                case 1:
                    newLocation = GetRandomLocation();

                    for (int j = 0; j < i; j++)
                    {
                        if (newLocation == spawnedAsteroids[j])
                        {
                            newLocation = GetRandomLocation();
                            j = 0;
                        }
                    }

                    currentAsteroid = Instantiate(mediumAsteroid, newLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    spawnedAsteroids[i] = currentAsteroid.transform.position;

                    asteroidsLeft += 2;

                    break;
                case 2:
                    newLocation = GetRandomLocation();

                    for (int j = 0; j < i; j++)
                    {
                        if (newLocation == spawnedAsteroids[j])
                        {
                            newLocation = GetRandomLocation();
                            j = 0;
                        }
                    }

                    currentAsteroid = Instantiate(largeAsteroid, newLocation, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    spawnedAsteroids[i] = currentAsteroid.transform.position;

                    asteroidsLeft += 4;
                    break;
            }
        }

    }

    private bool IsCenterOpen()
    {
        return !playerSpawn.GetComponent<CircleCollider2D>().IsTouchingLayers();
    }

    public void AddScore(int shotScore)
    {
        score += shotScore;
    }
    /*
    private bool LocationHasNoCollision(Vector3 location)
    {

    }
    */
}
