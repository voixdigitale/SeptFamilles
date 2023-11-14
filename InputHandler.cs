using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {
    internal class InputHandler {

        private ScreenHandler screen = new ScreenHandler();

        public int DemandeJoueur() {
            int? joueurChoisi = -1;
            while (joueurChoisi == null || joueurChoisi <= 0 || joueurChoisi > GameLoop.JoueursIA.Count()) {
                screen.ClearZone(screen.positionInput.X, screen.positionInput.Y, 60, 10);
                screen.InputCursor();
                Console.WriteLine("À quel joueur tu veux demander une carte ?");
                try {
                    joueurChoisi = Int32.Parse(Console.ReadLine());
                    if (joueurChoisi == null || joueurChoisi <= 0 || joueurChoisi > GameLoop.JoueursIA.Count()) {
                        Joueur.Say("Ce joueur n'existe pas.");
                    }
                } catch (Exception e) {
                    Joueur.Say("Je n'ai pas compris.");
                }
            }

            return (int)joueurChoisi - 1;
        }

        public Carte DemandeCarte() {
            Carte carteIdentifiee = null;
            while (carteIdentifiee == null) {
                screen.ClearZone(screen.positionInput.X, screen.positionInput.Y, 60, 10);
                screen.InputCursor();
                Console.WriteLine($"Quelle carte tu veux vérifier dans la main de ce joueur ?");

                try {
                    string demandeCarte = Console.ReadLine();
                    carteIdentifiee = Carte.IdentifierCarte(demandeCarte);
                } catch (Exception e) {
                    Joueur.Say("Je n'ai pas compris");
                }
            }

            return carteIdentifiee;
        }

        public void Pause() {
            screen.ClearZone(screen.positionInput.X, screen.positionInput.Y, 60, 10);
            screen.InputCursor();
            Console.WriteLine("Appuye sur une touche pour sauter le tour de ce joueur");
            Console.ReadKey();
        }
    }
}
