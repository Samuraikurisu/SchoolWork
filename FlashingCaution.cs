//Chris Hilgenberg
//4-11-12
//Exam 2 Lab Portion

/*This code was written during an exam. It's main purpose is to stop a flashing display when an onscreen button would be pressed*/


using System;

using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Controls;
using System.Threading;

namespace FlashingCaution
{
    public class Program : Microsoft.SPOT.Application
    {
        public static void Main()
        {
            Program myApplication = new Program();

            Window mainWindow = myApplication.CreateWindow();

            // Create the object that configures the GPIO pins to buttons.
            GPIOButtonInputProvider inputProvider = new GPIOButtonInputProvider(null);

            // Start the application
            myApplication.Run(mainWindow);
        }

        private Window mainWindow;
        Image Caution;
        Image CautionBackground;
        Brush WindowBackgroundBrush;
        Color WindowBackgroundColor;
        Canvas PositioningCaution;
        DispatcherTimer toggleImage;
        int Count;
        int ButtonCount;

        public Window CreateWindow()
        {
            // Create a window object and set its size to the
            // size of the display.
            mainWindow = new Window();
            mainWindow.Height = SystemMetrics.ScreenHeight;
            mainWindow.Width = SystemMetrics.ScreenWidth;

            /*// Create a single text control.
            Text text = new Text();

            text.Font = Resources.GetFont(Resources.FontResources.small);
            text.TextContent = Resources.GetString(Resources.StringResources.String1);
            text.HorizontalAlignment = Microsoft.SPOT.Presentation.HorizontalAlignment.Center;
            text.VerticalAlignment = Microsoft.SPOT.Presentation.VerticalAlignment.Center;

            // Add the text control to the window.
            mainWindow.Child = text;*/

            WindowBackgroundColor = ColorUtility.ColorFromRGB(0xff, 0xdf, 0x00);
            WindowBackgroundBrush = new SolidColorBrush(WindowBackgroundColor);
            mainWindow.Background = WindowBackgroundBrush; 

            /*Adding Images*/
            Caution = new Image(Resources.GetBitmap(Resources.BitmapResources.Caution));
            CautionBackground = new Image(Resources.GetBitmap(Resources.BitmapResources.CautionBlkBkgnd));

            PositioningCaution = new Canvas();

            Canvas.SetTop(Caution, 20);
            Canvas.SetLeft(Caution, 20);
            PositioningCaution.Children.Add(Caution);

            Canvas.SetTop(CautionBackground, 20);
            Canvas.SetLeft(CautionBackground, 20);
            PositioningCaution.Children.Add(CautionBackground);

            mainWindow.Child = PositioningCaution;

            
            toggleImage = new DispatcherTimer(PositioningCaution.Dispatcher);
            toggleImage.Tick += new EventHandler(TimeCaution);
            toggleImage.Interval = new TimeSpan(0, 0, 1);
            toggleImage.Start();

            // Connect the button handler to all of the buttons.
            mainWindow.AddHandler(Buttons.ButtonUpEvent, new ButtonEventHandler(OnButtonUp), false);

            // Set the window visibility to visible.
            mainWindow.Visibility = Visibility.Visible;

            // Attach the button focus to the window.
            Buttons.Focus(mainWindow);

            return mainWindow;
        }

        private void OnButtonUp(object sender, ButtonEventArgs e)
        {
            
            ++ButtonCount;
            toggleImage.Stop();
            if (((ButtonCount % 2) == 0)&&(ButtonCount!=1))
            {
                toggleImage.Start();
            }

        }
        private void TimeCaution(object sender, EventArgs e)
        {
            //Debug.Print("Are you executing?");
            if ((Count % 2) == 0)
            {
                CautionBackground.Visibility = Visibility.Visible;
                Caution.Visibility = Visibility.Hidden;
                PositioningCaution.Invalidate();
                //Debug.Print("Are you doing something?");
                //Debug.Print(Count.ToString());
            }
            else
            {
                CautionBackground.Visibility = Visibility.Hidden;
                Caution.Visibility = Visibility.Visible;
                PositioningCaution.Invalidate();
            }
            ++Count;
           
        }

    }
}
