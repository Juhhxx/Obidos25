using System.Collections.Generic;
using Obidos25;
using Obidos25.Assets.Scripts;
using UnityEngine;

public class MilitaryManager : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;
    [SerializeField] private Military _mole;

    private void Start()
    {
        SetMilitaryOrder();
    }
    private void SetMilitaryOrder()
    {
        int moleChooser = Random.Range(0,_militaryList.Count - 1);

        SetMole(_militaryList[moleChooser]);

        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);
    }
    private void SetMole(Military mole)
    {
        _mole = mole;
        mole.IsMole = true;
    }
}
