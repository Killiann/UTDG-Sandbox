using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB_Editor.Handlers
{
    class UndoRedoHandler
    {
        private List<int[]> savedEdits;
        private readonly int maxUndo = 20;
        private int currentIndex = -1;

        public UndoRedoHandler(int[] map)
        {
            savedEdits = new List<int[]>();
        }

        public void AddIteration(int[] newIteration)
        {
            int[] newIterClone = (int[])newIteration.Clone();
            //if (currentIndex > -1 && currentIndex < savedEdits.Count - 1)
            //    savedEdits.RemoveRange(currentIndex + 1, savedEdits.Count - 1 - currentIndex);
            savedEdits.Add(newIterClone);
            if (savedEdits.Count > maxUndo)
                savedEdits.RemoveAt(0);
            //currentIndex++;
        }

        public void Undo(ref int[] tileMap)
        {
            if (savedEdits.Count > 0)
            {
                tileMap = savedEdits[savedEdits.Count - 1];
                savedEdits.RemoveAt(savedEdits.Count - 1);
            }
        }

        public void Redo(ref int[] map)
        {
            //if (currentIndex < savedEdits.Count - 1)
            //    currentIndex++;
            //map = savedEdits[currentIndex];
        }
    }
}
