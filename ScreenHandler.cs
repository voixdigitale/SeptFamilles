using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {
    internal class ScreenHandler {

        public ScreenPosition positionCartes = new ScreenPosition(0, 0);
        public ScreenPosition positionInput = new ScreenPosition(0, 14);
        public ScreenPosition positionPlayerChat = new ScreenPosition(0, 12);
        public ConsoleColor originalFG = Console.ForegroundColor;

        public void Clear() {
            Console.Clear();
        }

        public void ClearZone(int x, int y, int width, int height) {
            for (; height > 0;) {
                Console.SetCursorPosition(x, y + --height);
                Console.Write(new string(' ', width));
            }
            ZeroCursor();
        }

        public void MoveCursor(ScreenPosition positionCartes) {
            Console.SetCursorPosition(positionCartes.X, positionCartes.Y);
        }

        public void ZeroCursor() {
            Console.SetCursorPosition(0, 0);
        }

        public void InputCursor() {
            Console.SetCursorPosition(positionInput.X, positionInput.Y);
        }

        public void ReadInputCursor() {
            Console.SetCursorPosition(positionInput.X, positionInput.Y + 1);
        }

        private void BlankLine() {
            for (int i = 0; i < 10; i++) {
                Console.Write(" ");
            }
        }

        public async Task LineTypeWriter(int col, int row, string userString, ConsoleColor color, int delay, int startDelay = 0) {
            await Task.Delay(startDelay);
            for (int i = 0; i < userString.Length; i++) {
                (int cursorLeft, int cursorTop) = Console.GetCursorPosition();
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = color;
                Console.Write(userString[i]);
                col++;
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.ForegroundColor = originalFG;
                await Task.Delay(delay);
            }
        }

        public void DessinerCartes(List<Carte> mainCartes) {
            Console.Clear();
            Console.WriteLine("Voici les cartes !\n");

            foreach (Carte carte in mainCartes) {
                if (carte != null) {
                    carte.MontreNom();
                }
            }
        }
    }

    public struct ScreenPosition {
        public int X { get; private set; }
        public int Y { get; private set; }

        public ScreenPosition(int x, int y) {
            X = x; Y = y;
        }
    }
}
