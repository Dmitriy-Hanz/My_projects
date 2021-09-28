#define _CRT_SECURE_NO_WARNINGS
#include <io.h>
#include <stdio.h>
#include <iostream>
#include <istream>
#include <fstream>
#include <stdlib.h>
#include <time.h>
#include <Windows.h>
using namespace std;
#define bar cout << "\n\n"; system("pause");

struct ANKETA
{
	char FIO[100];
	char birth_Data[10];
	short unsigned int your_age = 0;
	bool gender = 0;
	short int first_homehold_person = 0;
	short int marital_status = 0;
	short unsigned int your_native_country = 0;
	//____
	short unsigned int your_living_place = 0;
	short unsigned int aducation_status = 0;
	short unsigned int your_income_sourse = 0;//!!!
	short unsigned int your_income_lvl = 0;//!!!
	short unsigned int your_work_status = 0;
	short unsigned int your_child_count = 0;//!!!
	int system_perc_integer = 0;
	int true_SPI = 0;
};
struct DT_income
{
	int mas_MIC[14];
	int mas_people[14];
	float mas_perc[14];
	int mWL[14][6];
};

class STATISTIC
{
protected:
public:
	STATISTIC()
	{

	}
	~STATISTIC()
	{

	}
	int Middle_Income(DT_income cc, int J)
	{
		int middle_income_count = 0, u2 = 0;
		for (int i = 0; i < 14; i++)
		{
			u2 = u2 + cc.mas_people[i];
		}
		middle_income_count = cc.mas_MIC[J] / u2;
		return middle_income_count;
	}
	int MCC(DT_income cc, int J)
	{
		int middle_income_count;
		cc.mas_people[J] == 0 ? middle_income_count = 0: middle_income_count = cc.mas_MIC[J] / cc.mas_people[J];
		return middle_income_count;
	}
	int percents(int sto, int del)
	{
		return (sto * 100) / del;
	}
};

ANKETA NEW_REGLIST(ANKETA NUM);
void AGE_GENDAR_STRUCT(ANKETA reg, DT_income cc);
DT_income AGE_GENDAR_SYSTEM(ANKETA reg, DT_income cc);
void FILE_INSERT(ANKETA NUM);
void RANDOMIZATOR(ANKETA reg);
void DEPENDENCE_TAB_INCOM(ANKETA reg, DT_income cc);
DT_income TAB_INCOM_SISTEM(DT_income cc, int Case, ANKETA reg);
void DEPENDENCE_TAB_CHILDREN(ANKETA reg, DT_income cc);
DT_income TAB_CHILD_SISTEM(DT_income cc, int Case, ANKETA reg);
void DEPENDENCE_TAB_LTYPE(ANKETA reg, DT_income cc);
DT_income TAB_LTYPE_SISTEM(DT_income cc, int Case, ANKETA reg);

void DEPENDENCE_FILE_CHILDREN(DT_income cc);
void DEPENDENCE_FILE_INCOM(DT_income cc);
void DEPENDENCE_TAB_LTYPE(DT_income cc);

void T_FSTATUS(DT_income cc, ANKETA reg);
void T_WSPHERE(DT_income cc, ANKETA reg);
void T_LTYPE(DT_income cc, ANKETA reg);
void T_ADLVL(DT_income cc, ANKETA reg);

void bolvanka()
{
	cout << "================================================================================\n";
	cout << "\n";
	cout << "================================================================================\n";
	cout << "<< ЭЛЕКТРОННАЯ ПЕРЕПИСЬ НАСЕЛЕНИЯ >>\n";
	cout << "================================================================================\n\n";

}
void SPRAVKA();

int main()
{
	setlocale(LC_ALL, "Russian");
	srand(time(NULL));
	int meniu = -1, meniu2 = -1, meniu3 = -1; bool dep_flag = 0;
	ANKETA A;
	DT_income I;
	ifstream TR("d:\\Pylo\\kadat.txt", ios::in);
	char tr = 0; int num;
	while (!(TR.eof()))
	{
		for (int i = 0; tr != '|'; i++)
		{
			TR >> tr;
			if (tr == NULL)
			{
				goto m;
			}
			if (TR.eof())
			{
				break;
			}
		}	
		TR >> num >> num >> num >> tr >> num >> tr >> num >> tr >> num >> tr >> num >> tr >> num >> tr;
		TR >> num >> tr >> num >> tr >> num >> tr >> num >> tr >> num >> tr;
		tr = 0;
		A.true_SPI++;
	}
m:	for (int i = 0; i < 14; i++)
	{
		I.mas_people[i] = 0;
		I.mas_MIC[i] = 0;
	}
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			I.mWL[i][j] = 0;
		}
	}

	while (meniu != 0)
	{
		bolvanka();
		cout << "1.Новая анкета\n2.Итоговые таблицы\n3.Таблицы зависимости\n4.Просмотр статистических данных\n5.Отчистить данные о населении\n6.О программе\n\n0.Выход";
		cout << "\n================================================================================\n";
		cin >> meniu;
		switch (meniu)
		{
		case 1:
		{
			while (meniu2 != 3)
			{
				system("cls");
				bolvanka();
				cout << "1.Анкетирование пользователя\n2.Автозаполняемая анкета\n\n3.В главное меню\n\n";
				cin >> meniu2;
				switch (meniu2)
				{
					case 1:
					{
						dep_flag = 1;
						A = NEW_REGLIST(A);
						FILE_INSERT(A);
						cout << "\nанкетирование окончено\n";
						system("pause");
						system("cls");
						break;
					}
					case 2:
					{
						dep_flag = 1;
						cout << "количество автозаполняемых анкет: ";
						cin >> A.system_perc_integer;
						A.true_SPI = A.true_SPI + A.system_perc_integer;
						RANDOMIZATOR(A);
						cout << "\nавтозаполнение окончено\n";
						system("pause");
						system("cls");
						break;
					}
					case 3:
					{
						break;
					}
					default:
					{
						cout << "\nтакого пункта нет в меню\n";
						system("pause");
						break;
					}
				}
				system("cls");
			}
			meniu2 = -1;
			break;
		}
		case 2:
		{
			if (dep_flag == 0)
			{
				cout << "отсутствуют данные для расчетов\n";
				system("pause");
				system("cls");
				break;
			}
			while (meniu2 != 0)
			{
				system("cls");
				bolvanka();
				cout << "Список итоговых таблиц:\n1.Половозрастная структура населения\n2.Сферы деятельности\n3.семейный статус";
				cout << "\n4.тип местности проживания\n5.уровень образования\n\n0.В главное меню\n";
				cin >> meniu2;
				switch (meniu2)
				{
				case 1:
				{	
					AGE_GENDAR_STRUCT(A,I);
					system("pause");
					system("cls");
					break;
				}
				case 2:
				{
					T_WSPHERE(I, A);
					system("cls");
					break;
				}
				case 3:
				{
					T_FSTATUS(I, A);
					system("cls");
					break;
				}
				case 4:
				{
					T_LTYPE(I, A);
					system("cls");
					break;
				}
				case 5:
				{
					T_ADLVL(I, A);
					system("cls");
					break;
				}
				case 0:
				{
					break;
				}
				default:
				{
					cout << "\nтакого пункта нет в меню\n";
					system("pause");
					break;
				}
				}
				system("cls");
			}
			meniu2 = -1;
			break;
		}
		case 3:
		{
			if (dep_flag == 0)
			{
				cout << "отсутствуют данные для расчетов\n";
				system("pause");
				system("cls");
				break;
			}
			while (meniu2 != 0)
			{
				system("cls");
				bolvanka();
				cout << "Список расчетных таблиц зависимостей:\n1.Уровень дохода от профессии, уровня образования, пола, возраста, места проживания\n";
				cout << "2.количество детей в семье от возраста, дохода, профессии родителя\n";
				cout << "3.тип местности проживания человека от дохода, профессии, уровня образования\n\n";
				cout << "0.В главное меню\n\n";
				cin >> meniu2;
				switch (meniu2)
				{
				case 1:
				{	
					DEPENDENCE_TAB_INCOM(A, I);
					system("pause");
					system("cls");
					break;
				}
				case 2:
				{
					DEPENDENCE_TAB_CHILDREN(A,I);
					system("pause");
					system("cls");
					break;
				}
				case 3:
				{
					DEPENDENCE_TAB_LTYPE(A, I);
					system("pause");
					system("cls");
					break;
				}
				case 0:
				{
					break;
				}
				default:
				{
					cout << "\nтакого пункта нет в меню\n";
					system("pause");
					break;
				}
				}
				system("cls");
			}
			meniu2 = -1;
			break;
		}
		case 4:
		{
			while (meniu2 != 0)
			{
				system("cls");
				bolvanka();
				cout << "Таблицы:\n\n1.Уровень дохода от профессии, уровня образования, пола, возраста, места проживания";
				cout << "\n2.количество детей в семье от возраста, дохода, профессии родителя\n";
				cout << "3.тип местности проживания человека от дохода, профессии, уровня образования\n\n";
				cout << "0.Назад\n\n";
				cin >> meniu2;
				switch (meniu2)
				{
				case 1:
				{
					DEPENDENCE_FILE_INCOM(I);
					bar;
					break;
				}
				case 2:
				{
					DEPENDENCE_FILE_CHILDREN(I);
					bar;
					break;
				}
				case 3:
				{
					DEPENDENCE_TAB_LTYPE(I);
					bar;
					break;
				}
				case 0:
				{
					break;
				}
				default:
				{
					cout << "\nтакого пункта нет в меню\n";
					system("pause");
					break;
				}
				}
				system("cls");
			}
			meniu2 = -1;
			break;
		}
		case 5:
		{	system("cls");
			bolvanka();
			int n = -1;
			cout << "\n\n\t\t\tДальнейшие деиствия приведут к уничтожению собранных данных о населении.Вы уверены?\n";
			cout << "\t\t\t1.Да\n\t\t\t2.Нет\n\t\t\t\t\t";
			cin >> n;
			if (n == 2)
			{
				system("cls");
				break;
			}
			else
			{
				cout << "\t\t\tВы ТОЧНО уверенны?\n";
				cout << "\t\t\t1.Да\n\t\t\t2.Нет\n\t\t\t\t\t";
				cin >> n;
				if (n == 2)
				{
					system("cls");
					break;
				}
				else
				{
					ofstream D1("d:\\Pylo\\kadat.txt", ios::out);
					D1.clear();
					A.system_perc_integer = 0;
					A.true_SPI = 0;
					cout << "\t\t\tВсе данные удалены\n";
					system("pause");
					system("cls");
				}
			}
			break;
		}
		case 6:
		{
			SPRAVKA();
			bar;
			system("cls");
			break;
		}
		case 0:
		{
			break;
		}
		default:
		{
			cout << "нет такого пункта";
			meniu = NULL;
			bar;
		}
		system("cls");
		}//конец первого SWITCH
	}
	bar;
	return 0;
}

ANKETA NEW_REGLIST(ANKETA NUM)
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	cin.ignore();
	cout << "Ваше ФИО (фамилия, имя, отчество):" ;
	cin.getline(NUM.FIO, 99, '\n');
	cout << "\n_______________________________________________________________________________________________________________________\n";
	cout << "Дата рождения (писать цифрами через пробел): ";
	cin.getline(NUM.birth_Data , 11, '\n');
	cout << "\n_______________________________________________________________________________________________________________________\n";
	cout << "Ваш пол: \n\n0.Мужчина\t1.Жинщина\n";
	cin >> NUM.gender;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Родственные или другие отношения с лицом, указанным первым в домохозяйстве: \n\n";
	cout << "1.лицо, указанное  первым в домохозяйстве\t2.жена, муж\t\t3.дочь, сын, падчерица, пасынок\n4.мать, отец\t\t\t\t\t";
	cout << "5.сестра, брат \t\t6.свекровь, свекор, теща, тесть\n7.невестка, зять\t\t\t\t8.бабка, дед\t\t9.внучка, внук\n";
	cout <<	"10.другая степень родства, свойства\t\t11.не родственник\n";
	cin >> NUM.first_homehold_person;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Ваше семейное положение: \n\n1.никогда не состоял(а) в браке\t\t\t2.состою в зарегистрированном браке\n";
	cout << "3.состою в незарегистрированных отношениях\t4.вдовец, вдова\n5.разведен(а)\t\t\t\t\t6.разошелся(лась)\n";
	cin >> NUM.marital_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Место вашего рождения:\n\n1.Республика Беларусь\t2.другая страна.\n";
	cin >> NUM.your_native_country;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << " Укажите тип населенного пункта, в котором вы проживаете:\n\n1.город\t2.поселок городского типа\t3.сельская местность\n";
	cin >> NUM.your_living_place;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Ваш уровень (ступень) образования: \n\n1.начальное\t\t4.проф техническое\t7.высшее (с магистратурой)\n";
	cout << "2.общее базовое\t\t5.среднее специальное\t8.послевузовское\n";
	cout << "3.общее среднее\t\t6.высшее\t\t9.не имею образование\n";
	cin >> NUM.aducation_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Укажите источники средств к существованию, имеющиеся в году проведения переписи населения:\n\n";
	cout << "1.работа по найму\t\t\t\t\t9.пособие по безработице\n2.работа в личном подсобном хозяйстве\t\t\t10.стипендия\n";
	cout << "3.самозанятость\t\t\t\t\t\t11.иные виды пособий и помощи\n4.производство товаров для собственного использования\t12.ссуды или использование сбережений, реализация имущества\n";
	cout << "5.государственная пенсия\t\t\t\t13.на иждивении другого лица\n6.пенсия (иная социальная выплата)\t\t\t14.прочие источники \n";
	cout << "7.пособие по болезни, материнству\n8.семейные пособия \n";
	cin >> NUM.your_income_sourse;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "укажите ваш средний уровень дохода на основной работе(число, руб.):\n";
	cin >> NUM.your_income_lvl;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Укажите, к какому виду экономической деятельности относится ваша профессия:\n\n1.Информация и связь\t\t\t\t\t8.Оптовая и розничная торговля\n2.Финансовая и страховая деятельность\t\t\t9.Операции с недвижимым имуществом";
	cout << "\n3.Профессиональная, научная и техническая деятельность\t10.Здравоохранение\n4.Промышленность\t\t\t\t\t11.Творчество, спорт, развлечения и отдых";
	cout << "\n5.Транспортная деятельность, складирование\t\t12.Услуги по временному проживанию и питанию\n6.Строительство\t\t\t\t\t\t13.Сельское, лесное и рыбное хозяйство";
	cout << "\n7.Деятельность в сфере адменистративных услуг\t\t14.Образование\n";
	cin >> NUM.your_work_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "Сколько детей вы родили(укажите количество):\n";
	cin >> NUM.your_child_count;
	return NUM;
}
void RANDOMIZATOR(ANKETA reg)
{
	ofstream T("d:\\Pylo\\kadat.txt", ios::app);
	int j = 0, ii = 0;
	for (int i = 0; i < reg.system_perc_integer; i++)
	{
		reg.FIO[0] = 'n';
		reg.FIO[1] = 'a';
		reg.FIO[2] = 'm';
		reg.FIO[3] = 'e';
		reg.birth_Data[0] = '1';
		reg.birth_Data[1] = '0';
		reg.birth_Data[2] = ' ';
		reg.birth_Data[3] = '1';
		reg.birth_Data[4] = '0';
		reg.birth_Data[5] = ' ';
		int dek = 1000,randmn = (1930 + rand() % 76);
		for (ii = 6; ii < 10; ii++)
		{
			reg.birth_Data[ii] = (randmn / dek);
			randmn = randmn % dek;
			dek = dek / 10;
			for (int j = 0; j < 10; j++)
			{
				if (reg.birth_Data[ii] == j)
				{
					reg.birth_Data[ii] = 48 + j;
				}
			}
		}
		ii = 0;

		reg.gender = 0 + rand() % 2;
		reg.first_homehold_person = 1 + rand() % 12;
		reg.marital_status = 1 + rand() % 6;
		reg.your_native_country = 0 + rand() % 2;
		reg.your_living_place = 1 + rand() % 3;
		reg.aducation_status = 1 + rand() % 9;
		reg.your_income_sourse = 1 + rand() % 14;
		reg.your_income_lvl = 300 + rand() % 2701;
		reg.your_work_status = 1 + rand() % 14;
		reg.your_child_count = 0 + rand() % 11;

		for (int i = 0; i < 4; i++)
		{
			T << reg.FIO[i];
		}
		T << "|";
		while (j != 10)
		{
			T << reg.birth_Data[j];
			j++;
		}
		j = 0;
		T << "|" << reg.gender << "|" << reg.first_homehold_person << "|" << reg.marital_status << "|" << reg.your_native_country << "|";
		T << reg.your_living_place << "|" << reg.aducation_status << "|" << reg.your_income_sourse << "|" << reg.your_income_lvl << "|";
		T << reg.your_work_status << "|" << reg.your_child_count << "~\n";
	}
	T.close();
}
void FILE_INSERT(ANKETA NUM)
{
	ofstream T("d:\\Pylo\\kadat.txt", ios::out);
	for (int i = 0; NUM.FIO[i] != NULL ; i++)
	{
		T << NUM.FIO[i];
	}
	T << '|';
	for (int i = 0; i < 10 ; i++)
	{
		T << NUM.birth_Data[i];
	}
	T << '|' << NUM.gender << '|' << NUM.first_homehold_person << '|' << NUM.marital_status << '|' << NUM.your_native_country << '|';
	T << NUM.your_living_place << '|' << NUM.aducation_status << '|' << NUM.your_income_sourse << '|' << NUM.your_income_lvl << '|';
	T << NUM.your_work_status << '|' << NUM.your_child_count << "~\n";
	return;
}

void AGE_GENDAR_STRUCT(ANKETA reg, DT_income cc)
{
	cc = AGE_GENDAR_SYSTEM(reg, cc);
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\nВсе население: \n\n";
	cout << "Оба пола:\tМужчины: "<< cc.mWL[0][0] <<"\tЖенщины: "<< cc.mWL[0][1] <<"\nМужчины(в процентах): "<< cc.mWL[0][2] <<"\nЖенщины(в процентах): " << cc.mWL[0][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\nМоложе трудоспособного возраста (мужчины и женщины до 16 лет): \n\n";
	cout << "Оба пола:\tМужчины: " << cc.mWL[1][0] << "\tЖенщины: " << cc.mWL[1][1] << "\nМужчины(в процентах): " << cc.mWL[1][2] <<"\nЖенщины(в процентах): " << cc.mWL[1][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\nТрудоспособный возраст (мужчины 16-59 лет, женщины 16-54 года): \n\n";
	cout << "Оба пола:\tМужчины: " << cc.mWL[2][0] << "\tЖенщины: " << cc.mWL[2][1] << "\nМужчины(в процентах): " << cc.mWL[2][2] << "\nЖенщины(в процентах): " << cc.mWL[2][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\nСтарше трудоспособного (мужчины 60 лет и более, женщины 55 лет и более): \n\n";
	cout << "Оба пола:\tМужчины: " << cc.mWL[3][0] << "\tЖенщины: " << cc.mWL[3][1] << "\nМужчины(в процентах): " << cc.mWL[3][2] << "\nЖенщины(в процентах): " << cc.mWL[3][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	return;
}
DT_income AGE_GENDAR_SYSTEM(ANKETA reg, DT_income cc)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda = 0; int s, data; bool gen;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (trash == NULL)
			{
				goto m;
			}
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> gen >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash;//living_place->
		TT >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> tilda;
		trash = tilda;

		if (TT.eof())
		{
			break;
		}
		for (int i = 0; i < 2; i++)
		{
			if (gen == i)
			{
				cc.mWL[0][i]++;
			}
		}

		if (2019 - data <= 16)
		{
			for (int i = 0; i < 2; i++)
			{
				if (gen == i)
				{
					cc.mWL[1][i]++;
				}
			}
		}
		if ((2019 - data >= 16) & (2019 - data <= 56))
		{
			if ((gen == 1)&(2019 - data <= 54))
			{
				cc.mWL[2][1]++;
			}
			if (gen == 0)
			{
				cc.mWL[2][0]++;
			}
		}
		if (2019 - data >= 54)
		{
			if ((gen == 0)&(2019 - data >= 56))
			{
				cc.mWL[3][0]++;
			}
			if (gen == 1)
			{
				cc.mWL[3][1]++;
			}
		}
	}
m:	TT.close();
	
	for (int i = 0; i < 4; i++)
	{
		if (cc.mWL[i][0] + cc.mWL[i][1] + cc.mWL[i][2] != 0)
		{
			cc.mWL[i][2] = (cc.mWL[i][0] * 100) / (cc.mWL[i][0] + cc.mWL[i][1]);
			cc.mWL[i][3] = (cc.mWL[i][1] * 100) / (cc.mWL[i][0] + cc.mWL[i][1]);
		}
	}
	return cc;
}

void T_FSTATUS(DT_income cc, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda = 0; int s, data, mstat; bool gen;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> gen >> trash >> s >> trash >> mstat >> trash >> s >> trash >> s >> trash;//living_place->
		TT >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> tilda;
		trash = 0;
		if (TT.eof())
		{
			break;
		}
		for (int i = 0; i < 7; i++)
		{
			if (mstat == i + 1)
			{
				cc.mas_people[i]++;
			}
		}
	}
	for (int i = 0; i < 6; i++)
	{
		reg.true_SPI != 0 ? cc.mas_perc[i] = (cc.mas_people[i] * 100) / reg.true_SPI : cc.mas_perc[i] = 0;
	}
	cout << "_________________________________________________\n";
	cout << "|семейный статус     |количество     |количество |\n";
	cout << "|                    |граждан, чел.  |граждан, % |\n";
	cout << "|====================|===============|===========|\n";
	cout << "|никогда не          |" << cc.mas_people[0] << "\t     |" << cc.mas_perc[0] << "\t |\n";
	cout << "|состоял(а) в браке  |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|зарегистрированный  |" << cc.mas_people[1] << "\t     |" << cc.mas_perc[1] << "\t |\n";
	cout << "|брак                |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|незарегистрированные|" << cc.mas_people[2] << "\t     |" << cc.mas_perc[2] << "\t |\n";
	cout << "|отношения           |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|вдовец, вдова       |" << cc.mas_people[3] << "\t     |" << cc.mas_perc[3] << "\t |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|разведен(а)         |" << cc.mas_people[4] << "\t     |" << cc.mas_perc[4] << "\t |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|разошелся(лась)     |" << cc.mas_people[5] << "\t     |" << cc.mas_perc[5] << "\t |\n";
	cout << "|____________________|_______________|___________|\n";
	for (int i = 0; i < 6; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	TT.close();
	bar;
	return;
}
void T_WSPHERE(DT_income cc, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda = 0; int s, data, mstat, work; bool gen;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> gen >> trash >> s >> trash >> mstat >> trash >> s >> trash >> s >> trash;//living_place->
		TT >> s >> trash >> s >> trash >> s >> trash >> work >> trash >> s >> tilda;
		trash = 0;
		if (TT.eof())
		{
			break;
		}
		for (int i = 0; i < 14; i++)
		{
			if (work == i + 1)
			{
				cc.mas_people[i]++;
			}
		}
	}
	for (int i = 0; i < 14; i++)
	{
		reg.true_SPI != 0 ? cc.mas_perc[i] = (cc.mas_people[i] * 100) / reg.true_SPI : cc.mas_perc[i] = 0;
	}
	cout << "_________________________________________________\n";
	cout << "|эконом. деятельность|количество    |количество |\n";
	cout << "|                    |граждан, чел. |граждан, % |\n";
	cout << "|====================|==============|===========|\n";
	cout << "|Информация и связь  |" << cc.mas_people[0] << "\t    |" << cc.mas_perc[0] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Опт.и розн.торговля |" << cc.mas_people[1] << "\t    |" << cc.mas_perc[1] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Финансовая и стра-  |" << cc.mas_people[2] << "\t    |" << cc.mas_perc[2] << "\t\t|\n";
	cout << "|ховая деятельность  |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Операции с недви-   |" << cc.mas_people[3] << "\t    |" << cc.mas_perc[3] << "\t\t|\n";
	cout << "|жимым имуществом    |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Проф. науч. и тех.  |" << cc.mas_people[4] << "\t    |" << cc.mas_perc[4] << "\t\t|\n";
	cout << "|деятельность        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Здравоохранение     |" << cc.mas_people[5] << "\t    |" << cc.mas_perc[5] << "\t\t| \n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Промышленность      |" << cc.mas_people[6] << "\t    |" << cc.mas_perc[6] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Творчество, спорт,  |" << cc.mas_people[7] << "\t    |" << cc.mas_perc[7] << "\t\t|\n";
	cout << "|развлечения и отдых |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Транспортная        |" << cc.mas_people[8] << "\t    |" << cc.mas_perc[8] << "\t\t|\n";
	cout << "|деятельность        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Услуги по временному|" << cc.mas_people[9] << "\t    |" << cc.mas_perc[9] << "\t\t|\n";
	cout << "|проживанию и питанию|              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Образование         |" << cc.mas_people[10] << "\t    |" << cc.mas_perc[10] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Сельское, лесное и  |" << cc.mas_people[11] << "\t    |" << cc.mas_perc[11] << "\t\t|\n";
	cout << "|рыбное хозяйство    |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Деятельность в сфере|" << cc.mas_people[12] << "\t    |" << cc.mas_perc[12] << "\t\t|\n";
	cout << "|админ. услуг        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|Строительство       |" << cc.mas_people[13] << "\t    |" << cc.mas_perc[13] << "\t\t|\n";
	cout << "|====================|==============|===========|\n\n";
	for (int i = 0; i < 6; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	TT.close();
	bar;
	return;
}
void T_LTYPE(DT_income cc, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda = 0; int s, data, mstat, ltype; bool gen;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> ltype >> trash;//living_place->
		TT >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> tilda;
		trash = 0;
		if (TT.eof())
		{
			break;
		}
		for (int i = 0; i < 3; i++)
		{
			if (ltype == i + 1)
			{
				cc.mas_people[i]++;
			}
		}
	}
	for (int i = 0; i < 3; i++)
	{
		reg.true_SPI != 0 ? cc.mas_perc[i] = (cc.mas_people[i] * 100) / reg.true_SPI : cc.mas_perc[i] = 0;
	}
	cout << "_________________________________________________\n";
	cout << "|тип места          |количество      |количество |\n";
	cout << "|проживания         |граждан, чел.   |граждан, % |\n";
	cout << "|===================|================|===========|\n";
	cout << "|Город              |" << cc.mas_people[0] << "\t\t     |" << cc.mas_perc[0] << "\t |\n";
	cout << "|___________________|________________|___________|\n";
	cout << "|ПГТ                |" << cc.mas_people[1] << "\t\t     |" << cc.mas_perc[1] << "\t |\n";
	cout << "|___________________|________________|___________|\n";
	cout << "|Сельская местность |" << cc.mas_people[2] << "\t\t     |" << cc.mas_perc[2] << "\t |\n";
	cout << "|___________________|________________|___________|\n\n";
	for (int i = 0; i < 6; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	TT.close();
	bar;
	return;
}
void T_ADLVL(DT_income cc, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda = 0; int s, data, mstat, ad_status; bool gen;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash;//living_place->
		TT >> ad_status >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> tilda;
		trash = 0;
		if (TT.eof())
		{
			break;
		}
		for (int i = 0; i < 9; i++)
		{
			if (ad_status == i + 1)
			{
				cc.mas_people[i]++;
			}
		}
	}
	for (int i = 0; i < 9; i++)
	{
		reg.true_SPI != 0 ? cc.mas_perc[i] = (cc.mas_people[i] * 100) / reg.true_SPI : cc.mas_perc[i] = 0;
	}
	cout << "____________________________________________________\n";
	cout << "|ур. образования         |количество    |количество |\n";
	cout << "|                        |граждан, чел. |граждан, % |\n";
	cout << "|========================|==============|===========|\n";
	cout << "|начальное               |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|общее базовое           |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|общее среднее           |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|проф техническое        |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|среднее специальное     |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|высшее                  |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|высшее (с магистратурой)|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|послевузовское          |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|нет образование         |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	for (int i = 0; i < 6; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	TT.close();
	bar;
	return;
}

DT_income TAB_INCOM_SISTEM(DT_income cc, int Case, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda; int s, income, work, ad_status, gen, data, place_type;
	STATISTIC sa;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> gen >> trash >> s >> trash >> s >> trash >> s >> trash >> place_type >> trash;//living_place->
		TT >> ad_status >> trash >> s >> trash >> income >> trash >> work >> trash >> s >> tilda;
		trash = tilda;
		if (TT.eof())
		{
			break;
		}
		switch (Case)
		{
		case 1:
		{
			for (int i = 0; i < 15; i++)
			{
				if (work == i+1)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + income;
					cc.mas_people[i]++;
				}
			}

			break;
		}
		case 2:
		{
			for (int i = 0; i < 10; i++)
			{
				if (ad_status == i + 1)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + income;
					cc.mas_people[i]++;
				}
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < 2; i++)
			{
				if (gen == i)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + income;
					cc.mas_people[i]++;
				}
			}
			break;
		}
		case 4:
		{
			if (2019 - data <= 16)
			{
				cc.mas_people[0]++;
				cc.mas_MIC[0] = cc.mas_MIC[0] + income;
			}
			if ((2019 - data > 16) & (2019 - data <= 56) & (gen != 1))
			{
				cc.mas_people[1]++;
				cc.mas_MIC[1] = cc.mas_MIC[1] + income;
			}
			else
			{
				if ((2019 - data > 16) & (2019 - data <= 54) & (gen == 1))
				{
					cc.mas_people[1]++;
					cc.mas_MIC[1] = cc.mas_MIC[1] + income;
				}
			}
			if (2019 - data > 54 && gen == 1)
			{
				cc.mas_people[2]++;
				cc.mas_MIC[2] = cc.mas_MIC[2] + income;
			}
			else
			{
				if (2019 - data > 56 && gen != 1)
				{
					cc.mas_people[2]++;
					cc.mas_MIC[2] = cc.mas_MIC[2] + income;
				}
			}
			break;
		}
		case 5:
		{
			for (int i = 0; i < 4; i++)
			{
				if (place_type == i+1)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + income;
					cc.mas_people[i]++;
				}
			}

			break;
		}
		}
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_perc[i] = sa.percents(cc.mas_people[i], reg.true_SPI);
	}
	TT.close();
	return cc;
}
void DEPENDENCE_TAB_INCOM(ANKETA reg, DT_income cc)
{
	ofstream D1("d:\\Pylo\\dt_incom.txt", ios::out);
	STATISTIC sa;
	cc = TAB_INCOM_SISTEM(cc, 1, reg);
	cout << "____________________________________________________________________\n";
	cout << "|эконом. деятельность|среднее значение  |количество     |количество |\n";
	cout << "|                    |дохода            |граждан, чел.  |граждан, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|Информация и связь  |" << sa.Middle_Income(cc, 0) << "\t\t|"<< cc.mas_people[0] <<"\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Опт.и розн.торговля |" << sa.Middle_Income(cc, 1) << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Финансовая и стра-  |" << sa.Middle_Income(cc, 2) << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|ховая деятельность  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Операции с недви-   |" << sa.Middle_Income(cc, 3) << "\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|жимым имуществом    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Проф. науч. и тех.  |" << sa.Middle_Income(cc, 4) << "\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Здравоохранение     |" << sa.Middle_Income(cc, 5) << "\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Промышленность      |" << sa.Middle_Income(cc, 6) << "\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Творчество, спорт,  |" << sa.Middle_Income(cc, 7) << "\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|развлечения и отдых |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Транспортная        |" << sa.Middle_Income(cc, 8) << "\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Услуги по временному|" << sa.Middle_Income(cc, 9) << "\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|проживанию и питанию|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Образование         |" << sa.Middle_Income(cc, 10) << "\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Сельское, лесное и  |" << sa.Middle_Income(cc, 11) << "\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|рыбное хозяйство    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Деятельность в сфере|" << sa.Middle_Income(cc, 12) << "\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|админ. услуг        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Строительство       |" << sa.Middle_Income(cc, 13) << "\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
	cout << "|====================|==================|===============|===========|\n\n";
	for (int i = 0; i < 14; i++)
	{
		D1 << sa.Middle_Income(cc, i) << '|' << cc.mas_people[i] <<  '|' << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	cc = TAB_INCOM_SISTEM(cc,2,reg);
	cout << "____________________________________________________________________\n";
	cout << "|ур. образования         |среднее        |количество    |количество |\n";
	cout << "|                        |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|========================|===============|==============|===========|\n";
	cout << "|начальное               |" << sa.Middle_Income(cc, 0) << "\t\t |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|общее базовое           |" << sa.Middle_Income(cc, 1) << "\t\t |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|общее среднее           |" << sa.Middle_Income(cc, 2) << "\t\t |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|проф техническое        |" << sa.Middle_Income(cc, 3) << "\t\t |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|среднее специальное     |" << sa.Middle_Income(cc, 4) << "\t\t |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|высшее                  |" << sa.Middle_Income(cc, 5) << "\t\t |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|высшее (с магистратурой)|" << sa.Middle_Income(cc, 6) << "\t\t |" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|послевузовское          |" << sa.Middle_Income(cc, 7) << "\t\t |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|нет образование         |" << sa.Middle_Income(cc, 8) << "\t\t |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 9; i++)
	{
		D1 << sa.Middle_Income(cc, i) << '|' << cc.mas_people[i] << "|" << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	cc = TAB_INCOM_SISTEM(cc, 3, reg);
	cout << "________________________________________________________\n";
	cout << "|Пол         |среднее        |количество    |количество |\n";
	cout << "|            |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|============|===============|==============|===========|\n";
	cout << "|мужчина     |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t    |" << cc.mas_perc[0] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n";
	cout << "|женщина     |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t    |" << cc.mas_perc[1] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 2; i++)
	{
		D1 << sa.Middle_Income(cc, i) << '|' << cc.mas_people[i] << "|" << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	cc = TAB_INCOM_SISTEM(cc, 4, reg);
	cout << "__________________________________________________________________\n";
	cout << "|возрастная группа     |среднее        |количество    |количество |\n";
	cout << "|                      |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|Моложе трудоспособного|" << sa.Middle_Income(cc, 0) << "\t       |" << cc.mas_people[0] << "\t      |" << cc.mas_perc[0] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|трудоспособный возраст|" << sa.Middle_Income(cc, 1) << "\t       |" << cc.mas_people[1] << "\t      |" << cc.mas_perc[1] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|старше трудоспособного|" << sa.Middle_Income(cc, 2) << "\t       |" << cc.mas_people[2] << "\t      |" << cc.mas_perc[2] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D1 << sa.Middle_Income(cc, i) << '|' << cc.mas_people[i] << "|" << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	cc = TAB_INCOM_SISTEM(cc, 5, reg);
	cout << "___________________________________________________________________\n";
	cout << "|тип места          |среднее         |количество       |количество |\n";
	cout << "|проживания         |значение дохода |граждан, чел.    |граждан, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|Город              |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|ПГТ                |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|Сельская местность |" << sa.Middle_Income(cc, 2) << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D1 << sa.Middle_Income(cc, i) << '|' << cc.mas_people[i] << "|" << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	D1.close();
	return;
}

DT_income TAB_CHILD_SISTEM(DT_income cc, int Case, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda; int s, income, work, data, place_type, child_count;
	STATISTIC sa;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash;//living_place->
		TT >> s >> trash >> s >> trash >> income >> trash >> work >> trash >> child_count >> tilda;
		trash = tilda;
		if (TT.eof())
		{
			break;
		}
		switch (Case)
		{
		case 1:
		{
			for (int i = 0; i < 15; i++)
			{
				if (work == i + 1)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + child_count;
					cc.mas_people[i]++;
				}
			}
			break;
		}
		case 2:
		{
			if (child_count == 0)
			{
				cc.mas_MIC[0] = cc.mas_MIC[0] + income;
				cc.mas_people[0]++;
			}
			if (child_count <= 3 && child_count != 0)
			{
				cc.mas_MIC[1] = cc.mas_MIC[1] + income;
				cc.mas_people[1]++;
			}
			if (child_count > 3)
			{
				cc.mas_MIC[2] = cc.mas_MIC[2] + income;
				cc.mas_people[2]++;
			}
			break;
		}
		case 3:
		{
			if (2019 - data <= 16)
			{
				cc.mas_people[0]++;
				cc.mas_MIC[0] = cc.mas_MIC[0] + child_count;
			}
			if ((2019 - data >= 16) & (2019 - data <= 56))
			{
				cc.mas_people[1]++;
				cc.mas_MIC[1] = cc.mas_MIC[1] + child_count;
			}
			if (2019 - data >= 54)
			{
				cc.mas_people[2]++;
				cc.mas_MIC[2] = cc.mas_MIC[2] + child_count;
			}
			break;
		}
		}
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_perc[i] = sa.percents(cc.mas_people[i], reg.true_SPI);
	}
	TT.close();
	return cc;
}
void DEPENDENCE_TAB_CHILDREN(ANKETA reg, DT_income cc)
{
	ofstream D2("d:\\Pylo\\dt_deti.txt", ios::out);
	STATISTIC sa;
	cc = TAB_CHILD_SISTEM(cc, 1,reg);
	cout << "____________________________________________________________________\n";
	cout << "|эконом. деятельность|среднее кол-во    |количество     |количество |\n";
	cout << "|                    |детей  семье      |граждан, чел.  |граждан, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|Информация и связь  |" << sa.MCC(cc, 0) << "\t\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Опт.и розн.торговля |" << sa.MCC(cc, 1) << "\t\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Финансовая и стра-  |" << sa.MCC(cc, 2) << "\t\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|ховая деятельность  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Операции с недви-   |" << sa.MCC(cc, 3) << "\t\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|жимым имуществом    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Проф. науч. и тех.  |" << sa.MCC(cc, 4) << "\t\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Здравоохранение     |" << sa.MCC(cc, 5) << "\t\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Промышленность      |" << sa.MCC(cc, 6) << "\t\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Творчество, спорт,  |" << sa.MCC(cc, 7) << "\t\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|развлечения и отдых |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Транспортная        |" << sa.MCC(cc, 8) << "\t\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Услуги по временному|" << sa.MCC(cc, 9) << "\t\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|проживанию и питанию|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Образование         |" << sa.MCC(cc, 10) << "\t\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Сельское, лесное и  |" << sa.MCC(cc, 11) << "\t\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|рыбное хозяйство    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Деятельность в сфере|" << sa.MCC(cc, 12) << "\t\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|админ. услуг        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Строительство       |" << sa.MCC(cc, 13) << "\t\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
	cout << "|====================|==================|===============|===========|\n\n";
	for (int i = 0; i < 14; i++)
	{
		D2 << sa.MCC(cc, i) << '|' << cc.mas_people[i] << '|' << cc.mas_perc[i] <<"\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
	}
	cc = TAB_CHILD_SISTEM(cc, 2,reg);
	cout << "____________________________________________________________\n";
	cout << "|кол-во детей  |среднее значение|количество     |количество |\n";
	cout << "|в семье       |дохода          |граждан, чел.  |граждан, % |\n";
	cout << "|==============|================|===============|===========|\n";
	cout << "|нет детей     |" << sa.MCC(cc, 0) << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|от 1 до 3     |" << sa.MCC(cc, 1) << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|больше 3      |" << sa.MCC(cc, 2) << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D2 << sa.MCC(cc, i) << '|' << cc.mas_people[i] << '|' << cc.mas_perc[i] <<"\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
	}
	cc = TAB_CHILD_SISTEM(cc, 3,reg);
	cout << "__________________________________________________________________\n";
	cout << "|возрастная группа     |среднее кол-во |количество    |количество |\n";
	cout << "|                      |детей в семье  |граждан, чел. |граждан, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|Моложе трудоспособного|" << sa.MCC(cc, 0) << "\t       |" << cc.mas_people[0] << "\t      |" << cc.mas_perc[0] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|трудоспособный возраст|" << sa.MCC(cc, 1) << "\t       |" << cc.mas_people[1] << "\t      |" << cc.mas_perc[1] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|старше трудоспособного|" << sa.MCC(cc, 2) << "\t       |" << cc.mas_people[2] << "\t      |" << cc.mas_perc[2] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D2 << sa.MCC(cc, i) << '|' << cc.mas_people[i] << '|' << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
	}
	D2.close();
	return;
}

DT_income TAB_LTYPE_SISTEM(DT_income cc, int Case, ANKETA reg)
{
	ifstream TT("d:\\Pylo\\kadat.txt", ios::in);
	char trash = 0, tilda; int s, income, work, data, place_type, ad_status;
	STATISTIC sa;
	while (!(TT.eof()))
	{
		for (int i = 0; trash != '|'; i++)
		{
			TT >> trash;
			if (TT.eof())
			{
				break;
			}
		}
		TT >> s >> s >> data >> trash >> s >> trash >> s >> trash >> s >> trash >> s >> trash >> place_type >> trash;//living_place->
		TT >> ad_status >> trash >> s >> trash >> income >> trash >> work >> trash >> s >> tilda;
		trash = tilda;
		if (TT.eof())
		{
			break;
		}
		switch (Case)
		{
		case 1:
		{
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if ((work == i + 1)&(place_type == j + 1))
					{
						cc.mWL[i][j]++;
					}
				}
			}
			break;
		}
		case 2:
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if ((ad_status == i + 1)&(place_type == j + 1))
					{
						cc.mWL[i][j]++;
					}
				}
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < 4; i++)
			{
				if (place_type == i + 1)
				{
					cc.mas_MIC[i] = cc.mas_MIC[i] + income;
					cc.mas_people[i]++;
				}
			}
			for (int i = 0; i < 14; i++)
			{
				cc.mas_perc[i] = sa.percents(cc.mas_people[i], reg.true_SPI);
			}
			break;
		}
		}
	}
	if (Case != 3)
	{
		int k = 3;
		for (int i = 0; i < 14; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (cc.mWL[i][0] + cc.mWL[i][1] + cc.mWL[i][2] != 0)
				{
					cc.mWL[i][k] = (cc.mWL[i][j] * 100) / (cc.mWL[i][0] + cc.mWL[i][1] + cc.mWL[i][2]);
					k++;
				}
			}
			k = 3;
		}
	}
	TT.close();
	return cc;
}
void DEPENDENCE_TAB_LTYPE(ANKETA reg, DT_income cc)
{
	ofstream D3("d:\\Pylo\\dt_ltype.txt", ios::out);
	STATISTIC sa;
	cc = TAB_LTYPE_SISTEM(cc, 1, reg);
	cout << "____________________________________________________________________________________________________\n";
	cout << "|эконом. деятельность|кол-во граждан (чел.), проживающих в:  |количество граждан, %:                |\n";
	cout << "|                    |_______________________________________|______________________________________|\n";
	cout << "|                    |Городе    |ПГТ     |Сельской местности |Городе    |ПГТ    |Сельской местности |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|Информация и связь  |" <<cc.mWL[0][0]<<"\t|"<< cc.mWL[0][1]<< "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Опт.и розн.торговля |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Финансовая и стра-  |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|ховая деятельность  |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Операции с недви-   |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|жимым имуществом    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Проф. науч. и тех.  |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|деятельность        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Здравоохранение     |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Промышленность      |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Творчество, спорт,  |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|развлечения и отдых |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Транспортная        |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|деятельность        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Услуги по временному|" << cc.mWL[9][0] << "\t|" << cc.mWL[9][1] << "\t |" << cc.mWL[9][2] << "\t\t     |" << cc.mWL[9][3] << "\t|" << cc.mWL[9][4] << "\t|" << cc.mWL[9][5] << "\t\t    |\n";
	cout << "|проживанию и питанию|          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Образование         |" << cc.mWL[10][0] << "\t|" << cc.mWL[10][1] << "\t |" << cc.mWL[10][2] << "\t\t     |" << cc.mWL[10][3] << "\t|" << cc.mWL[10][4] << "\t|" << cc.mWL[10][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Сельское, лесное и  |" << cc.mWL[11][0] << "\t|" << cc.mWL[11][1] << "\t |" << cc.mWL[11][2] << "\t\t     |" << cc.mWL[11][3] << "\t|" << cc.mWL[11][4] << "\t|" << cc.mWL[11][5] << "\t\t    |\n";
	cout << "|рыбное хозяйство    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Деятельность в сфере|" << cc.mWL[12][0] << "\t|" << cc.mWL[12][1] << "\t |" << cc.mWL[12][2] << "\t\t     |" << cc.mWL[12][3] << "\t|" << cc.mWL[12][4] << "\t|" << cc.mWL[12][5] << "\t\t    |\n";
	cout << "|админ. услуг        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Строительство       |" << cc.mWL[13][0] << "\t|" << cc.mWL[13][1] << "\t |" << cc.mWL[13][2] << "\t\t     |" << cc.mWL[13][3] << "\t|" << cc.mWL[13][4] << "\t|" << cc.mWL[13][5] << "\t\t    |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n\n";
	for (int i = 0; i < 14; i++)
	{
		D3 << cc.mWL[i][0] << '|' << cc.mWL[i][1] << "|" << cc.mWL[i][2] << '|' << cc.mWL[i][3] << "|" << cc.mWL[i][4] << '|' << cc.mWL[i][5] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	cc = TAB_LTYPE_SISTEM(cc, 2, reg);
	cout << "____________________________________________________________________________________________________\n";
	cout << "|уровень             |кол-во граждан (чел.), проживающих в:  |количество граждан, %:                |\n";
	cout << "|образования         |_______________________________________|______________________________________|\n";
	cout << "|                    |Городе    |ПГТ     |Сельской местности |Городе    |ПГТ    |Сельской местности |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|начальное           |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|общее базовое       |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|общее среднее       |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|проф техническое    |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|среднее специальное |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|высшее              |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|высшее              |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|(с магистратурой)   |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|послевузовское      |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|нет образование     |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	for (int i = 0; i < 9; i++)
	{
		D3 << cc.mWL[i][0] << '|' << cc.mWL[i][1] << "|" << cc.mWL[i][2] << '|' << cc.mWL[i][3] << "|" << cc.mWL[i][4] << '|' << cc.mWL[i][5] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	cc = TAB_LTYPE_SISTEM(cc, 3, reg);
	cout << "___________________________________________________________________\n";
	cout << "|тип места          |среднее         |количество       |количество |\n";
	cout << "|проживания         |значение дохода |граждан, чел.    |граждан, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|Город              |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|ПГТ                |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|Сельская местность |" << sa.Middle_Income(cc, 2) << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D3 << sa.MCC(cc, i) << '|' << cc.mas_people[i] << "|" << cc.mas_perc[i] << "\n";
	}
	for (int i = 0; i < 14; i++)
	{
		cc.mas_people[i] = 0;
		cc.mas_MIC[i] = 0;
		cc.mas_perc[i] = 0;
	}
	D3.close();
	return;
}


void DEPENDENCE_FILE_CHILDREN(DT_income cc)
{
	ifstream D2("d:\\Pylo\\dt_deti.txt", ios::in);
	char trash;
	for (int i = 0; i < 14; i++)
	{
		D2 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "____________________________________________________________________\n";
	cout << "|эконом. деятельность|среднее кол-во    |количество     |количество |\n";
	cout << "|                    |детей  семье      |граждан, чел.  |граждан, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|Информация и связь  |" << cc.mas_MIC[0] << "\t\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Опт.и розн.торговля |" << cc.mas_MIC[1] << "\t\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Финансовая и стра-  |" << cc.mas_MIC[2] << "\t\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|ховая деятельность  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Операции с недви-   |" << cc.mas_MIC[3] << "\t\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|жимым имуществом    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Проф. науч. и тех.  |" << cc.mas_MIC[4] << "\t\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Здравоохранение     |" << cc.mas_MIC[5] << "\t\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Промышленность      |" << cc.mas_MIC[6] << "\t\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Творчество, спорт,  |" << cc.mas_MIC[7] << "\t\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|развлечения и отдых |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Транспортная        |" << cc.mas_MIC[8] << "\t\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Услуги по временному|" << cc.mas_MIC[9] << "\t\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|проживанию и питанию|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Образование         |" << cc.mas_MIC[10] << "\t\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Сельское, лесное и  |" << cc.mas_MIC[11] << "\t\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|рыбное хозяйство    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Деятельность в сфере|" << cc.mas_MIC[12] << "\t\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|админ. услуг        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Строительство       |" << cc.mas_MIC[13] << "\t\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
	cout << "|====================|==================|===============|===========|\n\n";
	for (int i = 0; i < 14; i++)
	{
		cc.mas_MIC[i] = 0;
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	for (int i = 0; i < 6; i++)
	{
		D2 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "____________________________________________________________\n";
	cout << "|кол-во детей  |среднее значение|количество     |количество |\n";
	cout << "|в семье       |дохода          |граждан, чел.  |граждан, % |\n";
	cout << "|==============|================|===============|===========|\n";
	cout << "|нет детей     |" << cc.mas_MIC[0] << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|от 1 до 3     |" << cc.mas_MIC[1] << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|больше 3      |" << cc.mas_MIC[2] << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n\n";

	cout << "__________________________________________________________________\n";
	cout << "|возрастная группа     |среднее кол-во |количество    |количество |\n";
	cout << "|                      |детей в семье  |граждан, чел. |граждан, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|Моложе трудоспособного|" << cc.mas_MIC[3] << "\t       |" << cc.mas_people[3] << "\t      |" << cc.mas_perc[3] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|трудоспособный возраст|" << cc.mas_MIC[4] << "\t       |" << cc.mas_people[4] << "\t      |" << cc.mas_perc[4] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|старше трудоспособного|" << cc.mas_MIC[5] << "\t       |" << cc.mas_people[5] << "\t      |" << cc.mas_perc[5] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n\n";
	
	D2.close();
	return;
}
void DEPENDENCE_FILE_INCOM(DT_income cc)
{
	ifstream D1("d:\\Pylo\\dt_incom.txt", ios::in);
	char trash;
	for (int i = 0; i < 14; i++)
	{
		D1 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "____________________________________________________________________\n";
	cout << "|эконом. деятельность|среднее значение  |количество     |количество |\n";
	cout << "|                    |дохода            |граждан, чел.  |граждан, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|Информация и связь  |" << cc.mas_MIC[0] << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Опт.и розн.торговля |" << cc.mas_MIC[1] << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Финансовая и стра-  |" << cc.mas_MIC[2] << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|ховая деятельность  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Операции с недви-   |" << cc.mas_MIC[3] << "\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|жимым имуществом    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Проф. науч. и тех.  |" << cc.mas_MIC[4] << "\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Здравоохранение     |" << cc.mas_MIC[5] << "\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Промышленность      |" << cc.mas_MIC[6] << "\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Творчество, спорт,  |" << cc.mas_MIC[7] << "\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|развлечения и отдых |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Транспортная        |" << cc.mas_MIC[8] << "\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|деятельность        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Услуги по временному|" << cc.mas_MIC[9] << "\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|проживанию и питанию|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Образование         |" << cc.mas_MIC[10] << "\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Сельское, лесное и  |" << cc.mas_MIC[11] << "\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|рыбное хозяйство    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Деятельность в сфере|" << cc.mas_MIC[12] << "\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|админ. услуг        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|Строительство       |" << cc.mas_MIC[13] << "\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
	cout << "|====================|==================|===============|===========|\n\n";
	for (int i = 0; i < 14; i++)
	{
		D1 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "____________________________________________________________________\n";
	cout << "|ур. образования         |среднее        |количество    |количество |\n";
	cout << "|                        |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|========================|===============|==============|===========|\n";
	cout << "|начальное               |" << cc.mas_MIC[0] << "\t\t |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|общее базовое           |" << cc.mas_MIC[1] << "\t\t |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|общее среднее           |" << cc.mas_MIC[2] << "\t\t |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|проф техническое        |" << cc.mas_MIC[3] << "\t\t |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|среднее специальное     |" << cc.mas_MIC[4] << "\t\t |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|высшее                  |" << cc.mas_MIC[5] << "\t\t |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|высшее (с магистратурой)|" << cc.mas_MIC[6] << "\t\t |" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|послевузовское          |" << cc.mas_MIC[7] << "\t\t |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|нет образование         |" << cc.mas_MIC[8] << "\t\t |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n\n";

	cout << "________________________________________________________\n";
	cout << "|Пол         |среднее        |количество    |количество |\n";
	cout << "|            |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|============|===============|==============|===========|\n";
	cout << "|мужчина     |" << cc.mas_MIC[9] << "\t     |" << cc.mas_people[9] << "\t    |" << cc.mas_perc[9] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n";
	cout << "|женщина     |" << cc.mas_MIC[10] << "\t     |" << cc.mas_people[10] << "\t    |" << cc.mas_perc[10] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n\n";

	cout << "__________________________________________________________________\n";
	cout << "|возрастная группа     |среднее        |количество    |количество |\n";
	cout << "|                      |значение дохода|граждан, чел. |граждан, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|Моложе трудоспособного|" << cc.mas_MIC[11] << "\t       |" << cc.mas_people[11] << "\t      |" << cc.mas_perc[11] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|трудоспособный возраст|" << cc.mas_MIC[12] << "\t       |" << cc.mas_people[12] << "\t      |" << cc.mas_perc[12] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|старше трудоспособного|" << cc.mas_MIC[13] << "\t       |" << cc.mas_people[13] << "\t      |" << cc.mas_perc[13] << "\t  |\n";
	cout << "|возраста              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D1 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "___________________________________________________________________\n";
	cout << "|тип места          |среднее         |количество       |количество |\n";
	cout << "|проживания         |значение дохода |граждан, чел.    |граждан, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|Город              |" << cc.mas_MIC[0] << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|ПГТ                |" << cc.mas_MIC[1] << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|Сельская местность |" << cc.mas_MIC[2] << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n\n";
	for (int i = 0; i < 14; i++)
	{
		cc.mas_MIC[i] = 0;
		cc.mas_people[i] = 0;
		cc.mas_perc[i] = 0;
	}
	D1.close();
	return;
}
void DEPENDENCE_TAB_LTYPE(DT_income cc)
{
	ifstream D3("d:\\Pylo\\dt_ltype.txt", ios::in);
	char trash;
	for (int i = 0; i < 14; i++)
	{
		D3 >> cc.mWL[i][0] >> trash >> cc.mWL[i][1] >> trash >> cc.mWL[i][2] >> trash >> cc.mWL[i][3] >> trash >> cc.mWL[i][4] >> trash >> cc.mWL[i][5];
	}
	cout << "____________________________________________________________________________________________________\n";
	cout << "|эконом. деятельность|кол-во граждан (чел.), проживающих в:  |количество граждан, %:                |\n";
	cout << "|                    |_______________________________________|______________________________________|\n";
	cout << "|                    |Городе    |ПГТ     |Сельской местности |Городе    |ПГТ    |Сельской местности |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|Информация и связь  |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Опт.и розн.торговля |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Финансовая и стра-  |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|ховая деятельность  |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Операции с недви-   |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|жимым имуществом    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Проф. науч. и тех.  |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|деятельность        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Здравоохранение     |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Промышленность      |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Творчество, спорт,  |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|развлечения и отдых |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Транспортная        |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|деятельность        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Услуги по временному|" << cc.mWL[9][0] << "\t|" << cc.mWL[9][1] << "\t |" << cc.mWL[9][2] << "\t\t     |" << cc.mWL[9][3] << "\t|" << cc.mWL[9][4] << "\t|" << cc.mWL[9][5] << "\t\t    |\n";
	cout << "|проживанию и питанию|          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Образование         |" << cc.mWL[10][0] << "\t|" << cc.mWL[10][1] << "\t |" << cc.mWL[10][2] << "\t\t     |" << cc.mWL[10][3] << "\t|" << cc.mWL[10][4] << "\t|" << cc.mWL[10][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Сельское, лесное и  |" << cc.mWL[11][0] << "\t|" << cc.mWL[11][1] << "\t |" << cc.mWL[11][2] << "\t\t     |" << cc.mWL[11][3] << "\t|" << cc.mWL[11][4] << "\t|" << cc.mWL[11][5] << "\t\t    |\n";
	cout << "|рыбное хозяйство    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Деятельность в сфере|" << cc.mWL[12][0] << "\t|" << cc.mWL[12][1] << "\t |" << cc.mWL[12][2] << "\t\t     |" << cc.mWL[12][3] << "\t|" << cc.mWL[12][4] << "\t|" << cc.mWL[12][5] << "\t\t    |\n";
	cout << "|админ. услуг        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|Строительство       |" << cc.mWL[13][0] << "\t|" << cc.mWL[13][1] << "\t |" << cc.mWL[13][2] << "\t\t     |" << cc.mWL[13][3] << "\t|" << cc.mWL[13][4] << "\t|" << cc.mWL[13][5] << "\t\t    |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n\n";
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	for (int i = 0; i < 9; i++)
	{
		D3 >> cc.mWL[i][0] >> trash >> cc.mWL[i][1] >> trash >> cc.mWL[i][2] >> trash >> cc.mWL[i][3] >> trash >> cc.mWL[i][4] >> trash >> cc.mWL[i][5];
	}
	cout << "____________________________________________________________________________________________________\n";
	cout << "|уровень             |кол-во граждан (чел.), проживающих в:  |количество граждан, %:                |\n";
	cout << "|образования         |_______________________________________|______________________________________|\n";
	cout << "|                    |Городе    |ПГТ     |Сельской местности |Городе    |ПГТ    |Сельской местности |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|начальное           |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|общее базовое       |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|общее среднее       |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|проф техническое    |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|среднее специальное |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|высшее              |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|высшее              |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|(с магистратурой)   |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|послевузовское      |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|нет образование     |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	for (int i = 0; i < 3; i++)
	{
		D3 >> cc.mWL[i][0] >> trash >> cc.mWL[i][1] >> trash >> cc.mWL[i][2];
	}
	cout << "___________________________________________________________________\n";
	cout << "|тип места          |среднее         |количество       |количество |\n";
	cout << "|проживания         |значение дохода |граждан, чел.    |граждан, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|Город              |" << cc.mWL[0][0] << "\t     |" << cc.mWL[0][1] << "\t       |" << cc.mWL[0][2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|ПГТ                |" << cc.mWL[1][0] << "\t     |" << cc.mWL[1][1] << "\t       |" << cc.mWL[1][2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|Сельская местность |" << cc.mWL[2][0] << "\t     |" << cc.mWL[2][1] << "\t       |" << cc.mWL[2][2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n\n";
	for (int i = 0; i < 14; i++)
	{
		for (int j = 0; j < 6; j++)
		{
			cc.mWL[i][j] = 0;
		}
	}
	D3.close();
	return;
}

void SPRAVKA()
{
	cout << "Справка\n\n";
	cout << "ОБЩИЕ СВЕДЕНИЯ:\n\n";
	cout << "Данная программа представляет собой набор средств, позволяющих решать почти все задачи\n";
	cout << "переписи населения.\n";
	cout << "Взаимодействие с пользователем происходит посредством консольного меню\n";
	cout << "Для того, чтобы выбрать один из пунктов меню, введите его номер и нажмите клавишу Enter\n";
	cout << "Программа проводит необходимые расчеты и заносит их результыты в файлы.\n";
	cout << "Все анкетные данные о населении так-же хранятся в файле\n\n";
	cout << "ИСПОЛЬЗОВАНИЕ:\n\n";
	cout << "Перепись населения проводится посредством анкетирования большей части граждан страны.\n";
	cout << "Для того, чтобы начать анкетирование, в главном меню выберите пункт 'анкетирование пользователя'.\n";
	cout << "Откроется подменю с двумя пунктами: первый пункт позволяет проводить опрос одного человека путем\n";
	cout << "ответов на вопросы. вопросы будут указаны в анкете, которая будет выведена на экран после выбора\n";
	cout << "первого пункта\n";
	cout << "второй пункт в основном нужен для проверки корректности работы программы. после его выбора пльзователю\n";
	cout << "нужно ввести число анкет, которые нужно заполнить. после этого программа сгенерирует случайные ответы\n";
	cout << "на вопросы анкет, число которых ранее было введено.\n";
	cout << "в каждом подменю предусмотрен пункт возврата в главное меню\n\n";
	cout << "Описание остальных пунктов главного меню:\n";
	cout << "	1) пункт 'Итоговые таблицы' - вывод на экран подменю со списком названий итоговых таблиц\n";
	cout <<	"	2) пункт 'Таблицы зависимости' - аналогичен первому пункту в своей работе за исключением самих таблиц:\n";
	cout << "они представляют собой комплексы более мелких таблиц и служат для определения зависимости одной\n";
	cout << "характеристики от другой (других)\n";
	cout << "	3) пункт 'Просмотр статистических данных' - почти тоже самое, что и предыдущие два пункта. отличие\n";
	cout << "заключается в том, что программа не проводит расчеты заново, а берет их из файлов, в которые они были\n";
	cout << "записаны ранее, если вы уже посещали пункты 1 и 2";
	cout << "	4) пункт 'Отчистить данные о населении' - этот пункт предусмотрен для удаления из файла всех собранных\n";
	cout << "ранее анкетных данных о населении (гражданах, проходивших анкетирование, или случайно сгенерированных данных)\n";
	cout << "	5) пункт 'О программе' (вы сейчас сдесь) - выводит на экран справочную систему\n\n";
	cout << "Конец справки.";
	return;
}