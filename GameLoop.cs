using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace SeptFamilles {
    internal class GameLoop {

        public static List<Carte> MainCartes = new List<Carte>();
        public static List<Joueur> JoueursIA = new List<Joueur>();
        public static Joueur Player = new Joueur("0", true);
        public static int NumJoueursIA = 2;
        public static int Tour = 0;

        private static int joueurChoisi;
        private static Carte carteIdentifiee;
        private static int joueurGagnant = -1;

        static void Main(string[] args) {
            ScreenHandler screen = new ScreenHandler();
            InputHandler inputHandler = new InputHandler();

            string sayMessage = "";

            //On crée une pioche prête à jouer
            Pioche pioche = new Pioche();
            Pioche.Creer(42);
            Pioche.MelangerCartes();

            CreerJoueursIA();

            Player.PiocherCartes(10);

            foreach(Joueur joueur in JoueursIA) {
                joueur.PiocherCartes(6);
            }

            screen.Clear();

            Player.DevoilerCartes();

            foreach (Joueur joueur in JoueursIA) {
                //joueur.DevoilerCartes();
                joueur.DebugEvaluation();
            }

            while (pioche.CartesRestantes() > 0 || joueurGagnant < 0) {
                screen.Clear();
                Player.DevoilerCartes();

                Joueur.Say(sayMessage);

                if (Tour == 0) {

                    joueurChoisi = inputHandler.DemandeJoueur();

                    carteIdentifiee = inputHandler.DemandeCarte();

                    if (JoueursIA[(int)joueurChoisi].CarteEstDansLaMain(carteIdentifiee)) {
                        sayMessage = "Oui, j'ai la carte " + carteIdentifiee.NomCarte() + ", tiens !";
                        JoueursIA[(int)joueurChoisi].DonnerCarteAuJoueur(carteIdentifiee, Player);
                    } else {
                        sayMessage = "Non, je n'ai pas cette carte, pioche !";
                        Player.PiocherCartes(1);
                    }

                } else {
                    int joueur = Tour - 1;
                    
                    Random rand = new Random();
                    do {
                        joueurChoisi = rand.Next(0, NumJoueursIA + 1);
                    } while (joueurChoisi != joueur);

                    Carte carteChoisie = JoueursIA[joueur].EvaluerCartes();
                    bool cartesEchangees = false;

                    if (JoueursIA[(int)joueurChoisi].CarteEstDansLaMain(carteChoisie)) {
                        cartesEchangees = true;
                        JoueursIA[(int)joueurChoisi].DonnerCarteAuJoueur(carteChoisie, JoueursIA[joueur]);
                    } else {
                        cartesEchangees = false;
                        JoueursIA[joueur].PiocherCartes(1);
                    }

                    sayMessage = $"Le joueur {(cartesEchangees ? "a pris " : "demande ") + carteChoisie.NomCarte()} au joueur {joueurChoisi}.{(!cartesEchangees ? " Mais il pioche !" : "")}";
                    inputHandler.Pause();
                }

                if(Player.CompteFamilles().First().Value == 6) {
                    joueurGagnant = 0;
                    break;
                } else {
                    for (int i = 0; i < NumJoueursIA; i++) {
                        if (JoueursIA[i].CompteFamilles().First().Value == 6) {
                            joueurGagnant = i;
                            break;
                        }
                    }
                }

                joueurChoisi = -1;
                TourSuivant();
            }

            Console.Clear();
            
            if (joueurGagnant == 0) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Félicitations, t'as gagné cette manche !\n\n\n\n");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Oooooh ! Le joueur {joueurGagnant} a gagné cette manche !\n\n\n\n");
            }
                       
        }

        private static void TourSuivant() {
            Tour++;
            if (Tour > NumJoueursIA) {
                Tour = 0;
            }
        }

        private static void CreerJoueursIA() {
            for (int i = 0; i < NumJoueursIA; i++) {
                JoueursIA.Add(new Joueur(i.ToString()));
            }
        }
        
    }
}