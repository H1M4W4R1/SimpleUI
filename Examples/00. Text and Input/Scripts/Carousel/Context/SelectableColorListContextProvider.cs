using System.Collections.Generic;
using Systems.SimpleUserInterface.Context.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Carousel.Context
{
    public sealed class SelectableColorListContextProvider : ContextProviderBase<SelectableColorListContext>
    {
        [field: SerializeField] private List<Color> Colors { get; set; }
        private SelectableColorListContext _context;
        
        private void Awake()
        {
            _context = new SelectableColorListContext(Colors);
        }

        public override SelectableColorListContext GetContext() => _context;
    }
}