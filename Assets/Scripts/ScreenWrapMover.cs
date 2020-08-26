using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapMover : MonoBehaviour
{
    GameObject obj;
    Rigidbody2D objBody;

    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
        objBody = obj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBoundaries.Instance.Initialized == false)
        {
            return;
        }

        //Check if outside boundaries
        if (objBody != null)
        {
            //Right to Left
            if (objBody.transform.position.x >= GameBoundaries.Instance.RightBorder)
            {
                transform.position = (new Vector3(GameBoundaries.Instance.LeftBorder, transform.position.y, 0f));
            }
            //Left to Right
            else if (objBody.transform.position.x <= GameBoundaries.Instance.LeftBorder)
            {
                transform.position = (new Vector3(GameBoundaries.Instance.RightBorder, objBody.transform.position.y, 0f));
            }

            //Top to Bottom
            if (objBody.transform.position.y >= GameBoundaries.Instance.TopBorder)
            {
                transform.position = (new Vector3(objBody.transform.position.x, GameBoundaries.Instance.BottomBorder, 0f));
            }
            //Bottom to Top
            else if (objBody.transform.position.y <= GameBoundaries.Instance.BottomBorder)
            {
                transform.position = (new Vector3(objBody.transform.position.x, GameBoundaries.Instance.TopBorder, 0f));
            }
        }
    }
}
