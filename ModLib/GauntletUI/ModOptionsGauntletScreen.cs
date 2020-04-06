using ModLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;

namespace ModLib.GauntletUI
{
    internal class ModOptionsGauntletScreen : ScreenBase
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            var gLayer = new GauntletLayer(1);
            gLayer.LoadMovie("ModOptionsScreen", new ModOptionsViewModel());
            gLayer.InputRestrictions.SetInputRestrictions();
        }
    }
}
