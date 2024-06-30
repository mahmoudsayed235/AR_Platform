using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidChoicesKeys : MonoBehaviour
{
    public int[] keys;

    public bool IsValidChoice(int choiceKey)
    {
        if(keys != null)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if(choiceKey == keys[i])
                {
                    return true;
                }
            }
        }

        return false;
    }
}
