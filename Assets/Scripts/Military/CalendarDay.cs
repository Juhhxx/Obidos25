using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarDay", menuName = "Scriptable Objects/CalendarDay")]
public class CalendarDay : ScriptableObject
{
    [field: SerializeField] public WeekDay WeekDay { get; private set; }
    [field: SerializeField] public Sprite CalendarSprite { get; private set; }
}
