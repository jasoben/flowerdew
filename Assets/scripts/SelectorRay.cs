using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectorRay : ScriptableObject
{

    public Ray selectorRay;
    public RaycastHit[] hits;

}
