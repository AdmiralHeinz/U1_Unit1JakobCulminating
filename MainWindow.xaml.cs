//Jakob Heinz
//monday febuary 26
// powercube picture pixles
// find a power cube in an image

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace canvasImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        double x = 0;
        double y = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileD = new Microsoft.Win32.OpenFileDialog();
            openFileD.ShowDialog();
            BitmapImage bi = new BitmapImage(new Uri(openFileD.FileName));
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            canvas.Background = ib;
            if (canvas.Children.Count > 0)
            {
                canvas.Children.RemoveAt(0);
            }

            //MessageBox.Show("find cube");

            Rectangle r = new Rectangle();
            r.Stroke = System.Windows.Media.Brushes.Black;
            r.Width = 100;
            r.Height = 100;
            r.StrokeThickness = 2;

            //get pixel
            int stride = bi.PixelWidth * 4;
            int size = bi.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);


            int c = 1;
            while (c > 0)/// start loop to check pixles continuously 
            {

               Random rnd = new Random();
               int x = (int)rnd.Next(0, 640); // creates a random number 
               int y = (int)rnd.Next(0, 479); // creates a random number 

                if (canvas.Children.Count > 0)
                {
                    canvas.Children.RemoveAt(0);
                }
                canvas.Children.Add(r);
                Canvas.SetLeft(r, x);
                Canvas.SetTop(r, y);

                int x2 = (int)x;
                int y2 = (int)y;
                int index = y2 * stride + 4 * x2;


                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];
                byte alpha = pixels[index + 3];
                ///Console.WriteLine(red.ToString() + ", " + green.ToString() + ", " + blue.ToString());


                if (red > 140 && red < 205)/// test red values
                {
                    if (green > 140 && green < 200)/// test green values
                    {
                        if (blue > 10 && blue < 70)/// test blue values
                        {
                            ///MessageBox.Show("almost there");

                            /// define percentage values
                            int rgb = red + green + blue;
                            double percent_red = ((double)red / (double)rgb) * (double)100;
                            double percent_green = ((double)green / (double)rgb) * (double)100;
                            double percent_blue = ((double)blue / (double)rgb) * (double)100;


                            /// test percentage values
                            if (percent_red > 40 && percent_red < 55 && percent_green > 40 && percent_green < 55 && percent_blue > 1 && percent_blue <20)
                            {
                                c = 0; /// stop loop
                                MessageBox.Show("You Found It");/// tell user the cube has been found

                            }



                        }

                    }


                    
                    

                }
            }
        }
       
    }
}
/*
 * REQUIREMENTS
 * the image must be 640 pixles by 480 pixles (the same sixe as the canvas)
 * the box will not draw around the cube it will keep the searched pixle within the box 
 * due to the fact that not all pixles withing the area of the cube will meet the requirements 
 * the requirements are set to insure that only the cube will be found
 */
