using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Approacheth.Configs
{
    [CreateAssetMenu(fileName = "Asteroid", menuName = "ScriptableObjects/Configs/Asteroid")]
    public class GeneralAsteroidConfig : ScriptableObject
    {
        [Header("Possible Resources")]
        public List<MaterialData> PossibleResources = new List<MaterialData>();

        [Header("Min mass")]
        public float minMass = 50;
        [Header("Max mass")]
        public float maxMass = 100;

        public List<MaterialData> GenerateAsteroidResources()
        {
            List<MaterialData> generatedResources = new List<MaterialData>();
            generatedResources = generatedResources.Concat(PossibleResources).ToList();
            return generatedResources;
        }
    }
}