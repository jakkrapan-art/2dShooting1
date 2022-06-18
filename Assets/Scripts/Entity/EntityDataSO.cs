using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Entity/EntityData")]
public class EntityDataSO : ScriptableObject
{
    [SerializeField] private int _healthPoint;

    public int GetHealthPoint => _healthPoint;
}
