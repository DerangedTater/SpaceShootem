using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject LargeAsteroid;
    public GameObject MediumAsteroid;
    public GameObject SmallAsteroid;

    public Rigidbody2D asteroidBody;

    public float LargeAsteroidVelocity;
    public float MediumAsteroidVelocity;
    public float SmallAsteroidVelocity;

    private int YVelocity;
    private int XVelocity;


    private Vector2 startingForce;
    private Vector3 spawnOffset = new Vector2(5, 0);
    // Start is called before the first frame update
    void Start()
    {
        YVelocity = Random.Range(-1, 2);
        XVelocity = Random.Range(-1, 2);
        switch(this.gameObject.tag)
        {
            case "LargeAsteroid":
                startingForce = new Vector2(LargeAsteroidVelocity * XVelocity, LargeAsteroidVelocity * YVelocity);
                asteroidBody.AddForce(startingForce);
                //Debug.Log(startingForce);
                break;
            case "MediumAsteroid":
                startingForce = new Vector2(MediumAsteroidVelocity * XVelocity, MediumAsteroidVelocity * YVelocity);
                asteroidBody.AddForce(startingForce);
                //Debug.Log(startingForce);
                break;
            case "SmallAsteroid":
                startingForce = new Vector2(SmallAsteroidVelocity * XVelocity, SmallAsteroidVelocity * YVelocity);
                asteroidBody.AddForce(startingForce);
                //Debug.Log(startingForce);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "LargeAsteroid" && collision.gameObject.tag != "MediumAsteroid" && collision.gameObject.tag != "SmallAsteroid")
        {
            switch (this.gameObject.tag)
            {
                case "LargeAsteroid":
                    GameObject newMedAst = Instantiate(MediumAsteroid);
                    newMedAst.transform.position = this.transform.position + spawnOffset;                   
                    GameObject newMedAst2 = Instantiate(MediumAsteroid);
                    newMedAst2.transform.position = this.transform.position - spawnOffset;
                    
                    //Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                case "MediumAsteroid":
                    GameObject newSmallAst = Instantiate(SmallAsteroid);
                    newSmallAst.transform.position = this.transform.position + spawnOffset;
                    GameObject newSmallAst2 = Instantiate(SmallAsteroid);
                    newSmallAst2.transform.position = this.transform.position - spawnOffset;
                    //Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                    break;
                case "SmallAsteroid":
                    Destroy(this.gameObject);
                    break;
                default:
                    break;

            }

        }
    }
}
