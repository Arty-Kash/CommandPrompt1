//using System.Collections.Generic;
//using System.Collections.ObjectModel;

using System;
using Xamarin.Forms;

namespace CommandPrompt
{
    public partial class MainPage : ContentPage
    {
        int NoL = 0;        // The number of all Console lines
        int NoC = 0;        // The number of Commands
        int NoR1 = 0;       // The number of result lines on main console
        int NoR2 = 0;       // The number of result lines on debugScrollView
        int height = 24;    // Height of command and result line


        public MainPage()
        {
            InitializeComponent();

            // Display the 1st Command Prompt
            CommandPrompt();
        }

                
        // Create and Display a new Command Prompt
        void CommandPrompt()
        {
            // Command Prompt
            string prompt = "Prompt "+NoC.ToString()+">";            

            // Command Prompt
            Label Prompt = new Label
            {
                Text = prompt, 
                FontSize=15, 
                TextColor=Color.Blue, 
                HeightRequest= height
            };

            // Entry for command input
            Entry Command = new Entry
            {
                FontSize=15, 
                HeightRequest = height, 
                HorizontalOptions = LayoutOptions.FillAndExpand, 
                Keyboard = Keyboard.Plain  // No Capitalization and Spellcheck
            };
            Command.Completed += CommandCompleted;

            // Button to delete Entry gray border line
            Button DeleteEntryBorder = new Button
            {
                HeightRequest = height,
                HorizontalOptions = LayoutOptions.FillAndExpand,                
                BorderColor = Color.White, BorderWidth = 2,
                BackgroundColor = Color.Transparent,
                InputTransparent = true
            };

            // Overwrite "DeleteEntryBorder" Button on "Command" Entry
            Grid CommandGrid = new Grid
            {
                HorizontalOptions=LayoutOptions.FillAndExpand,                  
                Children = { Command, DeleteEntryBorder }
            };

            // Line up command prompt and command line
            StackLayout PromptCommand = new StackLayout
            {
                Orientation = StackOrientation.Horizontal, 
                Spacing = 0, 
                Children = { Prompt, CommandGrid }
            };

            // Add a new Prompt and Command Entry to Console
            Console.Children.Add(PromptCommand);


            // Expand scrollView content to show the last command line
            // (Somehow, Added this line, ScrollView are correctly displayed)
            scrollView.Content.HeightRequest = (NoL + 1 + NoR1) * height + 100;

            // Display the values on Label Monitor
            Label1.Text = string.Format("scrollView: H={0}, Con.H={1}",
                                        scrollView.Height.ToString(), scrollView.Content.Height.ToString());
            Label2.Text = string.Format("NoL={0}, (NoL+1)*h={1}",
                                        NoL.ToString(), ((NoL+1)*height).ToString());
            Label3.Text = string.Format("(Con.H-100)/24={0}, Con.H={1}",
                                        ((Console.Height-100)/height).ToString(), Console.Height.ToString());
            
            // Scroll to the last command line and focus its Entry 
            scrollView.ScrollToAsync(0, (NoL + 1) * height - scrollView.Height, false);

            Command.Focus();
            NoR1 = 0;
        }


        // This method is called, when RETURN Key in "Command" Entry
        void CommandCompleted(object sender, EventArgs args)
        {
            Entry commandLine = sender as Entry;
            
            // Display data on ListView
            DisplayDebugScrollView(commandLine.Text);

            // Execute the inputted command
            CommandExecute(commandLine.Text);

            // increment NoL and NoC
            NoL++;
            NoC++;

            // Create next Command Prompt
            CommandPrompt();
        }


        // Identify Command and Execute it
        void CommandExecute(string command)
        {
            // Get the first three characters of command
            string com = "";
            if(command == string.Empty)
            {
                return;     // No command input, Return Key only
            }
            else if(command.Length<3)
            {
                com = command;
            }
            else
            {
                com = command.Substring(0, 3);
            }
            

            // Identify the command from the three characters
            switch (com)
            {
                case "cat":
                    DisplayResult("show file content");
                    NoL++; NoR1=1;
                    break;
                    
                case "cd ":
                    DisplayResult("change directory");
                    NoL++; NoR1=1;
                    break;

                case "ech":  //echo
                    DisplayResult("write text in file");
                    NoL++; NoR1=1;
                    break;

                case "ls ":
                case "ls":
                    DisplayResult("show files");
                    NoL++; NoR1=1;
                    break;

                case "mkd":  //mkdir
                    DisplayResult("make directory");
                    NoL++; NoR1=1;
                    break;

                case "pwd":
                    DisplayResult("show directory");
                    NoL++; NoR1=1;
                    break;

                case "rm ":
                    DisplayResult("delete file");
                    NoL++; NoR1=1;
                    break;

                case "rmd":  //rmdir
                    DisplayResult("delete directory");
                    NoL++; NoR1=1;
                    break;

                case "tou":  //touch
                    DisplayResult("create file");
                    NoL++; NoR1=1;
                    break;

                default:    //others
                    DisplayResult("command not found");
                    NoL++; NoR1=1;
                    break;
            }
        }



        //Display command result on Main Console
        void DisplayResult(string result)
        {
            // Label to show Result
            Label Result = new Label
            {
                Text = result,
                FontSize = 15,
                HeightRequest = height
            };
            Console.Children.Add(Result);
        }


        //Display results on debugScrollView
        void DisplayDebugScrollView(string result)
        {
            // Label to show Result
            Label Result = new Label
            {
                Text = result,
                FontSize = 15,
                HeightRequest = height
            };
            debugResultList.Children.Add(Result);

            // Expand resultScrollView content to show the last Result
            debugScrollView.Content.HeightRequest = (NoR2 + 1) * height + height;

            // Scroll to the last Result
            debugScrollView.ScrollToAsync(0, (NoR2 + 1) * height - debugScrollView.Height, false);

            NoR2++;
        }


        // Three Buttons for Debug (check values and action)
        void Button1(object sender, EventArgs args)
        {
            scrollView.ScrollToAsync(0, 1000, false);
        }
        void Button2(object sender, EventArgs args)
        {
            CommandPrompt();
        }
        void Button3(object sender, EventArgs args)
        {
            scrollView.ScrollToAsync(0, (NoL + 1) * height - scrollView.Height, false);
        }
    }
}







//static int max = 1000;  // maximum number of commands
//string[] command = new string[max];  // string of command line



//public static ObservableCollection<string> result =
//          new ObservableCollection<string>();

//string RETURN = Environment.NewLine;  // "¥r", if UWP

/*
                case "123":
                    NoR1 = 10;
                    StackLayout Results = new StackLayout
                    {
                        //HeightRequest = 240,
                        Spacing = 0
                    };
                    for (int i = 0; i < NoR1; i++)
                    {
                        NoL++;

                        // Label to show Result
                        Label Result = new Label
                        {
                            Text = "result",
                            FontSize = 15,
                            HeightRequest = height
                        };
                        Results.Children.Add(Result);
                    }
                    Console.Children.Add(Results);

                    break;


                case "111":
                    
                    StringBuilder sb = new StringBuilder();

                    NoR1 = 10;
                    for (int i = 0; i < NoR1; i++)
                    {
                        NoL++;
                        sb.Append("result" + Environment.NewLine);
                    }

                    Editor editor = new Editor
                    {
                        Text = sb.ToString(),
                        BackgroundColor = Color.Yellow,
                        //HeightRequest = NoR1 * 15, 
                        FontSize = 15
                    };

                    Console.Children.Add(editor);

                    break;


*/


/*
scrollView.Content.HeightRequest = scrollView.Height + 24;

//if(scrollView.Content.Height > scrollView.Height)
if((NoL + 1) * 24 > scrollView.Height)
{
    //scrollView.Content.HeightRequest = Console.Height + 30;
    scrollView.Content.HeightRequest = (NoL + 1) * 24 + 24;
}
*/
// Bind "result" to ListView "resultList"
//resultList.ItemsSource = result;


/*
// Detect Console Input (Text Changed)
void ConsoleChanged(object sender, TextChangedEventArgs args)
{
    string s_new = args.NewTextValue;
    //string s_old = args.OldTextValue;
    int length = s_new.Length;
    string s = s_new.Remove(0, length-1);


    // RETURN pressed
    if(s==RETURN)
    {
        Label3.Text = args.OldTextValue;
        // To remove "\r" from the Editor input, 
        // replace the input text to args.OldTextValue
        Editor commandLine = sender as Editor;
        commandLine.Text = args.OldTextValue;

        // Display data on ListView
        //DisplayResult(command[NoL]);
        DisplayResult(args.OldTextValue);

        // Display Command Prompt
        //NoL++;
        CommandPrompt();
        return;
    }

    //command[NoL] += s;


    //Label1.Text = command[NoL];
    Label2.Text = length.ToString();
    //Label3.Text = args.OldTextValue;

}
*/

//scrollView.ScrollToAsync(PromptCommand, ScrollToPosition.End, false);

/*
        //Display the results in ListView
        void DisplayResult(ListView ListViewName, string[] result)
        {

            
            
            //Label resultLabel = new Label
            //{
            //    Text = s
            //};

            //resultList.Children.Add(resultLabel);
            //resultScrollView.ScrollToAsync(resultList, ScrollToPosition.End, false);
            

            //result.Add(s);
            //resultList.ScrollTo(s, ScrollToPosition.End, false);

                        
            //resultList.Text += s;            
            //resultList.Text += Environment.NewLine;
            //resultList.Text += "\n";
            //resultList.Focus();
            


        }


*/
