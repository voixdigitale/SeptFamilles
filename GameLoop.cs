using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeptFamilles {
    internal class GameLoop {

        public static List<Carte> MainCartes = new List<Carte>();
        public static List<Joueur> JoueursIA = new List<Joueur>();
        public static Joueur Player = new Joueur("0", true);
        public static int NumJoueursIA = 1;
        public static int Tour = 1;

        private static int joueurChoisi;
        private static Carte carteIdentifiee;

        static void Main(string[] args) {
            ScreenHandler screen = new ScreenHandler();
            InputHandler inputHandler = new InputHandler();

            //On crée une pioche prête à jouer
            Pioche pioche = new Pioche();
            Pioche.Creer(42);
            Pioche.MelangerCartes();

            CreerJoueursIA();

            Player.PiocherCartes(10);

            foreach(Joueur joueur in JoueursIA) {
                joueur.PiocherCartes(6);
            }

            Player.DevoilerCartes();

            //foreach (Joueur joueur in JoueursIA) {
            //    joueur.DevoilerCartes();
            //    joueur.DebugEvaluation();
            //}

            while (pioche.CartesRestantes() > 0) {
                joueurChoisi = inputHandler.DemandeJoueur();

                carteIdentifiee = inputHandler.DemandeCarte();

                if (JoueursIA[(int)joueurChoisi].CarteEstDansLaMain(carteIdentifiee)) {
                    Joueur.Say("Oui, j'ai la carte " + carteIdentifiee.NomCarte() + ", tiens !");
                    JoueursIA[(int)joueurChoisi].DonnerCarteAuJoueur(carteIdentifiee, Player);
                } else {
                    Joueur.Say("Non, je n'ai pas cette carte, pioche !");
                    Player.PiocherCartes(1);
                }

                Player.DevoilerCartes();
                joueurChoisi = -1;
            }
        }

        private static void CreerJoueursIA() {
            for (int i = 0; i < NumJoueursIA; i++) {
                JoueursIA.Add(new Joueur(i.ToString()));
            }
        }
        
    }
}