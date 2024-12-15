using Controllers;
using Objects;
using UnityEngine;

namespace UI
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private InteractObject _achievementObject;
        [SerializeField] private AudioClip _clip;
        
        private Animator _achievementAnimator;
        private AudioSource _audioSource;
        private AudioController _audioController;
        
        public void Init(AudioController audioController)
        {
            _achievementAnimator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _audioController = audioController;
            
            _achievementObject.OnAchievement += UnlockAchievement;
        }

        private void UnlockAchievement()
        {
            _audioController.PlayOneShot(_audioSource, _clip);
            _achievementAnimator.SetTrigger("unlock");
        }

        private void OnDisable()
        {
            _achievementObject.OnAchievement -= UnlockAchievement;
        }
    }
}
