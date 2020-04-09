using ModLib.Interfaces;
using System.Collections.Generic;

namespace ModLib
{
    public class UndoRedoStack
    {
        public Stack<IAction> UndoStack { get; }
        public Stack<IAction> RedoStack { get; }
        public Stack<IInitial> InitialStack { get; }

        public bool CanUndo => UndoStack.Count > 0;
        public bool CanRedo => RedoStack.Count > 0;
        public bool HasInitials => InitialStack.Count > 0;

        public UndoRedoStack()
        {
            UndoStack = new Stack<IAction>();
            RedoStack = new Stack<IAction>();
            InitialStack = new Stack<IInitial>();
        }

        /// <summary>
        /// Adds the action to the UndoStack and calls the Do function.
        /// </summary>
        /// <param name="action"></param>
        public void Do(IAction action)
        {
            action.Do();
            UndoStack.Push(action);
            RedoStack.Clear();
        }

        /// <summary>
        /// Calls the Undo function for the top item in the UndoStack. If there is nothing in the stack, does nothing.
        /// </summary>
        public void Undo()
        {
            if (CanUndo)
            {
                IAction a = UndoStack.Pop();
                a.Undo();
                RedoStack.Push(a);
            }
        }

        /// <summary>
        /// Calls the Do function for the top item in the RedoStack. If there is nothing in the stack, does nothing.
        /// </summary>
        public void Redo()
        {
            if (CanRedo)
            {
                IAction a = RedoStack.Pop();
                a.Do();
                UndoStack.Push(a);
            }
        }

        /// <summary>
        /// Calls Undo method for all actions in the UndoStack and the InitialStack, from top to bottom.
        /// </summary>
        public void UndoAll()
        {
            if (CanUndo)
            {
                while (UndoStack.Count > 0)
                {
                    IAction a = UndoStack.Pop();
                    a.Undo();
                }
            }
            if (HasInitials)
            {
                while (InitialStack.Count > 0)
                {
                    IInitial i = InitialStack.Pop();
                    i.Reset();
                }
            }
        }

        /// <summary>
        /// Merges the UndoStack with given UndoRedoStack's UndoStack.
        /// </summary>
        /// <param name="urs">Foreign UndoRedoStack to add to top of UndoStack</param>
        public void MergeURStack(UndoRedoStack urs)
        {
            UndoStack.AppendToTop(urs.UndoStack);
        }

        /// <summary>
        /// Adds the action to the InitialStack. This stack is undone when UndoAll is called.
        /// </summary>
        /// <param name="action"></param>
        public void SetInitial(IInitial initial)
        {
            InitialStack.Push(initial);
        }

        /// <summary>
        /// Checks the initial setup actions for changes.
        /// </summary>
        /// <returns>Returns True if there are any changes made.</returns>
        public bool CheckInitials()
        {
            if (!HasInitials)
                return false;
            IInitial[] i = InitialStack.ToArray();
            foreach (var x in i)
            {
                if (x.Changed()) return true;
            }
            return false;
        }

        public void ClearStack()
        {
            UndoStack.Clear();
            RedoStack.Clear();
            InitialStack.Clear();
        }

        public bool ChangesMade()
        {
            if (UndoStack.Count > 0)
                return true;
            if (CheckInitials())
                return true;

            return false;
        }
    }
}
