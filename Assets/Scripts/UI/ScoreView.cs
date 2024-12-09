using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ParticleSystem _particleSystem;
        
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _audioClips;

        private int _currentClip;
        
        public void Init(int maxPoints)
        {
            _text.text = "0/" + maxPoints;
            ListTools.Shuffle(_audioClips);
        }
        
        public void SetNewPointsInText(int points, int maxPoints)
        {
            _text.text = points + "/" + maxPoints;
            PlayAudioClip();
            PlayTextAnim();
        }

        private void PlayAudioClip()
        {
            _audioSource.PlayOneShot(_audioClips[_currentClip]);
            _currentClip++;
            if(_currentClip == _audioClips.Count) _currentClip = 0;
        }

        private void PlayTextAnim()
        {
            _particleSystem.Play();
            _text.gameObject.transform.DOScale(1.2f, 0.35f).OnComplete(() =>
            {
                _text.gameObject.transform.DOScale(1f, 0.2f);
            });
        }
    }
}
