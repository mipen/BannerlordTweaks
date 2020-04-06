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
        private GauntletLayer gauntletLayer;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            gauntletLayer = new GauntletLayer(1);
            gauntletLayer.LoadMovie("ModOptionsScreen", new ModOptionsViewModel());
            gauntletLayer.InputRestrictions.SetInputRestrictions();
            gauntletLayer.IsFocusLayer = true;
            AddLayer(gauntletLayer);
            ScreenManager.TrySetFocus(gauntletLayer);
        }

        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
            if (gauntletLayer.Input.IsHotKeyReleased("Exit"))
            {
                ScreenManager.TrySetFocus(gauntletLayer);
                ScreenManager.PopScreen();
            }
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            RemoveLayer(gauntletLayer);
            gauntletLayer = null;
        }
    }
}
