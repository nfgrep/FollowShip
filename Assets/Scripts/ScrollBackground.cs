using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    private Vector3 mouseWorldPos;
    private Vector2 moveDirection;
    public float scrollSpeed = 1;
    public Rigidbody2D player;
    Renderer rend;

	void Start () {
        rend = GetComponent<Renderer>();    
	}
	
	void FixedUpdate () {

        //Scrolls background based on player velocity
        if (player.velocity.x != 0 && player.velocity.y != 0) {
            rend.material.SetTextureOffset("_MainTex", player.velocity * Time.deltaTime);
        } 
	}
}
