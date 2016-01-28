using UnityEngine;
using System.Collections;

public class Mouse_Interaction_Script : MonoBehaviour {


    //This is if dragging is occuring.
    bool b_Is_Dragging = false;
    //This is the object that we are working with when dragging.
    Collider2D Collider_Working_With = null;
    //this is the sprite renderer of the collider we are working with.
    SpriteRenderer This_SR;


    //used for dragging
    Vector3 V_Offset;
    Vector3 CurP;
    GameObject Old_Parent = null;

    // Use this for initialization
    void Start()
    {

        
    }
	
	// Update is called once per frame
	void Update () {

        //First we check to see if the mouse is being dragged.
        if (b_Is_Dragging)
        {
            //the dragging event is fired.
            Mouse_Dragging_Fired();
        }

        //Check and see if the left mouse click has been pressed down.
        if (Input.GetMouseButtonDown(0))
        {
            //Call the mouse down fired event.
            Mouse_Down_Fired();
        }

        //Check and see if the left mouse click as been released.
        if (Input.GetMouseButtonUp(0))
        {
            Mouse_Up_Fired();
            

        }

    }



    //This is called when the mouse down is fired.
    void Mouse_Down_Fired()
    {
        //Debug.Log("Mouse Down");

        //we get the highest collider at the mouses location.
        Collider2D Highest_Collider = Find_Highest_Collider_At_Mouse();
        
        //Verify that there is a highest collider to work with.
        if (Highest_Collider != null)
        {
            //now with this collider we just need to determain if it's a button,object,ect.
            //we will check to see what information the tag contains.
            string s_Tag = Highest_Collider.gameObject.tag;

            //Now we check if the tag contains what information.
            if (s_Tag.Contains("Menu"))
            {
                //is a menu type object so we will pass it to the menu click.
                Menu_Click(Highest_Collider);
            }

            if (s_Tag.Contains("Drag"))
            {
                //is a menu type object so we will pass it to the menu click.
                Dragable_Object(Highest_Collider);
            }


        }

    }

    //This is the update for dragging objects.
    void Mouse_Dragging_Fired()
    {

        string s_Tag = Collider_Working_With.gameObject.tag;

        if (s_Tag.Contains("Field"))
        {

            //this is all for testing, but is close to final for dragging.

            Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition -= V_Offset;
            curPosition += CurP;
            //have to do this otherwise the mouse over events will fire.
            //curPosition.z = SR.sortingOrder * -1;

            Collider_Working_With.gameObject.transform.parent.transform.position = curPosition;
        }
        else
        {


            //this is all for testing, but is close to final for dragging.

            Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition -= V_Offset;
        curPosition += CurP;
        //have to do this otherwise the mouse over events will fire.
        //curPosition.z = SR.sortingOrder * -1;

        Collider_Working_With.gameObject.transform.position = curPosition;

        }

    }



    //this is the update for when the mouse is let go.
    void Mouse_Up_Fired()
    {
        b_Is_Dragging = false;

        if (Collider_Working_With != null)
        {

            string s_Tag = Collider_Working_With.gameObject.tag;

            if (s_Tag.Contains("Field"))
            {

                //this is all for testing, but is close to final for dragging.

                Vector3 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition -= V_Offset;
                curPosition += CurP;
                //have to do this otherwise the mouse over events will fire.
                //curPosition.z = SR.sortingOrder * -1;

                Collider_Working_With.gameObject.transform.parent.transform.position = curPosition;
            }
            else
            {

                //for now we will just test for the dragging objects and placing them down.
                //Debug.Log("Pressed left click.");
                //setting this for now.
                Collider2D Highest_Collider = Find_Highest_Collider_At_Mouse();



                //need to make sure highest collider is not null.
                if (Highest_Collider != null)
                {


                    //now we check if the collider has the spot tag.
                    if (Highest_Collider.tag.Contains("Spot"))
                    {
                        Snap_To_Spot(Highest_Collider.gameObject, Collider_Working_With.gameObject);
                    }
                    else
                    {
                        Snap_To_Spot(Old_Parent, Collider_Working_With.gameObject);
                    }
                }
                else
                {
                    Snap_To_Spot(Old_Parent, Collider_Working_With.gameObject);
                }
            }
        }
        //regaurdless, at the end we set it back to null.
        Collider_Working_With = null;
    }


    //This will perform the action that the click will do when it is clicked.
    void Menu_Click(Collider2D Passed_Collider)
    {
        string s_Tag = Passed_Collider.gameObject.tag;

        //First we see what menu we are working with.
        if (s_Tag.Contains("Stats_Menu_Button"))
        {
            //we know this is the stats menu button so we need to enable/click it.
        }
    }

    //This will start the dragging process if it can be dragged.
    void Dragable_Object(Collider2D Passed_Collider)
    {
        //Debug.Log("Drag Down");


        string s_Tag = Passed_Collider.gameObject.tag;

        if (s_Tag.Contains("Field"))
        {
            //set the collider working with and set the dragging to true along with the sprite renderer.
            Collider_Working_With = Passed_Collider;
            //This_SR = Collider_Working_With.gameObject.GetComponent<SpriteRenderer>();
            b_Is_Dragging = true;

            //Collider_Working_With.gameObject.transform.parent = null;

            V_Offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            //V_Offset.z = transform.position.z;

            //Vector2 Center_On_Mouse = new Vector2(V_Offset.x, V_Offset.y);

            //Collider_Working_With.gameObject.transform.localScale = new Vector2(.5f, .5f);

            //Collider_Working_With.gameObject.transform.position = Center_On_Mouse;

            CurP = Collider_Working_With.gameObject.transform.parent.transform.position;



            //set the layering to be above everything
            //This_SR.sortingOrder = 80;
        }
        else
        {


            //this will be test stuff to get a drag working.
            //normally we would check and see if it can be dragged with something. maybe even a script attached to the object
            //but right now we just want to test it in the editor.

            //set the collider working with and set the dragging to true along with the sprite renderer.
            Collider_Working_With = Passed_Collider;
            This_SR = Collider_Working_With.gameObject.GetComponent<SpriteRenderer>();
            b_Is_Dragging = true;


            Old_Parent = Collider_Working_With.gameObject.transform.parent.gameObject;

            Collider_Working_With.gameObject.transform.parent = null;

            V_Offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            //V_Offset.z = transform.position.z;

            Vector2 Center_On_Mouse = new Vector2(V_Offset.x, V_Offset.y);

            Collider_Working_With.gameObject.transform.localScale = new Vector2(.5f, .5f);

            Collider_Working_With.gameObject.transform.position = Center_On_Mouse;

            CurP = Collider_Working_With.gameObject.transform.position;



            //set the layering to be above everything
            This_SR.sortingOrder = 80;

        }
    }



    //This will find the highest layered (by sprite level) at the mouses location. It does not include what is being dragged.
    Collider2D Find_Highest_Collider_At_Mouse()
    {
        //get where the mouse is on the screen.
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //This is the list of all the objects that overlap with the point on the screen.
        Collider2D[] col = Physics2D.OverlapPointAll(v);

        //this is the highest layered collider.
        Collider2D Highest_Collider = null;


        //Now we go through each of the colliders.
        foreach (Collider2D c in col)
        {
            //this is to make sure it doesn't look at the dragging/working with collider.
            if (c != Collider_Working_With)
            {
                //the sprite renderer attached to the collider object. Should be one for every 2d collider object.
                SpriteRenderer T_Sprite = c.gameObject.GetComponent<SpriteRenderer>();

                //check if there is a highest collider, and if not we set it. along with making sure there is a sprite for that collider.
                if (Highest_Collider == null && T_Sprite != null)
                {
                    Highest_Collider = c;
                }

                //here is the highest collider's sprite layer order.
                int Layer_Number = Highest_Collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

                //Make sure there is a sprite renderer before attempting to do anything with it.
                if (T_Sprite != null)
                {
                    //check if the sprite has a higher layer.
                    if (T_Sprite.sortingOrder > Layer_Number)
                    {

                        //does have a higher layer so we change the highest collider.
                        Highest_Collider = c;
                    }
                }
            }
        }

        //we return the highest
        return Highest_Collider;

    }

    //This is called when we want to snap a draggable item or something to spot. it will set it as it's parent and scale it and move it and set the order.
    void Snap_To_Spot(GameObject Parent, GameObject Child)
    {
        //here we move the object to where the parent is.
        Child.transform.position = Parent.transform.position;

        //set the lock as the parent. This also changes the scale to 0 for some reason..
        Child.transform.parent = Parent.transform;
        //now we change the scale so it matches where it is.
        Child.transform.localScale = Parent.transform.localScale;

        //then we switch layers around. need to make sure that the object that can be dragged around is always on top of the spot it is placed no matter what.
        SpriteRenderer SR_t = Parent.GetComponent<SpriteRenderer>();

        //change the layer no matter what to what the item it's above is plus 1.
        This_SR.sortingOrder = SR_t.sortingOrder + 1;

    }



}
