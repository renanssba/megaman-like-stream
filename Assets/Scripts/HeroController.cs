using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroController : MonoBehaviour {

  public Rigidbody2D body;
  public float xSpeed = 2f;
  public float jumpSpeed = 10f;

  void Update() {
    if(Input.GetKey(KeyCode.RightArrow)){
      FaceRight();
      body.velocity = new Vector2(xSpeed, body.velocity.y);
    }else if(Input.GetKey(KeyCode.LeftArrow)) {
      FaceLeft();
      body.velocity = new Vector2(-xSpeed, body.velocity.y);
    }else{
      body.velocity = new Vector2(0f, body.velocity.y);
    }

    if(Input.GetKeyDown(KeyCode.Space) && CanIJump()) {
      Jump();
    }

    //if(body.velocity.y > 0f){

    //}
	}

  void Jump(){
    body.velocity = new Vector2(body.velocity.x, jumpSpeed);
  }

  void LeavePlatform() {
    transform.SetParent(null);
  }



  public bool CanIJump(){
    if(transform.parent != null && transform.parent.tag == "Platform"){
      return true;
    }
    return false;
  }


  void FaceRight(){
    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) , transform.localScale.y, transform.localScale.z);
  }
  void FaceLeft() {
    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
  }


  public void OnCollisionEnter2D(Collision2D collision) {
    Debug.Log("Colidiu");

    if (collision.gameObject.tag == "Platform") {
      transform.SetParent(collision.transform);
    }
  }

  public void OnCollisionExit2D(Collision2D collision) {
    Debug.Log("Saiu da colisao");
    if (collision.transform == transform.parent) {
      LeavePlatform();
    }
  }
}
