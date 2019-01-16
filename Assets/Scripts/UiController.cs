using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

  public Slider hpSlider;


  public static UiController instance;

  void Awake() {
    instance = this;
	}


  private void Start() {
    UpdateUI();
  }

  public void UpdateUI(){
    hpSlider.maxValue = HeroController.instance.maxHp;
    hpSlider.value = HeroController.instance.hp;
  }
}
