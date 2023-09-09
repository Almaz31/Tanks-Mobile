
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    public void SetPlayerColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
