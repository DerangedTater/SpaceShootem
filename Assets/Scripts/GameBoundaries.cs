using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundaries : MonoBehaviour
{
    // Singleton pattern
    private static GameBoundaries _instance = null;
    public static GameBoundaries Instance
    {
        get
        {
            return _instance;
        }
    }

    private bool initialized = false;
    public bool Initialized
    {
        get
        {
            return initialized;
        }
        private set
        {
            initialized = value;
        }
    }

    public float EdgeBuffer;
    public GameObject Right;
    public GameObject Left;
    public GameObject Top;
    public GameObject Bottom;

    public float RightBorder
    {
        get { return Right.transform.position.x; }
    }
    public float LeftBorder
    {
        get { return Left.transform.position.x; }
    }
    public float TopBorder
    {
        get { return Top.transform.position.y; }
    }
    public float BottomBorder
    {
        get { return Bottom.transform.position.y; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Singleton Pattern
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        ComputeScreenBoundaries();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ComputeScreenBoundaries()
    {
        float CamZ = Camera.main.transform.position.z;
        float halfScreenWidth = Screen.width / 2;
        float halfScreenHeight = Screen.height / 2;

        Vector3 rightEdge3D = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, halfScreenHeight, -CamZ));
        Vector3 LeftEdge3D = Camera.main.ScreenToWorldPoint(new Vector3(0, halfScreenHeight, -CamZ));
        Vector3 TopEdge3D = Camera.main.ScreenToWorldPoint(new Vector3(halfScreenWidth, Screen.height, -CamZ));
        Vector3 BottomEdge3D = Camera.main.ScreenToWorldPoint(new Vector3(halfScreenWidth, 0, -CamZ));

        Right.transform.position = rightEdge3D + (Vector3.right * EdgeBuffer);
        Left.transform.position = LeftEdge3D + (Vector3.left * EdgeBuffer);
        Top.transform.position = TopEdge3D + (Vector3.up * EdgeBuffer);
        Bottom.transform.position = BottomEdge3D + (Vector3.down * EdgeBuffer);

        Initialized = true;
    }
}
