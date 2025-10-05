using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Password", menuName = "Scriptable Objects/Password")]
public class Password : ScriptableObject
{
    [field: SerializeField] public string PasswordQuestion;
    [field: SerializeField] public string MondayAnswer;
    [field: SerializeField] public string TuesdayAnswer;
    [field: SerializeField] public string WednesdayAnswer;
    [field: SerializeField] public string ThursdayAnswer;
    [field: SerializeField] public string FridayAnswer;
    [field: SerializeField] public string SaturdayAnswer;
    [field: SerializeField] public string SundayAnswer;

    public string GetPasswordAnswer(WeekDay weekDay)
    {
        switch (weekDay)
        {
            case WeekDay.Monday:
                return MondayAnswer;

            case WeekDay.Tuesday:
                return TuesdayAnswer;

            case WeekDay.Wednesday:
                return WednesdayAnswer;

            case WeekDay.Thursday:
                return ThursdayAnswer;

            case WeekDay.Friday:
                return FridayAnswer;

            case WeekDay.Saturday:
                return SaturdayAnswer;

            case WeekDay.Sunday:
                return SundayAnswer;

            default:
                return "Invalid Week Day Selected";
        }
    }
    public string GetPasswordAnswerWrong(WeekDay weekDay)
    {
        List<WeekDay> days = ((WeekDay[])Enum.GetValues(typeof(WeekDay))).ToList();

        days.Remove(weekDay);
        int rnd = UnityEngine.Random.Range(0, days.Count);

        return GetPasswordAnswer(days[rnd]);        
    }
}
