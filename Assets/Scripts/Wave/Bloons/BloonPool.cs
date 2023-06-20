using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;

namespace ServiceLocator.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private List<BloonConfiguration> bloonConfigurations;

        public BloonPool(List<BloonConfiguration> bloonConfigurations) => this.bloonConfigurations = bloonConfigurations;

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonConfiguration configToUse = bloonConfigurations.Find(config => config.BloonType == bloonType);
            BloonController bloon = GetItem();
            bloon.Init(configToUse.BloonPrefab, configToUse.BloonScriptableObject);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController();
    }
}