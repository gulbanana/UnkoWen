using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public StoryController story;
    private List<PlotEntity> entities;
    private bool doneFirstStart;

    private void Awake()
    {
        entities = GetComponentsInChildren<PlotEntity>().ToList();
    }

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
            entity.gameObject.SetActive(false);
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

    private bool WithEntity(string name, Action<PlotEntity> f)
    {
        if (entities.SingleOrDefault(go => go.name == name) is PlotEntity entity)
        {
            f(entity);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Activate(string name) => WithEntity(name, e =>
    {
        e.gameObject.SetActive(true);
    });

    public bool Deactivate(string name) => WithEntity(name, e =>
    {
        e.gameObject.SetActive(true);
    });

    public bool Rename(string name, string label) => WithEntity(name, e =>
    {
        e.label.text = label;
    });
}
