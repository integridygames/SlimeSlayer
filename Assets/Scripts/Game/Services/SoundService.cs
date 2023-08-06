using System.Collections.Generic;
using Game.DataBase;
using UnityEngine;

namespace Game.Services
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundService : MonoBehaviour
    {
        [SerializeField] private SoundsDataBase _soundsDataBase;
        [SerializeField] private AudioSource _audioSource;

        private readonly Dictionary<string, AudioSource> _musicSources = new();
        private readonly Dictionary<string, AudioSource> _loopedSources = new();

        private PlayerSettings _playerSettings;

        public void Init(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
        }

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(string clipName, float volume = 1.0f)
        {
            if (_playerSettings.SoundsEnabled.Value == false) return;

            var sound = _soundsDataBase.GetSound(clipName);
            _audioSource.PlayOneShot(sound, volume);
        }

        public void PlayMusic(string clipName, float volume = 1.0f, float delay = 0f)
        {
            PlayMusicInternal(clipName, volume, delay, _musicSources, _playerSettings.MusicEnabled.Value);
        }
        
        public void PlayLoopedSound(string clipName, float volume = 1.0f, float delay = 0f)
        {
            PlayMusicInternal(clipName, volume, delay, _loopedSources, _playerSettings.SoundsEnabled.Value);
        }

        private void PlayMusicInternal(string clipName, float volume, float delay, Dictionary<string, AudioSource> sources, bool enableValue)
        {
            if (string.IsNullOrEmpty(clipName))
            {
                return;
            }

            if (sources.TryGetValue(clipName, out var audioSource) == false)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                sources[clipName] = audioSource;
            }

            var sound = _soundsDataBase.GetSound(clipName);

            audioSource.clip = sound;
            audioSource.loop = true;
            audioSource.volume = volume;

            if (enableValue == false) return;

            audioSource.PlayDelayed(delay);
        }

        public void StopMusic(string clipName)
        {
            if (_musicSources.TryGetValue(clipName, out var audioSource))
            {
                audioSource.Stop();
            }
        }

        public void PlayAllMusic()
        {
            foreach (var key in _musicSources.Keys)
            {
                _musicSources[key].Play();
            }
        }
        
        public void StopAllMusic()
        {
            foreach (var key in _musicSources.Keys)
            {
                _musicSources[key].Stop();
            }
        }
        
        public void StopLoopedSound(string clipName)
        {
            if (_loopedSources.TryGetValue(clipName, out var audioSource))
            {
                audioSource.Stop();
            }
        }

        public void PlayAllLoopedSounds()
        {
            foreach (var key in _loopedSources.Keys)
            {
                _loopedSources[key].Play();
            }
        }
        
        public void StopAllLoopedSounds()
        {
            foreach (var key in _loopedSources.Keys)
            {
                _loopedSources[key].Stop();
            }
        }
    }
}