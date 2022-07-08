using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Sound Manager Variables")]
    [SerializeField] private string musicVolumeParameter;
    [SerializeField] private string soundVolumeParameter;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private float multiplier;
    [SerializeField] private AudioClip[] sfxClips;

    [Header("Sound Manager Refereces")]
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(musicVolumeParameter, musicSlider.value);
        soundSlider.value = PlayerPrefs.GetFloat(soundVolumeParameter, soundSlider.value);
    }

    public void MusicSliderValueChanged(float value)
    {
        musicMixer.SetFloat(musicVolumeParameter, Mathf.Log10(value) * multiplier);
    }

    public void SoundsSliderValueChanged(float value)
    {
        soundMixer.SetFloat(soundVolumeParameter, Mathf.Log10(value) * multiplier);
    }

    public void PlaySoundEffect(int effectNum)
    {
        sfxSource.clip = sfxClips[effectNum];
        sfxSource.Play();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(musicVolumeParameter, musicSlider.value);
        PlayerPrefs.SetFloat(soundVolumeParameter, soundSlider.value);
    }
}
