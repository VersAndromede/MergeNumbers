using Lean.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData")]
public class BossData : ScriptableObject
{
    [field: Header("Translation Name")]
    [field: SerializeField, LeanTranslationName] public string TranslationName { get; private set; }

    [field: SerializeField] public Boss Prefab { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public uint Award { get; private set; }
}