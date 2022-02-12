using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Holder : MonoBehaviour
{
    [SerializeField]
    private int iron;
    public int Iron { get { return iron; } set { iron = value; } }
    [SerializeField]
    private int oil;
    public int Oil { get { return oil; } set { oil = value; } }


    private void Update()
    {
        MinMax();
    }

    //set min max iron and oil 
    private void MinMax()
    {
        if(iron > 500)
        {
            iron = 500;
        }

        if(oil > 500)
        {
            oil = 500;
        }
    }
}
