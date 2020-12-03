using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet2
{
    class Image_Cachée
    {

        string myfile;
        string myfile2;
        MyImage file;
        MyImage file2;

        public Image_Cachée(string myfile, string myfile2, MyImage file, MyImage file2)
        {
            this.file = file;
            this.file2 = file2;
            this.myfile = myfile;
            this.myfile2 = myfile2;

            try
            { 
            byte[] cache = File.ReadAllBytes(myfile);
            byte[] fond = File.ReadAllBytes(myfile2);


            string reponse = "";
            Console.WriteLine("Que voulez-vous faire ?");
            while (true)
            {
                Console.WriteLine("1- Voir Image");
                Console.WriteLine("2- Voir Image 2");
                Console.WriteLine("3- Voir avec Image cachée");
                Console.WriteLine("4- Retrouver Image 1");
                Console.WriteLine("5- Retrouver Image 2");
                Console.WriteLine("'sortir' pour arréter");

                reponse = Console.ReadLine();
                if (reponse != "")
                {
                    if (reponse == "1")
                    {
                        File.WriteAllBytes("Sortie.bmp", cache);
                        Process.Start("Sortie.bmp");
                    }
                    if (reponse == "2")
                    {
                        File.WriteAllBytes("Sortie.bmp", fond);
                        Process.Start("Sortie.bmp");
                    }
                    if (reponse == "3")
                    {
                        byte[] Fichier = Cacher_image(cache, fond);
                        File.WriteAllBytes("Sortie.bmp", Fichier);
                        Process.Start("Sortie.bmp");
                    }
                    if (reponse == "4")
                    {
                        byte[] Fichier = Cacher_image(cache, fond);
                        byte[] Fond = Retrouver_image_de_fond(Fichier);
                        File.WriteAllBytes("Sortie.bmp", Fond);
                        Process.Start("Sortie.bmp");
                    }
                    if (reponse == "5")
                    {
                        byte[] Fichier = Cacher_image(cache, fond);
                        byte[] Fond = Retrouver_image_cachée(Fichier);
                        File.WriteAllBytes("Sortie.bmp", Fond);
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
        /// recupère les 2 images et en cache une dans l'autre
        /// </summary>
        /// <param name="cache">l'image qui va être cachée</param>
        /// <param name="fond">l'image qui va apparaitre en cachant l'autre</param>
        /// <returns>ça va retourner une image qui contient les 2 images avec une cachée</returns>
        public byte[] Cacher_image(byte[] cache, byte[] fond)
        {
            byte[,] imagemat = Convert_Tab_to_Mat(cache);
            byte[,] fondmat = Convert_Tab_to_Mat(fond);
            byte[,] finalmat = new byte[file.Hauteur, file.Largeur * 3];
            char[] finalchar = new char[8];

            for (int ligne = 0; ligne < file.Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < file.Largeur * 3; colonne++)
                {
                    string A = Convert.ToString(imagemat[ligne, colonne], 2).PadLeft(8, '0');
                    string B = Convert.ToString(fondmat[ligne, colonne], 2).PadLeft(8, '0');
                    for (int i = 0; i < 8; i++)
                    {
                        char[] Atab = A.ToCharArray();
                        char[] Btab = B.ToCharArray();

                        for (int debut = 0; debut < 4; debut++)
                        {
                            finalchar[debut] = Atab[debut];
                        }
                        for (int fin = 4; fin < 8; fin++)
                        {
                            finalchar[fin] = Btab[fin - 4];
                        }
                    }
                    string finalstring = new string(finalchar);
                    finalmat[ligne, colonne] = Convert.ToByte(finalstring, 2);

                }
            }

            byte[] finaltab = Convert_Mat_to_Tab(finalmat, fond);
            return finaltab;

        }

        /// <summary>
        /// retrouver l'image de fond, celle qui n'est pas cachée sur l'image qui contient les 2 images de base
        /// </summary>
        /// <param name="image_cachée">l'image qui continet les 2 images</param>
        /// <returns>l'image de fond qui sera un peu différente de celle de base</returns>
        public byte[] Retrouver_image_de_fond(byte[] image_cachée)
        {
            byte[] ImagedeFond = new byte[image_cachée.Length];
            char[] Char = new char[8];
            Console.WriteLine(image_cachée.Length);

            for (int i = 0; i < 55; i++)
            {
                ImagedeFond[i] = image_cachée[i];
            }
                for (int ligne = 54; ligne < image_cachée.Length; ligne++)
            {
                string A = Convert.ToString(image_cachée[ligne], 2).PadLeft(8, '0');
                for (int i = 0; i < 8; i++)
                {
                    char[] Atab = A.ToCharArray();

                    for (int debut = 0; debut < 4; debut++)
                    {
                        Char[debut] = Atab[debut];
                    }
                    for (int fin = 4; fin < 8; fin++)
                    {
                        Char[fin] = '0';
                    }
                    //Console.WriteLine(finalchar);

                }
                string finalstring = new string(Char);
                ImagedeFond[ligne] = Convert.ToByte(finalstring, 2);
            }
            return ImagedeFond;
        }

        /// <summary>
        /// retrouver l'image cachée sur l'image qui contient les 2 images de base
        /// </summary>
        /// <param name="image_cachée">l'image qui continet les 2 images</param>
        /// <returns>l'image cachée qui sera un peu différente de celle de base<</returns>
        public byte[] Retrouver_image_cachée(byte[] image_cachée)
        {
            byte[] ImageCachée = new byte[image_cachée.Length];
            char[] Char = new char[8];
            Console.WriteLine(image_cachée.Length);

            for (int i = 0; i < 55; i++)
            {
                ImageCachée[i] = image_cachée[i];
            }
            for (int ligne = 54; ligne < image_cachée.Length; ligne++)
            {
                string A = Convert.ToString(image_cachée[ligne], 2).PadLeft(8, '0');
                for (int i = 0; i < 8; i++)
                {
                    char[] Atab = A.ToCharArray();

                    for (int debut = 0; debut < 4; debut++)
                    {
                        Char[debut] = Atab[debut+4];
                    }
                    for (int fin = 4; fin < 8; fin++)
                    {
                        Char[fin] = '0';
                    }
                    //Console.WriteLine(finalchar);

                }
                string finalstring = new string(Char);
                ImageCachée[ligne] = Convert.ToByte(finalstring, 2);
            }
            return ImageCachée;
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
            byte[,] matrice = new byte[file.Hauteur, file.Largeur * 3];
            int colonne = 0;
            int ligne = 0;
            for (int casetab = 54; casetab < fichier.Length - 1; casetab++)
            {
                matrice[ligne, colonne] = fichier[casetab];
                colonne++;
                if ((casetab - 53) % (file.Largeur * 3) == 0)
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
