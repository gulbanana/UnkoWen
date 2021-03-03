using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Sprite)), RequireComponent(typeof(Collider2D))]
public class Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer sprite;
    private Color save;
    private bool toggled;

    public void Awake() => sprite = GetComponent<SpriteRenderer>();

    public void Start() => sprite.color = Color.blue;

    public void OnPointerEnter(PointerEventData _) => Hover();

    public void OnPointerExit(PointerEventData _) => Unhover();

    public void OnPointerClick(PointerEventData _) => Toggle();

    private void Hover()
    {
        save = sprite.color;
        GetComponent<SpriteRenderer>().color = Color.Lerp(sprite.color, Color.white, 0.5f);
    }

    private void Unhover()
    {
        sprite.color = save;
    }

    private void Toggle()
    {
        if (toggled)
        {
            sprite.color = Color.blue;
            toggled = false;
        }
        else
        {
            sprite.color = Color.green;
            toggled = true;
        }

        Hover();
    }
}
