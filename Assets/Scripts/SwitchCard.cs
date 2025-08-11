using UnityEngine;

public class SwitchCard : MonoBehaviour
{
    public enum Side { Office, Desk }
    [SerializeField] private Side _side;

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        CardItem card = other.gameObject.GetComponent<CardItem>();

        if (card != null)
        {
            bool item = _side == Side.Office ? false : true;

            card.ToggleCardItemSprite(item);
        }
    }
}
