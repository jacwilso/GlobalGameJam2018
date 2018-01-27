using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Skin Set")]
public class SkinObject : ScriptableObject {

    public Material Skin
    {
        get
        {
            if (skins.Length == 0)
            {
                Debug.LogError("YOU HAVE NO SKINS YOU NINKUMPOOP!");
                return null;
            }
            if (index >= skins.Length)
            {
                index = 0;
                for (int i = 0; i < skins.Length - 1; i++)
                {
                    for (int j = i + 1; j < skins.Length; j++)
                    {
                        Material tmp = skins[i];
                        skins[i] = skins[j];
                        skins[j] = skins[i];
                    }
                }
            }
            return skins[index++];
        }
    }

    [SerializeField] Material[] skins;
    private static int index = 0;
}
