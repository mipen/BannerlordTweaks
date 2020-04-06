using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace ModLib.ViewModels
{
    public class ModOptionsViewModel : ViewModel
    {
        private string _titleLabel;

        [DataSourceProperty]
        public string TitleLabel
        {
            get=> _titleLabel;
            set
            {
                _titleLabel = value;
                OnPropertyChanged();
            }
        }

        public ModOptionsViewModel()
        {
            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            TitleLabel = "Mod Options";
        }
    }
}
