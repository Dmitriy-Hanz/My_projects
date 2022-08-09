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
	cout << "<< ����������� �������� ��������� >>\n";
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
		cout << "1.����� ������\n2.�������� �������\n3.������� �����������\n4.�������� �������������� ������\n5.��������� ������ � ���������\n6.� ���������\n\n0.�����";
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
				cout << "1.������������� ������������\n2.��������������� ������\n\n3.� ������� ����\n\n";
				cin >> meniu2;
				switch (meniu2)
				{
					case 1:
					{
						dep_flag = 1;
						A = NEW_REGLIST(A);
						FILE_INSERT(A);
						cout << "\n������������� ��������\n";
						system("pause");
						system("cls");
						break;
					}
					case 2:
					{
						dep_flag = 1;
						cout << "���������� ��������������� �����: ";
						cin >> A.system_perc_integer;
						A.true_SPI = A.true_SPI + A.system_perc_integer;
						RANDOMIZATOR(A);
						cout << "\n�������������� ��������\n";
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
						cout << "\n������ ������ ��� � ����\n";
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
				cout << "����������� ������ ��� ��������\n";
				system("pause");
				system("cls");
				break;
			}
			while (meniu2 != 0)
			{
				system("cls");
				bolvanka();
				cout << "������ �������� ������:\n1.�������������� ��������� ���������\n2.����� ������������\n3.�������� ������";
				cout << "\n4.��� ��������� ����������\n5.������� �����������\n\n0.� ������� ����\n";
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
					cout << "\n������ ������ ��� � ����\n";
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
				cout << "����������� ������ ��� ��������\n";
				system("pause");
				system("cls");
				break;
			}
			while (meniu2 != 0)
			{
				system("cls");
				bolvanka();
				cout << "������ ��������� ������ ������������:\n1.������� ������ �� ���������, ������ �����������, ����, ��������, ����� ����������\n";
				cout << "2.���������� ����� � ����� �� ��������, ������, ��������� ��������\n";
				cout << "3.��� ��������� ���������� �������� �� ������, ���������, ������ �����������\n\n";
				cout << "0.� ������� ����\n\n";
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
					cout << "\n������ ������ ��� � ����\n";
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
				cout << "�������:\n\n1.������� ������ �� ���������, ������ �����������, ����, ��������, ����� ����������";
				cout << "\n2.���������� ����� � ����� �� ��������, ������, ��������� ��������\n";
				cout << "3.��� ��������� ���������� �������� �� ������, ���������, ������ �����������\n\n";
				cout << "0.�����\n\n";
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
					cout << "\n������ ������ ��� � ����\n";
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
			cout << "\n\n\t\t\t���������� �������� �������� � ����������� ��������� ������ � ���������.�� �������?\n";
			cout << "\t\t\t1.��\n\t\t\t2.���\n\t\t\t\t\t";
			cin >> n;
			if (n == 2)
			{
				system("cls");
				break;
			}
			else
			{
				cout << "\t\t\t�� ����� ��������?\n";
				cout << "\t\t\t1.��\n\t\t\t2.���\n\t\t\t\t\t";
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
					cout << "\t\t\t��� ������ �������\n";
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
			cout << "��� ������ ������";
			meniu = NULL;
			bar;
		}
		system("cls");
		}//����� ������� SWITCH
	}
	bar;
	return 0;
}

ANKETA NEW_REGLIST(ANKETA NUM)
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	cin.ignore();
	cout << "���� ��� (�������, ���, ��������):" ;
	cin.getline(NUM.FIO, 99, '\n');
	cout << "\n_______________________________________________________________________________________________________________________\n";
	cout << "���� �������� (������ ������� ����� ������): ";
	cin.getline(NUM.birth_Data , 11, '\n');
	cout << "\n_______________________________________________________________________________________________________________________\n";
	cout << "��� ���: \n\n0.�������\t1.�������\n";
	cin >> NUM.gender;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "����������� ��� ������ ��������� � �����, ��������� ������ � �������������: \n\n";
	cout << "1.����, ���������  ������ � �������������\t2.����, ���\t\t3.����, ���, ���������, �������\n4.����, ����\t\t\t\t\t";
	cout << "5.������, ���� \t\t6.��������, ������, ����, �����\n7.��������, ����\t\t\t\t8.�����, ���\t\t9.������, ����\n";
	cout <<	"10.������ ������� �������, ��������\t\t11.�� �����������\n";
	cin >> NUM.first_homehold_person;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "���� �������� ���������: \n\n1.������� �� �������(�) � �����\t\t\t2.������ � ������������������ �����\n";
	cout << "3.������ � �������������������� ����������\t4.������, �����\n5.��������(�)\t\t\t\t\t6.���������(����)\n";
	cin >> NUM.marital_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "����� ������ ��������:\n\n1.���������� ��������\t2.������ ������.\n";
	cin >> NUM.your_native_country;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << " ������� ��� ����������� ������, � ������� �� ����������:\n\n1.�����\t2.������� ���������� ����\t3.�������� ���������\n";
	cin >> NUM.your_living_place;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "��� ������� (�������) �����������: \n\n1.���������\t\t4.���� �����������\t7.������ (� �������������)\n";
	cout << "2.����� �������\t\t5.������� �����������\t8.��������������\n";
	cout << "3.����� �������\t\t6.������\t\t9.�� ���� �����������\n";
	cin >> NUM.aducation_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "������� ��������� ������� � �������������, ��������� � ���� ���������� �������� ���������:\n\n";
	cout << "1.������ �� �����\t\t\t\t\t9.������� �� �����������\n2.������ � ������ ��������� ���������\t\t\t10.���������\n";
	cout << "3.�������������\t\t\t\t\t\t11.���� ���� ������� � ������\n4.������������ ������� ��� ������������ �������������\t12.����� ��� ������������� ����������, ���������� ���������\n";
	cout << "5.��������������� ������\t\t\t\t13.�� ��������� ������� ����\n6.������ (���� ���������� �������)\t\t\t14.������ ��������� \n";
	cout << "7.������� �� �������, �����������\n8.�������� ������� \n";
	cin >> NUM.your_income_sourse;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "������� ��� ������� ������� ������ �� �������� ������(�����, ���.):\n";
	cin >> NUM.your_income_lvl;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "�������, � ������ ���� ������������� ������������ ��������� ���� ���������:\n\n1.���������� � �����\t\t\t\t\t8.������� � ��������� ��������\n2.���������� � ��������� ������������\t\t\t9.�������� � ���������� ����������";
	cout << "\n3.����������������, ������� � ����������� ������������\t10.���������������\n4.��������������\t\t\t\t\t11.����������, �����, ����������� � �����";
	cout << "\n5.������������ ������������, �������������\t\t12.������ �� ���������� ���������� � �������\n6.�������������\t\t\t\t\t\t13.��������, ������ � ������ ���������";
	cout << "\n7.������������ � ����� ���������������� �����\t\t14.�����������\n";
	cin >> NUM.your_work_status;
	cout << "_______________________________________________________________________________________________________________________\n";
	cout << "������� ����� �� ������(������� ����������):\n";
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
	cout << "\n��� ���������: \n\n";
	cout << "��� ����:\t�������: "<< cc.mWL[0][0] <<"\t�������: "<< cc.mWL[0][1] <<"\n�������(� ���������): "<< cc.mWL[0][2] <<"\n�������(� ���������): " << cc.mWL[0][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\n������ ��������������� �������� (������� � ������� �� 16 ���): \n\n";
	cout << "��� ����:\t�������: " << cc.mWL[1][0] << "\t�������: " << cc.mWL[1][1] << "\n�������(� ���������): " << cc.mWL[1][2] <<"\n�������(� ���������): " << cc.mWL[1][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\n�������������� ������� (������� 16-59 ���, ������� 16-54 ����): \n\n";
	cout << "��� ����:\t�������: " << cc.mWL[2][0] << "\t�������: " << cc.mWL[2][1] << "\n�������(� ���������): " << cc.mWL[2][2] << "\n�������(� ���������): " << cc.mWL[2][3];
	cout << "\n_______________________________________________________________________________________________________________________";
	cout << "\n������ ��������������� (������� 60 ��� � �����, ������� 55 ��� � �����): \n\n";
	cout << "��� ����:\t�������: " << cc.mWL[3][0] << "\t�������: " << cc.mWL[3][1] << "\n�������(� ���������): " << cc.mWL[3][2] << "\n�������(� ���������): " << cc.mWL[3][3];
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
	cout << "|�������� ������     |����������     |���������� |\n";
	cout << "|                    |�������, ���.  |�������, % |\n";
	cout << "|====================|===============|===========|\n";
	cout << "|������� ��          |" << cc.mas_people[0] << "\t     |" << cc.mas_perc[0] << "\t |\n";
	cout << "|�������(�) � �����  |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|������������������  |" << cc.mas_people[1] << "\t     |" << cc.mas_perc[1] << "\t |\n";
	cout << "|����                |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|��������������������|" << cc.mas_people[2] << "\t     |" << cc.mas_perc[2] << "\t |\n";
	cout << "|���������           |               |           |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|������, �����       |" << cc.mas_people[3] << "\t     |" << cc.mas_perc[3] << "\t |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|��������(�)         |" << cc.mas_people[4] << "\t     |" << cc.mas_perc[4] << "\t |\n";
	cout << "|____________________|_______________|___________|\n";
	cout << "|���������(����)     |" << cc.mas_people[5] << "\t     |" << cc.mas_perc[5] << "\t |\n";
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
	cout << "|������. ������������|����������    |���������� |\n";
	cout << "|                    |�������, ���. |�������, % |\n";
	cout << "|====================|==============|===========|\n";
	cout << "|���������� � �����  |" << cc.mas_people[0] << "\t    |" << cc.mas_perc[0] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|���.� ����.�������� |" << cc.mas_people[1] << "\t    |" << cc.mas_perc[1] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|���������� � ����-  |" << cc.mas_people[2] << "\t    |" << cc.mas_perc[2] << "\t\t|\n";
	cout << "|����� ������������  |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|�������� � �����-   |" << cc.mas_people[3] << "\t    |" << cc.mas_perc[3] << "\t\t|\n";
	cout << "|����� ����������    |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|����. ����. � ���.  |" << cc.mas_people[4] << "\t    |" << cc.mas_perc[4] << "\t\t|\n";
	cout << "|������������        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|���������������     |" << cc.mas_people[5] << "\t    |" << cc.mas_perc[5] << "\t\t| \n";
	cout << "|____________________|______________|___________|\n";
	cout << "|��������������      |" << cc.mas_people[6] << "\t    |" << cc.mas_perc[6] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|����������, �����,  |" << cc.mas_people[7] << "\t    |" << cc.mas_perc[7] << "\t\t|\n";
	cout << "|����������� � ����� |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|������������        |" << cc.mas_people[8] << "\t    |" << cc.mas_perc[8] << "\t\t|\n";
	cout << "|������������        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|������ �� ����������|" << cc.mas_people[9] << "\t    |" << cc.mas_perc[9] << "\t\t|\n";
	cout << "|���������� � �������|              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|�����������         |" << cc.mas_people[10] << "\t    |" << cc.mas_perc[10] << "\t\t|\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|��������, ������ �  |" << cc.mas_people[11] << "\t    |" << cc.mas_perc[11] << "\t\t|\n";
	cout << "|������ ���������    |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|������������ � �����|" << cc.mas_people[12] << "\t    |" << cc.mas_perc[12] << "\t\t|\n";
	cout << "|�����. �����        |              |           |\n";
	cout << "|____________________|______________|___________|\n";
	cout << "|�������������       |" << cc.mas_people[13] << "\t    |" << cc.mas_perc[13] << "\t\t|\n";
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
	cout << "|��� �����          |����������      |���������� |\n";
	cout << "|����������         |�������, ���.   |�������, % |\n";
	cout << "|===================|================|===========|\n";
	cout << "|�����              |" << cc.mas_people[0] << "\t\t     |" << cc.mas_perc[0] << "\t |\n";
	cout << "|___________________|________________|___________|\n";
	cout << "|���                |" << cc.mas_people[1] << "\t\t     |" << cc.mas_perc[1] << "\t |\n";
	cout << "|___________________|________________|___________|\n";
	cout << "|�������� ��������� |" << cc.mas_people[2] << "\t\t     |" << cc.mas_perc[2] << "\t |\n";
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
	cout << "|��. �����������         |����������    |���������� |\n";
	cout << "|                        |�������, ���. |�������, % |\n";
	cout << "|========================|==============|===========|\n";
	cout << "|���������               |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|����� �������           |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|����� �������           |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|���� �����������        |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|������� �����������     |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|������                  |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|������ (� �������������)|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|��������������          |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|______________|___________|\n";
	cout << "|��� �����������         |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
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
	cout << "|������. ������������|������� ��������  |����������     |���������� |\n";
	cout << "|                    |������            |�������, ���.  |�������, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|���������� � �����  |" << sa.Middle_Income(cc, 0) << "\t\t|"<< cc.mas_people[0] <<"\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���.� ����.�������� |" << sa.Middle_Income(cc, 1) << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������� � ����-  |" << sa.Middle_Income(cc, 2) << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|����� ������������  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������� � �����-   |" << sa.Middle_Income(cc, 3) << "\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|����� ����������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����. ����. � ���.  |" << sa.Middle_Income(cc, 4) << "\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������������     |" << sa.Middle_Income(cc, 5) << "\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������������      |" << sa.Middle_Income(cc, 6) << "\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����������, �����,  |" << sa.Middle_Income(cc, 7) << "\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|����������� � ����� |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������        |" << sa.Middle_Income(cc, 8) << "\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������ �� ����������|" << sa.Middle_Income(cc, 9) << "\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|���������� � �������|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�����������         |" << sa.Middle_Income(cc, 10) << "\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������, ������ �  |" << sa.Middle_Income(cc, 11) << "\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|������ ���������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������ � �����|" << sa.Middle_Income(cc, 12) << "\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|�����. �����        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������������       |" << sa.Middle_Income(cc, 13) << "\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
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
	cout << "|��. �����������         |�������        |����������    |���������� |\n";
	cout << "|                        |�������� ������|�������, ���. |�������, % |\n";
	cout << "|========================|===============|==============|===========|\n";
	cout << "|���������               |" << sa.Middle_Income(cc, 0) << "\t\t |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|����� �������           |" << sa.Middle_Income(cc, 1) << "\t\t |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|����� �������           |" << sa.Middle_Income(cc, 2) << "\t\t |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|���� �����������        |" << sa.Middle_Income(cc, 3) << "\t\t |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������� �����������     |" << sa.Middle_Income(cc, 4) << "\t\t |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������                  |" << sa.Middle_Income(cc, 5) << "\t\t |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������ (� �������������)|" << sa.Middle_Income(cc, 6) << "\t\t |" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|��������������          |" << sa.Middle_Income(cc, 7) << "\t\t |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|��� �����������         |" << sa.Middle_Income(cc, 8) << "\t\t |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
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
	cout << "|���         |�������        |����������    |���������� |\n";
	cout << "|            |�������� ������|�������, ���. |�������, % |\n";
	cout << "|============|===============|==============|===========|\n";
	cout << "|�������     |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t    |" << cc.mas_perc[0] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n";
	cout << "|�������     |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t    |" << cc.mas_perc[1] << "\t\t|\n";
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
	cout << "|���������� ������     |�������        |����������    |���������� |\n";
	cout << "|                      |�������� ������|�������, ���. |�������, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|������ ���������������|" << sa.Middle_Income(cc, 0) << "\t       |" << cc.mas_people[0] << "\t      |" << cc.mas_perc[0] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|�������������� �������|" << sa.Middle_Income(cc, 1) << "\t       |" << cc.mas_people[1] << "\t      |" << cc.mas_perc[1] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|������ ���������������|" << sa.Middle_Income(cc, 2) << "\t       |" << cc.mas_people[2] << "\t      |" << cc.mas_perc[2] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
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
	cout << "|��� �����          |�������         |����������       |���������� |\n";
	cout << "|����������         |�������� ������ |�������, ���.    |�������, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|�����              |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|���                |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|�������� ��������� |" << sa.Middle_Income(cc, 2) << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
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
	cout << "|������. ������������|������� ���-��    |����������     |���������� |\n";
	cout << "|                    |�����  �����      |�������, ���.  |�������, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|���������� � �����  |" << sa.MCC(cc, 0) << "\t\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���.� ����.�������� |" << sa.MCC(cc, 1) << "\t\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������� � ����-  |" << sa.MCC(cc, 2) << "\t\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|����� ������������  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������� � �����-   |" << sa.MCC(cc, 3) << "\t\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|����� ����������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����. ����. � ���.  |" << sa.MCC(cc, 4) << "\t\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������������     |" << sa.MCC(cc, 5) << "\t\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������������      |" << sa.MCC(cc, 6) << "\t\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����������, �����,  |" << sa.MCC(cc, 7) << "\t\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|����������� � ����� |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������        |" << sa.MCC(cc, 8) << "\t\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������ �� ����������|" << sa.MCC(cc, 9) << "\t\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|���������� � �������|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�����������         |" << sa.MCC(cc, 10) << "\t\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������, ������ �  |" << sa.MCC(cc, 11) << "\t\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|������ ���������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������ � �����|" << sa.MCC(cc, 12) << "\t\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|�����. �����        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������������       |" << sa.MCC(cc, 13) << "\t\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
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
	cout << "|���-�� �����  |������� ��������|����������     |���������� |\n";
	cout << "|� �����       |������          |�������, ���.  |�������, % |\n";
	cout << "|==============|================|===============|===========|\n";
	cout << "|��� �����     |" << sa.MCC(cc, 0) << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|�� 1 �� 3     |" << sa.MCC(cc, 1) << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|������ 3      |" << sa.MCC(cc, 2) << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
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
	cout << "|���������� ������     |������� ���-�� |����������    |���������� |\n";
	cout << "|                      |����� � �����  |�������, ���. |�������, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|������ ���������������|" << sa.MCC(cc, 0) << "\t       |" << cc.mas_people[0] << "\t      |" << cc.mas_perc[0] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|�������������� �������|" << sa.MCC(cc, 1) << "\t       |" << cc.mas_people[1] << "\t      |" << cc.mas_perc[1] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|������ ���������������|" << sa.MCC(cc, 2) << "\t       |" << cc.mas_people[2] << "\t      |" << cc.mas_perc[2] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
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
	cout << "|������. ������������|���-�� ������� (���.), ����������� �:  |���������� �������, %:                |\n";
	cout << "|                    |_______________________________________|______________________________________|\n";
	cout << "|                    |������    |���     |�������� ��������� |������    |���    |�������� ��������� |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|���������� � �����  |" <<cc.mWL[0][0]<<"\t|"<< cc.mWL[0][1]<< "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���.� ����.�������� |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���������� � ����-  |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|����� ������������  |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�������� � �����-   |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|����� ����������    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����. ����. � ���.  |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|������������        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���������������     |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������������      |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����������, �����,  |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|����������� � ����� |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������������        |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|������������        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������ �� ����������|" << cc.mWL[9][0] << "\t|" << cc.mWL[9][1] << "\t |" << cc.mWL[9][2] << "\t\t     |" << cc.mWL[9][3] << "\t|" << cc.mWL[9][4] << "\t|" << cc.mWL[9][5] << "\t\t    |\n";
	cout << "|���������� � �������|          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�����������         |" << cc.mWL[10][0] << "\t|" << cc.mWL[10][1] << "\t |" << cc.mWL[10][2] << "\t\t     |" << cc.mWL[10][3] << "\t|" << cc.mWL[10][4] << "\t|" << cc.mWL[10][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������, ������ �  |" << cc.mWL[11][0] << "\t|" << cc.mWL[11][1] << "\t |" << cc.mWL[11][2] << "\t\t     |" << cc.mWL[11][3] << "\t|" << cc.mWL[11][4] << "\t|" << cc.mWL[11][5] << "\t\t    |\n";
	cout << "|������ ���������    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������������ � �����|" << cc.mWL[12][0] << "\t|" << cc.mWL[12][1] << "\t |" << cc.mWL[12][2] << "\t\t     |" << cc.mWL[12][3] << "\t|" << cc.mWL[12][4] << "\t|" << cc.mWL[12][5] << "\t\t    |\n";
	cout << "|�����. �����        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�������������       |" << cc.mWL[13][0] << "\t|" << cc.mWL[13][1] << "\t |" << cc.mWL[13][2] << "\t\t     |" << cc.mWL[13][3] << "\t|" << cc.mWL[13][4] << "\t|" << cc.mWL[13][5] << "\t\t    |\n";
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
	cout << "|�������             |���-�� ������� (���.), ����������� �:  |���������� �������, %:                |\n";
	cout << "|�����������         |_______________________________________|______________________________________|\n";
	cout << "|                    |������    |���     |�������� ��������� |������    |���    |�������� ��������� |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|���������           |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����� �������       |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����� �������       |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���� �����������    |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������� ����������� |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������              |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������              |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|(� �������������)   |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������������      |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��� �����������     |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
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
	cout << "|��� �����          |�������         |����������       |���������� |\n";
	cout << "|����������         |�������� ������ |�������, ���.    |�������, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|�����              |" << sa.Middle_Income(cc, 0) << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|���                |" << sa.Middle_Income(cc, 1) << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|�������� ��������� |" << sa.Middle_Income(cc, 2) << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
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
	cout << "|������. ������������|������� ���-��    |����������     |���������� |\n";
	cout << "|                    |�����  �����      |�������, ���.  |�������, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|���������� � �����  |" << cc.mas_MIC[0] << "\t\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���.� ����.�������� |" << cc.mas_MIC[1] << "\t\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������� � ����-  |" << cc.mas_MIC[2] << "\t\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|����� ������������  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������� � �����-   |" << cc.mas_MIC[3] << "\t\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|����� ����������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����. ����. � ���.  |" << cc.mas_MIC[4] << "\t\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������������     |" << cc.mas_MIC[5] << "\t\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������������      |" << cc.mas_MIC[6] << "\t\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����������, �����,  |" << cc.mas_MIC[7] << "\t\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|����������� � ����� |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������        |" << cc.mas_MIC[8] << "\t\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������ �� ����������|" << cc.mas_MIC[9] << "\t\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|���������� � �������|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�����������         |" << cc.mas_MIC[10] << "\t\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������, ������ �  |" << cc.mas_MIC[11] << "\t\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|������ ���������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������ � �����|" << cc.mas_MIC[12] << "\t\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|�����. �����        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������������       |" << cc.mas_MIC[13] << "\t\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
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
	cout << "|���-�� �����  |������� ��������|����������     |���������� |\n";
	cout << "|� �����       |������          |�������, ���.  |�������, % |\n";
	cout << "|==============|================|===============|===========|\n";
	cout << "|��� �����     |" << cc.mas_MIC[0] << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|�� 1 �� 3     |" << cc.mas_MIC[1] << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n";
	cout << "|������ 3      |" << cc.mas_MIC[2] << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|______________|________________|_______________|___________|\n\n";

	cout << "__________________________________________________________________\n";
	cout << "|���������� ������     |������� ���-�� |����������    |���������� |\n";
	cout << "|                      |����� � �����  |�������, ���. |�������, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|������ ���������������|" << cc.mas_MIC[3] << "\t       |" << cc.mas_people[3] << "\t      |" << cc.mas_perc[3] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|�������������� �������|" << cc.mas_MIC[4] << "\t       |" << cc.mas_people[4] << "\t      |" << cc.mas_perc[4] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|������ ���������������|" << cc.mas_MIC[5] << "\t       |" << cc.mas_people[5] << "\t      |" << cc.mas_perc[5] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
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
	cout << "|������. ������������|������� ��������  |����������     |���������� |\n";
	cout << "|                    |������            |�������, ���.  |�������, % |\n";
	cout << "|====================|==================|===============|===========|\n";
	cout << "|���������� � �����  |" << cc.mas_MIC[0] << "\t\t|" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���.� ����.�������� |" << cc.mas_MIC[1] << "\t\t|" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������� � ����-  |" << cc.mas_MIC[2] << "\t\t|" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|����� ������������  |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������� � �����-   |" << cc.mas_MIC[3] << "\t\t|" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|����� ����������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����. ����. � ���.  |" << cc.mas_MIC[4] << "\t\t|" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|���������������     |" << cc.mas_MIC[5] << "\t\t|" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������������      |" << cc.mas_MIC[6] << "\t\t|" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|����������, �����,  |" << cc.mas_MIC[7] << "\t\t|" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|����������� � ����� |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������        |" << cc.mas_MIC[8] << "\t\t|" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|������������        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������ �� ����������|" << cc.mas_MIC[9] << "\t\t|" << cc.mas_people[9] << "\t\t|" << cc.mas_perc[9] << "\t    |\n";
	cout << "|���������� � �������|                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�����������         |" << cc.mas_MIC[10] << "\t\t|" << cc.mas_people[10] << "\t\t|" << cc.mas_perc[10] << "\t    |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|��������, ������ �  |" << cc.mas_MIC[11] << "\t\t|" << cc.mas_people[11] << "\t\t|" << cc.mas_perc[11] << "\t    |\n";
	cout << "|������ ���������    |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|������������ � �����|" << cc.mas_MIC[12] << "\t\t|" << cc.mas_people[12] << "\t\t|" << cc.mas_perc[12] << "\t    |\n";
	cout << "|�����. �����        |                  |               |           |\n";
	cout << "|____________________|__________________|_______________|___________|\n";
	cout << "|�������������       |" << cc.mas_MIC[13] << "\t\t|" << cc.mas_people[13] << "\t\t|" << cc.mas_perc[13] << "\t    |\n";
	cout << "|====================|==================|===============|===========|\n\n";
	for (int i = 0; i < 14; i++)
	{
		D1 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "____________________________________________________________________\n";
	cout << "|��. �����������         |�������        |����������    |���������� |\n";
	cout << "|                        |�������� ������|�������, ���. |�������, % |\n";
	cout << "|========================|===============|==============|===========|\n";
	cout << "|���������               |" << cc.mas_MIC[0] << "\t\t |" << cc.mas_people[0] << "\t\t|" << cc.mas_perc[0] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|����� �������           |" << cc.mas_MIC[1] << "\t\t |" << cc.mas_people[1] << "\t\t|" << cc.mas_perc[1] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|����� �������           |" << cc.mas_MIC[2] << "\t\t |" << cc.mas_people[2] << "\t\t|" << cc.mas_perc[2] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|���� �����������        |" << cc.mas_MIC[3] << "\t\t |" << cc.mas_people[3] << "\t\t|" << cc.mas_perc[3] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������� �����������     |" << cc.mas_MIC[4] << "\t\t |" << cc.mas_people[4] << "\t\t|" << cc.mas_perc[4] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������                  |" << cc.mas_MIC[5] << "\t\t |" << cc.mas_people[5] << "\t\t|" << cc.mas_perc[5] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|������ (� �������������)|" << cc.mas_MIC[6] << "\t\t |" << cc.mas_people[6] << "\t\t|" << cc.mas_perc[6] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|��������������          |" << cc.mas_MIC[7] << "\t\t |" << cc.mas_people[7] << "\t\t|" << cc.mas_perc[7] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n";
	cout << "|��� �����������         |" << cc.mas_MIC[8] << "\t\t |" << cc.mas_people[8] << "\t\t|" << cc.mas_perc[8] << "\t    |\n";
	cout << "|________________________|_______________|______________|___________|\n\n";

	cout << "________________________________________________________\n";
	cout << "|���         |�������        |����������    |���������� |\n";
	cout << "|            |�������� ������|�������, ���. |�������, % |\n";
	cout << "|============|===============|==============|===========|\n";
	cout << "|�������     |" << cc.mas_MIC[9] << "\t     |" << cc.mas_people[9] << "\t    |" << cc.mas_perc[9] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n";
	cout << "|�������     |" << cc.mas_MIC[10] << "\t     |" << cc.mas_people[10] << "\t    |" << cc.mas_perc[10] << "\t\t|\n";
	cout << "|____________|_______________|______________|___________|\n\n";

	cout << "__________________________________________________________________\n";
	cout << "|���������� ������     |�������        |����������    |���������� |\n";
	cout << "|                      |�������� ������|�������, ���. |�������, % |\n";
	cout << "|======================|===============|==============|===========|\n";
	cout << "|������ ���������������|" << cc.mas_MIC[11] << "\t       |" << cc.mas_people[11] << "\t      |" << cc.mas_perc[11] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|�������������� �������|" << cc.mas_MIC[12] << "\t       |" << cc.mas_people[12] << "\t      |" << cc.mas_perc[12] << "\t  |\n";
	cout << "|______________________|_______________|______________|___________|\n";
	cout << "|������ ���������������|" << cc.mas_MIC[13] << "\t       |" << cc.mas_people[13] << "\t      |" << cc.mas_perc[13] << "\t  |\n";
	cout << "|��������              |               |              |           |\n";
	cout << "|______________________|_______________|______________|___________|\n\n";
	for (int i = 0; i < 3; i++)
	{
		D1 >> cc.mas_MIC[i] >> trash >> cc.mas_people[i] >> trash >> cc.mas_perc[i];
	}
	cout << "___________________________________________________________________\n";
	cout << "|��� �����          |�������         |����������       |���������� |\n";
	cout << "|����������         |�������� ������ |�������, ���.    |�������, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|�����              |" << cc.mas_MIC[0] << "\t     |" << cc.mas_people[0] << "\t       |" << cc.mas_perc[0] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|���                |" << cc.mas_MIC[1] << "\t     |" << cc.mas_people[1] << "\t       |" << cc.mas_perc[1] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|�������� ��������� |" << cc.mas_MIC[2] << "\t     |" << cc.mas_people[2] << "\t       |" << cc.mas_perc[2] << "\t   |\n";
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
	cout << "|������. ������������|���-�� ������� (���.), ����������� �:  |���������� �������, %:                |\n";
	cout << "|                    |_______________________________________|______________________________________|\n";
	cout << "|                    |������    |���     |�������� ��������� |������    |���    |�������� ��������� |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|���������� � �����  |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���.� ����.�������� |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���������� � ����-  |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|����� ������������  |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�������� � �����-   |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|����� ����������    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����. ����. � ���.  |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|������������        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���������������     |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������������      |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����������, �����,  |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|����������� � ����� |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������������        |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
	cout << "|������������        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������ �� ����������|" << cc.mWL[9][0] << "\t|" << cc.mWL[9][1] << "\t |" << cc.mWL[9][2] << "\t\t     |" << cc.mWL[9][3] << "\t|" << cc.mWL[9][4] << "\t|" << cc.mWL[9][5] << "\t\t    |\n";
	cout << "|���������� � �������|          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�����������         |" << cc.mWL[10][0] << "\t|" << cc.mWL[10][1] << "\t |" << cc.mWL[10][2] << "\t\t     |" << cc.mWL[10][3] << "\t|" << cc.mWL[10][4] << "\t|" << cc.mWL[10][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������, ������ �  |" << cc.mWL[11][0] << "\t|" << cc.mWL[11][1] << "\t |" << cc.mWL[11][2] << "\t\t     |" << cc.mWL[11][3] << "\t|" << cc.mWL[11][4] << "\t|" << cc.mWL[11][5] << "\t\t    |\n";
	cout << "|������ ���������    |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������������ � �����|" << cc.mWL[12][0] << "\t|" << cc.mWL[12][1] << "\t |" << cc.mWL[12][2] << "\t\t     |" << cc.mWL[12][3] << "\t|" << cc.mWL[12][4] << "\t|" << cc.mWL[12][5] << "\t\t    |\n";
	cout << "|�����. �����        |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|�������������       |" << cc.mWL[13][0] << "\t|" << cc.mWL[13][1] << "\t |" << cc.mWL[13][2] << "\t\t     |" << cc.mWL[13][3] << "\t|" << cc.mWL[13][4] << "\t|" << cc.mWL[13][5] << "\t\t    |\n";
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
	cout << "|�������             |���-�� ������� (���.), ����������� �:  |���������� �������, %:                |\n";
	cout << "|�����������         |_______________________________________|______________________________________|\n";
	cout << "|                    |������    |���     |�������� ��������� |������    |���    |�������� ��������� |\n";
	cout << "|====================|==========|========|===================|==========|=======|===================|\n";
	cout << "|���������           |" << cc.mWL[0][0] << "\t|" << cc.mWL[0][1] << "\t |" << cc.mWL[0][2] << "\t\t     |" << cc.mWL[0][3] << "\t|" << cc.mWL[0][4] << "\t|" << cc.mWL[0][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����� �������       |" << cc.mWL[1][0] << "\t|" << cc.mWL[1][1] << "\t |" << cc.mWL[1][2] << "\t\t     |" << cc.mWL[1][3] << "\t|" << cc.mWL[1][4] << "\t|" << cc.mWL[1][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|����� �������       |" << cc.mWL[2][0] << "\t|" << cc.mWL[2][1] << "\t |" << cc.mWL[2][2] << "\t\t     |" << cc.mWL[2][3] << "\t|" << cc.mWL[2][4] << "\t|" << cc.mWL[2][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|���� �����������    |" << cc.mWL[3][0] << "\t|" << cc.mWL[3][1] << "\t |" << cc.mWL[3][2] << "\t\t     |" << cc.mWL[3][3] << "\t|" << cc.mWL[3][4] << "\t|" << cc.mWL[3][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������� ����������� |" << cc.mWL[4][0] << "\t|" << cc.mWL[4][1] << "\t |" << cc.mWL[4][2] << "\t\t     |" << cc.mWL[4][3] << "\t|" << cc.mWL[4][4] << "\t|" << cc.mWL[4][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������              |" << cc.mWL[5][0] << "\t|" << cc.mWL[5][1] << "\t |" << cc.mWL[5][2] << "\t\t     |" << cc.mWL[5][3] << "\t|" << cc.mWL[5][4] << "\t|" << cc.mWL[5][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|������              |" << cc.mWL[6][0] << "\t|" << cc.mWL[6][1] << "\t |" << cc.mWL[6][2] << "\t\t     |" << cc.mWL[6][3] << "\t|" << cc.mWL[6][4] << "\t|" << cc.mWL[6][5] << "\t\t    |\n";
	cout << "|(� �������������)   |          |        |                   |          |       |                   |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��������������      |" << cc.mWL[7][0] << "\t|" << cc.mWL[7][1] << "\t |" << cc.mWL[7][2] << "\t\t     |" << cc.mWL[7][3] << "\t|" << cc.mWL[7][4] << "\t|" << cc.mWL[7][5] << "\t\t    |\n";
	cout << "|____________________|__________|________|___________________|__________|_______|___________________|\n";
	cout << "|��� �����������     |" << cc.mWL[8][0] << "\t|" << cc.mWL[8][1] << "\t |" << cc.mWL[8][2] << "\t\t     |" << cc.mWL[8][3] << "\t|" << cc.mWL[8][4] << "\t|" << cc.mWL[8][5] << "\t\t    |\n";
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
	cout << "|��� �����          |�������         |����������       |���������� |\n";
	cout << "|����������         |�������� ������ |�������, ���.    |�������, % |\n";
	cout << "|===================|================|=================|===========|\n";
	cout << "|�����              |" << cc.mWL[0][0] << "\t     |" << cc.mWL[0][1] << "\t       |" << cc.mWL[0][2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|���                |" << cc.mWL[1][0] << "\t     |" << cc.mWL[1][1] << "\t       |" << cc.mWL[1][2] << "\t   |\n";
	cout << "|___________________|________________|_________________|___________|\n";
	cout << "|�������� ��������� |" << cc.mWL[2][0] << "\t     |" << cc.mWL[2][1] << "\t       |" << cc.mWL[2][2] << "\t   |\n";
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
	cout << "�������\n\n";
	cout << "����� ��������:\n\n";
	cout << "������ ��������� ������������ ����� ����� �������, ����������� ������ ����� ��� ������\n";
	cout << "�������� ���������.\n";
	cout << "�������������� � ������������� ���������� ����������� ����������� ����\n";
	cout << "��� ����, ����� ������� ���� �� ������� ����, ������� ��� ����� � ������� ������� Enter\n";
	cout << "��������� �������� ����������� ������� � ������� �� ���������� � �����.\n";
	cout << "��� �������� ������ � ��������� ���-�� �������� � �����\n\n";
	cout << "�������������:\n\n";
	cout << "�������� ��������� ���������� ����������� ������������� ������� ����� ������� ������.\n";
	cout << "��� ����, ����� ������ �������������, � ������� ���� �������� ����� '������������� ������������'.\n";
	cout << "��������� ������� � ����� ��������: ������ ����� ��������� ��������� ����� ������ �������� �����\n";
	cout << "������� �� �������. ������� ����� ������� � ������, ������� ����� �������� �� ����� ����� ������\n";
	cout << "������� ������\n";
	cout << "������ ����� � �������� ����� ��� �������� ������������ ������ ���������. ����� ��� ������ �����������\n";
	cout << "����� ������ ����� �����, ������� ����� ���������. ����� ����� ��������� ����������� ��������� ������\n";
	cout << "�� ������� �����, ����� ������� ����� ���� �������.\n";
	cout << "� ������ ������� ������������ ����� �������� � ������� ����\n\n";
	cout << "�������� ��������� ������� �������� ����:\n";
	cout << "	1) ����� '�������� �������' - ����� �� ����� ������� �� ������� �������� �������� ������\n";
	cout <<	"	2) ����� '������� �����������' - ���������� ������� ������ � ����� ������ �� ����������� ����� ������:\n";
	cout << "��� ������������ ����� ��������� ����� ������ ������ � ������ ��� ����������� ����������� �����\n";
	cout << "�������������� �� ������ (������)\n";
	cout << "	3) ����� '�������� �������������� ������' - ����� ���� �����, ��� � ���������� ��� ������. �������\n";
	cout << "����������� � ���, ��� ��������� �� �������� ������� ������, � ����� �� �� ������, � ������� ��� ����\n";
	cout << "�������� �����, ���� �� ��� �������� ������ 1 � 2";
	cout << "	4) ����� '��������� ������ � ���������' - ���� ����� ������������ ��� �������� �� ����� ���� ���������\n";
	cout << "����� �������� ������ � ��������� (���������, ����������� �������������, ��� �������� ��������������� ������)\n";
	cout << "	5) ����� '� ���������' (�� ������ �����) - ������� �� ����� ���������� �������\n\n";
	cout << "����� �������.";
	return;
}