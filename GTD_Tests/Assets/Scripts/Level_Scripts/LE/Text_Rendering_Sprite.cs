using UnityEngine;
using System.Collections;

public class Text_Rendering_Sprite : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Renderer>().sortingLayerID =
        this.transform.parent.GetComponent<Renderer>().sortingLayerID + 1;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
