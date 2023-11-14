using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeptFamilles {
    internal class Joueur {
        public bool IsPlayer {  get; private set; }
        public string Name {  get; private set; }

        private List<Carte> _mainCartes;
        private Dictionary<Familles, int> _familles;

        private static ScreenHandler screen = new ScreenHandler();

        public Joueur(string playername = "", bool isPlayer = false) {
            _mainCartes = new List<Carte>();
            _familles = new Dictionary<Familles, int>();
            Name = playername;
            IsPlayer = isPlayer;
        }

        public void PiocherCartes(int numCartes) {
            _mainCartes.AddRange(Pioche.Piocher(numCartes));

            EvaluerCartes();
        }

        public Carte EvaluerCartes() {
            _familles = CompteFamilles();

            //On va les ranger pour lisibilité
            var listeFamilles = _familles.OrderByDescending(x => x.Value).ToList();

            _familles.Clear();
            foreach (KeyValuePair<Familles, int> famille in listeFamilles) {
                _familles.Add(famille.Key, famille.Value);
            }

            //On choisit la famille avec le plus de cartes
            Familles premierChoixFamille = _familles.First().Key;


            List<Membres> _membres = new List<Membres>();

            //On parcourt la main pour voir quelles membres on a de cette famille
            foreach (Carte carte in _mainCartes) {
                if (carte.Famille == premierChoixFamille) {
                    _membres.Add(carte.Membre);
                }
            }

            List<Membres> membresManquants = new List<Membres>();
            //Attention au monstre: On convertit Membres en une liste et on fait la difference avec les membres déjà en main
            membresManquants = Enum.GetValues(typeof(Membres)).Cast<Membres>().ToList().Except(_membres).ToList();

            Random rand = new Random();

            return new Carte(membresManquants[rand.Next(0, membresManquants.Count)], premierChoixFamille);
        }

        public Dictionary<Familles, int> CompteFamilles() {
            _familles.Clear();
            //Pour chaque famille
            foreach (Familles famille in Enum.GetValues(typeof(Familles))) {
                int numMembres = 0;

                //On compte combien de cartes on a de cette famille
                foreach (Carte carte in _mainCartes) {
                    if (carte.Famille == famille) {
                        numMembres++;
                    }
                }

                //Et on mémorise
                _familles.Add(famille, numMembres);
            }

            return _familles;
        }

        public void DevoilerCartes() {
            screen.DessinerCartes(_mainCartes);
        }

        public bool CarteEstDansLaMain(Carte carte) {
            return _mainCartes.Contains(carte);
        }

        public void RecevoirCarte(Carte carte) {
            _mainCartes.Add(carte);
        }

        public void DonnerCarteAuJoueur(Carte carte, Joueur joueur) {
            joueur.RecevoirCarte(carte);
            _mainCartes.Remove(carte);
        }

        public void DebugEvaluation() {
            Console.WriteLine("\nIci ma réflexion !\n");
            var dictString = string.Join(Environment.NewLine, _familles);
            Console.WriteLine(dictString);
            var premierChoix = _familles.First();
            Console.WriteLine($"\nJe vais demander une carte de la famille: {premierChoix.Key.ToString()}");
        }

        public static void Say(string dialog) {
            screen.ClearZone(screen.positionPlayerChat.X, screen.positionPlayerChat.Y, 60, 1);
            screen.LineTypeWriter(screen.positionPlayerChat.X, screen.positionPlayerChat.Y, dialog, ConsoleColor.Blue, 1, 3);
        }
    }
}
