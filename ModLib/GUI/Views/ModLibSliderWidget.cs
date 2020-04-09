using TaleWorlds.GauntletUI;

namespace ModLib.GUI.Views
{
    public class ModLibSliderWidget : SliderWidget
    {
        //private float oldFloatValue;
        //private int oldIntValue;
        private float _finalisedFloatValue;
        private int _finalisedIntValue;

        public float FinalisedFloatValue
        {
            get
            {
                return _finalisedFloatValue;
            }
            set
            {
                _finalisedFloatValue = value;
                OnPropertyChanged(value, "FinalisedFloatValue");
            }
        }
        public int FinalisedIntValue
        {
            get
            {
                return _finalisedIntValue;
            }
            set
            {
                _finalisedIntValue = value;
                OnPropertyChanged(value, "FinalisedIntValue");
            }
        }

        public ModLibSliderWidget(UIContext context) : base(context)
        {
        }

        protected override void OnValueFloatChanged(float value)
        {
            base.OnValueFloatChanged(value);
            FinalisedFloatValue = value;
        }

        protected override void OnValueIntChanged(int value)
        {
            base.OnValueIntChanged(value);
            FinalisedIntValue = value;
        }
    }
}
