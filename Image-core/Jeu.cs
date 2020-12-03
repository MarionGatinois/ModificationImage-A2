using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    class Jeu
    {
        string myfile;
        MyImage image;

        public Jeu(string myfile, MyImage image)
        {
            this.image = image;
            this.myfile = myfile;
            try
            { 
                byte[] fichier = File.ReadAllBytes(myfile);
            
            string reponse = "";
            Console.WriteLine("Que voulez-vous faire ?");
            while (true)


            {
                // AFFICHAGE DU MENU 

                Console.WriteLine("1- Image en nuance de gris");
                Console.WriteLine("2- Image en noir et blanc ");
                Console.WriteLine("3- Image effet stylé négatif");
                Console.WriteLine("4- Image effet miroir vertical");
                Console.WriteLine("5- Image effet miroir horizontal");
                Console.WriteLine("6- Image rotation 90");
                Console.WriteLine("7- Image rotation 180");
                Console.WriteLine("8- Image rotation 270");
                Console.WriteLine("9- Image agrandie");
                Console.WriteLine("10- Image rétrécie");
                Console.WriteLine("'sortir' pour arréter");

                reponse = Console.ReadLine(); // reponse = reponse de l'utilisateur
                if (reponse != "")
                {
                    if (reponse == "1")

                    {
                        byte[] tab = nuanceDeGris(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "2")
                    {
                        byte[] tab = noirEtBlanc(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "3")
                    {
                        byte[] tab = Stylé(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "4")
                    {
                        byte[] tab = effetMiroirVertical(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "5")
                    {
                        byte[] tab = effetMiroirHorizontal(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "6")
                    {
                        byte[] tab = rotation90(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "7")
                    {
                        byte[] tab = rotation180(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "8")
                    {
                        byte[] tab = rotation270(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "9")
                    {
                        byte[] tab = agrandir(fichier);
                        File.WriteAllBytes("Sortie.bmp", tab);
                        Process.Start("Sortie.bmp");
                    }

                    if (reponse == "10")
                    {
                        byte[] tab = Image_Rétrecie(fichier);
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
        /// Mets une image en nuance de gris
        /// </summary>
        /// <param name="fichier"> tableau de bytes correspondnat à l'image à modifier</param>
        /// <returns>engris tableau de bytes correspondant à l'image en nuance de gris</returns>
        public byte[] nuanceDeGris(byte[] fichier)
        {
            byte[] engris = new byte[fichier.Length];
            for (int j = 0; j < 54; j++)
            {
                engris[j] = fichier[j];
            }
            for (int i = 54; i < fichier.Length - 3; i = i + 3)
            {
                int totale = fichier[i] + fichier[i + 1] + fichier[i + 2];
                int somme = totale / 3;
                byte octet = Convert.ToByte(somme);
                engris[i] = octet;
                engris[i + 1] = octet;
                engris[i + 2] = octet;
            }
            return engris;
        }

        /// <summary>
        /// Mets une image en noir et blanc
        /// </summary>
        /// <param name="fichier">tableau de bytes correspondnat à l'image à modifier</param>
        /// <returns>noirBlanc tableau de bytes correspondant à l'image en noir et blanc</returns>
        public byte[] noirEtBlanc(byte[] fichier)
        {
            byte[] noirBlanc = new byte[fichier.Length];
            for (int j = 0; j < 54; j++)
            {
                noirBlanc[j] = fichier[j];
            }
            for (int i = 54; i < fichier.Length - 3; i = i + 3)
            {
                int totale = fichier[i] + fichier[i + 1] + fichier[i + 2];
                byte octet = 0;
                if (totale > 382)
                {
                    int noir = 0;
                    octet = Convert.ToByte(noir);
                }
                else
                {
                    int blanc = 255;
                    octet = Convert.ToByte(blanc);
                }
                noirBlanc[i] = octet;
                noirBlanc[i + 1] = octet;
                noirBlanc[i + 2] = octet;
            }
            return noirBlanc;

        }

        public byte[] Stylé(byte[] fichier)
        {
            for (int i = 54; i < (fichier.Length) / 2; i = i + 3)
            {
                byte k = fichier[i];
                fichier[i] = fichier[fichier.Length + 53 - (2 + i)];
                fichier[fichier.Length + 53 - (2 + i)] = k;
                byte l = fichier[i + 1];
                fichier[i + 1] = fichier[fichier.Length + 53 - i];
                fichier[fichier.Length + 53 - (i)] = l;
                byte m = fichier[i + 2];
                fichier[i + 2] = fichier[fichier.Length + 53 - i - 2];
                fichier[fichier.Length + 53 - i - 2] = m;
            }
            return fichier;
        }

        /// <summary>
        /// /Retourne l'image en miroir vertical
        /// </summary>
        /// <param name="fichier"> image initiale, tableau de byte </param>
        /// <returns> miroir : tableau de byte, image finale retournée en miroir verticale</returns>
        public byte[] effetMiroirVertical(byte[] fichier)
        {
            byte[] miroir = new byte[fichier.Length]; //creation d'un tableau vide
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier); //création d'une matrice comportant les information de fichier
            byte[,] miroirmat = new byte[image.Hauteur, image.Largeur * 3]; //creation d'une matrice vide 

            for (int ligne = 0; ligne < fichiermat.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fichiermat.GetLength(1); colonne = colonne + 3) // on avance de 3 case pour avancer de pixel en pixel
                {
                    int ajoutcolonne = 0;
                    for (int tour = 3; tour > 0; tour--) //parcourt les valeurs R V B de chaque pixel
                    {
                        long a = image.Largeur * 3 - colonne - tour; //position du pixel symétrique par rapport au centre vertical de l'image
                        miroirmat[ligne, colonne + ajoutcolonne] = fichiermat[ligne, a]; //remplissage de la matrice miroir
                        ajoutcolonne++;
                    }
                }
            }
            miroir = Convert_Mat_to_Tab(miroirmat, fichier); //convertion de la matrice en un tableau pour renvoyer une image
            return miroir;
        }

        /// <summary>
        /// Retourne l'image en miroir horizontal
        /// </summary>
        /// <param name="fichier"> image initiale, tableau de bytes </param>
        /// <returns> miroir, tableau de byte, image finale retournée en miroir horizontale</returns>
        public byte[] effetMiroirHorizontal(byte[] fichier)
        {
            byte[] miroir = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] miroirmat = new byte[image.Hauteur, image.Largeur * 3];

            for (int ligne = 0; ligne < fichiermat.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fichiermat.GetLength(1); colonne++) // pas besoin d'avancer de pixel en pixel, on avance par sous pixel (R,V,B)
                {
                    miroirmat[ligne, colonne] = fichiermat[fichiermat.GetLength(0) - ligne - 1, colonne]; //(-1 car longueur commence a 0)  
                                                                                                          // rempli la matrice miroir. Prend le sous pixel symétrique par rapport au milieu horizontal de l'image
                }
            }
            miroir = Convert_Mat_to_Tab(miroirmat, fichier);
            return miroir;
        }

        /// <summary>
        /// Tourne l'image de 90°
        /// </summary>
        /// <param name="fichier"> tableau de byte, image initiale</param>
        /// <returns> rotation, tableua de bytes, image finale </returns>
        public byte[] rotation90(byte[] fichier)
        {
            byte[] rotation = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] rotationmat = new byte[image.Largeur, image.Hauteur * 3];// largeur et hauteur sont inversées

            for (int ligne = 0; ligne < image.Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < image.Largeur; colonne++)
                {
                    {
                        for (int tour = 0; tour < 3; tour++)
                        {
                            rotationmat[image.Largeur - colonne - 1, ligne * 3 + tour] = fichiermat[ligne, colonne * 3 + tour];
                        }
                    }
                }
            }
            rotation = Convert_Mat_to_Tab(rotationmat, fichier);
            rotation = Mat_Horizontal_to_Vertical(rotation);
            return rotation;
        }

        /// <summary>
        /// Tourne l'image de 180°
        /// </summary>
        /// <param name="fichier"> tableau de byte, image initiale</param>
        /// <returns> rotation, tableua de bytes, image finale </returns>
        public byte[] rotation180(byte[] fichier)
        {
            byte[] rotation = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] rotationmat = new byte[image.Hauteur, image.Largeur * 3];

            for (int ligne = 0; ligne < fichiermat.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fichiermat.GetLength(1); colonne = colonne + 3) ///on avance en +3 pour avancer de pixel en pixel
                {
                    int ajoutcolonne = 0;
                    for (int tour = 3; tour > 0; tour--) //on traite les infos R V B de chaque pixel
                    {
                        long a = image.Largeur * 3 - colonne - tour;
                        rotationmat[ligne, colonne + ajoutcolonne] = fichiermat[fichiermat.GetLength(0) - ligne - 1, a];
                        ajoutcolonne++;
                    }
                }
            }
            rotation = Convert_Mat_to_Tab(rotationmat, fichier);
            return rotation;
        }

        /// <summary>
        /// Tourne l'image de 270°
        /// </summary>
        /// <param name="fichier"> tableau de byte, image initiale</param>
        /// <returns> rotation, tableua de bytes, image finale </returns>
        public byte[] rotation270(byte[] fichier)
        {
            byte[] rotation = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] rotationmat = new byte[image.Largeur, image.Hauteur * 3]; // largeur et hauteur sont inversées

            for (int ligne = 0; ligne < image.Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < image.Largeur; colonne++)
                {
                    {
                        for (int tour = 0; tour < 3; tour++)
                        {
                            rotationmat[colonne, (image.Hauteur - ligne - 1) * 3 + tour] = fichiermat[ligne, colonne * 3 + tour];
                        }
                    }
                }
            }
            rotation = Convert_Mat_to_Tab(rotationmat, fichier);
            rotation = Mat_Horizontal_to_Vertical(rotation);
            return rotation;
        }

        /// <summary>
        ///  Double la taille de l'image
        /// </summary>
        /// <param name="fichier"> tableau de bytes correspondant à l'image à modifier</param>
        /// <returns>agrandir tableau de bytes correspondant à l'image agrandie</returns>
        public byte[] agrandir(byte[] fichier)
        {
            int nombrePourGrandir = 2;
            byte[] agrandir = new byte[fichier.Length * nombrePourGrandir];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] agrandiemat = new byte[image.Hauteur * nombrePourGrandir, image.Largeur * 3 * nombrePourGrandir];
            int tour = 0;
            int tour2 = 0;
            for (int ligne = 0; ligne < agrandiemat.GetLength(0) - 1; ligne = ligne + nombrePourGrandir)
            {
                for (int colonne = 0; colonne < agrandiemat.GetLength(1) - 3; colonne = colonne + (3 * nombrePourGrandir))
                {
                    for (int k = 0; k < nombrePourGrandir; k++)
                    {
                        for (int pixel = 0; pixel < 3; pixel++)
                        {
                            for (int g = 0; g < nombrePourGrandir; g++)
                            {
                                agrandiemat[ligne + k, colonne + pixel + (3 * g)] = fichiermat[tour2, tour + pixel];
                            }
                        }
                    }
                    tour = tour + 3;
                }
                tour = 0;
                tour2++;
            }
            agrandir = Convert_Mat_to_Tab(agrandiemat, fichier);
            agrandir = Petite_to_Grande(agrandir, nombrePourGrandir);
            return agrandir;
        }

        /// <summary>
        /// Retrecie une image, en fonction du taux de reuction choisi par l'utilisateur 
        /// </summary>
        /// <param name="fichier"> tableau de byte de l'image à rétrécir</param>
        /// <returns>imageRetrecie tableau de bytes de l'image rétrécie</returns>
        public byte[] Image_Rétrecie(byte[] fichier)
        {
            int tauxReduction = 2;
            byte[] imageRetrecie = new byte[fichier.Length];
            byte[,] fichiermat = Convert_Tab_to_Mat(fichier);
            byte[,] retreciemat = new byte[image.Hauteur / tauxReduction, (image.Largeur / tauxReduction) * 3];

            int ligne2 = 0;
            int colonne2 = 0;

            // on ne garde que le pixel en haut à droite de chaque carré de pixel de tauxReduction x tauxReduction
            for (int ligne = 0; ligne < image.Hauteur - 1; ligne = ligne + tauxReduction)
            {
                for (int colonne = 0; colonne < image.Largeur * 3 - 1; colonne = colonne + tauxReduction * 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        retreciemat[ligne2, colonne2 + i] = fichiermat[ligne, colonne + i];
                    }
                    colonne2 = colonne2 + 3;
                }
                ligne2++;
                colonne2 = 0;
            }
            imageRetrecie = Convert_Mat_to_Tab(retreciemat, fichier);
            imageRetrecie = Grande_to_Petite(imageRetrecie, tauxReduction);

            return imageRetrecie;
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

        /// <summary>
        /// echange la valeur de la hauteur et la largeur d'une image pour transformer un image horizontal en verticale (ou vis-vers-ca)
        /// </summary>
        /// <param name="horizontal"> tableau de byte, image initiale </param>
        /// <returns>horizontal, tableau de byte, image finale verticale</returns>
        public byte[] Mat_Horizontal_to_Vertical(byte[] horizontal)
        {
            byte[] stock = new byte[22];
            for (int i = 18; i <= 21; i++)
            {
                stock[i] = horizontal[i];
            }
            for (int i = 18; i <= 21; i++)
            {
                horizontal[i] = horizontal[i + 4];
                horizontal[i + 4] = stock[i];
            }
            return horizontal;
        }

        /// <summary>
        /// Change la valeur de la largeur et de la hauteur de l'image pour la rétrécir
        /// </summary>
        /// <param name="fichier1">tableau de byte correspond a l'image a modifier</param>
        /// <param name="tauxVariation">nombre par lequel on veux "diviser" l'image</param>
        /// <returns>fichier1 l'image rétércie</returns>
        public byte[] Grande_to_Petite(byte[] fichier1, int tauxVariation)
        {
            long largeurReduite = image.Largeur / tauxVariation;
            long hauteurReduite = image.Hauteur / tauxVariation;
            long valeurLargeur = Convert.ToInt32(Convert.ToString(largeurReduite, 2));
            Console.WriteLine(valeurLargeur);
            long valeurHauteur = Convert.ToInt32(Convert.ToString(hauteurReduite, 2));
            int diviseur = 0;
            for (int i = 18; i <= 21; i++)
            {
                int découpé = Convert.ToInt32((valeurLargeur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier1[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                int découpé1 = Convert.ToInt32((valeurHauteur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier1[i + 4] = Convert.ToByte(Convert_binaire_to_décimal(découpé1));
                diviseur = diviseur + 8;
            }
            return fichier1;
        }

        /// <summary>
        /// Change la valeur de la largeur et de la hauteur de l'image pour l'agrandir
        /// </summary>
        /// <param name="fichier1"> tableau de byte de l'image a modifier</param>
        /// <param name="tauxVariation">nombre de fois que l'on veut agrandir l'image</param>
        /// <returns>fichier1 tableau de byte de l'image agrandie</returns>
        public byte[] Petite_to_Grande(byte[] fichier1, int tauxVariation)
        {
            long largeurReduite = image.Largeur * tauxVariation;
            long hauteurReduite = image.Hauteur * tauxVariation;
            long valeurLargeur = Convert.ToInt32(Convert.ToString(largeurReduite, 2));
            Console.WriteLine(valeurLargeur);
            long valeurHauteur = Convert.ToInt32(Convert.ToString(hauteurReduite, 2));
            int diviseur = 0;
            for (int i = 18; i <= 21; i++)
            {
                int découpé = Convert.ToInt32((valeurLargeur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier1[i] = Convert.ToByte(Convert_binaire_to_décimal(découpé));
                int découpé1 = Convert.ToInt32((valeurHauteur / (Math.Pow(10, diviseur))) % (Math.Pow(10, 8)));
                fichier1[i + 4] = Convert.ToByte(Convert_binaire_to_décimal(découpé1));
                diviseur = diviseur + 8;
            }
            return fichier1;
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
    }
}
