using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string logText = "Hello world again";
    public float speed = 2;

    // Use this for initialization
    void Start() {
        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(logText);
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        
        rigidBody.velocity = Vector2.right * speed;
    }
}
