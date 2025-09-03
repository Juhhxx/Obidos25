using UnityEngine;

public class TicketStack : MonoBehaviour
{
    [SerializeField] private GameObject _ticketType;

    public void GiveTicket()
    {
        GameObject ticket = Instantiate(_ticketType, PlayerInteraction.Instance.MousePosition, Quaternion.identity);

        Interactable intr = ticket.GetComponent<Draggabble>();

        PlayerInteraction.Instance.SetInteractable(intr);
    }
}
