using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public StoryController story;
    public List<GameObject> entities;
    private bool doneFirstStart;

    private void Start()
    {
        if (!doneFirstStart)
        {
            foreach (var c in GetComponentsInChildren<Clickable>())
            {
                c.Clicked += () => story.Choose("click." + c.gameObject.name);
            }
            doneFirstStart = true;
        }

        foreach (var entity in entities)
        {
            entity.SetActive(false);
        }
    }

    public bool Activate(string name)
    {
        if (entities.SingleOrDefault(go => go.name == name) is GameObject go)
        {
            go.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Deactivate(string name)
    {
        if (entities.SingleOrDefault(go => go.name == name) is GameObject go)
        {
            go.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisableInteraction()
    {
        foreach (var c in GetComponentsInChildren<Clickable>())
        {
            c.enabled = false;
        }
    }

    public void EnableInteraction()
    {
        foreach (var c in GetComponentsInChildren<Clickable>())
        {
            c.enabled = true;
        }
    }
}
