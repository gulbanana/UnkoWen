using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

[RequireComponent(typeof(Sprite)), RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;

    private Color[] save;
    private SpriteRenderer[] sprites;
    private SpriteShapeRenderer[] spriteShapes;
    //private TMPro.TextMeshPro[] texts;

    public event Action Clicked;

    public void OnPointerEnter(PointerEventData _) => Hover();

    public void OnPointerExit(PointerEventData _) => Unhover();

    public void OnPointerClick(PointerEventData _)
    {
        Clicked?.Invoke();
        Unhover();
    }

    private void Start()
    {
        // these all have .color, but it's three different properties
        sprites = GetComponentsInChildren<SpriteRenderer>();
        spriteShapes = GetComponentsInChildren<SpriteShapeRenderer>();
        //texts = GetComponentsInChildren<TMPro.TextMeshPro>();
        save = new Color[sprites.Length + spriteShapes.Length];
    }

    private void Hover()
    {
        for (var i = 0; i < sprites.Length; i++)
        {
            save[i] = sprites[i].color;
            sprites[i].color = hoverColor;
        }

        for (var i = 0; i < spriteShapes.Length; i++)
        {
            save[i + sprites.Length] = spriteShapes[i].color;
            spriteShapes[i].color = hoverColor;
        }

        //for (var i = 0; i < texts.Length; i++)
        //{
        //    save[i + sprites.Length + spriteShapes.Length] = texts[i].color;
        //    texts[i].color = hoverColor;
        //}
    }

    private void Unhover()
    {
        for (var i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = save[i];
        }

        for (var i = 0; i < spriteShapes.Length; i++)
        {
            spriteShapes[i].color = save[i + sprites.Length];
        }

        //for (var i = 0; i < texts.Length; i++)
        //{
        //    texts[i].color = save[i + sprites.Length + spriteShapes.Length];
        //}
    }
}
