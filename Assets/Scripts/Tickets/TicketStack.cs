using UnityEngine;

public class TicketStack : MonoBehaviour
{
    [SerializeField] private GameObject _ticketType;

    private bool _hasBeenUsed;

    public void Reset() => _hasBeenUsed = false;

    public void GiveTicket()
    {
        if (_hasBeenUsed) return;

        _hasBeenUsed = true;

        GameObject ticket = Instantiate(_ticketType);

        ticket.transform.position = PlayerInteraction.Instance.MousePosition;

        Interactable intr = ticket.GetComponent<Draggabble>();

        PlayerInteraction.Instance.SetInteractable(intr);
    }
}
