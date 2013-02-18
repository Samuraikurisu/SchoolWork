/*This is a custom class that opens and displays a 0-9 keypad with an enter and backspace key. This code has not
been tested on 4.0 .NET Microframework and will possibly enter a threading loop because of the fundamental changes
on how the stylus/touch events were changed between 3.0/3.5 and 4.0.*/

using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT;
using Microsoft.SPOT.Touch;
using Microsoft.SPOT.Input; 

namespace MFTouchKeyPadWindow
{
    public class MFTouchKeyPadWindow
    {
        //Image Declaration
        Image button1;
        Image button2;
        Image button3;
        Image button4;
        Image button5;
        Image button6;
        Image button7;
        Image button8;
        Image button9;
        Image button0;
        Image button1Pressed;
        Image button2Pressed;
        Image button3Pressed;
        Image button4Pressed;
        Image button5Pressed;
        Image button6Pressed;
        Image button7Pressed;
        Image button8Pressed;
        Image button9Pressed;
        Image button0Pressed;
        Image done;
        Image backspace;
        Image donePressed;
        Image backspacePressed;

        //Window Declaration
        Window popUpWindow;

        //Brush & Color Declaration
        Brush modalDialogBrush;
        Color backgroundColor;

        //Canvas Declaration
        Canvas exactPositioning;

        //Text and variable declaration
        Text keypadScreen;
        int keyVal=0;
        int total=0;
        int count = 0;
        int[] keysPressed = new int[100]; //this will hold values entered from button presses.

        public void ShowKeyPadDialog(Window window)
        {
            /*Setting Local Window variable to passed object*/
            popUpWindow = window;
            popUpWindow.Height = SystemMetrics.ScreenHeight;
            popUpWindow.Width = SystemMetrics.ScreenWidth;

            /*Brush & Color Declaration*/
            backgroundColor = ColorUtility.ColorFromRGB(0x5F, 0x9e, 0xa0); //CadetBlue 
            modalDialogBrush = new SolidColorBrush(backgroundColor);
            popUpWindow.Background = modalDialogBrush;

            Debug.Print("I am executing the second window"); //Making sure second window executes
           
            
            /*Image Declaration*/

            button1 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key1));
            button1Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key1Pressed));
            button2 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key2));
            button2Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key2Pressed));
            button3 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key3));
            button3Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key3Pressed));
            button4 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key4));
            button4Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key4Pressed));
            button5 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key5));
            button5Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key5Pressed));
            button6 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key6));
            button6Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key6Pressed));
            button7 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key7));
            button7Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key7Pressed));
            button8 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key8));
            button8Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key8Pressed));
            button9 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key9));
            button9Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key9Pressed));
            button0 = new Image(Resources.GetBitmap(Resources.BitmapResources.Key0));
            button0Pressed = new Image(Resources.GetBitmap(Resources.BitmapResources.Key0Pressed));
            done = new Image(Resources.GetBitmap(Resources.BitmapResources.Done));
            donePressed = new Image(Resources.GetBitmap(Resources.BitmapResources.DonePressed));
            backspace = new Image(Resources.GetBitmap(Resources.BitmapResources.Backspace));
            backspacePressed = new Image(Resources.GetBitmap(Resources.BitmapResources.BackspacePressed));

            
            //StackPanel Declaration

            StackPanel row1 = new StackPanel(Orientation.Horizontal);
            row1.Children.Add(button1);
            row1.Children.Add(button2);
            row1.Children.Add(button3);
            row1.Children.Add(button0);
            StackPanel row2 = new StackPanel(Orientation.Horizontal);
            row2.Children.Add(button4);
            row2.Children.Add(button5);
            row2.Children.Add(button6);
            row2.Children.Add(backspace);
            StackPanel row3 = new StackPanel(Orientation.Horizontal);
            row3.Children.Add(button7);
            row3.Children.Add(button8);
            row3.Children.Add(button9);
            row3.Children.Add(done);
            StackPanel stackContainer = new StackPanel(Orientation.Vertical);
            stackContainer.Children.Add(row1);
            stackContainer.Children.Add(row2);
            stackContainer.Children.Add(row3);
            //StackPanel for pressed keys
            StackPanel pressedRow1 = new StackPanel(Orientation.Horizontal);
            pressedRow1.Children.Add(button1Pressed);
            pressedRow1.Children.Add(button2Pressed);
            pressedRow1.Children.Add(button3Pressed);
            pressedRow1.Children.Add(button0Pressed);
            StackPanel pressedRow2 = new StackPanel(Orientation.Horizontal);
            pressedRow2.Children.Add(button4Pressed);
            pressedRow2.Children.Add(button5Pressed);
            pressedRow2.Children.Add(button6Pressed);
            pressedRow2.Children.Add(backspacePressed);
            StackPanel pressedRow3 = new StackPanel(Orientation.Horizontal);
            pressedRow3.Children.Add(button7Pressed);
            pressedRow3.Children.Add(button8Pressed);
            pressedRow3.Children.Add(button9Pressed);
            pressedRow3.Children.Add(donePressed);
            StackPanel pressedStackContainer = new StackPanel(Orientation.Vertical);
            pressedStackContainer.Children.Add(pressedRow1);
            pressedStackContainer.Children.Add(pressedRow2);
            pressedStackContainer.Children.Add(pressedRow3);
            exactPositioning = new Canvas();
            Canvas.SetLeft(pressedStackContainer, 70);
            Canvas.SetBottom(pressedStackContainer, 10);
            exactPositioning.Children.Add(pressedStackContainer);
            Canvas.SetLeft(stackContainer, 70);
            Canvas.SetBottom(stackContainer, 10);
            exactPositioning.Children.Add(stackContainer);
            
            
            //Text declaration
            keypadScreen = new Text();
            keypadScreen.Font = Resources.GetFont(Resources.FontResources.small);
            keypadScreen.TextContent = total.ToString();
            Canvas.SetLeft(keypadScreen, 150);
            Canvas.SetTop(keypadScreen, 50);
            exactPositioning.Children.Add(keypadScreen);
            window.Child = exactPositioning;
            
            popUpWindow.Visibility = Visibility.Visible;

            //Stylus Event Handlers
            done.StylusDown += new StylusEventHandler(DoneClicked);
            done.StylusUp += new StylusEventHandler(DoneReleased);
            backspace.StylusDown += new StylusEventHandler(BackspaceClicked);
            backspace.StylusUp += new StylusEventHandler(BackspaceReleased);
            button0.StylusDown += new StylusEventHandler(ZeroClicked);
            button0.StylusUp += new StylusEventHandler(ZeroReleased);
            button1.StylusUp += new StylusEventHandler(OneReleased);
            button1.StylusDown += new StylusEventHandler(OneClicked);
            button2.StylusDown += new StylusEventHandler(TwoClicked);
            button2.StylusUp += new StylusEventHandler(TwoReleased); 
            button3.StylusDown += new StylusEventHandler(ThreeClicked);
            button3.StylusUp += new StylusEventHandler(ThreeReleased); 
            button4.StylusDown += new StylusEventHandler(FourClicked);
            button4.StylusUp += new StylusEventHandler(FourReleased); 
            button5.StylusDown += new StylusEventHandler(FiveClicked);
            button5.StylusUp += new StylusEventHandler(FiveReleased); 
            button6.StylusDown += new StylusEventHandler(SixClicked);
            button6.StylusUp += new StylusEventHandler(SixReleased); 
            button7.StylusDown += new StylusEventHandler(SevenClicked);
            button7.StylusUp += new StylusEventHandler(SevenReleased); 
            button8.StylusDown += new StylusEventHandler(EightClicked);
            button8.StylusUp += new StylusEventHandler(EightReleased); 
            button9.StylusDown += new StylusEventHandler(NineClicked);
            button9.StylusUp += new StylusEventHandler(NineReleased);

            //Threads for Modal Dialog
            DispatcherFrame modalBlock = new DispatcherFrame();

            popUpWindow.IsVisibleChanged += delegate{ModalVisibleChanged(modalBlock,popUpWindow.Visibility);};

            Dispatcher.PushFrame(modalBlock);

            popUpWindow.IsVisibleChanged -= delegate{ModalVisibleChanged(modalBlock,popUpWindow.Visibility);};

            popUpWindow.Close();

        }
        //Stylus Events
        private void DoneReleased(object sender, StylusEventArgs e)
        {
            done.Visibility = Visibility.Visible;
            donePressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void OneClicked(object sender, StylusEventArgs e)
        {
            keyVal = 1;
            CalculateValue(keyVal);
            button1.Visibility = Visibility.Hidden;
            button1Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void OneReleased(object sender, StylusEventArgs e)
        {
            button1.Visibility = Visibility.Visible;
            button1Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void TwoClicked(object sender, StylusEventArgs e)
        {
            keyVal = 2;
            CalculateValue(keyVal);
            button2.Visibility = Visibility.Hidden;
            button2Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void TwoReleased(object sender, StylusEventArgs e)
        {
            button2.Visibility = Visibility.Visible;
            button2Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void ThreeClicked(object sender, StylusEventArgs e)
        {
            keyVal = 3;
            CalculateValue(keyVal);
            button3.Visibility = Visibility.Hidden;
            button3Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void ThreeReleased(object sender, StylusEventArgs e)
        {   
            button3.Visibility = Visibility.Visible;
            button3Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void FourClicked(object sender, StylusEventArgs e)
        {
            keyVal = 4;
            CalculateValue(keyVal);
            button4.Visibility = Visibility.Hidden;
            button4Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void FourReleased(object sender, StylusEventArgs e)
        {
            button4.Visibility = Visibility.Visible;
            button4Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void FiveClicked(object sender, StylusEventArgs e)
        {
            keyVal = 5;
            CalculateValue(keyVal);
            button5.Visibility = Visibility.Hidden;
            button5Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void FiveReleased(object sender, StylusEventArgs e)
        {
            button5.Visibility = Visibility.Visible;
            button5Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void SixClicked(object sender, StylusEventArgs e)
        {
            keyVal = 6;
            CalculateValue(keyVal);
            button6.Visibility = Visibility.Hidden;
            button6Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void SixReleased(object sender, StylusEventArgs e)
        {
            button6.Visibility = Visibility.Visible;
            button6Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void SevenClicked(object sender, StylusEventArgs e)
        {
            keyVal = 7;
            CalculateValue(keyVal);
            button7.Visibility = Visibility.Hidden;
            button7Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void SevenReleased(object sender, StylusEventArgs e)
        {
            button7.Visibility = Visibility.Visible;
            button7Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void EightClicked(object sender, StylusEventArgs e)
        {
            keyVal = 8;
            CalculateValue(keyVal);
            button8.Visibility = Visibility.Hidden;
            button8Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void EightReleased(object sender, StylusEventArgs e)
        {
            button8.Visibility = Visibility.Visible;
            button8Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void NineClicked(object sender, StylusEventArgs e)
        {
            keyVal = 9;
            CalculateValue(keyVal);
            button9.Visibility = Visibility.Hidden;
            button9Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void NineReleased(object sender, StylusEventArgs e)
        {
            button9.Visibility = Visibility.Visible;
            button9Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void ZeroClicked(object sender, StylusEventArgs e)
        {
            keyVal = 0;
            CalculateValue(keyVal);
            button0.Visibility = Visibility.Hidden;
            button0Pressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void ZeroReleased(object sender, StylusEventArgs e)
        {
            button0.Visibility = Visibility.Visible;
            button0Pressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void BackspaceClicked(object sender, StylusEventArgs e)
        {
            RemoveLastPressed();
            backspace.Visibility = Visibility.Hidden;
            backspacePressed.Visibility = Visibility.Visible;
            exactPositioning.Invalidate();
        }
        private void BackspaceReleased(object sender, StylusEventArgs e)
        {
            backspace.Visibility = Visibility.Visible;
            backspacePressed.Visibility = Visibility.Hidden;
            exactPositioning.Invalidate();
        }
        private void DoneClicked(object sender, StylusEventArgs e)
          {
              done.Visibility = Visibility.Hidden;
              donePressed.Visibility = Visibility.Visible;
              popUpWindow.Invalidate(); 
              popUpWindow.Visibility = Visibility.Hidden;
              

          }
        private void ModalVisibleChanged(DispatcherFrame Block, Visibility visibility)
        {
            if (visibility != Visibility.Visible)
            {
                Block.Continue = false; //this will close the window when the visibility of the window has changed to false.
            }
        }
       
        //This will write characters to the 'display' while placing the next value to the right
        public void CalculateValue(int val)
        {
            int value;
            
            value = val;
            try
            {
                if (count <= -1) //used because the backspace key removes numbers from the holding array.
                {
                    count = 0;
                    keysPressed[count] = value;
                    Debug.Print(" Count Reset to 0");
                }
            }
            catch
            {
                //This catch was used temporary to catch an error, but now might not be necessary.
                Debug.Print(count.ToString());
                Debug.Print("I don't like this. Close the window and try again.");
            }
            total = total * 10 + value;
            keypadScreen.TextContent = total.ToString();
            ++count;
            exactPositioning.Invalidate();

        }
        //Code for the backspace button to delete the last value pressed
        //This is the secret on how calculators work on deleting the last item entered. Fairly simple actually.
        public void RemoveLastPressed()
        {
           
           Debug.Print(count.ToString());
           if (count != 0)
           {
               try
               {
                   total = total - keysPressed[count];
               }
               catch 
               {
                   Debug.Print("Enter a number first you dummy!");
                   //count = 0;
               }
           }
           total = total / 10;
           Debug.Print(keyVal.ToString());
           keypadScreen.TextContent = total.ToString();
           --count; //potential exception caught in the calculate function
           exactPositioning.Invalidate();
        }
      
    }
}
