using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YBMForms
{


    static public class PaperSizes
    {
        public enum PaperSize
        {
            A4,
            A3
        }



        //A4 paper sizes
        static PaperSizes() { 
            SetType(PaperSize.A4);
            dpi = 0;
            }

        public static void  SetType(PaperSize s)
        {
            type = s;
            switch(s)
            {
                case PaperSize.A4:
                    bleedWidth = 216;
                    bleedHeight = 303;
                    unsafeHeight = 297;
                    unsafeWidth = 210;
                    safeHeight = 282;
                    safeWidth = 187;
                    break;

                case PaperSize.A3:
                    SetType(PaperSize.A4);
                    MessageBox.Show("Paper size not impelemented","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    break;

                default:
                    break;
            }
        }


        // Read/Writeible properties
        // About Page size

       
        static private PaperSize type;

        static public PaperSize Type 
        {
            get{ return type; }
            set{ SetType(value);}
        }

        static private int safeHeight;

        public static int SafeHeight
        {
            get { return PaperSizes.safeHeight; }
        }

        static private int safeWidth;

        public static int SafeWidth
        {
            get { return PaperSizes.safeWidth; }
        }
        static private int unsafeWidth;

        public static int UnsafeWidth
        {
            get { return PaperSizes.unsafeWidth; }
        }

        static private int unsafeHeight;

        public static int UnsafeHeight
        {
            get { return PaperSizes.unsafeHeight; }
        }

        static private int bleedWidth;

        public static int BleedWidth
        {
            get { return PaperSizes.bleedWidth; }
        }

        static private int bleedHeight;

        public static int BleedHeight
        {
            get { return PaperSizes.bleedHeight; }
        }

        static private int dpi;

        public static int Dpi
        {
            get { return PaperSizes.dpi; }
            set { dpi = value; }
        } 


        //Read Only Properties

        static public int PixelBleedHeight { get {return (int)Math.Ceiling(dpi*(0.03937301*bleedHeight));}}
        static public int PixelBleedWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * bleedWidth)); } }
        static public int PixelUnsafeHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * unsafeHeight)); } }
        static public int PixelUnsafeWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * unsafeWidth)); } }
        static public int PixelSafeWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * safeHeight)); } }
        static public int PixelSafeHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * safeWidth)); } }



    }
}
