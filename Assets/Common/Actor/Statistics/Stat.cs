using UnityEngine;

namespace Actor
{
    public class Stat
    {
        public string Name;
        public float Value { get; private set; }
        private float _maxValue;

        public Stat(float initValue)
        {
            SetMaxValue(initValue);
        }

        private void Modify(float amount)
        {
             Value = Mathf.Clamp(Value + amount, 0, _maxValue);
        }

        private void SetMaxValue(float value)
        {
            _maxValue = value;
            Value = value;
        }
    }
}
