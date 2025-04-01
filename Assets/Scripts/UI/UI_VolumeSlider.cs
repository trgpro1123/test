using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string parametr;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private int multiplier;
    public void SliderValue(float _value){
        audioMixer.SetFloat(parametr,Mathf.Log10(_value)*multiplier);
    }
    public void LoadSlider(float _value){
        if(slider.value>=0.001){
            slider.value=_value;
        }
    }
}
