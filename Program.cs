using System;

namespace assignment7
{
    // Class Light  
    public class Light
    {
        private bool light = true;
        // Print state of light
        public void lightState() 
        {
            string lightState;
            if (light) { lightState = "ON"; }
            else { lightState = "OFF"; }
            Console.WriteLine("Light is " + lightState);
        }
        // Turn on the light
        public void on() { light = true; }
        // Turn off the light
        public void off() { light = false; }
    }

    // Class Thermostat
    public class Thermostat
    {
        private int temperature = 20;
        // Print statue of Thermostat
        public void tempState()
        {
            Console.WriteLine("Thermostat temperature is " + temperature);
        }
        // Increase temperature 
        public void up() { temperature += 1; }
        // Decrease temperature
        public void down() { temperature -= 1; }
    }


    // Command Interface
    public interface Command
    {
        public void execute();
    }

    // Class that provided when its execute method is called.
    public class NoCommand : Command
    {
        public void execute() { }
    }


    // Class that turns off the light
    public class LightOnCommand : Command
    {
        Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.on();
        }
    }

    // Class that turns off the light
    public class LightOffCommand : Command
    {
        Light light;
        public LightOffCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.off();
        }
    }

    // Class that increases the temperature by 1 degree
    public class TemperatureUpCommand : Command
    {
        Thermostat temperature;
        public TemperatureUpCommand(Thermostat temperature)
        {
            this.temperature = temperature;
        }
        public void execute()
        {
            temperature.up();
        }
    }

    // Class that decreases the temperature by 1 degree
    public class TemperatureDownCommand : Command
    {
        Thermostat temperature;
        public TemperatureDownCommand(Thermostat temperature)
        {
            this.temperature = temperature;
        }
        public void execute()
        {
            temperature.down();
        }
    }


    // Remote control that manipulates light and temperature
    public class RemoteControl
    {
        Command[] onCommands;
        Command[] offCommands;

        public RemoteControl()
        {
            onCommands = new Command[4];
            offCommands = new Command[4];

            Command noCommand = new NoCommand();
            for (int i = 0; i < 4; i++)
            {
                onCommands[i] = noCommand;
                offCommands[i] = noCommand;
            }
        }

        // Set on/off command to each slot
        public void setCommand(int slot, Command onCommand, Command offCommand)
        {
            onCommands[slot] = onCommand;
            offCommands[slot] = offCommand;
        }
        // Execute onCommands
        public void onButtonWasPushed(int slot)
        {
            if (onCommands[slot] != null)
            {
                onCommands[slot].execute();
            }
        }
        // Execute offCommands
        public void offButtonWasPushed(int slot)
        {
            if (offCommands[slot] != null)
            {
                offCommands[slot].execute();
            }
        }
    }


    public class RemoteController
    {
        public static void Main(String[] args)
        {
            RemoteControl remoteControl = new RemoteControl();
            // Create new Light, Thermostat
            Light Light = new Light();
            Thermostat Temperature = new Thermostat();

            // Enabled Light On and Off Commands.
            LightOnCommand LightOn = new LightOnCommand(Light);
            LightOffCommand LightOff = new LightOffCommand(Light);
            // Enabled Temperature Up and Down Commands.
            TemperatureUpCommand TemperatureUp = new TemperatureUpCommand(Temperature);
            TemperatureDownCommand TemperatureDown = new TemperatureDownCommand(Temperature);

            // Add the light Commands to the remote in slot 0.
            // Add the thermostat Commands to the remote in slot 1.
            remoteControl.setCommand(0, LightOn, LightOff);
            remoteControl.setCommand(1, TemperatureUp, TemperatureDown);

            // Providing clear instructions to the user on which keys to press for each command.
            Console.WriteLine("Light is ON, Temperature is 20 now.");
            Console.WriteLine("-----------Remote Control------------");
            Console.WriteLine("1 : Turn light On");
            Console.WriteLine("2 : Turn light Off");
            Console.WriteLine("3 : Increase the thermostat's temperature by 1 degree");
            Console.WriteLine("4 : Decrease the thermostat's temperature by 1 degree");
            Console.WriteLine("==> Press the number (If you want to remote both, press both numbers including the middle space (e.g. 1 3)");

            // Get input from user
            string input = Console.ReadLine();
            string[] inputs = input.Split(' ');
            bool correct = true;


            // Execute remote control according to user commands 
            foreach (var i in inputs)
            {
                if (i == "1") { remoteControl.onButtonWasPushed(0); }
                else if (i == "2") { remoteControl.offButtonWasPushed(0); }
                else if (i == "3") { remoteControl.onButtonWasPushed(1); }
                else if (i == "4") { remoteControl.offButtonWasPushed(1); }
                else
                {
                    Console.WriteLine("Input is incorrect.");
                    correct = false;
                }
            }

            // Print current state if input is correct
            if (correct)
            {
                Light.lightState();
                Temperature.tempState();
            }
        }
    }
}