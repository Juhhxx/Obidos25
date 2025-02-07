using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _badgeList;
    private Image _image;
    private AspectRatioFitter _fitter;

    private void Start()
    {
        _image = GetComponent<Image>();
        _fitter = _image.GetComponent<AspectRatioFitter>();
    }
    public void SetBadge(string badge, bool isMole)
    {
        for (int i = 0; i < _badgeList.Length; i++)
        {

            if (_badgeList[i].name == badge)
                if (isMole)
                {
                    if (i == 0)
                        ImageBadge(_badgeList[_badgeList.Length - 1]);
                    else
                        ImageBadge(_badgeList[i - 1]);
                }
                else
                    ImageBadge(_badgeList[i]);
        }
    }

    private void ImageBadge(Sprite b)
    {
        _image.sprite = b;
        float aspectRatio = b.rect.width / b.rect.height;
        _fitter.aspectRatio = aspectRatio;
    }
}
