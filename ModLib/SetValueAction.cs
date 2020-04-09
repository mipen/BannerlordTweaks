using ModLib.Interfaces;

namespace ModLib
{
    public class SetValueAction<T> : IAction where T : struct
    {
        public Ref Context { get; private set; }

        public object Value { get; private set; }

        private T original;

        public SetValueAction(Ref context, T value)
        {
            Context = context;
            Value = value;

            original = (T)Context.Value;
        }

        public void Do()
        {
            Context.Value = Value;
        }

        public void Undo()
        {
            Context.Value = original;
        }
    }
}
