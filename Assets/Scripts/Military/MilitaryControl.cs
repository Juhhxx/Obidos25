using UnityEngine;

public class MilitaryControl : MonoBehaviour
{
    [SerializeField] private MilitaryManager _militaryManager;
    [SerializeField] private MilitaryEyeManager _eyeManager;

    public void CallStartInterrogation() => _militaryManager.StartInterrogation();
    public void CallHasWalkedIn() => _militaryManager.HasWalkedIn();

    public void ChangeEyeColor(Color color) => _eyeManager.ChangeEyeColor(color);

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
