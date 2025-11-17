using UnityEngine;

public class GhostHandler : MonoBehaviour
{
    public Renderer rend;
    public Color validColor = new Color(0, 1, 0, 0.5f);
    public Color invalidColor = new Color(1, 0, 0, 0.5f);

    public void SetValid(bool isValid)
    {
        rend.material.color = isValid ? validColor : invalidColor;
    }
}
