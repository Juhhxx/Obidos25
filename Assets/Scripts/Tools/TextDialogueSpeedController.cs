using UnityEngine;
using NaughtyAttributes;
using Yarn.Unity;

public class TextDialogueSpeedController : MonoBehaviour
{
    [SerializeField][InputAxis] private string _speedButton;
    
    private LineView _lineView;

    private void Start()
    {
        _lineView = GetComponent<LineView>();
        
    }

    private void Check()
    {
        if (Input.GetButtonDown(_speedButton)) _lineView.OnContinueClicked();
    }

    private void Update()
    {
        Check();
    }
}
