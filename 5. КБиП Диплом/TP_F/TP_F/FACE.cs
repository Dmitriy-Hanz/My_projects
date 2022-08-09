using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using System.Windows.Shapes;

namespace TP_F
{
    class FACECreator: FACEHandlers
    {
        public static void RebootWorkPlaceList(Kabinet k)
        {
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.Visibility = Visibility.Hidden;
            MainWindow.Auditories_W.WorkPlacesList_WP.Children.Clear();
            MainWindow.Auditories_W.WorkPlacesList_WP.Height = 6;
            for (int i = 0; i < k.workPlaces.Count; i++)
            {
                CreateWorkPlaceMP(MainWindow.Auditories_W.WorkPlacesList_WP, $"workPlaces[{i}]");
                k.workPlaces[i].UpdateFACE();
            }
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = k;
        }
        public static void RebootInfoAuditories(WorkPlace wp)
        {
            if (wp.monitor == null)
            {
                MainWindow.Auditories_W.MonicInf_Panel.Uid = "";
                MainWindow.Auditories_W.MonicInf_Panel.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.AddMonitor_B.Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow.Auditories_W.MonicInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.MonicInf_Panel.Uid = wp.monitor.monitorID.ToString();
            }
            if (wp.kompukter == null)
            {
                MainWindow.Auditories_W.KompInf_Panel.Uid = "";
                MainWindow.Auditories_W.KompInf_Panel.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.AddKomputer_B.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = false;
            }
            else
            {
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = true;
                MainWindow.Auditories_W.KompInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.KompInf_Panel.Uid = wp.kompukter.kompID.ToString();
            }
            if (wp.kompukter == null & wp.monitor == null)
            { MainWindow.Auditories_W.Edit_B.IsEnabled = false; }
            else
            { MainWindow.Auditories_W.Edit_B.IsEnabled = true; }
        }

        public static void CreateKabinetMP(WrapPanel list, string Kpath)
        {
            Border border = CreateBorder_GradientMid(204, 68, Color.FromRgb(163, 255, 163)); //Создаем первую границу (та, что контейнер для всего остального дерьма)

            Canvas canvas = new Canvas { Margin = new Thickness(-1),Width = 204,Height=68 };//canvas для остального графического мусора

            Border blurBorder = new Border      //другой border, но с blur-эффектом
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Height = 68,
                Width=204,
                Effect = new System.Windows.Media.Effects.BlurEffect { RenderingBias=System.Windows.Media.Effects.RenderingBias.Quality }
            };
            Canvas.SetTop(blurBorder,0);        // установка положения в canvas для blurBorder
            Canvas.SetLeft(blurBorder, 0);
            canvas.Children.Add(blurBorder);    // добавление blurBorder в canvas

            Label label = new Label             //cоздание label для заголовка (заголовок с названием кабинета)
            {
                FontFamily = new FontFamily("Arial Rounded MT Bold"),
                FontSize = 14,
                Padding = new Thickness(0,7,12,7),
                Foreground = new SolidColorBrush(Color.FromRgb(36, 145, 185)),
                Height = 34,
                Width=204,
                VerticalAlignment= VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            label.SetBinding(Label.ContentProperty, new Binding($"{Kpath}.name"));
            Canvas.SetTop(label, 0);            // установка положения в canvas для label-заголовка
            Canvas.SetLeft(label, 0);
            canvas.Children.Add(label);         // добавление label-заголовка в canvas

            Button button = new Button          // кнопка с пикчей удаления
            {
                Height = 20,
                Width = 20,
                Background = Brushes.White,
                Content = new Image { Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Button_Delete.bmp")) },
            };
            button.SetBinding(Button.UidProperty, new Binding($"{Kpath}.kabinetID"));
            button.Click += DeleteKabinet_B_Click;
            Canvas.SetLeft(button, 3);
            Canvas.SetTop(button, 3);
            canvas.Children.Add(button);

            Separator separator = new Separator { Height = 5, Width = 204 };//просто разделитель
            Canvas.SetTop(separator, 29);       // установка положения в canvas для простого разделителя
            Canvas.SetLeft(separator, 0);
            canvas.Children.Add(separator);     // непосредственно добавление в canvas разделителя

            label = new Label                   //cоздание label-мини-заголовка для количества рабочих мест
            {
                Content = "Кол-во рабочих мест: ",
                FlowDirection = FlowDirection.LeftToRight,
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(104, 104, 104)),
                Padding = new Thickness(0, 4, 12, 4),
                Height = 29,
                Width = 204,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            Canvas.SetTop(label, 36);           // установка положения в canvas для label-мини-заголовка
            Canvas.SetLeft(label, 36);
            canvas.Children.Add(label);         // добавление label в canvas

            label = new Label                   //cоздание label для количества рабочих мест
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(104, 104, 104)),
                Padding = new Thickness(0, 4, 1, 4),
                Height = 29,
                Width = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            label.SetBinding(Label.ContentProperty, new Binding($"{Kpath}.workPlaceCount"));
            Canvas.SetTop(label, 36);           // установка положения в canvas для label с рабочими местами
            Canvas.SetLeft(label, 30);
            canvas.Children.Add(label);         // добавление label в canvas

            ToggleButton toggleButton = new ToggleButton      //переключатель - контейнер для основного border контейнера
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0)),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0)),
                Height = 76,
                Width = 215,
                Padding = new Thickness(3, 3, 3, 3),
            };

            border.Child = canvas;                              // запихиваем нагруженный графоном canvas в основной border
            toggleButton.Click += AuditoriesMI_Click;           //привязка к событию клика функции-обработчика
            toggleButton.SetBinding(ToggleButton.UidProperty, new Binding($"{Kpath}.kabinetID")); //привязка kabinetID к Uid
            toggleButton.Content = border;
            
            list.Children.Add(toggleButton);                    //добавляем основной border в меню с кабинетами
            list.Height = (list.Children.Count) * 76 + 6;   //настройка высоты списка после добавления
        }
        public static void RemoveKabinetMP(WrapPanel list, Kabinet kab)
        {
            foreach (ToggleButton item in list.Children)
            {
                if (item.Uid == kab.kabinetID)
                {
                    list.Children.Remove(item);
                    App.sysH.kabinets.Remove(kab);
                    foreach (WorkPlace item2 in kab.workPlaces)
                    {
                        if (item2.kompukter != null) { App.sysH.Inventory.Facilities.Add(item2.kompukter); }
                        if (item2.monitor != null) { App.sysH.Inventory.Facilities.Add(item2.monitor); }
                    }
                    SysHelperDB.ExecCom($"DELETE FROM РабочееМесто WHERE ID_Кабинета_F = {kab.kabinetID}");
                    SysHelperDB.ExecCom($"DELETE FROM Кабинет WHERE ID_Кабинета_P = {kab.kabinetID}");
                    list.Height -= 75;
                    MainWindow.Auditories_W.SetInf_SP.Visibility = Visibility.Hidden;
                    MainWindow.Auditories_W.WorkPlacesList_WP.Children.Clear();
                    MainWindow.Auditories_W.WorkPlacesList_WP.Height = 6;
                    return;
                }
            }
        }

        public static void CreateWorkPlaceMP(WrapPanel list, string WPpath)
        {
            Border border = CreateBorder_GradientMid(230, 67, Color.FromRgb(0xC2, 0xC8, 0xFF)); //Создаем первую границу (та, что контейнер для всего остального дерьма)

            Canvas canvas = new Canvas { Margin = new Thickness(-1), Width = 230, Height = 67};//canvas для остального графического мусора
            Border blurBorder = new Border      //другой border, но с blur-эффектом
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Height = 67,
                Width = 230,
                Effect = new System.Windows.Media.Effects.BlurEffect { RenderingBias = System.Windows.Media.Effects.RenderingBias.Quality }
            };
            Canvas.SetTop(blurBorder, 0);       // установка положения в canvas для blurBorder
            Canvas.SetLeft(blurBorder, 0);
            canvas.Children.Add(blurBorder);    // добавление blurBorder в canvas

            Image img = new Image { Height = 53, Width = 53 };   //cоздание image для графики (рисунка компа)
            img.SetBinding(Image.SourceProperty, new Binding($"{WPpath}.ImgSource"));

            Canvas.SetTop(img, 7);              // установка положения в canvas для img
            Canvas.SetRight(img, 7);
            canvas.Children.Add(img);           // добавление img в canvas

            Label label = new Label             //cоздание label для заголовка (заголовок с названием места)
            {
                FontFamily = new FontFamily("Arial Rounded MT Bold"),
                FontSize = 12,
                Padding = new Thickness(0, 10, 0, 4),
                Foreground = new SolidColorBrush(Color.FromRgb(36, 145, 185)),
                Height = 31,
                Width = 135,
                VerticalAlignment = VerticalAlignment.Top,
                FlowDirection = FlowDirection.LeftToRight,
            };
            label.SetBinding(Label.ContentProperty, new Binding ($"{WPpath}.name"));//привязка данных
            Canvas.SetTop(label, 0);            // установка положения в canvas для textBlock-заголовка
            Canvas.SetRight(label, 68);
            canvas.Children.Add(label);         // добавление textBlock-заголовка в canvas

            Button button = new Button          // кнопка с пикчей удаления
            {
                Height = 20,
                Width = 20,
                Background = Brushes.White,
                Content = new Image { Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Button_Delete.bmp")) }
            };
            button.SetBinding(Button.UidProperty, new Binding($"{WPpath}.workPlaceID"));
            button.Click += DeleteWorkPlace_B_Click;
            Canvas.SetLeft(button, 3);
            Canvas.SetTop(button, 3);
            canvas.Children.Add(button);

            Separator separator = new Separator { Height = 5, Width = 155 };//просто разделитель
            Canvas.SetTop(separator, 31);       // установка положения в canvas для простого разделителя
            Canvas.SetRight(separator, 68);
            canvas.Children.Add(separator);     // непосредственно добавление в canvas разделителя

            label = new Label                   //cоздание label-мини-заголовка для оборудования
            {
                Content = ":Оборудование",
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
                Padding = new Thickness(0),
                Height = 13,
                Width = 153,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            Canvas.SetTop(label, 37);           // установка положения в canvas для label-мини-заголовка
            Canvas.SetRight(label, 68);
            canvas.Children.Add(label);         // добавление label в canvas

            label = new Label                   //cоздание label- для самого оборудования
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 10,
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
                Padding = new Thickness(0),
                Height = 13,
                Width = 153,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };
            label.SetBinding(Label.ContentProperty, new Binding ($"{WPpath}.extraInf"));//привязка данных
            Canvas.SetTop(label, 50);           // установка положения в canvas для label с оборудованием
            Canvas.SetRight(label, 68);
            canvas.Children.Add(label);         // добавление label в canvas

            ToggleButton toggleButton = new ToggleButton      //переключатель-контейнер для всего остального
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0)),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0)),
                Height = 75,
                Width = 240,
                Padding = new Thickness(3, 3, 3, 3)
            };

            border.Child = canvas;              // запихиваем нагруженный графоном canvas в основной border
            toggleButton.Content = border;
            toggleButton.SetBinding(ToggleButton.UidProperty, new Binding($"{WPpath}.workPlaceID")); //привязка workPlaceID к Uid
            list.Height = (list.Children.Count+1)*75+6;             //настройка высоты списка перед добавлением

            toggleButton.Click += WorkPlacesItem_Click; //подключение событий нажатия, наведения мыши и т.д.
            list.Children.Add(toggleButton);
        }
        public static void RemoveWorkPlaceMP(WrapPanel list, WorkPlace wp)
        {
            foreach (ToggleButton item in list.Children)
            {
                if (item.Uid == wp.workPlaceID.ToString())
                {
                    list.Children.Remove(item);
                    App.sysH.kabinets[App.sysH.kabinets.IndexOf(selectedK)].workPlaces.Remove(wp);
                    SysHelperDB.ExecCom($"DELETE FROM РабочееМесто WHERE ID_РабМеста_P = {wp.workPlaceID}");
                    if (wp.kompukter != null) { App.sysH.Inventory.Facilities.Add(wp.kompukter); }
                    if (wp.monitor != null) { App.sysH.Inventory.Facilities.Add(wp.monitor); }
                    list.Height -= 75;
                    return;
                }
            }
            
        }

        public static void CreateInventoryItem(WrapPanel list, Facility fc, string bindingPath)
        {
            Border border = CreateBorder_GradientMid(162, 46, Color.FromRgb(0xC2, 0xC8, 0xFF)); //Создаем первую границу (та, что контейнер для всего остального дерьма)

            Canvas canvas = new Canvas { Margin = new Thickness(-1)};//canvas для остального графического мусора

            Border blurBorder = new Border      //другой border, но с blur-эффектом
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Height = 46,
                Width = 162,
                Effect = new System.Windows.Media.Effects.BlurEffect { RenderingBias = System.Windows.Media.Effects.RenderingBias.Quality }
            };
            canvas.Children.Add(blurBorder);    // добавление blurBorder в canvas

            Label label;                                          //cоздание label для заголовка (заголовок с типом оборудования)
            if (fc is Komp)
            { label = CreateLabel_BlueB("Компьютер", 20, 157, 14); }
            else
            { label = CreateLabel_BlueB("Монитор", 20, 157, 14); }
            Canvas.SetTop(label, 5);                              // установка положения в canvas для label-заголовка
            Canvas.SetLeft(label, 5);
            canvas.Children.Add(label);                           // добавление label-заголовка в canvas

            label = new Label                                     //cоздание label-заголовка для ИН
            {
                Content = "Инвентарный №: ",
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
                Padding = new Thickness(0),
                Height = 13,
                Width = 157,
            };
            Canvas.SetLeft(label, 5);           // установка положения в canvas для label с прочим текстом
            Canvas.SetTop(label, 25);
            canvas.Children.Add(label);         // добавление label с текстом в canvas

            label = new Label                                     //cоздание поля для ИН
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
                Padding = new Thickness(0),
                Height = 13,
                Width = 157,
            };
            label.SetBinding(Label.ContentProperty, new Binding(bindingPath));//установка привязки 
            Canvas.SetLeft(label, 90);          // установка положения в canvas для label с прочим текстом
            Canvas.SetTop(label, 25);
            canvas.Children.Add(label);         // добавление label с текстом в canvas


            border.Child = canvas;              // запихиваем нагруженный графоном canvas в основной border

            ToggleButton toggleButton = new ToggleButton    //переключатель-контейнер для другого основного контейнера    
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(1),
                Background = Brushes.White,
                Height = 52,
                Width = 168,
                Padding = new Thickness(2),
            };
            toggleButton.Content = border;

            toggleButton.Uid = fc.uID.ToString() + "_" + fc.fType;         // присваиваем уникальный id оборудования + тип элементу
            list.Children.Add(toggleButton);                         // добавляем основной border в список оборудования

            toggleButton.Click += InventoryFacilityItem_Click; //не УДАЛИТЬ. зарекомендовавшая себя эксперементальная хрень
        }

        public static void CreateVideoCard(StackPanel list, VideoAdapter va)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 71 };//начало

            Image img = new Image { //пикча
                Height = 53,
                Width = 53,
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_VideoCard.bmp")),
            };
            canvas.Children.Add(img);

            Label label = CreateLabel_BlueB(va.name, 17, 394, 12);//заголовок
            Canvas.SetLeft(label, 69);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Граф.процессор:   ", 16);//дальше подзаголовки всякой левой инфы
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Версия драйвера:   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Объем RAM-памяти (МБ):   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 53);
            canvas.Children.Add(label);


            label = CreateLabel_12Grey(va.videoProcessor);//инфа
            Canvas.SetLeft(label, 183);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12Grey(va.driverVersion);
            Canvas.SetLeft(label, 186);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);

            label = CreateLabel_12Grey(va.adapterRAM.ToString());
            Canvas.SetLeft(label, 225);
            Canvas.SetTop(label, 53);
            canvas.Children.Add(label);

            list.Children.Add(canvas);
        }
        public static void CreateDisk(StackPanel list, Disk ds)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 71 };//начало

            Image img = new Image
            { //пикча
                Height = 53,
                Width = 53,
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_Disk.bmp")),
            };
            canvas.Children.Add(img);

            Label label = CreateLabel_BlueB(ds.model, 17, 394, 12);//заголовок
            Canvas.SetLeft(label, 69);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Интерфейс:   ",16);//дальше подзаголовки всякой левой инфы
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Тип носителя:   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Объем памяти (ГБ):   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 53);
            canvas.Children.Add(label);


            label = CreateLabel_12Grey(ds._interface);//инфа
            Canvas.SetLeft(label, 148);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12Grey(ds.type);
            Canvas.SetLeft(label, 161);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);

            label = CreateLabel_12Grey(ds.memoryValue.ToString());
            Canvas.SetLeft(label, 190);
            Canvas.SetTop(label, 53);
            canvas.Children.Add(label);

            list.Children.Add(canvas);
        }
        public static void CreateSystem(StackPanel list, OS os)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 53,Width=463 };//начало

            Image img = new Image
            { //пикча
                Height = 42,
                Width = 42,
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System0.bmp")),
            };
            //img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System0.bmp"));
            if (os.name.Contains("inux") == true) { img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System2.bmp")); }
            if (os.name.Contains("indows") == true) { img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System1.bmp")); }
            canvas.Children.Add(img);

            Label label = CreateLabel_BlueB(os.name, 17, 416,12 );//заголовок
            Canvas.SetLeft(label, 47);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Версия:   ", 16);//подзаголовок версии
            Canvas.SetLeft(label, 47);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Архитектура:   ", 16);//подзаголовок архитектуры
            Canvas.SetLeft(label, 47);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);


            label = CreateLabel_12Grey(os.version);//версия винды
            Canvas.SetLeft(label, 103);
            Canvas.SetTop(label, 17);
            canvas.Children.Add(label);

            label = CreateLabel_12Grey(os.architecture);//архитектура
            Canvas.SetLeft(label, 133);
            Canvas.SetTop(label, 35);
            canvas.Children.Add(label);

            list.Children.Add(canvas);
        }
        public static void CreateVideoCardE(WrapPanel list, VideoAdapter va, string BindingPath)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 89,Width= 463 };//начало

            Image img = new Image//пикча видеокарты
            { 
                Height = 53,
                Width = 53,
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_VideoCard.bmp")),
            };
            canvas.Children.Add(img);

            Button button = new Button
            {
                Height = 20,
                Width = 20,
                Content = new Image { Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Button_Delete.bmp")) },
                Uid = va.adapterID.ToString()
            };
            button.Click += KI_VideoDevices_Delete_B_Click;
            Canvas.SetLeft(button, 443);
            canvas.Children.Add(button);

            Label label = CreateLabel_12GreyB("Граф.процессор:   ",20);//подзаголовки 
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 22);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Версия драйвера:   ", 20);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 44);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Объем RAM-памяти:   ", 20);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 66);
            canvas.Children.Add(label);

            TextBox textBox = CreateTextBox_BlueB(372, 20, 12, $"{BindingPath}.name");
            Canvas.SetLeft(textBox, 69);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(286, 20, $"{BindingPath}.videoProcessor");
            Canvas.SetLeft(textBox, 177);
            Canvas.SetTop(textBox, 22);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(280, 20, $"{BindingPath}.driverVersion");
            Canvas.SetLeft(textBox, 183);
            Canvas.SetTop(textBox, 44);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(263, 20, $"{BindingPath}.adapterRAM");
            Canvas.SetLeft(textBox, 200);
            Canvas.SetTop(textBox, 66);
            canvas.Children.Add(textBox);

            list.Children.Add(canvas);
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void CreateDiskE(WrapPanel list, Disk ds, string BindingPath)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 89,Width = 463 };//начало

            Image img = new Image
            { //пикча
                Height = 53,
                Width = 53,
                Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_Disk.bmp")),
            };
            canvas.Children.Add(img);

            Button button = new Button
            {
                Height = 20,
                Width = 20,
                Content = new Image { Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Button_Delete.bmp")) },
                Uid = ds.diskID.ToString()
            };
            button.Click += KI_Disks_Delete_B_Click;
            Canvas.SetLeft(button, 443);
            canvas.Children.Add(button);

            Label label = CreateLabel_12GreyB("Интерфейс:   ", 16);//подзаголовки
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 22);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Тип носителя:   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 44);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Объем памяти (ГБ):   ", 16);
            Canvas.SetLeft(label, 69);
            Canvas.SetTop(label, 66);
            canvas.Children.Add(label);

            TextBox textBox = CreateTextBox_BlueB(372, 20, 12, $"{BindingPath}.model");//заголовок
            Canvas.SetLeft(textBox, 69);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(315, 20, $"{BindingPath}._interface");//интерфейс
            Canvas.SetLeft(textBox, 148);
            Canvas.SetTop(textBox, 22);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(302, 20, $"{BindingPath}.type");//тип носителя
            Canvas.SetLeft(textBox, 161);
            Canvas.SetTop(textBox, 44);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(269, 20, $"{BindingPath}.memoryValue");//объем памяти
            Canvas.SetLeft(textBox, 194);
            Canvas.SetTop(textBox, 66);
            canvas.Children.Add(textBox);

            list.Children.Add(canvas);
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void CreateSystemE(WrapPanel list, OS os, string BindingPath)
        {
            Canvas canvas = new Canvas { Margin = new Thickness(0), Height = 42, Width = 463 };//начало

            Image img = new Image
            { //пикча
                Height = 42,
                Width = 42
            };
            img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System0.bmp"));
            if (os.name.Contains("inux") == true) { img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System2.bmp")); }
            if (os.name.Contains("indows") == true) { img.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Icon_System1.bmp")); }

            canvas.Children.Add(img);

            Button button = new Button
            {
                Height = 20,
                Width = 20,
                Content = new Image { Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/Button_Delete.bmp")) },
                Uid = os.osID.ToString()
            };
            button.Click += KI_Systems_Delete_B_Click;
            Canvas.SetLeft(button, 443);
            canvas.Children.Add(button);

            Label label = CreateLabel_12GreyB("Версия:   ", 16);//подзаголовки
            Canvas.SetLeft(label, 47);
            Canvas.SetTop(label, 22);
            canvas.Children.Add(label);

            label = CreateLabel_12GreyB("Разрядность:   ", 16);
            Canvas.SetLeft(label, 234);
            Canvas.SetTop(label, 22);
            canvas.Children.Add(label);

            TextBox textBox = CreateTextBox_BlueB(394, 20, 12, $"{BindingPath}.name");//название системы
            Canvas.SetLeft(textBox, 47);
            canvas.Children.Add(textBox);

            textBox = CreateTextBox_12Grey(126, 20, $"{BindingPath}.version");//интерфейс
            Canvas.SetLeft(textBox, 103);
            Canvas.SetTop(textBox, 22);
            canvas.Children.Add(textBox);

            ComboBox comboBox = new ComboBox
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
                Height=20,
                Width= 141,
                Padding = new Thickness(3,2,0,0)
            };
            comboBox.Items.Add(new ComboBoxItem { Content = "32-х разрядная", IsSelected = true });
            comboBox.Items.Add(new ComboBoxItem { Content = "64-х разрядная"});
            comboBox.Items.Add(new ComboBoxItem { Content = "86-х разрядная" });
            if (os.architecture != null)
            {
                if (os.architecture.Contains("32") == true) { ((ComboBoxItem)comboBox.Items[0]).IsSelected = true; }
                if (os.architecture.Contains("64") == true) { ((ComboBoxItem)comboBox.Items[1]).IsSelected = true; }
                if (os.architecture.Contains("86") == true) { ((ComboBoxItem)comboBox.Items[3]).IsSelected = true; }
            }
            Canvas.SetLeft(comboBox, 322);
            Canvas.SetTop(comboBox, 22);
            canvas.Children.Add(comboBox);

            list.Children.Add(canvas);
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void ResizeInventoryFacilityList()
        {
            if (MainWindow.Inventory_W.InventoryItems_WP.Children.Count != 0)
            { MainWindow.Inventory_W.InventoryItems_WP.Height = Math.Round(((double)MainWindow.Inventory_W.InventoryItems_WP.Children.Count) / 2, MidpointRounding.AwayFromZero) * 52; }
            else
            { MainWindow.Inventory_W.InventoryItems_WP.Height = 3; }
        }

        //дальше идут системные функции, они вам и нахер не нужны
        private static Label CreateLabel_12GreyB(string text, double h)
        {
            return new Label
            {
                Content = text,
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 12,
                Height=h,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
            };
        }
        private static Label CreateLabel_12Grey(string text)
        {
            return new Label
            {
                Content = text,
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = 12,
                Padding = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
            };
        }
        private static Label CreateLabel_BlueB(string text,double h,double w,int fontSize)
        {
            return new Label
            {
                Content = text,
                FontFamily = new FontFamily("Arial Rounded MT Bold"),
                FontSize = fontSize,
                Padding = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(0x24, 0x91, 0xB9)),
                Height = h,
                Width = w,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Left
            };
        }
        private static Border CreateBorder_GradientMid(double w,double h,Color color)
        {
            Border border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(156, 156, 156)),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = h,
                Width = w,
                VerticalAlignment = VerticalAlignment.Top,
                CornerRadius = new CornerRadius(5),
                ClipToBounds = true,
            };
            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop { Color = Color.FromRgb(255, 255, 255), Offset = 0.493 });
            gradientStops.Add(new GradientStop { Color = color, Offset = 1 });
            gradientStops.Add(new GradientStop { Color = color });
            border.Background = new LinearGradientBrush(gradientStops, new Point(1, 1), new Point(1, 0));
            return border;
        }
        private static TextBox CreateTextBox_BlueB(double w, double h, int fontSize,string binding)
        {
            TextBox textBox = new TextBox
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(0x24, 0x91, 0xB9)),
                Height = h,
                Width = w,
            };
            
            textBox.SetBinding(TextBox.TextProperty,new Binding { Path = new PropertyPath(binding,null), ValidatesOnDataErrors = true, NotifyOnValidationError = true, ValidatesOnExceptions = true });

            //ValidationRules.
            Validation.AddErrorHandler(textBox, ErrorsValidator);
            return textBox;
        }
        private static TextBox CreateTextBox_12Grey(double w, double h, string binding)
        {
            TextBox textBox = new TextBox
            {
                FontFamily = new FontFamily("Arial Unicode MS"),
                Width=w,
                Height =h,
                FontSize = 12,
                Padding = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(0x68, 0x68, 0x68)),
            };
            textBox.SetBinding(TextBox.TextProperty, new Binding { Path = new PropertyPath(binding, null), ValidatesOnDataErrors = true, NotifyOnValidationError = true, ValidatesOnExceptions = true });
            Validation.AddErrorHandler(textBox, ErrorsValidator);
            return textBox;
        }
    }

    class FACEHandlers
    {
        public static WorkPlace selectetWP;
        public static Kabinet selectedK;
        public static Facility selectedF;

        public static ToggleButton kabPrefObj = new ToggleButton();
        public static ToggleButton facPrefObj = new ToggleButton();
        public static ToggleButton wpPrefObj = new ToggleButton();

        public FACEHandlers() { }

        //ВСЯКИЕ ДРУГИЕ НАЖАТИЯ, НАВЕДЕНИЯ, ОБРАБОТЧИКИ ИЗ РАЗНЫХ ФОРМ
        protected static void AuditoriesMI_Click(object sender, RoutedEventArgs e)
        {
            if(((ToggleButton)sender).Parent == null) { return; }
            if (MainWindow.Auditories_W.EditKabinet_B.IsEnabled == false) { MainWindow.Auditories_W.EditKabinet_B.IsEnabled = true; }
            if (MainWindow.Auditories_W.AddWorkPlace_B.IsEnabled == false) { MainWindow.Auditories_W.AddWorkPlace_B.IsEnabled = true; }
            if (MainWindow.Auditories_W.MiniSearch_GB.Width == 254) { MainWindow.Auditories_W.MiniSearch_GB.Width = 507; }

            if (((ToggleButton)sender).IsChecked == false) { ((ToggleButton)sender).IsChecked = true; return; }

            if (MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled == true) { MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled = false; }
            kabPrefObj.IsChecked = false;
            FACECreator.RebootWorkPlaceList(selectedK = App.sysH.FindKabinet(((ToggleButton)sender).Uid));
            kabPrefObj = (ToggleButton)sender;
        }
        protected static void WorkPlacesItem_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)sender).Parent == null) { return; }
            if (MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled == false) { MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled = true; }
            if (MainWindow.Auditories_W.SetInf_SP.Visibility == Visibility.Hidden) { MainWindow.Auditories_W.SetInf_SP.Visibility = Visibility.Visible; }

            if (((ToggleButton)sender).IsChecked == false) { ((ToggleButton)sender).IsChecked = true; return; }
            wpPrefObj.IsChecked = false;
            wpPrefObj = (ToggleButton)sender;
            selectetWP = App.sysH.FindWorkPlace(int.Parse(wpPrefObj.Uid));

            MainWindow.Auditories_W.Edit_B.IsEnabled = true;
            if (selectetWP.kompukter != null)
            {
                MainWindow.Auditories_W.DeleteFacilityK_B.Uid = selectetWP.kompukter.kompID + "_K";
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = true;
                MainWindow.Auditories_W.AddKomputer_B.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.KompInf_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow.Auditories_W.MoreInf_B.IsEnabled = false;
                MainWindow.Auditories_W.AddKomputer_B.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.KompInf_Panel.Visibility = Visibility.Collapsed;
            }
            if (selectetWP.monitor != null)
            {
                MainWindow.Auditories_W.DeleteFacilityM_B.Uid = selectetWP.monitor.monitorID + "_M";
                MainWindow.Auditories_W.AddMonitor_B.Visibility = Visibility.Collapsed;
                MainWindow.Auditories_W.MonicInf_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow.Auditories_W.AddMonitor_B.Visibility = Visibility.Visible;
                MainWindow.Auditories_W.MonicInf_Panel.Visibility = Visibility.Collapsed;
            }

            if (selectetWP.kompukter == null & selectetWP.monitor == null)
            { MainWindow.Auditories_W.Edit_B.IsEnabled = false; }
            MainWindow.Auditories_W.SetInf_SP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.DataContext = selectetWP;
        }
        protected static void InventoryFacilityItem_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.Inventory_W.SetInf_SP.Visibility == Visibility.Hidden) { MainWindow.Inventory_W.SetInf_SP.Visibility = Visibility.Visible; }
            if (((ToggleButton)sender).IsChecked == false) { ((ToggleButton)sender).IsChecked = true; return; }

            facPrefObj.IsChecked = false;
            facPrefObj = (ToggleButton)sender;
            MainWindow.Inventory_W.Delete_B.Uid = facPrefObj.Uid;
            selectedF = App.sysH.FindFacility(facPrefObj.Uid.Split('_')[0], facPrefObj.Uid.Split('_')[1]);
            RebootInventoryInfo();
        }
        public static void RebootInventoryInfo()
        {
            if (selectedF is Komp)
            {
                MainWindow.Inventory_W.SetInf_SP.DataContext = null;
                MainWindow.Inventory_W.SetInf_SP.DataContext = ((Komp)selectedF);
                MainWindow.Inventory_W.KompInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Inventory_W.MonicInf_Panel.Visibility = Visibility.Collapsed;
                MainWindow.Inventory_W.MoreInf_B.IsEnabled = true;
            }
            else
            {
                MainWindow.Inventory_W.SetInf_SP.DataContext = null;
                MainWindow.Inventory_W.SetInf_SP.DataContext = ((Monitor)selectedF);
                MainWindow.Inventory_W.KompInf_Panel.Visibility = Visibility.Collapsed;
                MainWindow.Inventory_W.MonicInf_Panel.Visibility = Visibility.Visible;
                MainWindow.Inventory_W.MoreInf_B.IsEnabled = false;
            }
            MainWindow.Inventory_W.Delete_B.IsEnabled = true;
            MainWindow.Inventory_W.Edit_B.IsEnabled = true;
            MainWindow.Inventory_W.InventoryItems_WP.DataContext = null;
            MainWindow.Inventory_W.InventoryItems_WP.DataContext = App.sysH.Inventory;
        }


        public static void FinderFacilityItem_Click(object sender, SelectedCellsChangedEventArgs e)//FinderWin
        {
            if (App.Finder_W.RequestResult_DG.SelectedCells.Count == 0) { return; }
            App.Finder_W.SetInf_SP.Visibility = Visibility.Visible;

            string id = ((DataRowView)App.Finder_W.RequestResult_DG.SelectedCells[1].Item)[1].ToString();
            if (((ComboBoxItem)App.Finder_W.FacilityType_CB.SelectedItem).Content.ToString() == "Компьютер")
            {
                selectedF = App.sysH.FindKomp(int.Parse(id));
                if (selectedF == null) { selectedF = App.sysH.FindFacility(id, "K"); }
                App.Finder_W.DataContext = ((Komp)selectedF);
                App.Finder_W.Delete_B.Uid = $"{id}_K";
                App.Finder_W.KompInf_Panel.Visibility = Visibility.Visible;
                App.Finder_W.MonicInf_Panel.Visibility = Visibility.Collapsed;
                App.Finder_W.MoreInf_B.IsEnabled = true;
            }
            else
            {
                selectedF = App.sysH.FindMonitor(int.Parse(id));
                if (selectedF == null) { selectedF = App.sysH.FindFacility(id, "M"); }
                App.Finder_W.DataContext = ((Monitor)selectedF);
                App.Finder_W.Delete_B.Uid = $"{id}_M";
                App.Finder_W.MonicInf_Panel.Visibility = Visibility.Visible;
                App.Finder_W.KompInf_Panel.Visibility = Visibility.Collapsed;
                App.Finder_W.MoreInf_B.IsEnabled = false;
            }
            App.Finder_W.Delete_B.IsEnabled = true;
            App.Finder_W.Edit_B.IsEnabled = true;
        }

        protected static void DeleteWorkPlace_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данное рабочее место? (в случае удаления оборудование данного рабочего места будет помещено в инвентарь)", "Сообщение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            FACECreator.RemoveWorkPlaceMP(MainWindow.Auditories_W.WorkPlacesList_WP,App.sysH.FindWorkPlace(int.Parse(((Button)sender).Uid), selectedK));
            MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled = false;
            selectedK.workPlaceCount = selectedK.workPlaces.Count;
            MainWindow.Auditories_W.AuditoriesList_WP.DataContext = null;
            MainWindow.Auditories_W.AuditoriesList_WP.DataContext = App.sysH;
        }
        protected static void DeleteKabinet_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данный кабинет? (в случае удаления оборудование, находящееся на рабочих местах данного кабинета, будет помещено в инвентарь)", "Сообщение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            FACECreator.RemoveKabinetMP(MainWindow.Auditories_W.AuditoriesList_WP, App.sysH.FindKabinet(((Button)sender).Uid));
            MainWindow.Auditories_W.EditKabinet_B.IsEnabled = false;
            MainWindow.Auditories_W.AddWorkPlace_B.IsEnabled = false;
            MainWindow.Auditories_W.EditWorkPlace_B.IsEnabled = false;
        }

        protected static void ErrorsValidator(object sender, ValidationErrorEventArgs e)//FacilityEditorWin
        {
            if (Validation.GetHasError((TextBox)sender) == true)
            {
                if (App.FacilityEditorW.errorsList.Contains(sender) == false) { App.FacilityEditorW.errorsList.Add((TextBox)sender); }
                if(e.Error.ErrorContent.ToString() == "Входная строка имела неверный формат." == true)
                { MessageBox.Show("Введенный текст содержит недопустимые символы.");return; }
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
            else { App.FacilityEditorW.errorsList.Remove((TextBox)sender); }
        }
        public static void KI_CPU_Delete_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            App.FacilityEditorW.tempWorkplace.kompukter.cpu = null;
            App.FacilityEditorW.Processor_CAN.Visibility = Visibility.Collapsed;
            App.FacilityEditorW.KI_CPU_Add_B.IsEnabled = true;
            App.FacilityEditorW.KI_CPU_Delete_B.IsEnabled = false;
            App.FacilityEditorW.Processor_GB.Height -= App.FacilityEditorW.Processor_CAN.Height + 1;
            App.FacilityEditorW.SetHeightPanel();
        }
        protected static void KI_VideoDevices_Delete_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данный графический адаптер?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            foreach (var item in App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters)
            {
                if (item.adapterID.ToString() == ((Button)sender).Uid)
                {
                    App.FacilityEditorW.VideoDevicesList_WP.Children.Remove((Canvas)((Button)sender).Parent);
                    foreach (UIElement item2 in App.FacilityEditorW.VideoDevicesList_WP.Children)
                    {
                        if (item2.Uid == ((Button)sender).Uid) { App.FacilityEditorW.VideoDevicesList_WP.Children.Remove(item2); break; }
                    }
                    App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters.Remove(item);
                    App.FacilityEditorW.SetHeightGB("video");
                    return;
                }
            }
        }
        protected static void KI_Disks_Delete_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данный физический диск?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            foreach (var item in App.FacilityEditorW.tempWorkplace.kompukter.disks)
            {
                if (item.diskID.ToString() == ((Button)sender).Uid)
                {
                    App.FacilityEditorW.DisksList_WP.Children.Remove((Canvas)((Button)sender).Parent);
                    foreach (UIElement item2 in App.FacilityEditorW.DisksList_WP.Children)
                    {
                        if (item2.Uid == ((Button)sender).Uid) { App.FacilityEditorW.DisksList_WP.Children.Remove(item2); break; }
                    }
                    App.FacilityEditorW.tempWorkplace.kompukter.disks.Remove(item);
                    App.FacilityEditorW.SetHeightGB("disk");
                    return;
                }
            }
        }
        protected static void KI_Systems_Delete_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данную операционную систему?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            foreach (OS item in App.FacilityEditorW.tempWorkplace.kompukter.systems)
            {
                if (item.osID.ToString() == ((Button)sender).Uid)
                {
                    App.FacilityEditorW.SystemsList_WP.Children.Remove((Canvas)((Button)sender).Parent);
                    foreach (UIElement item2 in App.FacilityEditorW.SystemsList_WP.Children)
                    {
                        if (item2.Uid == ((Button)sender).Uid) { App.FacilityEditorW.SystemsList_WP.Children.Remove(item2); break; }
                    }
                    App.FacilityEditorW.tempWorkplace.kompukter.systems.Remove(item);
                    App.FacilityEditorW.SetHeightGB("system");
                    return;
                }
            }
        }
        public static void KI_CPU_Add_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            App.FacilityEditorW.tempWorkplace.kompukter.cpu = new CPU();
            App.FacilityEditorW.Processor_CAN.Visibility = Visibility.Visible;
            App.FacilityEditorW.KI_CPU_Add_B.IsEnabled = false;
            App.FacilityEditorW.KI_CPU_Delete_B.IsEnabled = true;
            App.FacilityEditorW.Processor_GB.Height += App.FacilityEditorW.Processor_CAN.Height + 1;
            App.FacilityEditorW.SetHeightPanel();
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void KI_VideoDevices_Add_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters.Add(new VideoAdapter());
            App.FacilityEditorW.VideoDevicesList_WP.Children.Add(new Separator { Uid = App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters[App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters.Count - 1].adapterID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
            FACECreator.CreateVideoCardE(App.FacilityEditorW.VideoDevicesList_WP, App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters[App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters.Count - 1], $"kompukter.videoAdapters[{App.FacilityEditorW.tempWorkplace.kompukter.videoAdapters.Count - 1}]");
            App.FacilityEditorW.SetHeightGB("video");
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void KI_Disks_Add_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            App.FacilityEditorW.tempWorkplace.kompukter.disks.Add(new Disk());
            App.FacilityEditorW.DisksList_WP.Children.Add(new Separator { Uid = App.FacilityEditorW.tempWorkplace.kompukter.disks[App.FacilityEditorW.tempWorkplace.kompukter.disks.Count - 1].diskID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
            FACECreator.CreateDiskE(App.FacilityEditorW.DisksList_WP, App.FacilityEditorW.tempWorkplace.kompukter.disks[App.FacilityEditorW.tempWorkplace.kompukter.disks.Count - 1], $"kompukter.disks[{App.FacilityEditorW.tempWorkplace.kompukter.disks.Count - 1}]");
            App.FacilityEditorW.SetHeightGB("disk");
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }
        public static void KI_Systems_Add_B_Click(object sender, RoutedEventArgs e)//FacilityEditorWin
        {
            App.FacilityEditorW.tempWorkplace.kompukter.systems.Add(new OS());
            App.FacilityEditorW.SystemsList_WP.Children.Add(new Separator { Uid = App.FacilityEditorW.tempWorkplace.kompukter.systems[App.FacilityEditorW.tempWorkplace.kompukter.systems.Count - 1].osID.ToString(), Height = 11, Width = 463, Margin = new Thickness(0), Background = Brushes.Black });
            FACECreator.CreateSystemE(App.FacilityEditorW.SystemsList_WP, App.FacilityEditorW.tempWorkplace.kompukter.systems[App.FacilityEditorW.tempWorkplace.kompukter.systems.Count - 1], $"kompukter.systems[{App.FacilityEditorW.tempWorkplace.kompukter.systems.Count - 1}]");
            App.FacilityEditorW.SetHeightGB("system");
            App.FacilityEditorW.DataContext = null;
            App.FacilityEditorW.DataContext = App.FacilityEditorW.tempWorkplace;
        }

        //КНОПКИ:
        public static void WriteToInventory_Click(object sender, RoutedEventArgs e)//AuditoriesWin
        {
            if (((Button)sender).Name.Contains('K') == true)
            {
                Loger.AppendLogEvent("Оборудование убрано в инвентарь", selectetWP.kompukter, selectedK, selectetWP);
                SysHelperDB.ExecCom($"UPDATE Компьютер SET статус = 'на складе' WHERE ID_компьютера_P = {selectetWP.kompukter.kompID}");
                App.sysH.Inventory.Facilities.Add(selectetWP.kompukter);
                selectedK.workPlaces[selectedK.workPlaces.IndexOf(selectetWP)].kompukter = null;
                MainWindow.Auditories_W.AddKomputer_B.Visibility = Visibility.Visible;
            }
            else
            {
                Loger.AppendLogEvent("Оборудование убрано в инвентарь", selectetWP.monitor, selectedK, selectetWP);
                SysHelperDB.ExecCom($"UPDATE Монитор SET статус = 'на складе' WHERE ID_монитора_P = {selectetWP.monitor.monitorID}");
                App.sysH.Inventory.Facilities.Add(selectetWP.monitor);
                selectedK.workPlaces[selectedK.workPlaces.IndexOf(selectetWP)].monitor = null;
                MainWindow.Auditories_W.AddMonitor_B.Visibility = Visibility.Visible;
            }
            selectedK.workPlaces[selectedK.workPlaces.IndexOf(selectetWP)].UpdateFACE();
            FACECreator.RebootInfoAuditories(selectetWP);
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = null;
            MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = selectedK;
            MainWindow.Auditories_W.SetInf_SP.DataContext = null;
            MainWindow.Auditories_W.SetInf_SP.DataContext = selectetWP;
        }
        public static void DeleteFacility(string senderUID)
        {
            if (senderUID.Contains("K") == true)
            {
                Loger.AppendLogEvent("Оборудование удалено", selectetWP.kompukter);
                App.sysH.DeleteKomputer(int.Parse(senderUID.TrimEnd('K', '_')));
            }
            else
            {
                Loger.AppendLogEvent("Оборудование удалено", selectetWP.monitor);
                App.sysH.DeleteMonitor(int.Parse(senderUID.TrimEnd('M', '_')));
            }
        }
        public static void AutoGenIN(Facility target)
        {
            Random rand = new Random();
            DataTable tempDT = new DataTable();

            if (target is Komp == true)
            {
                do
                {
                    tempDT = new DataTable();
                    ((Komp)target).kompIN = rand.Next(10000, 100000).ToString();
                    SysHelperDB.ExecCom($"SELECT ИН_компьютера,ID_компьютера_P FROM Компьютер WHERE ИН_компьютера = '{((Komp)target).kompIN}'", tempDT);
                }
                while (tempDT.Rows.Count != 0);
            }
            else
            {
                do
                {
                    tempDT = new DataTable();
                    ((Monitor)target).monitorIN = rand.Next(10000, 100000).ToString();
                    SysHelperDB.ExecCom($"SELECT ИН_монитора FROM Монитор WHERE ИН_монитора = '{((Monitor)target).monitorIN}'", tempDT);
                }
                while (tempDT.Rows.Count != 0);
            }
        }
    }
}


//ПОМОЙКА


//xaml-код для пункта меню кабинетов на форме AuditoriesWin

//<Border BorderBrush = "#FF9C9C9C" BorderThickness="1" HorizontalAlignment="Left" Height="68" Width="204" VerticalAlignment="Top"  Margin="25,318,0,0" CornerRadius="5" ClipToBounds="True">
//            <Border.Background>
//                <LinearGradientBrush StartPoint = "1,1" EndPoint="1,0">
//                    <GradientStop Color = "#FFC2C8FF" Offset="1"/>
//                    <GradientStop Color = "#FFA3FFA3" />
//                    < GradientStop Color="White" Offset="0.493"/>
//                </LinearGradientBrush>
//            </Border.Background>
//            <Canvas Margin = "-1" >
//                < Border Canvas.Left="0"  BorderThickness="1" BorderBrush="Black" CornerRadius="8" Height="68" VerticalAlignment="Top" Width="204">
//                    <Border.Effect>
//                        <BlurEffect RenderingBias = "Quality" />
//                    </ Border.Effect >
//                </ Border >
//                < Label Content="Аудитория №315" Background="{x:Null}" FontFamily="Arial Rounded MT Bold" FontSize="16" Padding="12,7,20,7" Foreground="#FF2491B9" Height="34" VerticalAlignment="Top" Canvas.Top="1" Width="204"/>
//                <Separator Height = "5" Margin="0" Canvas.Top="31" Width="204"></Separator>
//                <Label Content = "  Кол-во рабочих мест: 777" Background="{x:Null}" FontFamily="Arial Unicode MS" FontSize="14" Padding="5,4,12,4" Foreground="#FF686868" Height="33" VerticalAlignment="Top" Canvas.Top="36" Width="204" Margin="0"/>
//            </Canvas>
//</Border>


//xaml-код для пункта меню рабочих мест на форме AuditoriesWin

//<Border BorderBrush = "#FF9C9C9C" BorderThickness="1" HorizontalAlignment="Left" Height="67" Width="231" VerticalAlignment="Top"  Margin="272,303,0,0" CornerRadius="5" ClipToBounds="True">
//        <Border.Background>
//            <LinearGradientBrush StartPoint = "1,1" EndPoint="1,0">
//                <GradientStop Color = "#FFC2C8FF" Offset="1"/>
//                <GradientStop Color = "#FFC2C8FF" />
//                < GradientStop Color="White" Offset="0.493"/>
//            </LinearGradientBrush>
//        </Border.Background>
//        <Canvas Margin = "-1" >
//            < Border Canvas.Left="0"  BorderThickness="1" BorderBrush="Black" CornerRadius="8" Height="68" VerticalAlignment="Top" Width="231">
//                <Border.Effect>
//                    <BlurEffect RenderingBias = "Quality" />
//                </ Border.Effect >
//            </ Border >
//            < Label Content="Аудитория №315" Background="{x:Null}" FontFamily="Arial Rounded MT Bold" FontSize="14" Padding="0,10,0,4" Foreground="#FF2491B9" Height="31" VerticalAlignment="Top" Width="156" Canvas.Left="68"/>
//            <Separator Height = "5" Margin="0" Canvas.Top="31" Width="155" Canvas.Left="68"/>
//            <Label Content = "Оборудование:" FontFamily="Arial Unicode MS" FontSize="10" Foreground="#FF686868" Padding="0" Height="13" Width="153" FontWeight="Bold" Canvas.Left="68" Canvas.Top="36"/>
//            <Label Content = "Монитор, компьютер" FontFamily="Arial Unicode MS" FontSize="10" Foreground="#FF686868" Padding="0" Height="13" Width="138" Canvas.Left="68" Canvas.Top="49" />
//            <Image Height = "53" Width="53" Source="images/SborkaMonitor.bmp" Canvas.Left="7" Canvas.Top="7"/>
//        </Canvas>
//</Border>


//xaml-код для элемента компьютера в меню инвентаря

//<Border Margin = "3" BorderBrush="#FF9C9C9C" BorderThickness="1" HorizontalAlignment="Left" Height="46" Width="162" VerticalAlignment="Top" CornerRadius="5" ClipToBounds="True">
//                    <Border.Background>
//                        <LinearGradientBrush StartPoint = "1,1" EndPoint="1,0">
//                            <GradientStop Color = "#FFC2C8FF" Offset="1"/>
//                            <GradientStop Color = "#FFC2C8FF" />
//                            < GradientStop Color="White" Offset="0.493"/>
//                        </LinearGradientBrush>
//                    </Border.Background>
//                    <Canvas Margin = "-1" >
//                        < Border Canvas.Left="0" BorderThickness="1" BorderBrush="Black" CornerRadius="8" Height="46" VerticalAlignment="Top" Width="162">
//                            <Border.Effect>
//                                <BlurEffect RenderingBias = "Quality" />
//                            </ Border.Effect >
//                        </ Border >
//                        < Label Content="Компьютер" Background="{x:Null}" FontFamily="Arial Rounded MT Bold" FontSize="14" Padding="0,0,0,4" Foreground="#FF2491B9" Height="20" VerticalAlignment="Top" Width="157" Canvas.Left="5" Canvas.Top="5"/>
//                        <Label Content = "Инвентарный №:" FontFamily="Arial Unicode MS" FontSize="10" Foreground="#FF686868" Padding="0" Height="13" Width="157" FontWeight="Bold" Canvas.Left="5" Canvas.Top="25"/>
//                    </Canvas>
//                </Border>


////xaml-код для элемента монитора в меню инвентаря

//                <Border Margin = "3" BorderBrush="#FF9C9C9C" BorderThickness="1" HorizontalAlignment="Left" Height="46" Width="162" VerticalAlignment="Top" CornerRadius="5" ClipToBounds="True">
//                    <Border.Background>
//                        <LinearGradientBrush StartPoint = "1,1" EndPoint="1,0">
//                            <GradientStop Color = "#FFC2C8FF" Offset="1"/>
//                            <GradientStop Color = "#FFC2C8FF" />
//                            < GradientStop Color="White" Offset="0.493"/>
//                        </LinearGradientBrush>
//                    </Border.Background>
//                    <Canvas Margin = "-1" >
//                        < Border BorderThickness="1" BorderBrush="Black" CornerRadius="8" Height="46" VerticalAlignment="Top" Width="162">
//                            <Border.Effect>
//                                <BlurEffect RenderingBias = "Quality" />
//                            </ Border.Effect >
//                        </ Border >
//                        < Label Content="Компьютер" Background="{x:Null}" FontFamily="Arial Rounded MT Bold" FontSize="14" Padding="0" Foreground="#FF2491B9" Height="20" Width="157" Canvas.Left="5" Canvas.Top="5"/>
//                        <Label Content = "Инвентарный №:" FontFamily="Arial Unicode MS" FontSize="10" Foreground="#FF686868" Padding="0" Height="13" Width="157" FontWeight="Bold" Canvas.Left="5" Canvas.Top="25"/>
//                    </Canvas>
//                </Border>