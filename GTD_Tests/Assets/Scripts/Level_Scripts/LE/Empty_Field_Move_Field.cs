using UnityEngine;
using System.Collections;

public class Empty_Field_Move_Field : MonoBehaviour {


    Vector3 V_Offset;
    Vector3 CurP;

    SpriteRenderer SR;


    //bool if we are zooming.
    bool b_zooming = false;

    // Use this for initialization
    void Start () {

        SR = GetComponent<SpriteRenderer>();
        //transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, );

    }

    // Update is called once per frame
    void Update () {

        float new_scall = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(new_scall);

        Vector3 New = transform.parent.transform.localScale;

        New = New * (new_scall + 1);

        //transform.parent.transform.localScale = New;


    }

    void OnMouseOver()
    {

        //zoom amount. 10%
        float f_amount = .1f;

        //Debug.Log("mouse over field");


        //determin if the scroll was used.
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !b_zooming)
        {
            //we are zooming in so we do it.
            b_zooming = true;

            transform.parent.transform.localScale = new Vector3(transform.parent.transform.localScale.x - (f_amount * -1), transform.parent.transform.localScale.y - (f_amount * -1));
            //Debug.Log(transform.parent.transform.localScale.x - (f_amount * -1));

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && !b_zooming)
        {
            //we are zooming in so we do it.
            b_zooming = true;

            transform.parent.transform.localScale = new Vector3(transform.parent.transform.localScale.x - (f_amount), transform.parent.transform.localScale.y - (f_amount));
            //Debug.Log(transform.parent.transform.localScale.x - (f_amount));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") == 0 && b_zooming)
        {
            b_zooming = false;
        }



    }


    void OnMouseDown()
    {
        V_Offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        CurP = transform.parent.transform.position;


    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition -= V_Offset;
        curPosition += CurP;
        //have to do this otherwise the mouse over events will fire.
        curPosition.z = SR.sortingOrder * -1;

        transform.parent.transform.position = curPosition;


    }

}
