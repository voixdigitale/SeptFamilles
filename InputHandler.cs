using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {
    internal class InputHandler {

        public static int DemandeJoueur() {
            int? joueurChoisi = -1;
            while (joueurChoisi == null || joueurChoisi <= 0 || joueurChoisi > GameLoop.JoueursIA.Count()) {
                Console.WriteLine("\nÀ quel joueur tu veux demander une carte ?\n");
                try {
                    joueurChoisi = Int32.Parse(Console.ReadLine());
                } catch (Exception e) {
                    Console.WriteLine("\nJe n'ai pas compris.\n");
                }
            }

            return (int)joueurChoisi - 1;
        }

        public static Carte DemandeCarte() {
            Carte carteIdentifiee = null;
            while (carteIdentifiee == null) {
                Console.WriteLine($"\nJoueur : Quelle carte tu veux vérifier dans ma main ?\n");
                string demandeCarte = Console.ReadLine();
                try {
                    carteIdentifiee = Carte.IdentifierCarte(demandeCarte);
                } catch (Exception e) {
                    Console.WriteLine("\nJe n'ai pas compris.\n");
                }

                if (carteIdentifiee == null)
                    Console.WriteLine("\nJe n'ai pas compris.\n");
            }

            return carteIdentifiee;
        }
    }
}
