using System.Collections;
using UnityEngine;

namespace Actor
{
    public class Status: PlayerComponent
    {
        public Status(PlayerClass playerClass) : base(playerClass) { }

        public void UseItem()
        {
            StartCoroutine(ModifyResource());
        }

        private IEnumerator ModifyResource(float duration = 1f, int repeat = 1)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}