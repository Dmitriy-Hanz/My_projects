using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace TP_F
{
    public abstract class Facility //Абстрактный класс для компьютеров и мониторов
    {
        public string fType;
        public int uID;
        public string uIN { get; set; }
    }

    public class Kabinet//Класс для кабинетов
    {
        public string kabinetID { get; set; }
        public List<WorkPlace> workPlaces { get; set; }
        public int workPlaceCount { get; set; }
        public string name { get; set; }

        public Kabinet() { workPlaces = new List<WorkPlace>(); workPlaceCount = 0; }
        public Kabinet(string kabinetID, int workPlaceCount, string name)
        {
            workPlaces = new List<WorkPlace>();
            this.kabinetID = kabinetID;
            this.workPlaceCount = workPlaceCount;
            this.name = name;
        }
    }
    public class WorkPlace : ICloneable//Класс для рабочих мест
    {
        public int workPlaceID { get; set; }
        public Komp kompukter { get; set; }
        public Monitor monitor { get; set; }
        public string name { get; set; }

        public string extraInf { get; set; }
        public string ImgSource { get; set; }

        public WorkPlace() { }
        public WorkPlace(System.Data.DataRow data)
        {
            workPlaceID = (int)data[0];
            name = (string)data[4];
            if (data[1].ToString() != "") { kompukter = new Komp((int)data[1]); }
            if (data[2].ToString() != "") { monitor = new Monitor((int)data[2]); }
            UpdateFACE();
        }
        public void UpdateFACE()
        {
            extraInf = "Монитор, компьютер";
            if (kompukter == null) { extraInf = "Монитор";}
            if (monitor == null) { extraInf = "Компьютер";}
            if (monitor == null & kompukter == null) { extraInf = "Отсутствует";}

            ImgSource = @"pack://application:,,,/Images/SborkaHaveAll.bmp";
            if (kompukter == null & monitor == null)
            { ImgSource = @"pack://application:,,,/Images/SborkaNothing.bmp"; return; }
            if (kompukter == null)
            { ImgSource = @"pack://application:,,,/Images/SborkaMonitor.bmp"; return; }
            if (monitor == null)
            { ImgSource = @"pack://application:,,,/Images/SborkaKomputer.bmp"; return; }
        }
        public object Clone()
        {
            WorkPlace tempWP = new WorkPlace { workPlaceID = this.workPlaceID, kompukter = null, monitor = null, name = this.name};
            if (this.kompukter != null)
            {
                tempWP.kompukter = new Komp { kompID = this.kompukter.kompID, kompIN = this.kompukter.kompIN, name = this.kompukter.name, ozuSize = this.kompukter.ozuSize, status = this.kompukter.status, cpu = null, videoAdapters = new List<VideoAdapter>(), systems = new List<OS>(), disks = new List<Disk>() };
                if (this.kompukter.cpu != null)
                {
                    tempWP.kompukter.cpu = new CPU { cpuID = this.kompukter.cpu.cpuID, name = this.kompukter.cpu.name, coreCount = this.kompukter.cpu.coreCount, cashMemoryType = this.kompukter.cpu.cashMemoryType, cashMemoryValue = this.kompukter.cpu.cashMemoryValue };
                }
                for (int i = 0; i < this.kompukter.videoAdapters.Count; i++)
                {
                    tempWP.kompukter.videoAdapters.Add(new VideoAdapter { adapterID = this.kompukter.videoAdapters[i].adapterID, name = this.kompukter.videoAdapters[i].name, videoProcessor = this.kompukter.videoAdapters[i].videoProcessor, driverVersion = this.kompukter.videoAdapters[i].driverVersion, adapterRAM = this.kompukter.videoAdapters[i].adapterRAM });
                }
                for (int i = 0; i < this.kompukter.disks.Count; i++)
                {
                    tempWP.kompukter.disks.Add(new Disk { diskID = this.kompukter.disks[i].diskID, model = this.kompukter.disks[i].model, _interface = this.kompukter.disks[i]._interface, type = this.kompukter.disks[i].type, memoryValue = this.kompukter.disks[i].memoryValue });
                }
                for (int i = 0; i < this.kompukter.systems.Count; i++)
                {
                    tempWP.kompukter.systems.Add(new OS { osID = this.kompukter.systems[i].osID, name = this.kompukter.systems[i].name, version = this.kompukter.systems[i].version, architecture = this.kompukter.systems[i].architecture });
                }
            }
            if (this.monitor != null)
            {
                tempWP.monitor = new Monitor { monitorID = this.monitor.monitorID, monitorIN = this.monitor.monitorIN, model = this.monitor.model, diagonal = this.monitor.diagonal, sideRatio = this.monitor.sideRatio, matrix = this.monitor.matrix, freq = this.monitor.freq, resolution = this.monitor.resolution, status = this.monitor.status };
            }
            return tempWP;
        }
    }
    public class Komp : Facility//Класс для компьютеров
    {
        public int kompID;
        public string kompIN { get; set; }
        public string name { get; set; }
        public int ozuSize { get; set; }
        public string logDisks { get; set; }
        public string status;
        public List<VideoAdapter> videoAdapters { get; set; }
        public List<OS> systems { get; set; }
        public List<Disk> disks { get; set; }
        public CPU cpu { get; set; }

        public Komp() { fType = "K"; kompIN = name = logDisks = ""; videoAdapters = new List<VideoAdapter>(); systems = new List<OS>(); disks = new List<Disk>(); }
        public Komp(int id) { kompID = id; videoAdapters = new List<VideoAdapter>(); systems = new List<OS>(); disks = new List<Disk>(); }
        public Komp(System.Data.DataRow mas)
        {
            kompID = uID = (int)mas[0];
            kompIN = uIN = (string)mas[1];
            name = (string)mas[2];
            ozuSize = (int)mas[3];
            logDisks = (string)mas[4];
            status = (string)mas[5];
            videoAdapters = new List<VideoAdapter>();
            systems = new List<OS>();
            disks = new List<Disk>();
            fType = "K";
        }
        public void CopyHere(Komp source)
        {
            kompID = uID = source.kompID;
            kompIN = uIN = source.kompIN;
            name = source.name;
            ozuSize = source.ozuSize;
            logDisks = source.logDisks;
            status = source.status;
            videoAdapters = source.videoAdapters;
            systems = source.systems;
            disks = source.disks;
            cpu = source.cpu;
        }
    }
    public class Monitor : Facility//Класс для мониторов
    {
        public int monitorID;
        public string monitorIN { get; set; }
        public string model { get; set; }
        public string diagonal { get; set; }
        public string sideRatio { get; set; }
        public string matrix { get; set; }
        public string freq { get; set; }
        public string resolution { get; set; }
        public string status;
        public Monitor() { fType = "M"; monitorIN = model = diagonal = sideRatio = matrix = freq = resolution = ""; }
        public Monitor(int id) => monitorID = id;
        public Monitor(System.Data.DataRow data)
        {
            monitorID = (int)data[0];
            monitorIN = (string)data[1];
            model = (string)data[2];
            diagonal = data[3].ToString();
            sideRatio = data[4].ToString();
            matrix = data[5].ToString();
            freq = data[6].ToString();
            resolution = data[7].ToString();
            status = (string)data[8];
            fType = "M";
            uID = monitorID;
            uIN = monitorIN;
        }
        public void CopyHere(Monitor source)
        {
            monitorID = uID = source.monitorID;
            monitorIN = uIN = source.monitorIN;
            model = source.model;
            diagonal = source.diagonal;
            sideRatio = source.sideRatio;
            matrix = source.matrix;
            freq = source.freq;
            resolution = source.resolution;
            status = source.status;
        }
    }
    public class VideoAdapter : IDataErrorInfo//Класс для графических адаптеров
    {
        public int adapterID;
        public string name { get; set; }
        public string videoProcessor { get; set; }
        public string driverVersion { get; set; }
        public double adapterRAM { get; set; }
        public VideoAdapter() 
        {
            name = videoProcessor = driverVersion = "";
            adapterRAM = 0;
        }
        public VideoAdapter(System.Data.DataRow data)
        {
            adapterID = (int)data[0];
            name = (string)data[1];
            videoProcessor = (string)data[2];
            driverVersion = (string)data[3];
            adapterRAM = (double)data[4];
        }
        public void Initialize(System.Data.DataRow data)
        {
            adapterID = (int)data[0];
            name = (string)data[1];
            videoProcessor = (string)data[2];
            driverVersion = (string)data[3];
            adapterRAM = (double)data[4];
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "name":
                        if (name.Length > 150) { return "Текст, введенный в поле названия модели граф. адаптера, слишком большой"; }
                        break;
                    case "videoProcessor":
                        if (videoProcessor.Length > 50) { return "Текст, введенный в поле \"Графический процессор\", слишком большой"; }
                        break;
                    case "driverVersion":
                        if (driverVersion.Length > 50) { return "Текст, введенный в поле \"Версия драйвера\", слишком большой"; }
                        break;
                }
                return "";
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class OS : IDataErrorInfo//Класс для операционых систем
    {
        public int osID;
        public string name { get; set; }
        public string version { get; set; }
        public string architecture { get; set; }

        public OS() { name = version = architecture = ""; }
        public OS(System.Data.DataRow data)
        {
            osID = (int)data[0];
            name = (string)data[1];
            version = (string)data[2];
            architecture = (string)data[3];
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "name":
                        if (name.Length > 100) { return "Текст, введенный в поле названия операционной системы, слишком большой"; }
                        break;
                    case "version":
                        if (version.Length > 50) { return "Текст, введенный в поле \"Версия\", слишком большой"; }
                        break;
                }
                return "";
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class Disk : IDataErrorInfo//Класс для физических дисков
    {
        public int diskID;
        public string model { get; set; }
        public string _interface { get; set; }
        public string type { get; set; }
        public double memoryValue { get; set; }
        
        public Disk() { model = _interface = type = ""; }
        public Disk(System.Data.DataRow data)
        {
            diskID = (int)data[0];
            model = (string)data[1];
            _interface = (string)data[2];
            type = (string)data[3];
            memoryValue = (double)data[4];
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "model":
                        if (model.Length > 100) { return "Текст, введенный в поле названия модели диска, слишком большой"; }
                        break;
                    case "_interface":
                        if (_interface.Length > 10) { return "Текст, введенный в поле \"Интерфейс\", слишком большой"; }
                        break;
                    case "type":
                        if (type.Length > 50) { return "Текст, введенный в поле \"Тип носителя\", слишком большой"; }
                        break;
                }
                return "";
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class CPU//Класс для процессоров
    {
        public int cpuID;
        public string name { get; set; }
        public int coreCount { get; set; }
        public string cashMemoryType { get; set; }
        public double cashMemoryValue { get; set; }
        public CPU() { name = cashMemoryType = ""; }
        public CPU(System.Data.DataRow data)
        {
            cpuID = (int)data[0];
            name = (string)data[1];
            coreCount = (int)data[2];
            cashMemoryType = (string)data[3];
            cashMemoryValue = (double)data[4];
        }
    }
    
    public class Inventory//Класс-инвентарь
    {
        public List<Facility> Facilities { get; set; }
        public Inventory() => Facilities = new List<Facility>();
    }
}
