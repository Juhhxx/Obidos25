using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PasswordCalendar", menuName = "Scriptable Objects/PasswordCalendar")]
public class PasswordCalendar : ScriptableObject
{

    [SerializeField] private List<CalendarDay> _calendarDays;
    [SerializeField] private List<Password> _passwords;

    public CalendarDay ChooseWeekDay()
    {
        int rnd = Random.Range(0, _calendarDays.Count);

        return _calendarDays[rnd];
    }

    public Password GetPassword()
    {
        int rnd = Random.Range(0, _passwords.Count);

        return _passwords[rnd];
    }
}
