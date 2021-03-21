using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class AudioController : MonoBehaviour, IAudioHandler
{
    private AudioSource source;
    public AudioClip chooseLink;
    public AudioClip chooseEntity;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private async UniTask Queue(AudioClip clip)
    {
        while (source.isPlaying)
        {
            await UniTask.NextFrame();
        }

        source.clip = clip;
        source.Play();
    }

    public void ChooseLink()
    {
        Queue(chooseLink).Forget();
    }

    public void ChooseEntity()
    {
        Queue(chooseEntity).Forget();
    }
}
