using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

    public Slider volSlider;

    void Start()
    {
        volSlider = GetComponent<Slider>();
        volSlider.value = AudioListener.volume;
    }

    void Update()
    {
        //Set the master volume and save the setting
        AudioListener.volume = volSlider.value;
        GameSaves.saves.volume = AudioListener.volume;
    }
}
