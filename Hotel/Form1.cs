using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.DB;

namespace Hotel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HotelDB db = new HotelDB();
        private void Added(string str, string id) => dataGridView2.Rows.Add($"Добавлено в таблицу {str} значение с ID = {id}");
        private void Deleted(string str, string id) => dataGridView2.Rows.Add($"Удалено из таблицы {str} значение с ID = {id}");
        private void Changed(string str, string id) => dataGridView2.Rows.Add($"Изменено в таблице {str} значение с ID = {id}");

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            var item = comboBox1.Text;
            if (item == "Clients")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Client";
                dataGridView1.Columns[1].HeaderText = "FirstName";
                dataGridView1.Columns[2].HeaderText = "Surname";
                dataGridView1.Columns[3].HeaderText = "BirthDate";
                dataGridView1.Columns[4].HeaderText = "Telephone";
                dataGridView1.Columns[5].HeaderText = "";
                var query = from x in db.Clients select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Client, l[i].FirstName, l[i].Surname, l[i].BirthDate, l[i].Telephone);
            }
            if (item == "Free_Rooms")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Room";
                dataGridView1.Columns[1].HeaderText = "Room_Type";
                dataGridView1.Columns[2].HeaderText = "Cost";
                dataGridView1.Columns[3].HeaderText = "";
                dataGridView1.Columns[4].HeaderText = "";
                dataGridView1.Columns[5].HeaderText = "";
                var query = from x in db.Free_Rooms select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Room, l[i].Room_Type, l[i].Cost);
            }
            if (item == "Rooms")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Room";
                dataGridView1.Columns[1].HeaderText = "Room_Type";
                dataGridView1.Columns[2].HeaderText = "ID_Client";
                dataGridView1.Columns[3].HeaderText = "Date_Arrive";
                dataGridView1.Columns[4].HeaderText = "Date_Departure";
                dataGridView1.Columns[5].HeaderText = "Cost_Per_Day";
                var query = from x in db.Rooms select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Room, l[i].Room_Type, l[i].ID_Client, l[i].Date_Arrive, l[i].Date_Departure, l[i].Cost_Per_Day);
            }
            if (item == "Staff")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Staff";
                dataGridView1.Columns[1].HeaderText = "Full_Name";
                dataGridView1.Columns[2].HeaderText = "Position";
                dataGridView1.Columns[3].HeaderText = "Salary";
                dataGridView1.Columns[4].HeaderText = "Telephone";
                dataGridView1.Columns[5].HeaderText = "";
                var query = from x in db.Staff select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Staff, l[i].Full_Name, l[i].Position, l[i].Salary, l[i].Telephone);
            }
            if (item == "Service_")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Service";
                dataGridView1.Columns[1].HeaderText = "Name_of_service";
                dataGridView1.Columns[2].HeaderText = "Cost";
                dataGridView1.Columns[3].HeaderText = "";
                dataGridView1.Columns[4].HeaderText = "";
                dataGridView1.Columns[5].HeaderText = "";
                var query = from x in db.Service_ select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Service, l[i].Name_of_service, l[i].Cost);
            }
            //service-clients & service-staff не отобразились в студии, хотя на серваке есть
            if (item == "Contract_")
            {
                dataGridView1.Columns[0].HeaderText = "ID_Room";
                dataGridView1.Columns[1].HeaderText = "Room_Type";
                dataGridView1.Columns[2].HeaderText = "ID_Client";
                dataGridView1.Columns[3].HeaderText = "Date_Arrive";
                dataGridView1.Columns[4].HeaderText = "";
                dataGridView1.Columns[5].HeaderText = "";
                var query = from x in db.Contract_ select x;
                var l = query.ToList();
                for (int i = 0; i < l.Count; i++)
                    dataGridView1.Rows.Add(l[i].ID_Contract, l[i].ID_Client, l[i].Type_of_contact, l[i].Cost);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = comboBox1.Text;
            if (item == "Clients")
            {
                var length = 5;
                var query = from x in db.Clients select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Client.ToString()) && l2[i].Contains(l[j].FirstName) && l2[i].Contains(l[j].Surname) && l2[i].Contains(l[j].BirthDate.ToString()) && l2[i].Contains(l[j].Telephone))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Client.ToString()) && (!l2[i].Contains(l[j].FirstName) || !l2[i].Contains(l[j].Surname) || !l2[i].Contains(l[j].BirthDate.ToString()) ||
                                !l2[i].Contains(l[j].Telephone)))//на изменение имеющихся
                            {
                                l[j].FirstName = l2[i][1];//изменение и взаимоуничтожение
                                l[j].Surname = l2[i][2];
                                l[j].BirthDate = DateTime.Parse(l2[i][3]);
                                l[j].Telephone = l2[i][4];
                                Changed(item, l2[i][0]);//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Clients.Add(new Clients { ID_Client = Convert.ToInt32(l2[i][0]), FirstName = l2[i][1], Surname = l2[i][2], BirthDate = DateTime.Parse(l2[i][3]), Telephone = l2[i][4] });
                        Added(item, l2[i][0]);//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Clients.Remove(l[i]);
                        Deleted(item, l[i].ID_Client.ToString());//триггер на удаление
                    }
                db.SaveChanges();//сохранение
            }
            if (item == "Free_Rooms")
            {
                var length = 3;
                var query = from x in db.Free_Rooms select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Room.ToString()) && l2[i].Contains(l[j].Room_Type) && l2[i].Contains(l[j].Cost.ToString()))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Room.ToString()) && (!l2[i].Contains(l[j].Room_Type) || !l2[i].Contains(l[j].Cost.ToString())))//на изменение имеющихся
                            {
                                l[j].Room_Type = l2[i][1];//изменение и взаимоуничтожение
                                l[j].Cost = Convert.ToInt32(l2[i][2]);
                                Changed(item, l2[i][0]);//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Free_Rooms.Add(new Free_Rooms { ID_Room = Convert.ToInt32(l2[i][0]), Room_Type = l2[i][1], Cost = Convert.ToInt32(l2[i][2]) });
                        Added(item, l2[i][0]);//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Free_Rooms.Remove(l[i]);
                        Deleted(item, l[i].ID_Room.ToString());//триггер на удаление
                    }
                db.SaveChanges();//сохранение
            }
            if (item == "Rooms")
            {
                var length = 6;
                var query = from x in db.Rooms select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Room.ToString()) && l2[i].Contains(l[j].Room_Type) && l2[i].Contains(l[j].ID_Client.ToString()) &&
                                l2[i].Contains(l[j].Date_Arrive.ToString()) && l2[i].Contains(l[j].Date_Departure.ToString()) && l2[i].Contains(l[j].Cost_Per_Day.ToString()))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Room.ToString()) && l2[i].Contains(l[j].ID_Client.ToString()) && !l2[i].Contains(l[j].Room_Type) ||
                                !l2[i].Contains(l[j].Date_Arrive.ToString()) || !l2[i].Contains(l[j].Date_Departure.ToString()) || !l2[i].Contains(l[j].Cost_Per_Day.ToString()))//на изменение имеющихся
                            {//изменение и взаимоуничтожение
                                l[j].Room_Type = l2[i][1];
                                l[j].Date_Arrive = DateTime.Parse(l2[i][3]);
                                l[j].Date_Departure = DateTime.Parse(l2[i][4]);
                                l[j].Cost_Per_Day = Convert.ToInt32(l2[i][5]);
                                Changed(item, (l2[i][0] + " и " + l2[i][2]));//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Rooms.Add(new Rooms { ID_Room = Convert.ToInt32(l2[i][0]), Room_Type = l2[i][1], ID_Client = Convert.ToInt32(l2[i][2]),
                        Date_Arrive = DateTime.Parse(l2[i][3]), Date_Departure = DateTime.Parse(l2[i][4]), Cost_Per_Day = Convert.ToInt32(l2[i][5]) });
                        Added(item, $"{l2[i][0]} и {l2[i][2]}");//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Rooms.Remove(l[i]);
                        Deleted(item, $"{l[i].ID_Room} и {l[i].ID_Client}");//триггер на удаление
                    }
                db.SaveChanges();//сохранение
                //Примечание - надо было добавить datetime для комфорта и избежаний конфликта
            }
            if (item == "Staff")
            {
                var length = 5;
                var query = from x in db.Staff select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Staff.ToString()) && l2[i].Contains(l[j].Full_Name) && l2[i].Contains(l[j].Position) && l2[i].Contains(l[j].Salary.ToString()) && l2[i].Contains(l[j].Telephone))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Staff.ToString()) && (!l2[i].Contains(l[j].Full_Name) || !l2[i].Contains(l[j].Position) || !l2[i].Contains(l[j].Salary.ToString()) ||
                                !l2[i].Contains(l[j].Telephone)))//на изменение имеющихся
                            {
                                l[j].Full_Name = l2[i][1];//изменение и взаимоуничтожение
                                l[j].Position = l2[i][2];
                                l[j].Salary = Convert.ToInt32(l2[i][3]);
                                l[j].Telephone = l2[i][4];
                                Changed(item, l2[i][0]);//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Staff.Add(new Staff { ID_Staff = Convert.ToInt32(l2[i][0]), Full_Name = l2[i][1], Salary = Convert.ToInt32(l2[i][2]), Telephone = l2[i][3] });
                        Added(item, l2[i][0]);//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Staff.Remove(l[i]);
                        Deleted(item, l[i].ID_Staff.ToString());//триггер на удаление
                    }
                db.SaveChanges();//сохранение
            }
            if (item == "Service_")
            {
                var length = 3;
                var query = from x in db.Service_ select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Service.ToString()) && l2[i].Contains(l[j].Name_of_service) && l2[i].Contains(l[j].Cost.ToString()))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Service.ToString()) && (!l2[i].Contains(l[j].Name_of_service) || !l2[i].Contains(l[j].Cost.ToString())))//на изменение имеющихся
                            {
                                l[j].Name_of_service = l2[i][1];//изменение и взаимоуничтожение
                                l[j].Cost = Convert.ToInt32(l2[i][2]);
                                Changed(item, l2[i][0]);//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Service_.Add(new Service_ { ID_Service = Convert.ToInt32(l2[i][0]), Name_of_service = l2[i][2], Cost = Convert.ToInt32(l2[i][2]) });
                        Added(item, l2[i][0]);//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Service_.Remove(l[i]);
                        Deleted(item, l[i].ID_Service.ToString());//триггер на удаление
                    }
                db.SaveChanges();//сохранение
            }
            if (item == "Contract_")
            {
                var length = 4;
                var query = from x in db.Contract_ select x;
                var l = query.ToList();//список с выборки (текущий)
                var l2 = new List<List<string>>();//список с таблицы (с чем сверять)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    l2.Add(new List<string>());
                for (int i = 0; i < l2.Count; i++)
                    for (int j = 0; j < length; j++)
                        l2[i].Add(dataGridView1[j, i].Value.ToString());//Заливка данных в список

                for (int j = 0; j < l.Count; j++)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        try
                        {
                            if (l2[i].Contains(l[j].ID_Contract.ToString()) && l2[i].Contains(l[j].ID_Contract.ToString()) && l2[i].Contains(l[j].Type_of_contact) && l2[i].Contains(l[j].Cost.ToString()))//на добавление новых/удаление через фильтрацию списков
                            {
                                l2.Remove(l2[i]);//при равенстве взаимоуничтожение с откатом по индексу. Исключение обработано пустотой для продолжения итерации
                                l.Remove(l[j]);
                                i--;
                            }
                            if (l2[i].Contains(l[j].ID_Contract.ToString()) && (!l2[i].Contains(l[j].ID_Contract.ToString()) || !l2[i].Contains(l[j].Type_of_contact) || !l2[i].Contains(l[j].Cost.ToString())))//на изменение имеющихся
                            {
                                l[j].ID_Client = Convert.ToInt32(l2[i][1]);//изменение и взаимоуничтожение
                                l[j].Type_of_contact = l2[i][2];
                                l[j].Cost = Convert.ToInt32(l2[i][3]);
                                Changed(item, l2[i][0]);//триггер на обновление
                                l2.Remove(l2[i]);
                                l.Remove(l[j]);
                                i--;
                            }
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                if (l2.Count > 0)//добавление новых, если есть
                    for (int i = 0; i < l2.Count; i++)
                    {
                        db.Contract_.Add(new Contract_ { ID_Contract = Convert.ToInt32(l2[i][0]), ID_Client = Convert.ToInt32(l2[i][2]), Type_of_contact = l2[i][3], Cost = Convert.ToInt32(l2[i][4]) });
                        Added(item, l2[i][0]);//триггер на добавление
                    }
                if (l.Count > 0)//удаление, если есть
                    for (int i = 0; i < l.Count; i++)
                    {
                        db.Contract_.Remove(l[i]);
                        Deleted(item, l[i].ID_Contract.ToString());//триггер на удаление
                    }
                db.SaveChanges();//сохранение
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
