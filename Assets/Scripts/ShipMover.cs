using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    public Rigidbody2D ShipBody;
    public PolygonCollider2D Collider;

    public GameObject laser;
    public GameObject laserSpawner;
    public GameObject LargeAsteroid;
    public GameObject MediumAsteroid;
    public GameObject SmallAsteroid;

    public GameController gameController;

    public float ThrustForce;
    public float TurnForce;
    public float SpeedCap;
    public float TurnSpeedCap;
    public float shootDelay;

    private bool canFire = true;
    private float fireTimer = 0;

    bool justRespawned = true;
    private float invincibilityTimer = 0;
    public float invincibilityLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        ApplySpeedCap();
        LaserDelay();

        if (justRespawned)
        {
            if(invincibilityTimer <= invincibilityLimit)
            {
                invincibilityTimer += Time.deltaTime;
                Collider.enabled = false;
            }
            else
            {
                Collider.enabled = true;
                justRespawned = false;
            }
        }
    }

    private void ResetLocation()
    {

    }

    private void HandleInput()
    {       
        if(Input.GetKey(KeyCode.UpArrow))
        {
            //Forward Thrust
            Vector2 fwd = new Vector2(0.0f, ThrustForce);
            ShipBody.AddRelativeForce(fwd);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //Turn Left
            ShipBody.AddTorque(TurnForce);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Turn Right
            ShipBody.AddTorque(-TurnForce);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (canFire)
            {
                Instantiate(laser, laserSpawner.transform);
                canFire = false;
            }

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            Vector2 bcwd = new Vector2(0.0f, -ThrustForce);
            ShipBody.AddRelativeForce(bcwd);
            //Debug.Log(ShipBody.velocity.magnitude);

        }

    }

    private void LaserDelay()
    {
        if (!canFire)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= shootDelay)
            {
                canFire = true;
                fireTimer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!justRespawned)
        {
            Debug.Log("Ship GameObject: " + gameObject);
            gameController.OnDeath();
            Destroy(this.gameObject);
        }

    }

    private void ApplySpeedCap()
    {
        Vector2 CurVelocity = ShipBody.velocity;
        float speed = CurVelocity.magnitude;
        if(speed > SpeedCap)
        {
            CurVelocity.Normalize();
            CurVelocity *= SpeedCap;
            ShipBody.velocity = CurVelocity;
        }

        float curAngularVelocity = ShipBody.angularVelocity;
        if(curAngularVelocity > TurnSpeedCap)
        {
            curAngularVelocity = TurnSpeedCap;
            ShipBody.angularVelocity = curAngularVelocity;
        }
        if(curAngularVelocity < -TurnSpeedCap)
        {
            curAngularVelocity = -TurnSpeedCap;
            ShipBody.angularVelocity = curAngularVelocity;
        }
    }
}
