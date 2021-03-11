using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Sprite)), RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Color[] save;
    private bool toggled;
    public Color hoverColor;
    public event Action Clicked;

    public void OnPointerEnter(PointerEventData _) => Hover();

    public void OnPointerExit(PointerEventData _) => Unhover();

    public void OnPointerClick(PointerEventData _) => Clicked?.Invoke();

    private void Hover()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();
        save = new Color[sprites.Length];
        for (var i = 0; i < sprites.Length; i++)
        {
            save[i] = sprites[i].color;
            sprites[i].color = hoverColor;
        }
    }

    private void Unhover()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();
        for (var i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = save[i];
        }
    }
}
