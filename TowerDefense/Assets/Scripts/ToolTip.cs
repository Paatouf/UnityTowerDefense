using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public bool showToolTip = true;

    public GameObject toolTip;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(showToolTip)
            {
                showToolTip = false;
            }
            else
            {
                showToolTip = true;
            }
        }

        if(showToolTip)
        {
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
    }
}
