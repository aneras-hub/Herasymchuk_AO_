using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class UndoRedoAction
    {
        public Action UndoAction { get; set; }
        public Action RedoAction { get; set; }
        public string Description { get; set; }

        public UndoRedoAction(
            Action undoAction,
            Action redoAction,
            string description)
        {
            UndoAction = undoAction;
            RedoAction = redoAction;
            Description = description;
        }
    }

    public class UndoRedoManager
    {
        private Stack<UndoRedoAction> undoStack =
            new Stack<UndoRedoAction>();

        private Stack<UndoRedoAction> redoStack =
            new Stack<UndoRedoAction>();

        public void ExecuteAction(
            Action redoAction,
            Action undoAction,
            string description)
        {
            redoAction();

            undoStack.Push(
                new UndoRedoAction(
                    undoAction,
                    redoAction,
                    description
                )
            );

            redoStack.Clear();

            Console.WriteLine($"[ACTION] {description}");
        }

        public void Undo()
        {
            if (undoStack.Count == 0)
            {
                Console.WriteLine("Немає дій для Undo.");
                return;
            }

            UndoRedoAction action = undoStack.Pop();

            action.UndoAction();

            redoStack.Push(action);

            Console.WriteLine($"[UNDO] {action.Description}");
        }

        public void Redo()
        {
            if (redoStack.Count == 0)
            {
                Console.WriteLine("Немає дій для Redo.");
                return;
            }

            UndoRedoAction action = redoStack.Pop();

            action.RedoAction();

            undoStack.Push(action);

            Console.WriteLine($"[REDO] {action.Description}");
        }

        public void ShowHistory()
        {
            Console.WriteLine($"Undo stack: {undoStack.Count}");
            Console.WriteLine($"Redo stack: {redoStack.Count}");
        }
    }
}