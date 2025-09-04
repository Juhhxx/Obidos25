using UnityEngine;

public class Ticket : MonoBehaviour
{
    [SerializeField] private TicketTypes _ticketType;
    public TicketTypes TicketType => _ticketType;
}
