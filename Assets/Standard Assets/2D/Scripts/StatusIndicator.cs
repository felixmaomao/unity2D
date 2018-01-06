using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;


    private void Start()
    {
        if (healthBarRect==null)
        {
            Debug.LogError("Status Indicator: no health bar object refrenced");
        }
        if (healthText == null)
        {
            Debug.LogError("Status Indicator: no health text object refrenced");
        }
    }

    public void SetHealth(int _cur,int _max)
    {
        float _value =(float) _cur / _max;
        //相对父对象进行比例缩放
        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = _cur + "/" + _max + " HP";

    }
}
