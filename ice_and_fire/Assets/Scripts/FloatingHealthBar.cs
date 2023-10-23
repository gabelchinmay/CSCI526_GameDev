using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateHealthBar(float curr, float maxi)
    {
        slider.value = curr / maxi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
