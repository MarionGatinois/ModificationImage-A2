using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace Projet2
{
    class Invention_puzzle
    {
        string myfile;
        MyImage image;
        Random r = new Random();

        public Invention_puzzle(string myfile, MyImage image)
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

                    Console.WriteLine("1- Afficher l'image");
                    Console.WriteLine("2- Afficher l'image en puzzle ");
                    Console.WriteLine("3- Jouer avec l'image");
                    Console.WriteLine("'sortir' pour arréter");
                    reponse = Console.ReadLine(); // reponse = reponse de l'utilisateur
                    if (reponse != "")
                    {
                        if (reponse == "1")

                        {
                            File.WriteAllBytes("Sortie.bmp", fichier);
                            Process.Start("Sortie.bmp");
                        }

                        if (reponse == "2")
                        {
                            int[] tab = image_decoupee(fichier, image, r);

                        }

                        if (reponse == "3")
                        {
                            jouer_puzzle(fichier, image, image_decoupee(fichier, image, r));
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
        /// découpe l'image en 6 morceaux et les melange aléatoirement 
        /// </summary>
        /// <param name="fichier">fichier , info de l'image</param>
        /// <param name="image">l'image définie au début de la classe</param>
        /// <param name="r">random, chiffre aléatoire entre 1 et 6</param>
        /// <returns></returns>
        public int[] image_decoupee(byte[] fichier, MyImage image, Random r)
        {
            byte[] puzzle_tab = new byte[fichier.Length];
            byte[,] fichier_mat = Convert_Tab_to_Mat(fichier);
            byte[,] puzzlefinal = new byte[image.Hauteur, image.Largeur * 3];
            int ColonneRang = 0;
            int LigneRang = 0;
            int k = 0;
            int Ipuzzle = 0;
            int Jpuzzle = 0;
            int tour = 0;
            int ligne;
            int colonne;
            int[] tab = new int[6];


            for (int j = 0; j < 54; j++)
            {
                puzzle_tab[j] = fichier[j];
            }
            for (int place = 0; place < 6; place++)
            {
                //nombre aléatoire
                do
                {
                    k = r.Next(1, 7);
                }

                while (k == tab[0] || k == tab[1] || k == tab[2] || k == tab[3] || k == tab[4] || k == tab[5]);

                {
                    //Console.WriteLine(k);
                    tab[place] = k;
                    if (k == 1)
                    {
                        ColonneRang = 0;
                        LigneRang = 0;
                    }
                    if (k == 2)
                    {
                        ColonneRang = Convert.ToInt32(image.Largeur - 2);
                        LigneRang = 0;
                    }
                    if (k == 3)
                    {
                        ColonneRang = Convert.ToInt32(2 * (image.Largeur) - 1);
                        LigneRang = 0;
                    }
                    if (k == 4)
                    {
                        ColonneRang = 0;
                        LigneRang = Convert.ToInt32((0.5) * image.Hauteur);
                    }
                    if (k == 5)
                    {
                        ColonneRang = Convert.ToInt32((image.Largeur - 2));
                        LigneRang = Convert.ToInt32((0.5) * image.Hauteur);
                    }
                    if (k == 6)
                    {
                        ColonneRang = Convert.ToInt32(2 * (image.Largeur) - 1);
                        LigneRang = Convert.ToInt32((0.5) * image.Hauteur);
                    }
                }


                ligne = LigneRang;
                for (int i = Ipuzzle; i < Ipuzzle + (0.5) * image.Hauteur - 1; i++)
                {

                    colonne = ColonneRang;

                    for (int j = Jpuzzle; j < Jpuzzle + image.Largeur - 2; j++)
                    {
                        puzzlefinal[i, j] = fichier_mat[ligne, colonne];
                        colonne++;
                    }

                    ligne++;
                    colonne = ColonneRang;
                }

                tour++;
                if (tour == 1)
                {
                    Ipuzzle = 0;
                    Jpuzzle = Convert.ToInt32(image.Largeur - 2);
                }
                if (tour == 2)
                {
                    Ipuzzle = 0;
                    Jpuzzle = Convert.ToInt32(2 * image.Largeur - 1);
                }
                if (tour == 3)
                {
                    Ipuzzle = Convert.ToInt32(0.5 * image.Hauteur);
                    Jpuzzle = 0;
                }
                if (tour == 4)
                {
                    Ipuzzle = Convert.ToInt32(0.5 * image.Hauteur);
                    Jpuzzle = Convert.ToInt32(image.Largeur - 2);
                }
                if (tour == 5)
                {
                    Ipuzzle = Convert.ToInt32(0.5 * image.Hauteur);
                    Jpuzzle = Convert.ToInt32(2 * image.Largeur - 1);
                }
            }
            puzzle_tab = Convert_Mat_to_Tab(puzzlefinal, fichier);
            File.WriteAllBytes("Sortie.bmp", puzzle_tab);
            Process.Start("Sortie.bmp");
            return tab;
        }

        /// <summary>
        /// jeu qui demande à l'utilisateur de remettre les cases dans le bon ordre
        /// </summary>
        /// <param name="fichier">fichier , info de l'image</param>
        /// <param name="image">l'image définie au début de la classe</param>
        /// <param name="reponse">ce que renvoie la fonction image découpée</param>
        public void jouer_puzzle(byte[] fichier, MyImage image, int[] reponse)
        {
            byte[] puzzle_tab = new byte[fichier.Length];
            byte[,] fichier_mat = Convert_Tab_to_Mat(fichier);
            byte[,] puzzlefinal = new byte[image.Hauteur, image.Largeur * 3];
            int[] tab = new int[6];
            int[] reponseJoueur = new int[6];

            Console.WriteLine("Voici l'image de Coco mélangée. Vous devez remettre coco dans le bon ordre. Les pièces sont numérotée de la facon suivante");
            Console.WriteLine("______");
            Console.WriteLine("|4|5|6|");
            Console.WriteLine("______");
            Console.WriteLine("|1|2|3|");
            Console.WriteLine("______");
            Console.WriteLine("Quelle case est en bas à gauche ?");
            reponseJoueur[0] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle case est en bas au milieu ?");
            reponseJoueur[1] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle case est en bas à droite ?");
            reponseJoueur[2] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle case est en haut à gauche ?");
            reponseJoueur[3] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle case est en haut au milieu ?");
            reponseJoueur[4] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle case est en haut à doite ?");
            reponseJoueur[5] = Convert.ToInt32(Console.ReadLine());

            bool result = true;
            for (int i = 0; i < 6; i++)

            {
                if (reponseJoueur[i] != reponse[i])
                {
                    result = false;
                }

            }
            if (result == true)
            {
                Console.WriteLine("GAGNE");
            }
            else
            {
                Console.WriteLine("Perdu..., les réponses sont : ");
                for (int i = 0; i < 6; i++)
                {
                    Console.Write(reponse[i]+ ", ");
                }
                Console.WriteLine(" "+"\n");

            }
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
