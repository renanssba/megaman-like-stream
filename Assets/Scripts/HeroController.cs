using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HeroController : MonoBehaviour {

  public Rigidbody2D body;
  public float xSpeed = 2f;
  public float jumpSpeed = 10f;

  public float timeToMediumShot = 1f;
  public float timeToChargedShot = 2f;

  public GameObject basicShot;
  public GameObject mediumShot;
  public GameObject chargedShot;

  public float chargedTime = 0f;

  public int maxHp;
  public int hp;

  public ParticleSystem[] chargingParticleSystems;


  public static HeroController instance;

  private void Awake() {
    instance = this;
    hp = maxHp;
  }

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

    if (Input.GetKeyDown(KeyCode.D)) {
      InitialShoot();
    }

    if(Input.GetKeyUp(KeyCode.D)) {
      ChargedShoot();
    }

    if (Input.GetKey(KeyCode.D)){
      chargedTime += Time.deltaTime;
      UpdateParticles();
    } else{
      chargedTime = 0f;
    }



    //if(body.velocity.y > 0f){

    //}
  }

  void Jump(){
    body.velocity = new Vector2(body.velocity.x, jumpSpeed);
  }

  void UpdateParticles(){
    if (chargedTime > timeToChargedShot) {
      DeactivateParticles();
      //chargingParticleSystems[2].gameObject.SetActive(true);
      chargingParticleSystems[2].Play();
    }

    else if (chargedTime > timeToMediumShot) {
      DeactivateParticles();
      //chargingParticleSystems[1].gameObject.SetActive(true);
      chargingParticleSystems[1].Play();
    }

    else if (chargedTime > 0f){
      DeactivateParticles();
      //chargingParticleSystems[0].gameObject.SetActive(true);
      chargingParticleSystems[0].Play();
    }
  }

  void DeactivateParticles(){
    for (int i = 0; i < 3; i++) {
      chargingParticleSystems[i].Stop();
    }
  }

  void LeavePlatform() {
    transform.SetParent(null);
  }

  void InitialShoot(){
    GameObject newShot;
    newShot = Instantiate(basicShot, transform);
    newShot.transform.SetParent(null);
  }

  void ChargedShoot() {
    GameObject newShot;

    DeactivateParticles();

    if (chargedTime >= timeToChargedShot) {
      newShot = Instantiate(chargedShot, transform);
      newShot.transform.SetParent(null);
    } else if (chargedTime >= timeToMediumShot) {
      newShot = Instantiate(mediumShot, transform);
      newShot.transform.SetParent(null);
    }
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



  void TakeDamage(int dmg) {
    hp -= dmg;
    UiController.instance.UpdateUI();
    CheckDeath();
  }

  void CheckDeath() {
    if (hp <= 0) {
      Die();
    }
  }

  void Die() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }


  public void OnCollisionEnter2D(Collision2D collision) {
    Debug.Log("Colidiu");
    RayBit enemyHit = collision.gameObject.GetComponent<RayBit>();

    if (collision.gameObject.tag == "Platform") {
      transform.SetParent(collision.transform);
    } else if(enemyHit != null) {
      TakeDamage(enemyHit.tackleDamage);
    }
  }

  public void OnCollisionExit2D(Collision2D collision) {
    Debug.Log("Saiu da colisao");
    if (collision.transform == transform.parent) {
      LeavePlatform();
    }
  }
}
