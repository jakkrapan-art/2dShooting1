using UnityEngine;

public interface IStatusEffectAppliable
{
    public void ApplyStatusEffect(StatusEffect statusEffect);
    public void RemoveStatusEffect(StatusEffect statusEffect);
    public void EnableStatusEffectImmune();
    public void DisableStatusEffectImmune();
    public GameObject GetGameObject { get; }
}
