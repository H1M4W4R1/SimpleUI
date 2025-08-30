using System.Collections.Generic;
using Systems.SimpleUserInterface.Context.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.SimpleUserInterface.Examples._04._Lists.Scripts.Lists.Context
{
    public sealed class FloatArrayContextProvider : ContextProviderBase<FloatArrayListContext>
    {
        [SerializeField] private List<float> _floats = new List<float>();
        private FloatArrayListContext _context;

        private void Awake()
        {
            _context = new FloatArrayListContext(_floats);
        }

        [ContextMenu("Add Float")]
        internal void AddFloat()
        {
            _floats.Add(Random.Range(0f, 100f));
        }

        [ContextMenu("Remove At Random Index")] internal void RemoveAtRandomIndex()
        {
            int index = Random.Range(0, _floats.Count);
            if (_floats.Count > index) _floats.RemoveAt(index);
        }

        public override FloatArrayListContext GetContext()
        {
            return _context;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) AddFloat();
            if(Input.GetKeyDown(KeyCode.Delete)) RemoveAtRandomIndex();
        }
    }
}