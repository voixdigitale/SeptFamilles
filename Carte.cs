using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeptFamilles {
    internal class Carte : IEquatable<Carte> {
        public Membres Membre { get; private set; }
        public Familles Famille { get; private set; }

        public Carte(Membres membre, Familles famille) {
            Membre = membre;
            Famille = famille;
        }

        public string NomCarte() {
            return Membre.ToString() + " " + Famille.ToString();
        }

        public void MontreNom() {
            ConsoleColor originalFG = Console.ForegroundColor;
            Console.ForegroundColor = CouleurFamille(Famille);
            Console.WriteLine(NomCarte());
            Console.ForegroundColor = originalFG;
        }

        private ConsoleColor CouleurFamille(Familles famille) => famille switch {
            Familles.Zombie => ConsoleColor.DarkGreen,
            Familles.Ninja => ConsoleColor.DarkGray,
            Familles.Pirate => ConsoleColor.Magenta,
            Familles.Slime => ConsoleColor.Green,
            Familles.LoupGarou => ConsoleColor.DarkRed,
            Familles.RatMutante => ConsoleColor.DarkYellow,
            Familles.Avocat => ConsoleColor.Red,
        };

        public bool Equals(Carte? other) {
            if (other.Membre == Membre && other.Famille == Famille) {
                return true;
            }

            return false;
        }
    }
}
