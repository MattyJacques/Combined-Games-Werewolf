using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    //Reference the volume slider
    public Slider volSlider;

    void Start()
    {
        //Set the volume slider
        volSlider = GetComponent<Slider>();
        //Set the value to the current value of the audio listener
        volSlider.value = AudioListener.volume;
    }

    void Update()
    {
        //Set the master volume and save the setting
        AudioListener.volume = volSlider.value;
        //Set the save volume to the current volume
        GameSaves.saves.volume = AudioListener.volume;
    }
}
