using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    #region Variables
    //gene for color
    public float r;
    public float g;
    public float b;
    public float s;
    
    public bool dead = false;
    public float timeToDie = 0;
    SpriteRenderer sRenderer;
    Collider2D sCollider;
	#endregion


    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, b, g);
        this.transform.localScale = new Vector3(s, s, s); 
        
    }
    void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        /*Debug.Log("Dead At " + timeToDie);*/
        Debug.Log("the elapsed time from the populationManager"+ PopulationManager.elapsed);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    void Update()
    {
        
    }
}
