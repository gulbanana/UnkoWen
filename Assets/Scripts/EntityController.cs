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
        foreach (var entity in entities)
        {
            if (!doneFirstStart && entity.GetComponent<Clickable>() is Clickable c)
            {
                c.Clicked += () => story.Choose("click." + c.gameObject.name);
            }

            entity.SetActive(false);
        }

        doneFirstStart = true;
    }

    public bool Enable(string name)
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

    public bool Disable(string name)
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
}
