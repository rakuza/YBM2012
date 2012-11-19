using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YBMForms
{

    

    static public class PaperSizes
    {
        /// <summary>
        /// paper size enum
        /// contains a list of all printible paper sizes
        /// </summary>
        public enum Standard
        {
            A4
        }


        /// <summary>
        /// changes the type of paper
        /// </summary>
        /// <param name="s">Papersize to set to</param>
        public static void SetType(Standard s)
        {
            type = s;
            switch (s)
            {
                case Standard.A4:
                    
                    bleedWidth = 216;
                    bleedHeight = 303;
                    unsafeHeight = 297;
                    unsafeWidth = 210;
                    safeHeight = 282;
                    safeWidth = 187;
                    paperWidth = 297;
                    paperHeight = 420;
                    break;

                default:
                    break;
            }
        }


        // Read/Writeible properties
        // About Page size

        static private int paperWidth;

        static public int PaperWidth
        {
            get { return paperWidth; }
            set { paperWidth = value; }
        }

        static private int paperHeight;

        static public int PaperHeight
        {
            get { return paperHeight; }
            set { paperHeight = value; }
        } 

        static private Standard type;

        static public Standard Type
        {
            get { return type; }
            set { SetType(value); }
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

        /// <summary>
        /// Provides a set of pixel sized variables
        /// 
        /// read only
        /// </summary>
        static internal class Pixel
        {
            static public int BleedHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * bleedHeight)); } }
            static public int BleedWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * bleedWidth)); } }
            static public int UnsafeHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * unsafeHeight)); } }
            static public int UnsafeWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * unsafeWidth)); } }
            static public int SafeWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * safeWidth)); } }
            static public int SafeHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * safeHeight)); } }
            static public int PaperWidth { get { return (int)Math.Ceiling(dpi * (0.03937301 * paperWidth)); } }
            static public int PaperHeight { get { return (int)Math.Ceiling(dpi * (0.03937301 * paperHeight)); } }
        }




    }
}
