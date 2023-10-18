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
            Familles.Loupgarou => ConsoleColor.DarkRed,
            Familles.Rat => ConsoleColor.DarkYellow,
            Familles.Avocat => ConsoleColor.Red,
        };

        public bool Equals(Carte? other) {
            if (other.Membre == Membre && other.Famille == Famille) {
                return true;
            }

            return false;
        }

        public static Carte? IdentifierCarte(string stringValue) {
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
        Loupgarou,
        Rat,
        Avocat
    }
}

//lines = ['┌─────────┐'] + ['│░░░░░░░░░│'] * 7 + ['└─────────┘']