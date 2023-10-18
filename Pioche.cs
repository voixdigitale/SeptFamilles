using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {

    internal class Pioche {
        private static List<Carte> _jeuCartes = new List<Carte>();
        
        public static void Creer(int numCartes) {
            foreach (Familles famille in Enum.GetValues(typeof(Familles))) {
                foreach (Membres membre in Enum.GetValues(typeof(Membres))) {
                    _jeuCartes.Add(new Carte(membre, famille));
                }
            }
        }

        public int CartesRestantes() {
            return _jeuCartes.Count();
        }

        public static void MelangerCartes() {
            Random random = new Random();
            for (int i = 0; i < 100; i++) {
                _jeuCartes = _jeuCartes.OrderBy(item => random.Next()).ToList();
            }
        }

        public static List<Carte> Piocher(int numCartes) {
            List<Carte> cartesPiochees = new List<Carte>();
            for (int i = 0; i < numCartes; i++) {
                cartesPiochees.Add(_jeuCartes.First());
                _jeuCartes.Remove(_jeuCartes.First());
            }

            return cartesPiochees;
        }
    }
}
