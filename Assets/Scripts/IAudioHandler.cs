using UnityEngine.EventSystems;

interface IAudioHandler : IEventSystemHandler
{
    void ChooseLink();
    void ChooseEntity();
}
