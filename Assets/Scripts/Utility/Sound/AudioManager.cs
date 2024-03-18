using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


    public class AudioManager : MonoBehaviour {
        public Sound[] sounds;

        public static AudioManager AudioManagerInstance;
    
        private void Awake() {
        if (AudioManagerInstance != null && AudioManagerInstance != this) {
            Destroy(this);
        } else {
            AudioManagerInstance = this;
        }

        foreach (Sound s in sounds) {
             s.source = gameObject.AddComponent<AudioSource>();
             s.source.clip = s.clip;

             s.source.volume = s.volume;
             s.source.pitch = s.pitch;
             s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            }
        }

        public void PlayAudio(string name) {
            Sound s= Array.Find(sounds, sound => sound.name == name);
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found");
            }
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
        }

    public void PlayAudio(string name, float volume) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.volume = volume;
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void PlayAudio(string name, float volume, float pitch) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.pitch = pitch;
        s.source.volume = volume;
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void PlayMusic(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void PlayAudioAtTime(string name, float time) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.time = time;
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void PlayAudioAtTime(string name, float pitch, float time) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.time = time;
        s.source.pitch = pitch;
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void PlayAudioDelay(string name, float volume, float delaytime) {
        StartCoroutine(PlayAudioDelayCoroutine(name,volume, delaytime));
    }
    
    private IEnumerator PlayAudioDelayCoroutine(string name, float volume, float delaytime) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        yield return new WaitForSecondsRealtime(delaytime);
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.volume = volume;
        s.source.Play();
    }

    public void PlayAudioDelay(string name, float delaytime) {
        StartCoroutine(PlayAudioDelayCoroutine(name, delaytime));
    }

    private IEnumerator PlayAudioDelayCoroutine(string name,float delaytime) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        yield return new WaitForSecondsRealtime(delaytime);
        if (s.music) {
            s.source.volume = User.I.GetMusicVolume();
        } else {
            s.source.volume = User.I.GetSoundEffectVolume();
        }
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        if (s.source.isPlaying) {
            s.source.Stop();
        }
     }

    public void StopAllMusic() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying && s.music) {
                s.source.Stop();
            }
        }
    }

    public void StopAllSoundEffect() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying && !s.music) {
                s.source.Stop();
            }
        }
    }

    public void StopAll() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying) {
                s.source.Stop();
            }
        }
    }

    public void UpdateMusicVolume() {
        foreach (Sound s in sounds) {
            if(s.source.isPlaying && s.music) {
                s.source.volume = User.I.GetMusicVolume();
            }
        }
    }

    public void UpdateSoundEffect() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying && (!s.music)) {
                s.source.volume = User.I.GetSoundEffectVolume();
            }
        }
    }
}


