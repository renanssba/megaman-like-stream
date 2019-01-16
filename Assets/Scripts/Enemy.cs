using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  public int maxHp;
  public int hp;
  public int tackleDamage;

  public Rigidbody2D body;


	public void Start() {
    hp = maxHp;
	}

  public void TakeDamage(int dmg){
    hp -= dmg;
    CheckDeath();
  }

  public void CheckDeath(){
    if(hp <= 0){
      Die();
    }
  }

  public void Die(){
    Destroy(gameObject);
  }


  public void FaceTheHero(){
    HeroController hero = HeroController.instance;

    if(hero.gameObject.transform.position.x > transform.position.x) {
      FaceRight();
    }else{
      FaceLeft();
    }
  }

  void FaceRight() {
    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
  }
  void FaceLeft() {
    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
  }



  public void OnTriggerEnter2D(Collider2D collision) {
    Projectile proj = collision.gameObject.GetComponent<Projectile>();

    if (proj != null) {
      TakeDamage(proj.damage);
    }
  }
}
