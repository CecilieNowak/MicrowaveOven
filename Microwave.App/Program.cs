using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
       
        static void Main(string[] args)
        {
            int config = 700;
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Button timeDetractButton = new Button(); //Todo

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output, config);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            Buzzer buzzer = new Buzzer(output);

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, timeDetractButton, startCancelButton, door, display, light, cooker, buzzer,config);


            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            //System.Console.ReadLine();

            //Følgende er til test af Time Knapper:
            var cont = true;
            while (cont)
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'A':
                    case 'a':
                        ui.OnTimePressed(timeButton, EventArgs.Empty);
                        break;
                    case 'B':
                    case 'b':
                        ui.OnTimeDetractPressed(timeDetractButton, EventArgs.Empty);
                        break;
                }
            }
        }
    }
}
