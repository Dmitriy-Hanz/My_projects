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
using System.Windows.Threading;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace PRAKTIKA_KURS
{
    public partial class MainWindow
    {
        public static CreatePasWin_F CreatePasWin;
        public static ExportWin ReporterWin;
        public static Manual ManualWin;
        public static MainWindow MW;
        public static IT TaR;
        public static Elevator Лифт;
        Thread Mimic;
        Thread StopMimic;

        public static List<Passenger> Passmas;
        public static List<TextBox> Floors;
        public static List<Passenger>[] FLOORS = new List<Passenger>[26];

        public MainWindow()
        {
            TaR = new IT();
            MW = this;
            InitializeComponent();
            Floors = new List<TextBox>();
            for (int i = 0; i < 25; i++)
            {
                Floors.Add
                    (new TextBox
                    {
                        IsReadOnly = true,
                        IsReadOnlyCaretVisible = false,
                        Height = 30,
                        Width = 30,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5, 5, 0, 0),
                        BorderBrush = Brushes.Black,
                        Text=$"{i+1}"
                    }
                    );
            }
            for (int i = 0; i < 9; i++)
            {
                FloorContainer1_Board.Children.Add(Floors[i]);
            }
            for (int i = 9; i < 18; i++)
            {
                FloorContainer2_Board.Children.Add(Floors[i]);
            }
            for (int i = 18; i < 25; i++)
            {
                FloorContainer3_Board.Children.Add(Floors[i]);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartSimulation_B_Click(object sender, RoutedEventArgs e)
        {
            StartSimulation_B.IsEnabled = false;
            StopSimulation_B.IsEnabled = true;
            CreateReport_B.IsEnabled = false;
            Pause_B.IsEnabled = true;
            Mimic = new Thread(SimulationThread);
            Mimic.Start();
        }

        private void SimulationThread()
        {
            Random rand = new Random();
            Passmas = new List<Passenger>();
            FLOORS = new List<Passenger>[26];
            Thread.Sleep(1000);
            Dispatcher.Invoke(() => InfoBoard.Text = InfoBoard.Text + "Симуляция начата\n");
            Thread.Sleep(2000);

            Лифт = new Elevator(false, DoorsState.Close, 400,1);
            Dispatcher.Invoke(() => InfoBoard.Text = InfoBoard.Text + "Создан лифт\n");

            int Fi=0;bool hflag=false;
 W:         while (true)
            {
                hflag = false;
                Thread.Sleep(2000);
                FLOORS[0] = new List<Passenger>();
                for (int i = 0; i < FLOORS.Length; i++)
                {
                    if(FLOORS[i] != null){if (FLOORS[i].Count != 0){ hflag = true; Fi = i; }}
                }

                if (hflag == false)
                {
                    for (int i = 0; i < rand.Next(1, 6); i++) { Passmas.Add(new Passenger(0, rand.Next(50, 80), rand.Next(1, 26))); TaR.genPasCount++; }
                    TaR.InformationTargeting(Лифт);
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "Создан пассажир(пассажиры)\n"; InfoBoard.ScrollToEnd(); });

                    Thread.Sleep(5000);

                    Fi = Passmas[0].BasedFloor = rand.Next(1, 26);
                    for (int i = 1; i < Passmas.Count; i++) { Passmas[i].BasedFloor = Passmas[0].BasedFloor; }
                    FLOORS[Fi] = Passmas;
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + $"Пассажир нажимает на кнопку на {FLOORS[Passmas[0].BasedFloor][0].BasedFloor} этаже\n"; InfoBoard.ScrollToEnd(); });
                    Лифт.FloorDirection = FLOORS[Fi][0].BasedFloor; TaR.InformationTargeting(Лифт);
                }
                Thread.Sleep(2000);

                if (Fi != Лифт.LiftPos)
                {
                    Лифт.CloseDoors();
                    Thread.Sleep(2000);
                    Лифт.StartMoving();
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "Лифт начинает движение к пассажиру\n"; InfoBoard.ScrollToEnd(); });

                    if (Лифт.LiftPos < Fi)
                    {
                        for (int i = Лифт.LiftPos; i < Fi; i++, Лифт.LiftPos++)
                        {
                            TaR.InformationTargeting(Лифт);
                            Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + ". "; InfoBoard.ScrollToEnd(); });
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        for (int i = Лифт.LiftPos; i > Fi; i--, Лифт.LiftPos--)
                        {
                            TaR.InformationTargeting(Лифт);
                            Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + ". "; InfoBoard.ScrollToEnd(); });
                            Thread.Sleep(5000);
                        }
                    }
                }
                Лифт.StopMoving();
                Thread.Sleep(2000);
                Лифт.OpenDoors();
                Dispatcher.Invoke(() => {InfoBoard.Text = InfoBoard.Text + "\nЛифт прибыл на этаж"; InfoBoard.ScrollToEnd();});
                Thread.Sleep(2000);

                Лифт.EnterPassenger(FLOORS[Fi]);
                Dispatcher.Invoke(() => {InfoBoard.Text = InfoBoard.Text + "\nПассажир(ы) заходит(заходят) в лифт"; InfoBoard.ScrollToEnd();});
                Thread.Sleep(2000);

                Dispatcher.Invoke(() => {InfoBoard.Text = InfoBoard.Text + "\nРасчет веса пассажира(ов)"; InfoBoard.ScrollToEnd();});
                while (Лифт.AssecibleWeight < Лифт.CurrentWeight & Лифт.Passangers.Count == 1)
                {
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "\nпассажир слишком толстый\n"; InfoBoard.ScrollToEnd(); });
                    Thread.Sleep(2000);
                    Лифт.UnloadPassenger(Лифт.Passangers[0]);
                    FLOORS[Лифт.LiftPos] = new List<Passenger>();
                    Лифт.CloseDoors();
                    goto W;
                }
                while (Лифт.AssecibleWeight < Лифт.CurrentWeight)
                { Лифт.UnloadPassenger(Лифт.Passangers[0]); }
                
                
                Thread.Sleep(2000);

                Dispatcher.Invoke(() => {InfoBoard.Text = InfoBoard.Text + $"\nПассажир(ы) нажимает(ют) на кнопку "; });
                for (int i = 0; i < Лифт.Passangers.Count; i++)
                {
                    while (Лифт.Passangers[i].TargetFloor == Лифт.LiftPos || Лифт.Passangers[i].TargetFloor == 0)
                    {Лифт.Passangers[i].TargetFloor = rand.Next(1, 26);}
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + $"{Лифт.Passangers[i].TargetFloor} "; });
                }
                Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + $"этажа"; InfoBoard.ScrollToEnd(); });

                int imax=0,max=0,tf=0;

                int lpc = Лифт.Passangers.Count;
                while(Лифт.Passangers.Count != 0)
                {
                    max = tf = 0;
                    foreach (Passenger item in Лифт.Passangers)
                    { if (item.TargetFloor > max) { max = item.TargetFloor; imax = Лифт.Passangers.IndexOf(item); } }

                    Thread.Sleep(2000);
                    Лифт.FloorDirection = Лифт.Passangers[imax].TargetFloor;
                    Thread.Sleep(2000);

                    Лифт.CloseDoors();
                    Thread.Sleep(2000);
                    Лифт.StartMoving();
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "\nЛифт начинает движение на целевой этаж\n"; InfoBoard.ScrollToEnd(); });

                    if (Лифт.LiftPos < Лифт.Passangers[imax].TargetFloor)
                    {
                        for (int j = Лифт.LiftPos; j < Лифт.Passangers[imax].TargetFloor; j++, Лифт.LiftPos++)
                        {
                            TaR.InformationTargeting(Лифт);
                            Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + ". "; InfoBoard.ScrollToEnd(); });
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        for (int j = Лифт.LiftPos; j > Лифт.Passangers[imax].TargetFloor; j--, Лифт.LiftPos--)
                        {
                            TaR.InformationTargeting(Лифт);
                            Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + ". "; InfoBoard.ScrollToEnd(); });
                            Thread.Sleep(5000);
                        }
                    }

                    Лифт.StopMoving();
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "\nЛифт прибыл на этаж"; InfoBoard.ScrollToEnd(); });
                    Thread.Sleep(2000);
                    Лифт.OpenDoors();
                    Thread.Sleep(2000);

                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "\nПассажир выходит из лифта"; InfoBoard.ScrollToEnd(); });
                    tf = Лифт.Passangers[imax].TargetFloor;
                    for (int i = 0; i < Лифт.Passangers.Count; i++)
                    {
                        if(Лифт.Passangers[i].TargetFloor == Лифт.LiftPos)
                        {
                            Лифт.UnloadPassenger(Лифт.Passangers[i]); i = -1;
                        }
                    }
                    
                    Thread.Sleep(2000);
                    Лифт.CloseDoors();
                    Thread.Sleep(3000);
                    Dispatcher.Invoke(() => { InfoBoard.Text = InfoBoard.Text + "\nУдаление пассажира\n"; InfoBoard.ScrollToEnd(); });
                    FLOORS[tf] = new List<Passenger>();
                    TaR.InformationTargeting(Лифт);
                }
                Passmas = new List<Passenger>();
            }
        }

        private void StopSimulation_B_Click(object sender, RoutedEventArgs e)
        {
            StartSimulation_B.IsEnabled = true;
            StopSimulation_B.IsEnabled = false;
            CreateReport_B.IsEnabled = true;
            StopMimic = new Thread(StopSimulationThread);
            StopMimic.Start();
            InfoBoard.Text = "";
            DoorsState_Board.Text = "";
            LiftPos_Board.Text = "";
            MoveState_Board.Text = "";
            PassangersCount_Board.Text = "";
            PassCountFloor_Board.Text = "";
            SumWeight_Board.Text = "";
            PassFloors_Board.Text = "";
            for (int i = 0; i < 26; i++)
            {
                FLOORS[i] = new List<Passenger>();
            }
            for (int i = 0; i < 25; i++)
            {
                Dispatcher.Invoke(() => Floors[i].Background = Brushes.White);
            }
        }
        private void StopSimulationThread()
        {
            if (Mimic.ThreadState != ThreadState.Running & Mimic.ThreadState != ThreadState.WaitSleepJoin)
            {
                Mimic.Resume();
            }
            Mimic.Abort();

            Dispatcher.Invoke(() => Continue_B.IsEnabled = false);
            Dispatcher.Invoke(() => CreatePas_B.IsEnabled = false);
            Dispatcher.Invoke(() => Pause_B.IsEnabled = false);
            var bc = new BrushConverter();
            Dispatcher.Invoke(() => { PausePanel.Content = ""; PausePanel.Background = (Brush)bc.ConvertFrom("#FFE2E2E2"); });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Pause_B_Click(object sender, RoutedEventArgs e)
        {
            Continue_B.IsEnabled = true;
            CreatePas_B.IsEnabled = true;
            Pause_B.IsEnabled = false;
            Mimic.Suspend();
            Dispatcher.Invoke(() => PausePanel.Content = "Симуляция приостановлена");
            Dispatcher.Invoke(() => PausePanel.Background = Brushes.Red);
        }

        private void Continue_B_Click(object sender, RoutedEventArgs e)
        {
            CreatePas_B.IsEnabled = false;
            Continue_B.IsEnabled = false;
            Pause_B.IsEnabled = true;
            Mimic.Resume();
            var bc = new BrushConverter();
            Dispatcher.Invoke(() => { PausePanel.Content = ""; PausePanel.Background = (Brush)bc.ConvertFrom("#FFE2E2E2"); });
        }

        private void CreatePas_B_Click(object sender, RoutedEventArgs e)
        {
            CreatePasWin = new CreatePasWin_F();
            CreatePasWin.ShowDialog();
        }

        private void CreateReport_B_Click(object sender, RoutedEventArgs e)
        {
            ReporterWin = new ExportWin();
            ReporterWin.ShowDialog();
        }

        private void CallManual_B_Click(object sender, RoutedEventArgs e)
        {
            ManualWin = new Manual();
            ManualWin.Show();
        }
    }
}
