using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBit : Enemy {

  public Vector2 jumpSpeed;
  public float waitTime;


  public void Start() {
    base.Start();
    StartCoroutine(WaitAndJump());
  }


  IEnumerator WaitAndJump() {
    while(true) {
      yield return new WaitForSeconds(waitTime);
      FaceTheHero();
      Jump();
    }
  }



	void Jump(){
    if(transform.localScale.x > 0f){
      body.velocity = jumpSpeed;
    }else{
      body.velocity = new Vector2(-jumpSpeed.x, jumpSpeed.y);
    }
  }


  public void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Platform") {
      body.velocity = Vector2.zero;
    }
  }
}
