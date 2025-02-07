using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _badgeList;
    private Image _image;
    private RectTransform _rectTrans;

    private void Start()
    {
        _image = GetComponent<Image>();
        _rectTrans = GetComponent<RectTransform>();
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
        _image.SetNativeSize();
        Vector2 trans = _rectTrans.localScale;
        trans /= 2;
        _rectTrans.localScale = trans;
    }
}
