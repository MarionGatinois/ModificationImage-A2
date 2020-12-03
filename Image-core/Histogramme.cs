using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    class Histogramme
    {
        string myfile;
        MyImage image;

        public Histogramme(string myfile, MyImage image)
        {
            this.image = image;
            this.myfile = myfile;
            try { 
            byte[] fichier = File.ReadAllBytes(myfile);
            Console.WriteLine("Quel histogramme voulez vous (général, bleu, rouge, vert) ?");
            while (true)

            {
                // AFFICHAGE DU MENU 

                Console.WriteLine("1- Général");
                Console.WriteLine("2- Bleu");
                Console.WriteLine("3- Vert");
                Console.WriteLine("4- Rouge");
                Console.WriteLine("sortir");

                string reponse = Console.ReadLine();
                if (reponse != "")
                {

                    if (reponse == "1")

                    {
                        byte[] histogramme = Histogramme_General(fichier);
                        File.WriteAllBytes("Sortie.bmp", histogramme);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "2")

                    {
                        int début = 0;
                        byte[] histogramme = Histogramme_Couleur(fichier, début);
                        File.WriteAllBytes("Sortie.bmp", histogramme);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "3")

                    {
                        int début = 1;
                        byte[] histogramme = Histogramme_Couleur(fichier, début);
                        File.WriteAllBytes("Sortie.bmp", histogramme);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "4")

                    {
                        int début = 2;
                        byte[] histogramme = Histogramme_Couleur(fichier, début);
                        File.WriteAllBytes("Sortie.bmp", histogramme);
                        Process.Start("Sortie.bmp");
                    }
                    if (reponse == "sortir")
                    {
                        Console.WriteLine("Merci et appuyer sur entrée pour arrêter");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("////////////////");
                    Console.WriteLine("erreur sur la réponse, recommencez");
                    Console.WriteLine("////////////////");
                }
            }
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
        /// Crée l'histogramme de la couleur chosie d'une image
        /// </summary>
        /// <param name="fichier">tableau de byte de l'image dont on veut crée l'histogramme</param>
        /// <param name="début">rentré automatiquement debut le menu, en fonction de la couleur choisie</param>
        /// <returns> un tableau de byte pour afficher l'histogramme</returns>
        public byte[] Histogramme_Couleur(byte[] fichier, int début)
        {
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            int nbrpx = fichiermat.GetLength(0) * fichiermat.GetLength(1);
            byte[,] newMatrice = new byte[500, 768];

            int somme = 0;
            int tour = 0;

            for (int couleur = 0; couleur < 255; couleur++)
            {
                //Compter le nombre de pixel pour chaque nuance de couleur
                for (int ligne = 0; ligne < fichiermat.GetLength(0); ligne++)
                {
                    for (int colonne = début; colonne < fichiermat.GetLength(1); colonne = colonne + 3) ///on avance en +3 pour avancer de pixel en pixel
                    {
                        if (fichiermat[ligne, colonne] == couleur)
                        {
                            somme = somme + 1; //on compte le nombre de pixels qui ont la meme consentration de la couleur
                        }
                    }
                }

                //On rempli l'histogramme
                for (int hauteur = 0; hauteur < somme / 2000; hauteur++)
                {
                    newMatrice[hauteur, couleur + tour * 2 + début] = 255;
                }
                tour++;
            }
            byte[] histogramme_couleur = Convert_Tab_To_Histogramme(newMatrice, newMatrice.GetLength(0), newMatrice.GetLength(1) / 3, fichier);
            return histogramme_couleur;
        }

        /// <summary>
        /// Cree l'histograme général d'une image, en fonction de la luminosité de chaque pixel
        /// </summary>
        /// <param name="fichier"> tableau de byte de l'image dont on veut crée l'histogramme</param>
        /// <returns>  un tableau de byte pour afficher l'histogramme </returns>
        public byte[] Histogramme_General(byte[] fichier)
        {
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            int nbrpx = fichiermat.GetLength(0) * fichiermat.GetLength(1);
            byte[,] newMatrice = new byte[500, 768];

            int somme = 0;
            int tour = 0;

            for (int couleur = 0; couleur < 255; couleur++)
            {
                //Compter le nombre de pixel pour chaque nuance de couleur
                for (int ligne = 0; ligne < fichiermat.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < fichiermat.GetLength(1); colonne = colonne + 3) ///on avance en +3 pour avancer de pixel en pixel
                    {
                        int moyenne = (fichiermat[ligne, colonne] + fichiermat[ligne, colonne + 1] + fichiermat[ligne, colonne + 2]) / 3; //on fait la moyenne du pixel, comme pour niveau gris
                        if (moyenne == couleur)
                        {
                            somme = somme + 1; //on compte le nombre de pixels qui ont la meme consentration de la couleur
                        }
                    }
                }

                //On rempli l'histogramme
                for (int hauteur = 0; hauteur < (somme / 2000); hauteur++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        newMatrice[hauteur, couleur + tour * 2 + i] = 255; //tous les sous-pixels sont a 255 pour faire un pixel blanc
                    }
                }
                tour++;
            }
            byte[] histogramme_couleur = Convert_Tab_To_Histogramme(newMatrice, newMatrice.GetLength(0), newMatrice.GetLength(1) / 3, fichier);
            return histogramme_couleur;
        }

        /// <summary>
        /// Converti une matrice contenant les informations de l'histogramme en une image
        /// </summary>
        /// <param name="newMatrice"> matrice </param>
        /// <param name="hauteur"> hauteur de l'image</param>
        /// <param name="largeur"> largeur de l'image</param>
        /// <param name="fichieroriginal"> tableau de byte d'une image bitmap (peut importe la taille)</param>
        /// <returns> tableau de byte, image finale de l'histogramme </returns>
        public byte[] Convert_Tab_To_Histogramme(byte[,] newMatrice, int hauteur, int largeur, byte[] fichieroriginal)
        {
            byte[] fichier = new byte[hauteur * largeur * 3 + 54];

            //le fichier récupère le header
            for (int i = 0; i < 54; i++)
            {
                fichier[i] = fichieroriginal[i];
            }

            //on modifie les valeurs des hauteurs et des largeurs 
            int diviseur = 0;
            long taille = Convert.ToInt64(Convert.ToString(hauteur * largeur * 3 + 54, 2));
            for (int i = 2; i <= 5; i++)
            {
                int découpé = Convert.ToInt32((taille / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                diviseur = diviseur + 8;
            }
            diviseur = 0;
            long valeurLargeur = Convert.ToInt32(Convert.ToString(largeur, 2));
            long valeurHauteur = Convert.ToInt32(Convert.ToString(hauteur, 2));
            for (int i = 18; i <= 21; i++)
            {
                int découpé = Convert.ToInt32((valeurLargeur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                int découpé1 = Convert.ToInt32((valeurHauteur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i + 4] = Convert.ToByte(Convert_binaire_to_décimal(découpé1));
                diviseur = diviseur + 8;
            }

            //création image 
            int casetab = 54;
            for (int ligne = 0; ligne < newMatrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < newMatrice.GetLength(1); colonne++)
                {
                    fichier[casetab] = newMatrice[ligne, colonne];
                    casetab++;
                }
            }
            return fichier;
        }

        /// <summary>
        /// Creation d'une image vierge (entierement noire) pour les dimensions placées en paramètre
        /// </summary>
        /// <param name="hauteur"> hauteur de l'image (nombre entier)</param>
        /// <param name="largeur"> largeur de l'image (nombre entier)</param>
        /// <param name="fichieroriginal"> tableau de byte d'une image bitmap (de n'importe quelles dimensions)</param>
        /// <returns> une image de largeur et hauteur imposée en paramètre, entierement noire. Tableau de byte</returns>
        public byte[] Image_vierge(int hauteur, int largeur, byte[] fichieroriginal)
        {
            byte[] fichier = new byte[hauteur * largeur * 3 + 54];

            //le fichier récupère le header
            for (int i = 0; i < 54; i++)
            {
                fichier[i] = fichieroriginal[i];
            }

            //on modifie les valeurs des hauteurs et des largeurs 
            int diviseur = 0;
            long taille = Convert.ToInt64(Convert.ToString(hauteur * largeur * 3 + 54, 2));
            for (int i = 2; i <= 5; i++)
            {
                int découpé = Convert.ToInt32((taille / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                diviseur = diviseur + 8;
            }
            diviseur = 0;
            long valeurLargeur = Convert.ToInt32(Convert.ToString(largeur, 2));
            long valeurHauteur = Convert.ToInt32(Convert.ToString(hauteur, 2));
            for (int i = 18; i <= 21; i++)
            {
                int découpé = Convert.ToInt32((valeurLargeur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                int découpé1 = Convert.ToInt32((valeurHauteur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier[i + 4] = Convert.ToByte(Convert_binaire_to_décimal(découpé1));
                diviseur = diviseur + 8;
            }

            //création image vierge
            for (int i = 54; i < fichier.Length; i++)
            {
                fichier[i] = 0;
            }
            return fichier;
        }

        /// <summary>
        /// Convertit un nombre binaire en décimal
        /// </summary>
        /// <param name="binaire">nombre en binaire</param>
        /// <returns>entier un nombre en décimal</returns>
        public long Convert_binaire_to_décimal(long binaire)
        {
            long entier = 0;
            long division = 1;
            int longueur = Convert.ToString(binaire).Length;
            for (double puissance = 0; puissance <= longueur; puissance++)
            {
                long binaire1 = binaire / division;
                long entier1 = binaire1 % 10 * Convert.ToInt32(Math.Pow(2, puissance));
                entier = entier + entier1;
                division = division * 10;
            }
            return entier;
        }

        /// <summary>
        /// transforme le fichier byte[]fichier en une matrice (uniquement l'image en elle meme, pas ses infos de la case 0 à 54)
        /// </summary>
        /// <param name="fichier"> tableau de byte, image initiale</param>
        /// <returns> matrice, matrice de byte, uniquement les R,V,B de l'image</returns>
        public byte[,] Convert_Tab_to_Mat(byte[] fichier)
        {
            byte[,] matrice = new byte[image.Hauteur, image.Largeur * 3];
            int colonne = 0;
            int ligne = 0;
            for (int casetab = 54; casetab < fichier.Length - 2; casetab++)
            {
                matrice[ligne, colonne] = fichier[casetab];
                colonne++;
                if ((casetab - 53) % (image.Largeur * 3) == 0)
                {
                    ligne++;
                    colonne = 0;
                }
            }
            return matrice;
        }

    }
}
