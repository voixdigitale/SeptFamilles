using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SeptFamilles {
    internal class Program {

        public static List<Carte> JeuCartes = new List<Carte>();
        public static List<Carte> MainCartes = new List<Carte>();
        public static List<Joueur> Joueurs = new List<Joueur>();
        public static int NumJoueurs = 1;
        public static int Tour = 1;

        static void Main(string[] args) {

            CreerJeuCartes();
            MelangerCartes();

            CreerJoueurs();

            Piocher(6);

            foreach(Joueur joueur in Joueurs) {
                joueur.Piocher(6);
            }

            Console.WriteLine("Voici tes cartes !\n");

            DevoilerCartes();

            //foreach (Joueur joueur in Joueurs) {
            //    Console.WriteLine("\nVoici mes cartes !\n");
            //    joueur.DevoilerCartes();
            //    joueur.DebugEvaluation();
            //}

            while(JeuCartes.Count > 0) {
                Console.WriteLine("\nQuelle carte tu veux vérifier dans ta main ?\n");
                string demandeCarte = Console.ReadLine();
                Carte? carteIdentifiee = IdentifierCarte(demandeCarte);

                if (carteIdentifiee != null) {
                    Console.WriteLine("\nTa carte est " + carteIdentifiee.NomCarte());
                    if (MainCartes.Contains(carteIdentifiee)) {
                        Console.WriteLine("\nTu l'as déjà dans la main !\n");
                    }
                } else {
                    Console.WriteLine("\nJe n'ai pas compris, quelle carte tu veux vérifier dans ta main ?\n");
                }
            }
        }

        private static void CreerJoueurs() {
            for (int i = 0; i < NumJoueurs; i++) {
                Joueurs.Add(new Joueur());
            }
        }

        public static void CreerJeuCartes() {
            foreach (Familles famille in Enum.GetValues(typeof(Familles))) {
                foreach (Membres membre in Enum.GetValues(typeof(Membres))) {
                    JeuCartes.Add(new Carte(membre, famille));
                }
            }
        }

        public static void MelangerCartes() {
            Random random = new Random();
            for (int i = 0; i < 6; i++) {
                JeuCartes = JeuCartes.OrderBy(item => random.Next()).ToList();
            }
        }

        public static void Piocher(int numCartes) {
            for (int i = 0; i < numCartes; i++) {
                MainCartes.Add(JeuCartes.First());
                JeuCartes.Remove(JeuCartes.First());
            }
        }

        public static void DevoilerCartes() {
            foreach (Carte carte in MainCartes) {
                if (carte != null) {
                    carte.MontreNom();
                }
            }
        }

        private static Carte? IdentifierCarte(string stringValue) {
            string[] elementsString = stringValue.Split(' ');

            string possibleMembre = Char.ToUpperInvariant(elementsString[0][0]) + elementsString[0].Substring(1);
            string possibleFamille = Char.ToUpperInvariant(elementsString[1][0]) + elementsString[1].Substring(1);

            bool familleExiste = Enum.TryParse(possibleMembre, out Membres membre);
            bool membreExiste = Enum.TryParse(possibleFamille, out Familles famille);

            if (membreExiste && familleExiste) {
                return new Carte(membre, famille);
            } else {
                return null;
            }
        }
    }

    public enum Membres {
        Fils,
        Fille,
        Pere,
        Mere,
        Grandpere,
        Grandmere
    }

    public enum Familles {
        Zombie,
        Ninja,
        Pirate,
        Slime,
        LoupGarou,
        RatMutante,
        Avocat
    }
}