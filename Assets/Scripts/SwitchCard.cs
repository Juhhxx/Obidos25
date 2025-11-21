using UnityEngine;

public class SwitchCard : MonoBehaviour
{
    public enum Side { Office, Desk }
    [SerializeField] private Side _side;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        TAG_Cursor tmp = other.gameObject.GetComponent<TAG_Cursor>();

        if (tmp != null)
        {
            CardItem card = PlayerInteraction.Instance.CurrentInteractable?
                                        .gameObject.GetComponentInParent<CardItem>();

            if (card != null) Switch(card);
        }
    }
    public void Switch(CardItem card)
    {
        if (!PlayerInteraction.Instance.IsInteracting) return;

        bool item = _side == Side.Office;
        
        card.ToggleCardItemSprite(item);
    }
}
