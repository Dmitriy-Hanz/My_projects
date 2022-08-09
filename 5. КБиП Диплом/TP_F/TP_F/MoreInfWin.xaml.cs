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
using System.Windows.Shapes;

namespace TP_F
{
    public partial class MoreInfWin : Window
    {
        public static Komp kTemp;
        public MoreInfWin()
        {
            InitializeComponent();
            if (kTemp.cpu == null)
            { Processor_CAN.Visibility = Visibility.Hidden; Processor_GB.Height = 27; }

            DataContext = kTemp;

            for (int i = 0; i < kTemp.videoAdapters.Count; i++)//Видео адаптеры
            {
                FACECreator.CreateVideoCard(VideoCardsList_SP, kTemp.videoAdapters[i]);
                if (i + 1 != kTemp.videoAdapters.Count)
                { VideoCardsList_SP.Children.Add(new Separator { Height = 5, Margin = new Thickness(0), Background = Brushes.Black }); }
            }
            SetHeightGB("video");// расчет размеров группы видео

            for (int i = 0; i < kTemp.disks.Count; i++)//Физические диски
            {
                FACECreator.CreateDisk(DisksList_SP, kTemp.disks[i]);
                if (i + 1 != kTemp.disks.Count)
                { DisksList_SP.Children.Add(new Separator { Height = 5, Margin = new Thickness(0), Background = Brushes.Black }); }
            }
            SetHeightGB("disk");// расчет размеров группы дисков

            for (int i = 0; i < kTemp.systems.Count; i++)//Операционные системы
            {
                FACECreator.CreateSystem(SystemsList_SP, kTemp.systems[i]);
                if (i + 1 != kTemp.systems.Count)
                { SystemsList_SP.Children.Add(new Separator { Height = 5, Margin = new Thickness(0), Background = Brushes.Black }); }
            }
            SetHeightGB("system");
        }

        private void Close_B_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void SetHeightPanel()
        {
            KompInfPanel_BOR.Height = General_GB.Height + Processor_GB.Height + VideoDevices_GB.Height + Disks_GB.Height + Systems_GB.Height + 30 + 20;
            EquipInfList_WP.Height = KompInfPanel_BOR.Height + 20;
        }
        public void SetHeightGB(string gbName)
        {
            switch (gbName)
            {
                case "video":
                    {
                        if (kTemp.videoAdapters.Count == 0) { VideoDevices_GB.Height = 27; break; }
                        VideoDevices_GB.Height = 30 + (kTemp.videoAdapters.Count - 1) * 5 + kTemp.videoAdapters.Count * 71;
                        break;
                    }
                case "disk":
                    {
                        if (kTemp.disks.Count == 0) { Disks_GB.Height = 27; break; }
                        Disks_GB.Height = kTemp.disks.Count * 71 + (kTemp.disks.Count - 1) * 5 + 30;
                        break;
                    }
                case "system":
                    {
                        if (kTemp.systems.Count == 0) { Systems_GB.Height = 27; break; }
                        Systems_GB.Height = kTemp.systems.Count * 53 + (kTemp.systems.Count - 1) * 5 + 30;
                        break;
                    }
            }
            SetHeightPanel();
        }
        private void CreateReport_B_Click(object sender, RoutedEventArgs e)
        {
            ExcelReport EXR = new ExcelReport();
            EXR.CreateReport_Komputer(kTemp);
        }
    }
}

//ПОМОЙКА
//Код-шаблон для элемента списка компа:

        //<Canvas Height = "71" Margin="10,476,10,125" Visibility="Visible">
        //    <Image HorizontalAlignment = "Left" Height="64" VerticalAlignment="Top" Width="64" Source="images/Icon_VideoCard.bmp"/>
        //    <Label x:Name="KI_VC_Name_L" Content="Intel(R) HD Graphics Family" FontFamily="Arial Unicode MS" Foreground="#FF2491B9" Width="394" Height="17" Padding="0"  Canvas.Left="69" FontWeight="Bold" FontSize="12" />
        //    <Label Content = "Интерфейс:   " FontFamily="Arial Unicode MS" Foreground="#FF686868" Height="18" Padding="0" Canvas.Left="69" Canvas.Top="17" FontWeight="Bold" FontSize="12"/>
        //    <Label Content = "Тип носителя:   " FontFamily="Arial Unicode MS" Foreground="#FF686868" Height="18" Padding="0" Canvas.Left="69" Canvas.Top="35" FontWeight="Bold" FontSize="12"/>
        //    <Label Content = "Объем памяти:   " FontFamily="Arial Unicode MS" Foreground="#FF686868" Height="18" Padding="0" Canvas.Left="69" Canvas.Top="53" FontWeight="Bold" FontSize="12"/>
        //    <Label x:Name="KI_VC_Proc_L" Content="Dick" FontFamily="Arial Unicode MS" Foreground="#FF686868" Width="315" Height="18" Padding="0" Canvas.Left="148" Canvas.Top="17" VerticalContentAlignment="Center" FontSize="12" />
        //    <Label x:Name="KI_VC_DriverVer_L" Content="Dick" FontFamily="Arial Unicode MS" Foreground="#FF686868" Width="302" Height="18" Padding="0" Canvas.Left="161" Canvas.Top="35" VerticalContentAlignment="Center" FontSize="12" />
        //    <Label x:Name="KI_VC_ramValue_L" Content="Dick" FontFamily="Arial Unicode MS" Foreground="#FF686868" Width="296" Height="18" Padding="0"  Canvas.Left="167" Canvas.Top="53" VerticalContentAlignment="Center" FontSize="12" />
        //</Canvas>