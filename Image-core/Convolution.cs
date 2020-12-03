using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    class Convolution
    {
        string myfile;
        MyImage image;

        public Convolution(string myfile, MyImage image)
        {
            this.image = image;
            this.myfile = myfile;
            try { 
            byte[] fichier = File.ReadAllBytes(myfile);
            //Process.Start(myfile);
            string reponse = "";
            Console.WriteLine("Que voulez-vous faire ?");
            while (true)


            {
                // AFFICHAGE DU MENU 

                Console.WriteLine("1- Image, flou");
                Console.WriteLine("2- Image, renforcement des bords");
                Console.WriteLine("3- Image, détection de contour");
                Console.WriteLine("4- Image, repoussage");
                Console.WriteLine("5- Image, ID");
                Console.WriteLine("'sortir' pour arréter");

                reponse = Console.ReadLine(); // reponse = reponse de l'utilisateur

                if (reponse != "")
                {
                    if (reponse == "1")

                    {
                        int[,] filtre = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                        int diviseur = 9;
                        byte[] tab = Matrice_Convolution(fichier, filtre, diviseur);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "2")

                    {
                        int[,] filtre = new int[,] { { 0, 0, 0 }, { (-1), 1, 0 }, { 0, 0, 0 } };
                        int diviseur = 1;
                        byte[] tab = Matrice_Convolution(fichier, filtre, diviseur);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "3")

                    {
                        int[,] filtre = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                        int diviseur = 1;
                        byte[] tab = Matrice_Convolution(fichier, filtre, diviseur);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "4")

                    {
                        int[,] filtre = new int[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                        int diviseur = 1;
                        byte[] tab = Matrice_Convolution(fichier, filtre, diviseur);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "5")

                    {
                        int[,] filtre = new int[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                        int diviseur = 1;
                        byte[] tab = Matrice_Convolution(fichier, filtre, diviseur);
                        File.WriteAllBytes("Sortie.bmp", tab);
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
        /// Fonction qui applique un filtre à l'image
        /// </summary>
        /// <param name="fichier"> image a modifier </param>
        /// <param name="filtremat"> matrice du filtre a appliquer a l'image</param>
        /// <param name="diviseur"> donner en fonction du filtre choisi</param>
        /// <returns> tableau de bytes de l'image avec le filtre </returns>
        public byte[] Matrice_Convolution(byte[] fichier, int[,] filtremat, int diviseur)
        {
            byte[] ImageFiltree = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] ImageFiltreeMat = new byte[image.Hauteur, image.Largeur * 3];

            for (int ligne = 0; ligne < image.Hauteur - 1; ligne++) // parcourir l'image et récupérer les info des pixels 
            {
                for (int colonne = 3; colonne < (image.Largeur * 3) - 1; colonne++)
                {
                    // CREATION DE LA MATRICE CONVOLUTION
                    int colonne1 = 0;
                    int ligne1 = 0;
                    int[,] Matrice = new int[3, 3];
                    for (long l = ligne - 1; l <= ligne + 1; l++) //  création de la matrice de convolution
                    {
                        for (long c = colonne - 3; c <= (colonne + 3); c = c + 3)
                        {
                            long k = c;
                            long k2 = l;
                            if (l < 0)
                            {
                                l = 0;
                            }
                            if (l > (image.Hauteur - 1))
                            {
                                l = (image.Hauteur - 1);
                            }
                            if (c < 0)
                            {
                                c = 0;
                            }
                            if (c > (image.Largeur * 3) - 1)
                            {
                                c = (image.Largeur * 3) - 3;
                            }

                            Matrice[ligne1, colonne1] = fichiermat[l, c];
                            c = k;
                            l = k2;
                            colonne1++;
                        }
                        ligne1++;
                        colonne1 = 0;
                    }


                    // MULTIPLICATION FILTRE et CONVOLUTION
                    int somme = 0;
                    //int sommeFiltre = 0;
                    for (int colonne2 = 0; colonne2 < 3; colonne2++)
                    {
                        for (int ligne2 = 0; ligne2 < 3; ligne2++)
                        {
                            somme = somme + Matrice[ligne2, colonne2] * filtremat[ligne2, colonne2];
                            //sommeFiltre = sommeFiltre + filtremat[ligne2, colonne2];
                        }
                    }

                    // NOUVELLE VALEUR 
                    int valeurFinale = somme / diviseur;

                    if (valeurFinale <= 0)
                    {
                        valeurFinale = 0;//somme = - somme
                    }
                    else if (valeurFinale > 255)
                    {
                        valeurFinale = 255;//somme = somme - 255
                    }


                    ImageFiltreeMat[ligne, colonne] = Convert.ToByte(valeurFinale);

                }
            }
            ImageFiltree = Convert_Mat_to_Tab(ImageFiltreeMat, fichier);
            return ImageFiltree;
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

    }
}
