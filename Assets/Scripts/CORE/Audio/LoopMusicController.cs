using UnityEngine;

public class LoopMusicController : MonoBehaviour
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        PlaySound(_soundConfig);
    }

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
        _audioSource.volume = GlobalReferencesContainer.Instance.AudioHandler.GetVolumeByType(_soundConfig.Sound.Type);
    }
}
