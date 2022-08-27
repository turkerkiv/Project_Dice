using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartScr : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DoDestroy), 1f);
    }

    void DoDestroy()
    {
        Destroy(gameObject);
    }


}
