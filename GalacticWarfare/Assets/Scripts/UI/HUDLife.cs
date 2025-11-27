using UnityEngine;
using TMPro;

public class HUDLife : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateLife(int value)
    {
        text.text = "VIDA: " + value;
    }
}