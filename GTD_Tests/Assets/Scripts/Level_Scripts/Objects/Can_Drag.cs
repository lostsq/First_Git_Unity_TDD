using UnityEngine;
using System.Collections;

public class Can_Drag : MonoBehaviour
{


    Vector3 V_Offset;
    Vector3 CurP;

    Vector3 Lock_P;
    Collider2D c_Lock_Coll;
    bool b_On_Lock = false;
    SpriteRenderer This_SR;

    Vector2 Default_Scale = new Vector2(.5f, .5f);


    // Use this for initialization
    void Start()
    {
        This_SR = GetComponent<SpriteRenderer>();

    }





    // Update is called once per frame
    void Update()
    {

    }



    void OnMouseOver()
    {
        //Debug.Log("Mouse Over");
    }

    

    void OnMouseDown()
    {
        //Debug.Log("Mouse Down");

        transform.parent = null;

        V_Offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        //V_Offset.z = transform.position.z;

        Vector2 Center_On_Mouse = new Vector2(V_Offset.x, V_Offset.y);

        transform.localScale = Default_Scale;

        transform.position = Center_On_Mouse;

        CurP = transform.position;



        //set the layering to be above everything
        This_SR.sortingOrder = 80;

    }

    void OnMouseUp()
    {
        
        //Debug.Log("Mouse Up");
        //here we move the object to where the lock is.
        transform.position = c_Lock_Coll.transform.position;

        //set the lock as the parent. This also changes the scale to 0 for some reason..
        transform.parent = c_Lock_Coll.transform;

        //now we change the scale so it matches where it is.
        transform.localScale = c_Lock_Coll.transform.localScale;
        //then we switch layers around. need to make sure that the object that can be dragged around is always on top of the spot it is placed no matter what.
        SpriteRenderer SR_t = c_Lock_Coll.gameObject.GetComponent<SpriteRenderer>();


        //change the layer no matter what to what the item it's above is plus 1.
        This_SR.sortingOrder = SR_t.sortingOrder + 1;

        


    }


    void OnMouseDrag()
    {

        

            Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition -= V_Offset;
            curPosition += CurP;
            //have to do this otherwise the mouse over events will fire.
            //curPosition.z = SR.sortingOrder * -1;

            transform.position = curPosition;
      
            
        

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        
        //Debug.Log("Collision occured" + coll.gameObject.name);


        if (coll.gameObject.tag == "Move_Spot")
        {

            c_Lock_Coll = coll;

            Debug.Log("Collision occured with spot");

            //would do a check here to see if something is in this spot but for now just locking it.
            //set the lock posistion.
            Lock_P = coll.gameObject.transform.position;
            //set the unit to be here.
            //b_On_Lock = true;


        }


        //coll.gameObject.SendMessage("ApplyDamage", 10);

    }

    void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
        //b_On_Lock = false;
    }


}

