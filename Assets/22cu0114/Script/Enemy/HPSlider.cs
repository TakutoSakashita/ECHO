using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    // 変数宣言
    // -------------------------------------------------
        public Slider HpSlider;

        Character character;
    // ------------------------------------------------- 
    // ------------------------------------------------- 

    private void Start()
    {
        //Sliderを満タンにする。
        HpSlider.value = 1;

        character = this.gameObject.GetComponent<Character>();
    }

    private void Update()
    {
        _hpDown();
    }

    private void _hpDown()
    {
        //最大HPにおける現在のHPをSliderに反映。
        HpSlider.value = (float)character.status.GetHp() / (float)character.status.MaxHp;
    }

}
