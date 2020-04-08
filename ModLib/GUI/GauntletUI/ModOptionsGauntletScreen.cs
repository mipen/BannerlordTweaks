using ModLib.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace ModLib.GUI.GauntletUI
{
    internal class ModOptionsGauntletScreen : ScreenBase
    {
        private GauntletLayer gauntletLayer;
        private GauntletMovie movie;
        private ModSettingsScreenVM vm;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            gauntletLayer = new GauntletLayer(1);
            gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
            gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
            gauntletLayer.IsFocusLayer = true;
            ScreenManager.TrySetFocus(gauntletLayer);
            AddLayer(gauntletLayer);
            vm = new ModSettingsScreenVM();
            movie = gauntletLayer.LoadMovie("ModOptionsScreen", vm);
        }

        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
            if (gauntletLayer.Input.IsHotKeyReleased("Exit") || gauntletLayer.Input.IsGameKeyReleased(34))
            {
                vm.ExecuteCancel();
            }
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            RemoveLayer(gauntletLayer);
            gauntletLayer.ReleaseMovie(movie);
            gauntletLayer = null;
            movie = null;
            vm.ExecuteSelect(null);
            vm.AssignParent(true);
            vm = null;
        }
    }
}
