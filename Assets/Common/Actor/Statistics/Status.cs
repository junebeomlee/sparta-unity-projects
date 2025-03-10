using System.Collections;
using UnityEngine;

namespace Actor
{
    public class Status: MonoBehaviour
    {


        public void UseItem()
        {
            StartCoroutine(ModifyResource());
        }

        private IEnumerator ModifyResource()
        {
            yield return null;
        }
    }
}