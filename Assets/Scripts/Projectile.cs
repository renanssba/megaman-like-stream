using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  public Rigidbody2D body;
  public float xSpeed;
  public int damage;

  public GameObject hitParticle;

	void Start () {
    if(HeroController.instance.transform.localScale.x > 0f){
      body.velocity = new Vector2(xSpeed, 0f);
    }else{
      body.velocity = new Vector2(-xSpeed, 0f);
    }
	}

  private void OnTriggerEnter2D(Collider2D collision) {
    Destroy(gameObject);
    Instantiate(hitParticle, transform.position, Quaternion.identity);
  }
}
