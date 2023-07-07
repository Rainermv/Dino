using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Resources.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/LevelConfig", order = 1)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        public Level[] Levels;
    }
}
