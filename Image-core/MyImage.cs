using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    public class MyImage
    {
        string myfile;
        string typeImage;
        long tailleFichier;
        long tailleOffset;
        long largeur;
        long hauteur;
        long nbBits;

        public MyImage(string myfile)
        {
            this.myfile = myfile;
            try
            {
                byte[] fichier = File.ReadAllBytes(myfile);
                  

            //type image
            if (fichier[0] == 66 && fichier[1] == 77)
            {
                this.typeImage = "BM";
            }
            else
            {
                this.typeImage = "autre que BM";
            }

            //taille fichier
            this.tailleFichier = Convert_binaire_to_décimal(Convert_Tableau_To_Int(Convertir_LittleEndian_To_BigEndian(Convert_Decimal_to_binaire(2, 5, fichier))));

            //taille Offset
            this.tailleOffset = Convert_binaire_to_décimal(Convert_Tableau_To_Int(Convertir_LittleEndian_To_BigEndian(Convert_Decimal_to_binaire(14, 17, fichier))));

            //largeur
            this.largeur = Convert_binaire_to_décimal(Convert_Tableau_To_Int(Convertir_LittleEndian_To_BigEndian(Convert_Decimal_to_binaire(18, 21, fichier))));

            // hauteur
            this.hauteur = Convert_binaire_to_décimal(Convert_Tableau_To_Int(Convertir_LittleEndian_To_BigEndian(Convert_Decimal_to_binaire(22, 25, fichier))));

            // nombre Bits
            this.nbBits = Convert_binaire_to_décimal(Convert_Tableau_To_Int(Convertir_LittleEndian_To_BigEndian(Convert_Decimal_to_binaire(28, 29, fichier))));
            }
            catch (FileNotFoundException e) ///test des exception pour pas que le fichier ne plante
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Fonction qui convertit un chiffre décimal en binaire
        /// </summary>
        /// <param name="début"> Premiere case du chiffre à convertir</param>
        /// <param name="fin"> Derniere case du chiffre à convertire</param>
        /// <param name="myfile"> informations de l'image dans un tableau de byte</param>
        /// <returns> binaire un tableau de chiffres en binaire </returns>
        public int[] Convert_Decimal_to_binaire(int début, int fin, byte[] myfile) //on rentre le nombre de cases qu'on veut convertir, exemple largeur pixel debut = 18 et fin = 21 (4octets)
        {
            int case1 = 0;
            int nombrecases = fin - début + 1; //nombre d'octets
            int[] binaire = new int[nombrecases];
            for (int i = début; i <= fin; i++)
            {
                //Console.WriteLine(myfile[i]);
                binaire[case1] = Convert.ToInt32(Convert.ToString(myfile[i], 2)); //Convert.ToString(x, 2) convertit x en base binaire
                case1++;
            }
            return binaire;
        }

        /// <summary>
        /// Convertit un chiffre qui est en little endian en Big endian
        /// </summary>
        /// <param name="binaire"> tableau de chiffres en binaire et en little endian </param>
        /// <returns> binaire un tableau de chiffres en binaire et en big endian </returns>
        public int[] Convertir_LittleEndian_To_BigEndian(int[] binaire) //on retourne le tableau
        {
            int début = 0;
            int fin = binaire.Length - 1;
            while (début < fin)
            {
                int stock = binaire[début]; //stock variable de stockage
                binaire[début] = binaire[fin];
                binaire[fin] = stock;
                début++;
                fin--;
            }
            return binaire;
        }


        /// <summary>
        /// Convertit un chiffre binaire en en chiffre décimal
        /// </summary>
        /// <param name="binaire"> nombre en binaire </param>
        /// <returns> entier nombre en décimal</returns>
        public long Convert_binaire_to_décimal(decimal binaire)
        {
            long entier = 0;
            decimal division = 1;
            int longueur = Convert.ToString(binaire).Length;
            for (double puissance = 0; puissance <= longueur; puissance++)
            {
                decimal binaire1 = Math.Round(binaire / division);
                decimal entier1 = Math.Round(binaire1 % 10 * Convert.ToInt32(Math.Pow(2, puissance)));
                long entier2 = Convert.ToInt64(entier1);
                entier = entier + entier2;
                division = division * 10;
            }

            return entier;
        }

        /// <summary>
        /// Met tous les chiffres contenu dans un tableau les uns après les autres pour en faire un chiffre
        /// Exemple: Si un tableau contient les chiffres 12; 5 et 89, la foction renvoie 12589
        /// </summary> 
        /// <param name="fichier"> un tableau de nombres </param>
        /// <returns> nombre un nombre </returns>
        public decimal Convert_Tableau_To_Int(int[] fichier)
        {
            string nombrestring = "";
            for (int j = 0; j < fichier.Length; j++)
            {
                int taillenombre = Convert.ToString(fichier[j]).Length;
                if (taillenombre < 8)
                {
                    for (int k = 0; k < 8 - taillenombre; k++)
                    {
                        nombrestring = nombrestring + 0;
                    }
                }
                nombrestring = nombrestring + fichier[j];
            }
            decimal nombre = Convert.ToDecimal(nombrestring);
            return nombre;
        }

        /// <summary>
        /// Decrit l'image
        /// </summary>
        /// <returns> string qui dévrit l'image</returns>
        public string toString()
        {
            return "L'image est de type " + typeImage + " et la taille du fichier est de " + tailleFichier + " octets , offset :" + tailleOffset + "\n" +
                "L'image a comme dimension -> largeur : " + largeur + " / hauteur : " + hauteur + " et il y a " + nbBits + " bits par couleur.";


        }

        public string TypeImage
        {
            get { return typeImage; }
        }

        public long Largeur
        {
            get { return largeur; }
            set { largeur = value; }
        }

        public long Hauteur
        {
            get { return hauteur; }
            set { hauteur = value; }
        }

    }
}
