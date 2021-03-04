using TMPro;
using UnityEngine;

public class Aaaa : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void FixedUpdate()
    {
        textMeshPro.text += "a";
    }
}
