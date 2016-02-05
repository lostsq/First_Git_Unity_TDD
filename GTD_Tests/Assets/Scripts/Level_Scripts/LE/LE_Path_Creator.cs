using UnityEngine;
using System.Collections;

public class LE_Path_Creator : MonoBehaviour {

    //This is the tag database.
    Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();

    //this is the different sprites for the icons.
    public Sprite Sprite_Remove;
    public Sprite Sprite_Arrow_Right;
    public Sprite Sprite_Arrow_Left;
    public Sprite Sprite_Arrow_Up;
    public Sprite Sprite_Arrow_Down;
    public Sprite Sprite_Empty_Spot;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// This will find the child of the path creator and return it so the user can do stuff with it.
    /// </summary>
    /// <param name="Direction"></param>
    /// <returns></returns>
    private GameObject Get_Child(char Direction)
    {
        //this is the child we are returning. will return null if nothing is found.
        GameObject Child = null;

        //get the direction to a string.
        string String_Direction = "temp";
        if (Direction == 'w')
        {
            String_Direction = "Up";
        }
        else if (Direction == 's')
        {
            String_Direction = "Down";
        }
        else if (Direction == 'a')
        {
            String_Direction = "Left";
        }
        else if (Direction == 'd')
        {
            String_Direction = "Right";
        }

        //get all the childrend of the place.
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            //get the object we are working with.
            GameObject T_Child = gameObject.transform.GetChild(i).gameObject;
            //see if the name of the child matches the direction we are checking.
            if (T_Child.name == String_Direction)
            {
                Child = T_Child;
            }

        }

        //return the child. will be null if there is not one found.
        return Child;
    }

    /// <summary>
    /// This is called when the user wants to reset the children to what they need to be pending on what direction was last moved.
    /// when 'r' is passed then it will reset them all back to default.
    /// </summary>
    /// <param name="Direction"></param>
    public void Reset_Children(char Direction)
    {
        //first we reset all the direction.
        Set_Child('w', 'w');
        Set_Child('s', 's');
        Set_Child('a', 'a');
        Set_Child('d', 'd');

        char New_Dir = Direction;

        //perform various tasks pending the direction.
        switch (Direction)
        {
            //this is the Up.
            case 'w':
                //switch directions.
                New_Dir = 's';               
                break;
            //this is the Down.
            case 's':
                //switch directions.
                New_Dir = 'w';
                break;
            //this is the Left.
            case 'a':
                //switch directions.
                New_Dir = 'd';
                break;
            //this is the Right.
            case 'd':
                //switch directions.
                New_Dir = 'a';
                break;

            default:
                break;
        }


        if (New_Dir != 'r')
        {
            Set_Child(New_Dir, 'r');
        }
    }

    /// <summary>
    /// This will set a child as what you want. either remove, or left/right/add/ect.
    /// </summary>
    /// <param name="Direction"></param>
    /// <param name="Set_As"></param>
    private void Set_Child(char Direction, char Set_As)
    {
        //first we need the child we are working with.
        GameObject Child = Get_Child(Direction);
        SpriteRenderer Child_SR = Child.GetComponent<SpriteRenderer>();

        //now we need to set the child to what is going to be set.
        switch (Set_As)
        {
            //this is the remove.
            case 'r':
                //first we set the sprite.
                Child_SR.sprite = Sprite_Remove;
                //now we set the tag.
                Child.tag = G_Tags.Tag_Button_Path_Remove;
                break;
            //this is the Up.
            case 'w':
                //first we set the sprite.
                Child_SR.sprite = Sprite_Arrow_Up;
                //now we set the tag.
                Child.tag = G_Tags.Tag_Button_Path_Up;
                break;
            //this is the Down.
            case 's':
                //first we set the sprite.
                Child_SR.sprite = Sprite_Arrow_Down;
                //now we set the tag.
                Child.tag = G_Tags.Tag_Button_Path_Down;
                break;
            //this is the Left.
            case 'a':
                //first we set the sprite.
                Child_SR.sprite = Sprite_Arrow_Left;
                //now we set the tag.
                Child.tag = G_Tags.Tag_Button_Path_Left;
                break;
            //this is the Right.
            case 'd':
                //first we set the sprite.
                Child_SR.sprite = Sprite_Arrow_Right;
                //now we set the tag.
                Child.tag = G_Tags.Tag_Button_Path_Right;
                break;

            default:
                break;
        }


    }
}
