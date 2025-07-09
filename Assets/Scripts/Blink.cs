using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] [Range (0,1)] private float _blinkProbability;
    private Animator _anim;
    private YieldInstruction _wfs;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _wfs = new WaitForSeconds(1);

        StartCoroutine(BlinkController());
    }
    private IEnumerator BlinkController()
    {
        while(true)
        {
            float num = Random.Range(0f,1f);

            if (num <= _blinkProbability)
                _anim.SetTrigger("Blink");

            yield return _wfs;
        }
    }
}