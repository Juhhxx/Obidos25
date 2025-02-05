using UnityEngine;

public class SwitchCard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        CardManager card = other.gameObject.GetComponent<CardManager>();

        if (card != null)
            card.ToggleCardItemSprite();
    }
}
