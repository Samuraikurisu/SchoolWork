/*This program displays a button that opens a new window with a keypad for the user to press numbers into. This 
code requires the MFTouchKeyPadWindow.cs file as well.*/


using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

namespace MFTouchKeyPadWindow
{
    public class Program : Microsoft.SPOT.Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Window mainWindow = myApplication.CreateWindow();

            Touch.Initialize(myApplication);  //intialize touch screen for I/O

            // Start the application
            myApplication.Run(mainWindow);
        }

        private Window mainWindow;
        public Window windowDialog; 
        public Rectangle button;
        public Color buttonBackground;
        public SolidColorBrush buttonBackgroundBrush;
        public Color mainWindowColor;
        public SolidColorBrush mainWindowBackground;
        public Pen buttonPen;
        public Canvas buttonCanvas;

        public Window CreateWindow()
        {
            // Create a window object and set its size to the
            // size of the display.
            mainWindow = new Window();
            mainWindow.Height = SystemMetrics.ScreenHeight;
            mainWindow.Width = SystemMetrics.ScreenWidth;

            /*Creation of a custom button*/
            button = new Rectangle(); 
            button.Height = 125;
            button.Width = 125;
            
            /*Set background/button color*/
            buttonBackground = ColorUtility.ColorFromRGB(0xC0, 0xC0, 0xC0);
            buttonBackgroundBrush = new SolidColorBrush(buttonBackground);
            button.Fill = new SolidColorBrush(buttonBackground);
            buttonPen = new Pen(Color.Black, 3);
            button.Stroke = buttonPen;

            buttonCanvas = new Canvas();
            Canvas.SetLeft(button, 20);
            Canvas.SetTop(button, 20);
            buttonCanvas.Children.Add(button);
            /*End of code dealing with creation of custom button*/

            /*Background color creation*/
            mainWindowColor = ColorUtility.ColorFromRGB(0x00, 0xFF, 0xFF);
            mainWindowBackground = new SolidColorBrush(mainWindowColor);
            mainWindow.Background = mainWindowBackground; 
            
            // Creating a text for the button
            Text text = new Text();
            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.TextContent = Resources.GetString(Resources.StringResources.String1);
            Canvas.SetLeft(text, 50);
            Canvas.SetTop(text, 75);
            buttonCanvas.Children.Add(text);

            // Add the button canvas to the main window
            mainWindow.Child = buttonCanvas;
            
            //Stylus Event Handler
            button.StylusDown += new StylusEventHandler(ClickDown);
            text.StylusDown += new StylusEventHandler(ClickTextDown);
            

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;
            

            Debug.Print("Mainwindow finished");

            return mainWindow;
        }
        /*Either code event will catch and open the new window. Fixes issue with button text not opening the window when clicked*/

        private void ClickDown(object sender, StylusEventArgs e)
        {
            Window windowDialog = new Window();
            MFTouchKeyPadWindow keypadDialog = new MFTouchKeyPadWindow();
            Application.Current.MainWindow = windowDialog;
            keypadDialog.ShowKeyPadDialog(windowDialog);
            Application.Current.MainWindow = mainWindow;
        }
        
        private void ClickTextDown(object sender, StylusEventArgs e)
        {
            Window windowDialog = new Window();
            MFTouchKeyPadWindow keypadDialog = new MFTouchKeyPadWindow();
            Application.Current.MainWindow = windowDialog;
            keypadDialog.ShowKeyPadDialog(windowDialog);
            Application.Current.MainWindow = mainWindow;
        }
        

    }
}
