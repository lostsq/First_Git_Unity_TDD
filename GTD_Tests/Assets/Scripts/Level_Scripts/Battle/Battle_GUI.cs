using UnityEngine;
using System.Collections;

public class Battle_GUI : MonoBehaviour {

    //This is the variable that will define things like size or the screen, how much scale to apply to the base things, ect ect.
    float f_Game_Scale = 1;


    //Here we define all the objects that will be in the battle GUI that it starts with.
    public GameObject go_Hot_Bar;
    public Sprite test12;
    public Camera Cur_Camera;


	// Use this for initialization
	void Start () {

        //we will always leave the default of 100 pixels per unit.

        //get the camera details
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Cur_Camera.orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;
        Debug.Log(cameraWidth);

        //1920w / 100 pixels per unit equals 19.2 in game units.
        //256h/ 100 pixels per unit equals 2.56 in game units.
        //bottom will be a ratio of 1, and above it we will want the invintory to be a ratio of 3. total will be 4 heigh of 256 at very max.
        
        
        
        //This will create a hotbar.
        GameObject go_Cur_HotBar = Instantiate(go_Hot_Bar);

        BoxCollider2D v_Cur_HotBar_Box_Collider = go_Cur_HotBar.GetComponent<BoxCollider2D>();

        //Now we take the camera and we get some specs.
        int i_screen_width = Cur_Camera.pixelWidth;
        int i_screen_height = Cur_Camera.pixelHeight;

        Rect v_Camera_Rec = Cur_Camera.rect;

        //get the x scale to know how far we need to strech our hotbar across the bottom.
        float x_scale = cameraWidth / v_Cur_HotBar_Box_Collider.bounds.size.x;
        //get the y scale to know how tall our 256 boxes need to be to fix exactly 4 tall on screen.
        float y_scale = cameraHeight / (v_Cur_HotBar_Box_Collider.bounds.size.y * 4);

        //now we set the game scale to the y scale if it's less than 1. if it's 1 or higher we leave since our 256 is max resolution and we don't want to strech it.
        //also need to make sure the x scale is not lower than .533 since that would cut off the sides. if it is than that's our new size. should not happen except on phones.
        if (x_scale < 1 || y_scale < .533f)
        {
            //take the smallest scale from the x or y.
            f_Game_Scale = Mathf.Min(x_scale, y_scale);
        }


        go_Cur_HotBar.transform.localScale = new Vector2(x_scale, y_scale);
        float f_half_y = (v_Cur_HotBar_Box_Collider.bounds.size.y * y_scale) / 2;
        Debug.Log(f_half_y);


        //We take the hotbar and center it along the bottom of the camera and then we embed it.
        Vector2 v_Hot_Bar_Pos = new Vector2(0, (cameraHeight/2 * -1) + f_half_y);// - (v_Cur_HotBar_Box_Collider.bounds.size.y / 2));

        go_Cur_HotBar.transform.position = v_Hot_Bar_Pos;


        //with the height/width we can determain where to place our hotbar, and how much our scale is going to be.



    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
