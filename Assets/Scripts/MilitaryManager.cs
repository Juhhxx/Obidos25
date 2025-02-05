using System.Collections.Generic;
using Obidos25;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryManager : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    private Queue<Military> _militaryOrder;
    [SerializeField] private Military _mole;
    [SerializeField] private CardManager _card;
    [SerializeField] private Image _militaryImage;

    private void Start()
    {
        SetMilitaryOrder();
        StartInterrogation();
    }

    private void SetMilitaryOrder()
    {
        int moleChooser = Random.Range(0,_militaryList.Count);

        SetMole(_militaryList[moleChooser]);

        _militaryList.Shuffle();

        _militaryOrder = new Queue<Military>(_militaryList);
    }
    private void SetMole(Military mole)
    {
        _mole = mole;
    }

    private void StartInterrogation()
    {
        Military military = _militaryOrder.Dequeue();
        _card.SetUpCard(military);
        SetMilitary(military);
    }
    private void SetMilitary(Military military)
    {
        if (military == _mole)
        {
            _militaryImage.sprite = military.GetMoleSprite();
        }
        else
            _militaryImage.sprite = military.Sprite[0];
    }

}
