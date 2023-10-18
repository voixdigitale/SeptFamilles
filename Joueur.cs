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

        public void EvaluerCartes() {
            //Pour chaque famille
            foreach(Familles famille in Enum.GetValues(typeof(Familles))) {
                int numMembres = 0;

                //On compte combien de cartes on a de cette famille
                foreach(Carte carte in _mainCartes) {
                    if(carte.Famille == famille) {
                        numMembres++;
                    }
                }

                //Et on mémorise
                _familles.Add(famille, numMembres);
            }

            //On va les ranger pour lisibilité
            var listeFamilles = _familles.OrderByDescending(x => x.Value).ToList();

            _familles.Clear();
            foreach (KeyValuePair<Familles, int> famille in listeFamilles) {
                _familles.Add(famille.Key, famille.Value);
            }

        }

        public void DevoilerCartes() {
            Console.WriteLine("Voici les cartes !\n");

            foreach (Carte carte in _mainCartes) {
                if (carte != null) {
                    carte.MontreNom();
                }
            }
        }

        public bool CarteEstDansLaMain(Carte carte) {

            return _mainCartes.Contains(carte);
        }

        public void DebugEvaluation() {
            Console.WriteLine("\nIci ma réflexion !\n");
            var dictString = string.Join(Environment.NewLine, _familles);
            Console.WriteLine(dictString);
            var premierChoix = _familles.First();
            Console.WriteLine($"\nJe vais demander une carte de la famille: {premierChoix.Key.ToString()}");
        }
    }
}
