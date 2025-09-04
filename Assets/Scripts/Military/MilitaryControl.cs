using UnityEngine;

public class MilitaryControl : MonoBehaviour
{
    [SerializeField] private MilitaryManager _militaryManager;

    public void CallStartInterrogation() => _militaryManager.StartInterrogation();
    public void CallHasWalkedIn() => _militaryManager.HasWalkedIn();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("aaaaaaaa");
        Ticket ticket = other.GetComponent<Ticket>();

        if (ticket != null)
        {
            _militaryManager.GiveTicket(ticket.TicketType);

            PlayerInteraction.Instance.ResetInteractable();
            Destroy(ticket.gameObject);
            Debug.Log("GIVE TICKET");
        }
    }
}
