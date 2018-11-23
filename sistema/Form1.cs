using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExifLib;

namespace sistema
{

    public class Immagine
    {
        public string source;
        public string target;
        public string targetPath;

        public Immagine(string Source, string Target, string TargetPath)
        {
            source = Source;
            target = Target;
            targetPath = TargetPath;
        }
    }

    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Immagine> lista = new List<Immagine>();

            string[] allfiles = Directory.GetFiles(Environment.CurrentDirectory, "*.jpg", SearchOption.AllDirectories);
            foreach (var file in allfiles)
            {
                using (ExifReader reader = new ExifReader(file))
                {
                    DateTime datePictureTaken;
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized, out datePictureTaken))
                    {
                        string dataFile = datePictureTaken.ToString("yyyyMMdd");
                        string targetPath = Path.Combine(Environment.CurrentDirectory, dataFile);
                        
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(targetPath, fileName);

                        Immagine im = new Immagine(file, destFile, targetPath);
                        lista.Add(im);
                        
                    }
                }
            }

            foreach (var immagine in lista)
            {
                try
                {
                    if (!Directory.Exists(immagine.targetPath))
                    {
                        Directory.CreateDirectory(immagine.targetPath);
                    }

                    if (File.Exists(immagine.source))
                    {
                        File.Move(immagine.source, immagine.target);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("The process failed: {0}", ex.ToString());
                }
            }
            
        }
    }
}
