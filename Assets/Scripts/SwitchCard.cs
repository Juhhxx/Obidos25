using UnityEngine;

public class SwitchCard : MonoBehaviour
{
    public enum Side { Office, Desk }
    [SerializeField] private Side _side;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        CursorScript tmp = other.gameObject.GetComponent<CursorScript>();

        if (tmp != null)
        {
            CardItem card = PlayerInteraction.Instance.CurrentInteractable?
                                        .gameObject.GetComponent<CardItem>();

            if (card != null) Switch(card);
        }
    }
    public void Switch(CardItem card)
    {
        bool item = _side == Side.Office ? true : false;

        card.ToggleCardItemSprite(item);
    }
}
