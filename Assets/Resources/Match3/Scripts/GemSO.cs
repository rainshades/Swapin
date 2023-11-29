using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Water, Fire, Poison, Nature, Thunder, Physical, All }

[CreateAssetMenu()]
public class GemSO : ScriptableObject {

    public string gemName;
    public Sprite sprite;
    public Element Element;
}
