using UnityEngine;

public class SwitchCard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        CardItem card = other.gameObject.GetComponent<CardItem>();

        if (card != null)
            card.ToggleCardItemSprite();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        CardItem card = other.gameObject.GetComponent<CardItem>();

        if (card != null)
            card.ToggleCardItemSprite();
    }
}
