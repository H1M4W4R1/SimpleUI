using System;
using System.Collections.Generic;
using Systems.SimpleUserInterface.Context.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.SimpleUserInterface.Examples.Objects.Lists.Context
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
        private void AddFloat()
        {
            _floats.Add(Random.Range(0f, 100f));
        }

        [ContextMenu("Remove At Random Index")] private void RemoveAtRandomIndex()
        {
            int index = Random.Range(0, _floats.Count);
            if (_floats.Count > index) _floats.RemoveAt(index);
        }

        public override FloatArrayListContext ProvideContext()
        {
            return _context;
        }
    }
}