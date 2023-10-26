using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "Skin_", menuName = "Create/Skin")]
public class SphereScriptableObject : ScriptableObject
{
    public List<SkinItem> SkinItems;
}

[Serializable]
public class SkinItem
{
    [SerializeField] private string _nameSkin;
    public string NameSkin => _nameSkin;

    [SerializeField] private Material _material;
    public Material Material => _material;

    [SerializeField] private int _cost;
    public int Cost => _cost;

    [SerializeField] private int _index;
    public int Index => _index;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    public bool Bought;
}