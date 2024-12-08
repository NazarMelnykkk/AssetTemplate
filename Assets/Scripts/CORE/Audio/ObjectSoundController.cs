using UnityEngine;

public class ObjectSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundType _type; 

    public void PlaySound(SoundConfig soundConfig)
    {
        GlobalReferencesContainer.Instance.AudioHandler.PlaySound(soundConfig, _audioSource);
    }

    private void OnEnable()
    {
        GlobalReferencesContainer.Instance.AudioHandler.VolumeValueChanged += ChangeSoundVolume;
    }

    private void OnDisable()
    {
        GlobalReferencesContainer.Instance.AudioHandler.VolumeValueChanged -= ChangeSoundVolume;
    }

    private void ChangeSoundVolume()
    {
        _audioSource.volume = GlobalReferencesContainer.Instance.AudioHandler.GetVolumeByType(_type);
    }
}
