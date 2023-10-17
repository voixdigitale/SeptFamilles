using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {
    internal class Joueur {
        private List<Carte> _mainCartes;
        private Dictionary<Familles, int> _familles;

        public Joueur() {
            _mainCartes = new List<Carte>();
            _familles = new Dictionary<Familles, int>();
        }

        public void Piocher(int numCartes) {
            for (int i = 0; i < numCartes; i++) {
                _mainCartes.Add(Program.JeuCartes.First());
                Program.JeuCartes.Remove(Program.JeuCartes.First());
            }

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
            foreach (Carte carte in _mainCartes) {
                if (carte != null) {
                    carte.MontreNom();
                }
            }
        }

        public void DebugEvaluation() {
            Console.WriteLine("\nIci ma réflexion !\n");
            var asString = string.Join(Environment.NewLine, _familles);
            Console.WriteLine(asString);
            var premierChoix = _familles.First();
            Console.WriteLine($"\nJe vais demander une carte de la famille: {premierChoix.Key.ToString()}");
        }
    }
}
