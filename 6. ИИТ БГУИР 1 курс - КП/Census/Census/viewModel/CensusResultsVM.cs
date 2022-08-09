using Census.infrastructure.commands.Base;
using Census.view;
using Census.viewModel.Base;
using Census.utils;
using System.Windows;
using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;
using System.Data;

namespace Census.viewModel
{
    class CensusResultsVM : ViewModelBase
    {
        #region КОМАНДЫ
        public ICommand BackToMainMenuCommand { get; }
        private void OnBackToMainMenuCommandExecuted(object p)
        {
            TypicalActions.GoToWindow<CensusResultsWindow, MainMenuWindow>();
        }
        private bool CanBackToMainMenuCommandExecute(object p) => true;
        #endregion
        #region МОДЕЛЬ
        private PlotModel generalPopulationOfCountryPlot; public PlotModel GeneralPopulationOfCountryPlot
        {
            get => generalPopulationOfCountryPlot;
            set => Set(ref generalPopulationOfCountryPlot, value);
        }
        private PlotModel populationOfCountryByRegionsPlot; public PlotModel PopulationOfCountryByRegionsPlot
        {
            get => populationOfCountryByRegionsPlot;
            set => Set(ref populationOfCountryByRegionsPlot, value);
        }
        private PlotModel populationOfCountryByAgeGroupsPlot; public PlotModel PopulationOfCountryByAgeGroupsPlot
        {
            get => populationOfCountryByAgeGroupsPlot;
            set => Set(ref populationOfCountryByAgeGroupsPlot, value);
        }        
        private PlotModel populationOfCountryByGenderPlot; public PlotModel PopulationOfCountryByGenderPlot
        {
            get => populationOfCountryByGenderPlot;
            set => Set(ref populationOfCountryByGenderPlot, value);
        }       
        private PlotModel populationOfCountryByMaritalStatusPlot; public PlotModel PopulationOfCountryByMaritalStatusPlot
        {
            get => populationOfCountryByMaritalStatusPlot;
            set => Set(ref populationOfCountryByMaritalStatusPlot, value);
        }
        private PlotModel womenWithChildrenByCountPlot; public PlotModel WomenWithChildrenByCountPlot
        {
            get => womenWithChildrenByCountPlot;
            set => Set(ref womenWithChildrenByCountPlot, value);
        }
        private PlotModel womenWithChildrenByPlansPlot; public PlotModel WomenWithChildrenByPlansPlot
        {
            get => womenWithChildrenByPlansPlot;
            set => Set(ref womenWithChildrenByPlansPlot, value);
        }
        private PlotModel populationOfCountryByEducationLvlPlot; public PlotModel PopulationOfCountryByEducationLvlPlot
        {
            get => populationOfCountryByEducationLvlPlot;
            set => Set(ref populationOfCountryByEducationLvlPlot, value);
        }
        private PlotModel populationOfCountryByNationalityPlot; public PlotModel PopulationOfCountryByNationalityPlot
        {
            get => populationOfCountryByNationalityPlot;
            set => Set(ref populationOfCountryByNationalityPlot, value);
        }
        private PlotModel populationOfCountryBySpeakingLanguagePlot; public PlotModel PopulationOfCountryBySpeakingLanguagePlot
        {
            get => populationOfCountryBySpeakingLanguagePlot;
            set => Set(ref populationOfCountryBySpeakingLanguagePlot, value);
        }        
        private PlotModel populationOfCountryByEmploymentPlot; public PlotModel PopulationOfCountryByEmploymentPlot
        {
            get => populationOfCountryByEmploymentPlot;
            set => Set(ref populationOfCountryByEmploymentPlot, value);
        }
        
        
        
        
        private List<string> diagramsList = new List<string>(); public List<string> DiagramsList
        {
            get => diagramsList;
            set => Set(ref diagramsList, value);
        }

        #endregion
        public CensusResultsVM()
        {
            BackToMainMenuCommand = new LambdaCommand(OnBackToMainMenuCommandExecuted, CanBackToMainMenuCommandExecute);
            DataTable fullMaster = null;

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM GeneralPopulationOfCountryView");
            GeneralPopulationOfCountryPlot = CreateGeneralPopulationOfCountryPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByRegionsView");
            PopulationOfCountryByRegionsPlot = CreatePopulationOfCountryByRegionsPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByAgeGroupsView");
            PopulationOfCountryByAgeGroupsPlot = CreatePopulationOfCountryByAgeGroupsPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByGenderView");
            PopulationOfCountryByGenderPlot = CreatePopulationOfCountryByGenderPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByMaritalStatusView");
            PopulationOfCountryByMaritalStatusPlot = CreatePopulationOfCountryByMaritalStatusPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM WomenWithChildrenByCountView");
            WomenWithChildrenByCountPlot = CreateWomenWithChildrenByCountPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM WomenWithChildrenByPlansView");
            WomenWithChildrenByPlansPlot = CreateWomenWithChildrenByPlansPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByEducationLvlView");
            PopulationOfCountryByEducationLvlPlot = CreatePopulationOfCountryByEducationLvlPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByNationalityView");
            PopulationOfCountryByNationalityPlot = CreatePopulationOfCountryByNationalityPlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryBySpeakingLanguageView");
            PopulationOfCountryBySpeakingLanguagePlot = CreatePopulationOfCountryBySpeakingLanguagePlot(fullMaster.Rows);

            fullMaster = DBUtil.ExecuteReturn("SELECT * FROM PopulationOfCountryByEmploymentView");
            PopulationOfCountryByEmploymentPlot = CreatePopulationOfCountryByEmploymentPlot(fullMaster.Rows);
        
            
        }

        private PlotModel CreateGeneralPopulationOfCountryPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент численности городского и\r\nсельского населения по стране", TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinPlotArea };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string> { "Городское", "Сельское" };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 20
            };
            columnSeries.Items.AddRange(new List<ColumnItem>
            {
                new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") },
                new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") }
            });

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByRegionsPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент численности населения по областям" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>();
            foreach (DataRow item in rows)
            {
                categoriesNames.Add(item[0].ToString());
            }

            ColumnSeries columnSeries = new ColumnSeries 
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[1][1].ToString()), Color = OxyColor.Parse("#3498db") }); }
            if (rows.Count >= 3)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[2][1].ToString()), Color = OxyColor.Parse("#2ecc71") }); }
            if (rows.Count >= 4)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[3][1].ToString()), Color = OxyColors.Gold }); }
            if (rows.Count >= 5)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[4][1].ToString()), Color = OxyColors.Cyan }); }
            if (rows.Count >= 6)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[5][1].ToString()), Color = OxyColors.Khaki }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByAgeGroupsPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Численность населения основных возрастных\r\nгрупп" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Моложе трудоспособного",
                "Трудоспособное",
                "Старше трудоспособного"
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };
            columnSeries.Items.AddRange(new List<ColumnItem>
            {
                new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") },
                new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") },
                new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") },
            });

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByGenderPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Соотношение мужчин и женщин по стране" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "мужчины",
                "женщины",
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };
            columnSeries.Items.AddRange(new List<ColumnItem>
            {
                new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") },
                new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") },
            });

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByMaritalStatusPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент мужчин и женщин в возрасте 15 лет\r\nи старше по состоянию в браке" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Никогда не\r\nсостоявшие\r\nв браке",
                "Состоящие в браке/\r\nнезарегистрированных\r\nотношениях",
                "Вдовые",
                "Разведенные/\r\nразошедшиеся"
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") }); }
            if (rows.Count >= 3)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") }); }
            if (rows.Count >= 4)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][3].ToString()), Color = OxyColors.Gold }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreateWomenWithChildrenByCountPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент женщин, родивших определенное\r\nколичество детей" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>();
            foreach (DataRow item in rows)
            {
                categoriesNames.Add(item[0].ToString());
            }

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[1][1].ToString()), Color = OxyColor.Parse("#3498db") }); }
            if (rows.Count >= 3)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[2][1].ToString()), Color = OxyColor.Parse("#2ecc71") }); }
            if (rows.Count >= 4)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[3][1].ToString()), Color = OxyColors.Gold }); }
            if (rows.Count >= 5)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[4][1].ToString()), Color = OxyColors.Cyan }); }
            if (rows.Count >= 6)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[5][1].ToString()), Color = OxyColors.Khaki }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreateWomenWithChildrenByPlansPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент женщин, планирующих\r\nрождение детей" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Планируют",
                "Не планируют",
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#e74c3c") }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByEducationLvlPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент населения по стране по уровню\r\nобразования" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Начальное",
                "Общее\r\nбазовое",
                "Общее\r\nсреднее",
                "Проф.-\r\nтехни-\r\nческое",
                "Среднее\r\nспеци-\r\nальное",
                "Послевуз-\r\nовское/\r\nвысшее",
                "Без\r\nобразо-\r\nвания",
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") }); }
            if (rows.Count >= 3)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") }); }
            if (rows.Count >= 4)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][3].ToString()), Color = OxyColors.Gold }); }
            if (rows.Count >= 5)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][4].ToString()), Color = OxyColors.Coral }); }
            if (rows.Count >= 6)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][5].ToString()), Color = OxyColors.Khaki }); }
            if (rows.Count >= 7)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][6].ToString()), Color = OxyColors.Magenta }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByNationalityPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент населения по стране по\nнациональности" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Белорусы",
                "Русские",
                "Украинцы",
                "Поляки",
                "Другие"
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count >= 1)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") }); }
            if (rows.Count >= 2)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") }); }
            if (rows.Count >= 3)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") }); }
            if (rows.Count >= 4)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][3].ToString()), Color = OxyColors.Gold }); }
            if (rows.Count >= 5)
            { columnSeries.Items.Add(new ColumnItem { Value = double.Parse(rows[0][4].ToString()), Color = OxyColors.Coral }); }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryBySpeakingLanguagePlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент распространенности языков\nнациональных групп" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Белорусский",
                "Русский",
                "Украинский",
                "Польский",
                "Другие"
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count != 0)
            {
                columnSeries.Items.AddRange(new List<ColumnItem>
                {
                    new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") },
                    new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") },
                    new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") },
                    new ColumnItem { Value = double.Parse(rows[0][3].ToString()), Color = OxyColors.Gold },
                    new ColumnItem { Value = double.Parse(rows[0][4].ToString()), Color = OxyColors.Coral }
                });
            }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
        private PlotModel CreatePopulationOfCountryByEmploymentPlot(DataRowCollection rows)
        {
            PlotModel result = new PlotModel { Title = "Процент населения в зависимости от\nпринадлежности к рабочей силе" };
            DiagramsList.Add(result.Title);

            List<string> categoriesNames = new List<string>
            {
                "Занятые",
                "Безработные",
                "Вне категории"
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                Font = "Bahnschrift SemiBold Condensed",
                FontSize = 14
            };

            if (rows.Count != 0)
            {
                columnSeries.Items.AddRange(new List<ColumnItem>
                {
                    new ColumnItem { Value = double.Parse(rows[0][0].ToString()), Color = OxyColor.Parse("#e74c3c") },
                    new ColumnItem { Value = double.Parse(rows[0][1].ToString()), Color = OxyColor.Parse("#3498db") },
                    new ColumnItem { Value = double.Parse(rows[0][2].ToString()), Color = OxyColor.Parse("#2ecc71") }
                });
            }

            LinearAxis linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 100
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Selectable = false
            };

            categoryAxis.ActualLabels.AddRange(categoriesNames);
            result.Series.Add(columnSeries);
            result.Axes.Add(categoryAxis);
            result.Axes.Add(linearAxis);
            return result;
        }
    }
}
