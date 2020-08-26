using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipController : MonoBehaviour
{
    public Rigidbody2D ShipBody;
    public GameObject AlienLaser;
    public GameObject AlienLaserSpawner;
    public float ThrustForce;
    public float TurnForce;
    public float SpeedCap;
    public float TurnSpeedCap;
    public float RotationCheckPeriod = 1f;
    public float TotalRotateTime = 1f;
    public float shootDelay;

    private GameObject Player;
    private float TimeSinceLastRotationCheck;
    private Vector2 fwd;
    private bool turnFinished;
    private float rotateTime;
    private bool canFire = true;
    private float fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        TimeSinceLastRotationCheck = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //Move the ship
        fwd = new Vector2(0.0f, ThrustForce);
        ShipBody.AddRelativeForce(fwd);
        if (Player != null)
        {
            RotateShip();
        }
        else
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        //Shooting
        HandleShooting();
    }

    private bool TimeCheckOver()
    {
        TimeSinceLastRotationCheck += Time.deltaTime;

        if (TimeSinceLastRotationCheck >= RotationCheckPeriod)
        {
            //TimeSinceLastRotationCheck = 0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleShooting()
    {
        LaserDelay();
        if(canFire)
        {
            Instantiate(AlienLaser, AlienLaserSpawner.transform.position, this.transform.rotation);
            canFire = false;
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
    //Wait for RotationCheckPeriod amount of time and then add turnForce for TotalRotateTime
    private void RotateShip()
    {
        if (TimeCheckOver())
        {
            //Check the angle between the ship and the player, and rotate towards player
            Vector3 AlienShipFwd = transform.up;
            Vector3 PlayerPos = Player.transform.position;
            Vector3 ToPlayer = PlayerPos - transform.position;

            /*
            //Dot product to calc angle to player
            //step 1: normalize the vectors
            AlienShipFwd.Normalize();
            ToPlayer.Normalize();
            //step 2: multiply the vectors
            float product = Vector3.Dot(AlienShipFwd, ToPlayer);
            //step3: get arc cos
            float angle = Mathf.Acos(product);
            angle *= Mathf.Rad2Deg;
            */

            Vector3 cross = Vector3.Cross(AlienShipFwd, ToPlayer);
            if (cross.z < 0.0f)
            {
                //Debug.Log("Need to turn right");
                rotateTime += Time.deltaTime;
                if (rotateTime <= TotalRotateTime)
                    ShipBody.AddTorque(-TurnForce);
                else
                {
                    rotateTime = 0f;
                    //turnFinished = true;
                    TimeSinceLastRotationCheck = 0f;
                }
            }
            else
            {
                //Debug.Log("Need to turn left");
                rotateTime += Time.deltaTime;
                if (rotateTime <= TotalRotateTime)
                    ShipBody.AddTorque(TurnForce);
                else
                {
                    rotateTime = 0f;
                    //turnFinished = true;
                    TimeSinceLastRotationCheck = 0f;
                }
            }


            /*
            float DirectionAngle = Vector3.SignedAngle(AlienShipFwd, PlayerPos, Vector3.back);
            if(DirectionAngle > 0f)
            {               
                rotateTime += Time.deltaTime;
                if(rotateTime <= TotalRotateTime)
                    ShipBody.AddTorque(TurnForce);
                else
                {
                    rotateTime = 0f;
                    turnFinished = true;
                    TimeSinceLastRotationCheck = 0f;
                }
            }
            else if(DirectionAngle < 0f)
            {
                rotateTime += Time.deltaTime;
                if (rotateTime <= TotalRotateTime)
                    ShipBody.AddTorque(-TurnForce);
                else
                {
                    rotateTime = 0f;
                    turnFinished = true;
                    TimeSinceLastRotationCheck = 0f;
                }
            } 
            */
       
 }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
