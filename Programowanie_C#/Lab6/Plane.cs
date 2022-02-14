using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5_EngB
{
    /* Stage_1: */
    struct Person
    {
        public int weight;
        public string name, surname;
        public Person(string name, string surname, int weight = 75)
        {
            this.name = name;
            this.surname = surname;
            this.weight = weight;
        }
        public override string ToString()
        {
            return $"{this.name} {this.surname}";
        }
        public void Deconstruct(out string name, out string surname)
        {
            name = this.name;
            surname = this.surname;
        }
    }

    /* Stage_2: */
    class Plane
    {
        public enum Class
        {
            Business,
            Economy,
        }

        private Person[] crew;
        private Person[][] passengers;

        protected readonly int mass, torque, horsepower, tankCapacity;
        protected double flightHours = 0.0;
        protected double maxSpeed = 700.0;

        protected readonly int[] maxCapacity;

        public Plane(int mass, int torque, int horsepower, int tankCapacity, int[] maxCap, params Person[] crew)
        {
            this.mass = mass;
            this.torque = torque;
            this.horsepower = horsepower;
            this.tankCapacity = tankCapacity;
            this.crew = new Person[crew.Length];
            Array.Copy(crew, this.crew, crew.Length);

            this.passengers = new Person[2][];
            this.passengers[(int)Class.Business] = new Person[0];
            this.passengers[(int)Class.Economy] = new Person[0];

            this.maxCapacity = new int[2];
            this.maxCapacity[(int)Class.Business] = maxCap[(int)Class.Business]; // (int)Class.Business is 0 but if enum is declarated lets use it
            this.maxCapacity[(int)Class.Economy] = maxCap[(int)Class.Economy];
        }
        public void PrintInfo(bool printPassengers =true)
        {
            string info_base = $"Flight Hours: {this.flightHours}\nMass: {this.mass}\nTorque: {this.torque}" +
                $"\nHorsepower: {this.horsepower}\nMax Capacity Business: {this.maxCapacity[(int)Class.Business]}\n" +
                $"Max Capacity Economy: {this.maxCapacity[(int)Class.Economy]}\nTank Capacity: {this.tankCapacity}\n";

            string extra = "Businnes Class:\n";
            for(int i = 0; i <this.passengers[(int)Class.Business].Length;i++)
            {
                extra += this.passengers[(int)Class.Business][i].ToString();
                extra += "\n";
            }
            extra += "Economy Class:\n";
            for (int i = 0; i < this.passengers[(int)Class.Economy].Length; i++)
            {
                extra += this.passengers[(int)Class.Economy][i].ToString();
                extra += "\n";
            }
            Console.WriteLine(printPassengers == true ? info_base + extra : info_base);
        }
        public void Deconstruct(out int torque, out int horsepower, out int tankCapa)
        {
            torque = this.torque;
            horsepower = this.horsepower;
            tankCapa = this.tankCapacity;
        }

        /* Stage_3: */

        public double CalculateRange()
        {
            double pass_mass = 0.0;
            for(int i = 0; i <this.passengers.Length;i++)
            {
                for(int j = 0; j <this.passengers[i].Length;j++)
                {
                    pass_mass += this.passengers[i][j].weight;
                }
            }

            return (double)tankCapacity * (double)(this.torque + this.horsepower) / ((pass_mass + mass) / 17.46);
        }

        public bool AddPassenger(Class _class, Person passenger)
        {
            int currClass = (int)_class;
            if (this.passengers[currClass].Length == this.maxCapacity[currClass])
                    return false;
            Array.Resize(ref this.passengers[currClass], this.passengers[currClass].Length + 1);
            this.passengers[currClass][this.passengers[currClass].Length - 1] = passenger;
            return true;
        }
        public bool AddBusinessPassenger(Person passenger)
        {
            return AddPassenger(Class.Business, passenger);
        }
        public bool AddEconomyPassenger(Person passenger)
        {
            return AddPassenger(Class.Economy, passenger);
        }
        public (bool,double) Travel(double flightTime)
        {
            double range = CalculateRange();
            double remainingTime = range / this.maxSpeed;

            if((remainingTime - flightHours)<flightTime)
            {
                double x  = remainingTime - flightHours;
                flightHours += x;
                return (false, x);
            }
            flightHours += flightTime;
            return (true, 0.0);
        }
        public bool AddPassengers((Person, Plane.Class)[] x )
        {
            for(int i = 0;i <x.Length;i++)
            {
                bool ret = AddPassenger(x[i].Item2, x[i].Item1);
                if (ret == false) return false;
            }
            return true;
        }
    }
}
