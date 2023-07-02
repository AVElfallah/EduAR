using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSubLevelEnded : MonoBehaviour
{
    public bool isSubLevelEnded = false;
    public void EndTheSubLevel()
    {
        isSubLevelEnded = true;
    }

}
