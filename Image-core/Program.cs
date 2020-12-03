using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;



namespace Projet2
{
    class Program
    {
        static string Choix_Image()
        {
            Console.WriteLine("Quelle image souhaitez-vous?");
            Console.WriteLine("1- Perroquet");
            Console.WriteLine("2- Femme en noir et blanc");
            Console.WriteLine("3- Aigle, vue lac et montagne ");
            Console.WriteLine("4- Requin");
            Console.WriteLine("5- Echéquier");
            Console.WriteLine("6- Lettres 'Test'");
            Console.WriteLine("'sortir' pour arréter");

            string reponse = Console.ReadLine();
            string imageChoisie = "";

                if (reponse != "")
                {

                    if (reponse == "1")

                    {

                    imageChoisie = "coco.bmp";
                     }

                    if (reponse == "2")

                    {
                        imageChoisie = "lena.bmp";
                    }

                    if (reponse == "3")

                    {
                        imageChoisie = "lac_en_montagne.bmp";
                    }

                    if (reponse == "4")

                    {
                        imageChoisie = "test_100_100.bmp";
                    }

                    if (reponse == "5")

                    {
                        imageChoisie = "Test.bmp";
                    }

                    if (reponse == "6")

                    {
                        imageChoisie = "C.bmp";

                    }

                }
                else
                {
                    Console.WriteLine("////////////////");
                    Console.WriteLine("erreur sur la réponse");
                    Console.WriteLine("////////////////");

                }
            

                return imageChoisie;
        }
            
        



        static void Main(string[] args)
        {
            string reponse = "";
            string imageChoisie = "";

            Console.WriteLine("Que voulez-vous faire ?");
            while (true)

            {

                Console.WriteLine("1- Avoir des informations sur l'image");
                Console.WriteLine("2- Jouer avec l'image");
                Console.WriteLine("3- Jouer avec les filtres ");
                Console.WriteLine("4- Dessiner une fractale");
                Console.WriteLine("5- Afficher l'histogramme");
                Console.WriteLine("6- Afficher l'image");
                Console.WriteLine("7- Image cachée");
                Console.WriteLine("8- Jouer au puzzle");
                Console.WriteLine("'sortir' pour arréter");

                reponse = Console.ReadLine(); // reponse = reponse de l'utilisateur
                if (reponse != "")
                {
                    if (reponse == "1")

                    {
                        imageChoisie = Choix_Image();
                        MyImage InfoImage = new MyImage(imageChoisie);
                        Console.WriteLine(InfoImage.toString());
                        Console.WriteLine("\n");
                    }

                    if (reponse == "2")

                    {
                        imageChoisie = Choix_Image();
                        MyImage InfoImage = new MyImage(imageChoisie);
                        Jeu couleurGris = new Jeu(imageChoisie, InfoImage);
                    }

                    if (reponse == "3")
                    {
                        imageChoisie = Choix_Image();
                        MyImage InfoImage = new MyImage(imageChoisie);
                        Convolution Mat = new Convolution(imageChoisie, InfoImage);
                    }

                    if (reponse == "4")
                    {
                        Fractale Frac = new Fractale();
                    }

                    if (reponse == "5")
                    {
                        imageChoisie = Choix_Image();
                        MyImage InfoImage = new MyImage(imageChoisie);
                        Histogramme Hist = new Histogramme(imageChoisie, InfoImage);
                    }

                    if (reponse == "6")
                    {
                        try
                        {
                            imageChoisie = Choix_Image();
                            MyImage InfoImage = new MyImage(imageChoisie);
                            byte[] fichier = File.ReadAllBytes(imageChoisie);
                            File.WriteAllBytes("Sortie.bmp", fichier);
                            Process.Start("Sortie.bmp");
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

                    if (reponse == "7")
                    {
                        //Console.WriteLine("Choisir une image pour le fond");
                        //string imageFond = Choix_Image();
                        MyImage Fond = new MyImage("test_100_100.bmp");

                        //Console.WriteLine("Choisir une image pour la cacher");
                        //string imageCaché = Choix_Image();
                        MyImage Imagecachée = new MyImage("C.bmp");
                        Image_Cachée image = new Image_Cachée("test_100_100.bmp", "C.bmp", Imagecachée, Fond);
                    }

                    if (reponse == "8")
                    {
                        imageChoisie = "coco.bmp";
                        MyImage InfoImage = new MyImage(imageChoisie);
                        Invention_puzzle invention = new Invention_puzzle(imageChoisie, InfoImage);
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
                    Console.WriteLine("erreur sur la réponse, recommencez !");
                    Console.WriteLine("////////////////");
                }
            }

            Console.ReadLine();

        }
    }
}