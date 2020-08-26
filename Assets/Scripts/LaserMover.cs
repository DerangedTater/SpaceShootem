using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public Rigidbody2D laserBody;
    public float laserSpeed;
    public float selfDestructDelay;
    private float selfDestructTimer = 0;
    public GameObject alienShip;
    public GameObject mediumAsteroid;
    public GameObject smallAsteroid;
    public GameController theGameController;

    // Start is called before the first frame update
    void Start()
    {
        MoveForward();
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroySelf();
    }

    private void MoveForward()
    {
        Vector2 fwd = new Vector2(0.0f, laserSpeed);
        laserBody.AddRelativeForce(fwd);

    }

    private void DestroySelf()
    {
        if (selfDestructTimer >= selfDestructDelay)
        {
            Destroy(this.gameObject);
            selfDestructTimer = 0;
        }
        else
        {
            selfDestructTimer += Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "AlienShip":
                theGameController.AddScore(200);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;

            case "LargeAsteroid":
                theGameController.AddScore(10);
                Destroy(this.gameObject);
                break;
            case "MediumAsteroid":
                theGameController.AddScore(50);
                Destroy(this.gameObject);
                break;
            case "SmallAsteroid":
                theGameController.AddScore(100);
                Destroy(this.gameObject);
                break;
            default:
                Destroy(this.gameObject);
                break;
        }

        Debug.Log(collision.gameObject.tag);
    }
}
