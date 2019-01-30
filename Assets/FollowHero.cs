using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHero : MonoBehaviour {

  public GameObject hero;
  public float followSpeed;
  public float followTolerance;

  private void Update() {
    float dist = hero.transform.position.x - transform.position.x;
    if(dist > followTolerance) {
      transform.Translate(new Vector3( Mathf.Min(followSpeed, dist-followTolerance), 0f));
    }

    else if(dist < -followTolerance) {
      transform.Translate(new Vector3( Mathf.Max(-followSpeed, dist+followTolerance), 0f));
    }


  }
}
