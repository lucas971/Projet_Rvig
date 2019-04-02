using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class infoUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI temps;
    public TextMeshProUGUI température;
    private int temperature;
    public Transform target;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        temperature = (int) Random.Range(-200, -50);
    }

    // Update is called once per frame
    void Update()
    {
        temps.text = ((int) Time.time).ToString();
        var r = Random.Range(0, 100);
        if (r < 1)
        {
            temperature += 1;
            température.text = temperature.ToString() + "°";
        }
        if (r >98)
        {
            temperature -= 1;
            température.text = temperature.ToString() + "°";
        }
    }
}
