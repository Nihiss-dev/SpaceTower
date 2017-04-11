using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(30,30));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Launch(Vector2 target)
    {
        Vector2 pos = target;
        Vector2 dir = (new Vector2(transform.position.x, transform.position.y) - pos).normalized;
        rb.AddForce(dir);
    }
}
