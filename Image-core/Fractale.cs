using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    class Fractale
    {
        /// <summary>
        /// création d'une fractale qui récupère l'image vierge créée et qui va remplir des pixels en noir pour créer la fractale
        /// </summary>
        public Fractale()
        {
            double x1 = -2.1; //zone fractale
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;

            int image_x = 270;
            int image_y = 240;
            int iteration_max = 50;

            double zoom_x = image_x / (x2 - x1); //taille image en x :100
            double zoom_y = image_y / (y2 - y1); //taille image en y :100


            byte[] fichier = Creation_image_vierge(image_x, image_y);
            byte[,] matFichier = Convert_Tab_to_Mat(fichier, image_x, image_y);


            for (int x = 0; x < image_x; x++)
            {

                for (int y = 0; y < image_y; y++)
                {
                    double c_r = x / zoom_x + x1;
                    double c_i = y / zoom_y + y1;
                    int i = 0;
                    double z_r = 0;
                    double z_i = 0;


                    do
                    {
                        double tmp = z_r;
                        z_r = z_r * z_r - z_i * z_i + c_r;
                        z_i = 2 * z_i * tmp + c_i;
                        i = i + 1;
                    }
                    while (z_r * z_r + z_i * z_i < 4 && i < iteration_max);
                    if (i == iteration_max)
                    {
                        matFichier[x, y] = 0; //mets les pixels choisis en noir

                    }

                }
            }
            byte[] tabFichier = new byte[image_x * image_y * 3 + 54];
            for (int L = 0; L < 54; L++)
            {
                tabFichier[L] = fichier[L];
            }
            byte[] tab = Convert_Mat_to_Tab(matFichier, fichier);
            for (int L = 54; L < fichier.Length - 1; L++)
            {
                tabFichier[L] = tab[L];
            }
            File.WriteAllBytes("Sortie.bmp", tabFichier);
            Process.Start("Sortie.bmp");
        }

        /// <summary>
        /// création d'une image à partir de rien (création d'un header + reste des pixels en blanc)
        /// </summary>
        /// <param name="zoom_x">taille de l'image en longueur</param>
        /// <param name="zoom_y">taille de l'image en largeur</param>
        /// <returns>retourne une image blanche </returns>
        public byte[] Creation_image_vierge(double zoom_x, double zoom_y)
        {

            byte[] fichier = new byte[200 * 320 * 3 + 54];
            //création header
            byte[] header = { 66, 77, 54, 238, 2, 0, 0, 0, 0, 0, 54, 0, 0, 0, 40, 0, 0, 0, 64, 1, 0, 0, 200, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 238, 2, 0, 35, 46, 0, 0, 35, 46, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //changer valeurs hauteur et largeur

            //le fichier récupère le header
            for (int i = 0; i < 54; i++)
            {
                fichier[i] = header[i];
            }

            //création image vierge
            for (int i = 54; i < fichier.Length; i++)
            {
                fichier[i] = 255;
            }
            return fichier;
        }

        /// <summary>
        /// transforme une matrice (byte[,] matrice) en une image ayant les propriété de byte[] image
        /// Fonction utilisée dans les fonction miroir et rotation
        /// </summary>
        /// <param name="matrice"> matrice des R,V,B de l'image MODIFIES </param>
        /// <param name="image"> image initiale que l'on veut modifier (les modification sont dans matrice</param>
        /// <returns>tab, tableau de byte, image finale </returns>
        public byte[] Convert_Mat_to_Tab(byte[,] matrice, byte[] image)
        {
            int longueur = 54 + matrice.GetLength(0) * matrice.GetLength(1);
            byte[] tab = new byte[longueur];
            for (int M = 0; M < 54; M++)
            {
                tab[M] = image[M];
            }
            int casetab = 54;
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    tab[casetab] = matrice[ligne, colonne];
                    casetab++;
                }
            }
            return tab;
        }

        /// <summary>
        /// transforme le fichier byte[]fichier en une matrice (uniquement l'image en elle meme, pas ses infos de la case 0 à 54)
        /// </summary>
        /// <param name="fichier"> tableau de byte, image initiale</param>
        /// <returns> matrice, matrice de byte, uniquement les R,V,B de l'image</returns>
        public byte[,] Convert_Tab_to_Mat(byte[] fichier, double image_x, double image_y)
        {
            int imagex = Convert.ToInt32(image_x);
            int imagey = Convert.ToInt32(image_y);
            byte[,] matrice = new byte[imagex, imagey * 3];
            int colonne = 0;
            int ligne = 0;
            for (int casetab = 54; casetab < fichier.Length - 1; casetab++)
            {
                matrice[ligne, colonne] = fichier[casetab];
                colonne++;
                if ((casetab - 53) % (imagey * 3) == 0)
                {
                    ligne++;
                    colonne = 0;
                }
            }
            return matrice;
        }

    }
}
