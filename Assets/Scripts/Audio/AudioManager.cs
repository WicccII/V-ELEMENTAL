using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------Audio Clip----------")]
    public AudioClip background;
    public AudioClip death;

    [Header("Weapon Sound")]
    public AudioClip spearAttack;
    public AudioClip spearMaxAttack;
    public AudioClip bladeAttack;
    public AudioClip bladeMaxAttack;
    public AudioClip crossAttack;
    public AudioClip blazingAttack;
    public AudioClip waterCanonAttack;
    public AudioClip blewBlastCountdown;


    [Header("Player Sound")]
    public AudioClip playerAttacked;
    public AudioClip getItem;
    public AudioClip getSkill;
    public AudioClip levelUp;

    [Header("Enemy Sound")]
    public AudioClip batVoice;
    public AudioClip lancerVoice;
    public AudioClip skeletonVoice;
    public AudioClip orcVoice;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PauseBackgroundMusic()
    {
        musicSource.Pause();
    }
    public void PlayBackgroundMusic()
    {
        musicSource.UnPause();
    }
}
