using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClipWithType> audioClipsWithTypes = new List<AudioClipWithType>();
    [SerializeField] private AudioSource             skillsAudioSource;
    [SerializeField] private AudioSource             menuAudioSource;
    [SerializeField] private AudioSource             musicAudioSource;

    private Dictionary<SoundType, AudioClip> _audio_clips = new Dictionary<SoundType, AudioClip>();

    public void playSound(SoundType sound_type)
    {
        if (_audio_clips.TryGetValue(sound_type, out AudioClip clip))
        {
        }
        else
        {
            clip = audioClipsWithTypes.FirstOrDefault(x => x.soundType == sound_type)?.audioClip;

            if (!clip)
                return;

            _audio_clips[sound_type] = clip;
        }

        if (sound_type is > SoundType.SKILL_BEFORE_FIRST and < SoundType.SKILL_AFTER_LAST)
            skillsAudioSource.PlayOneShot(clip);
        else if (sound_type == SoundType.MUSIC)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
        else if (sound_type == SoundType.CLICK)
        {
            menuAudioSource.PlayOneShot(clip);
        }
    }

    public void playSoundTest(string name) => playSound(Enum.Parse<SoundType>(name));
}

public enum SoundType
{
    NONE,

    SKILL_BEFORE_FIRST,
    BASIC_ATTACK,
    STRONG_ATTACK,
    EVASION,
    BLOCK,


    SKILL_AFTER_LAST,

    CLICK,

    MUSIC
}

[Serializable]
public class AudioClipWithType
{
    public SoundType soundType;
    public AudioClip audioClip;
} 
