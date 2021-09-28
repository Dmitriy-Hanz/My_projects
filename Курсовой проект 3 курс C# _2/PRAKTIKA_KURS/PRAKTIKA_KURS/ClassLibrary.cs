using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media;

namespace PRAKTIKA_KURS
{
    public class IT
    {
        public int ridesCount;
        public int emptyRidesCount;
        public int generalWeight;
        public int genPasCount;
        public void InformationTargeting(object exempl)
        {
            Elevator obj1 = new Elevator();
            Passenger obj2 = new Passenger();
            int o = 0;
            if (exempl is Elevator)
            {
                obj1 = (Elevator)exempl;

                if (obj1.DoorsState == DoorsState.Close)
                { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.DoorsState_Board.Text = "Двери закрыты"); }
                else { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.DoorsState_Board.Text = "Двери открыты"); }

                if (obj1.IsMoving == true & obj1.FloorDirection < obj1.LiftPos)
                { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.MoveState_Board.Text = "Лифт движеться вниз"); }
                if (obj1.IsMoving == true & obj1.FloorDirection > obj1.LiftPos)
                { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.MoveState_Board.Text = "Лифт движеться вверх"); }
                if (obj1.IsMoving == false)
                { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.MoveState_Board.Text = "Лифт стоит"); }

                MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.LiftPos_Board.Text = $"Находиться на {obj1.LiftPos} этаже");
                MainWindow.MW.Dispatcher.Invoke(() => MainWindow.Floors[obj1.LiftPos - 1].Background = Brushes.LightGreen);
                if (obj1.LiftPos - 2 != -1) { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.Floors[obj1.LiftPos - 2].Background = Brushes.White); }
                if (obj1.LiftPos < 25) { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.Floors[obj1.LiftPos].Background = Brushes.White); }
                MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.PassangersCount_Board.Text = $"Пассажиры: {obj1.Passangers.Count}");
            }

            for (int i = 0; i < MainWindow.FLOORS.Length; i++)
            {
                if (MainWindow.FLOORS[i] != null)
                {
                    o = o + MainWindow.FLOORS[i].Count;
                }
            }
            MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.PassCountFloor_Board.Text = $"Кол-во пассажиров на этажах: {o}");
            MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.PassFloors_Board.Text = $"   Номера этажей: ");
            int ww1 = 0,ww2=0;
            for (int i = 0; i < MainWindow.FLOORS.Length; i++)
            {
                if (MainWindow.FLOORS[i] != null)
                {
                    if (MainWindow.FLOORS[i].Count != 0)
                    {
                        if (MainWindow.FLOORS[i][0].BasedFloor != 0)
                        { MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.PassFloors_Board.Text = MainWindow.MW.PassFloors_Board.Text + $"{MainWindow.FLOORS[i][0].BasedFloor} "); }
                        else { }
                    }
                }
            }

            for (int i = 0; i < MainWindow.FLOORS.Length; i++)
            {
                if (MainWindow.FLOORS[i] != null)
                {
                    for (int j = 0; j < MainWindow.FLOORS[i].Count; j++)
                    {
                        ww1 = ww1 + MainWindow.FLOORS[i][j].Weight;
                    }
                }
            }
            for (int i = 0; i < obj1.Passangers.Count; i++) { ww2 = ww2 + obj1.Passangers[i].Weight; }

            MainWindow.MW.Dispatcher.Invoke(() => MainWindow.MW.SumWeight_Board.Text = $"   Общий вес: {ww1+ww2} кг.");
        }
        public IT() { ridesCount = 0; emptyRidesCount = 0; generalWeight = 0; genPasCount = 0; }
    }

    public enum DoorsState { Open = 1,Close =0 }
    public class Passenger
    {
        private int based_floor;
        private int target_floor;
        private int weight;
        private int id;
        public int BasedFloor { get; set; }
        public int TargetFloor { get; set; }
        public int Weight { get; set; }
        public int ID { get; set; }
        public Passenger() { }
        public Passenger(int basedFloor, int weight, int target_floor)
        {
            BasedFloor = basedFloor;
            Weight = weight;
            TargetFloor = target_floor;
        }
    }
    public class Elevator
    {
        private List<Passenger> passangers;
        private bool is_moving;
        private int floor_direction;
        private DoorsState doors_state;
        private int assecible_weight;
        private int liftpos;

        public int CurrentWeight { get; set; }

        public int LiftPos { get {return liftpos; } set { liftpos = value; } }
        public bool IsMoving { get { return is_moving; } set { is_moving = value; } }
        public int FloorDirection { get { return floor_direction; } set { floor_direction = value; } }
        public DoorsState DoorsState { get { return doors_state; } set { doors_state = value; } }
        public int AssecibleWeight { get { return assecible_weight; } set { assecible_weight = value; } }
        public List<Passenger> Passangers {get { return passangers; } set { passangers = value; } }

        public Elevator() { }
        public Elevator(bool is_moving, DoorsState doors_state, int assecible_weight, int liftpos)
        {
            this.passangers = new List<Passenger>();
            IsMoving = is_moving;
            DoorsState = doors_state;
            AssecibleWeight = assecible_weight;
            LiftPos = liftpos;
            MainWindow.TaR.InformationTargeting(this);
        }

        public void StartMoving()
        {
            this.is_moving = true;
            MainWindow.TaR.ridesCount++;
            if (this.Passangers.Count == 0) { MainWindow.TaR.emptyRidesCount++; }
            MainWindow.TaR.InformationTargeting(this);
            return;
        }
        public Elevator StopMoving() { this.is_moving = false; MainWindow.TaR.InformationTargeting(this); return this; }
        public Elevator OpenDoors() { this.doors_state = DoorsState.Open; MainWindow.TaR.InformationTargeting(this); return this; }
        public Elevator CloseDoors() { this.doors_state = DoorsState.Close; MainWindow.TaR.InformationTargeting(this); return this; }
        public Elevator EnterPassenger(List<Passenger> passengers)
        {
            for (int i = 0; i < passengers.Count; i++)
            { this.Passangers.Add(passengers[i]);this.CurrentWeight = this.CurrentWeight + passengers[i].Weight; }
            MainWindow.TaR.generalWeight += this.CurrentWeight;
            MainWindow.FLOORS[passengers[0].BasedFloor] = new List<Passenger>();
            MainWindow.TaR.InformationTargeting(this);
            return this;
        }
        public Elevator UnloadPassenger(Passenger passenger)
        {
            this.Passangers.Remove(passenger);
            passenger.BasedFloor = this.LiftPos;
            if(this.CurrentWeight != 0) { this.CurrentWeight = this.CurrentWeight - passenger.Weight; }
            MainWindow.FLOORS[this.LiftPos] = new List<Passenger>();
            MainWindow.FLOORS[this.LiftPos].Add(passenger);
            MainWindow.TaR.InformationTargeting(this);
            return this;
        }

    }
}
