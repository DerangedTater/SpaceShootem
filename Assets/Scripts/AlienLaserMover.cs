using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLaserMover : MonoBehaviour
{
    public Rigidbody2D laserBody;
    public float laserSpeed;
    public float selfDestructDelay;
    private float selfDestructTimer = 0;
    //public GameController gameController;

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

    //TODO: Alien lasers as well as everything else (besides other asteroids) destroys asteroids on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("AlienLaser collided with: " + collision.gameObject);
        Destroy(this.gameObject);
    }
}
