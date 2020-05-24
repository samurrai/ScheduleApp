using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
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

namespace ScheduleApp
{
    /// <summary>
    /// Логика взаимодействия для DaySchedule.xaml
    /// </summary>
    public partial class TableForm : Window
    {
        ScheduleContext context;

        int current;

        string result = "";

        List<Schedule> schedules;
        List<Teachers> teachers;
        List<Cabinets> cabinets;

        public TableForm(List<Schedule> list, ScheduleContext context)
        {
            InitializeComponent();

            schedules = list;

            dataGrid.Columns.Add(new DataGridTextColumn { Header="ID", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="ID предмета", Binding = new Binding("LessonId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="ID класса", Binding = new Binding("ClassId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="ID дня недели", Binding = new Binding("DayOfWeekId") });

            dataGrid.ItemsSource = schedules;

            dataGrid.SelectedIndex = 0;

            this.context = context;

            current = 0;

            printBtn.Visibility = Visibility.Visible;
        }

        public TableForm(List<Teachers> list, ScheduleContext context)
        {
            InitializeComponent();

            teachers = list;

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Имя учителя", Binding = new Binding("Name") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ID кабинета", Binding = new Binding("CabinetId") });

            dataGrid.ItemsSource = teachers;

            dataGrid.SelectedIndex = 0;

            this.context = context;

            current = 1;

            lessonL.Content = "ID кабинета";

            dayL.Content = "";
            dayId.Visibility = Visibility.Collapsed;

            classL.Content = "Имя учителя";
            idL.Content = "ID учителя";
        }

        public TableForm(List<Cabinets> list, ScheduleContext context)
        {
            InitializeComponent();

            cabinets = list;

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер кабинета", Binding = new Binding("Number") });

            dataGrid.ItemsSource = cabinets;

            dataGrid.SelectedIndex = 0;

            this.context = context;

            current = 2;

            lessonL.Content = "";
            lessonId.Visibility = Visibility.Collapsed;

            dayL.Content = "";
            dayId.Visibility = Visibility.Collapsed;

            classL.Content = "Номер кабинета";
            idL.Content = "ID кабинета";
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (current == 0)
            {
                Schedule schedule = new Schedule
                {
                    Id = int.Parse(id.Text),
                    ClassId = int.Parse(classId.Text),
                    LessonId = int.Parse(lessonId.Text),
                    DayOfWeekId = int.Parse(dayId.Text)
                };
                schedules.Add(schedule);

                dataGrid.ItemsSource = schedules;
                dataGrid.Items.Refresh();
                MessageBox.Show("Объект с id " + schedule.Id + " был добавлен");
            }
            else if (current == 1)
            {
                Teachers teacher = new Teachers
                {
                    Id = int.Parse(id.Text),
                    Name = classId.Text,
                    CabinetId = int.Parse(lessonId.Text),
                };
                teachers.Add(teacher);

                dataGrid.ItemsSource = teachers;
                dataGrid.Items.Refresh();
                MessageBox.Show("Объект с id " + teacher.Id + " был добавлен");
            }
            else
            {
                Cabinets cabinet = new Cabinets
                {
                    Id = int.Parse(id.Text),
                    Number = int.Parse(classId.Text)
                };
                cabinets.Add(cabinet);

                dataGrid.ItemsSource = cabinets;
                dataGrid.Items.Refresh();
                MessageBox.Show("Объект с id " + cabinet.Id + " был добавлен");

            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int id = 0;

                if (current == 0)
                {
                    foreach(var item in schedules)
                    {
                        if (item.Id == (dataGrid.SelectedItem as Schedule).Id)
                        {
                            id = item.Id;
                            schedules.Remove(item);
                            break;
                        }
                    }
                    dataGrid.ItemsSource = schedules;
                }
                else if (current == 1)
                {
                    foreach (var item in teachers)
                    {
                        if (item.Id == (dataGrid.SelectedItem as Teachers).Id)
                        {
                            id = item.Id;
                            teachers.Remove(item);
                            break;
                        }
                    }
                    dataGrid.ItemsSource = teachers;
                }
                else
                {
                    foreach (var item in cabinets)
                    {
                        if (item.Id == (dataGrid.SelectedItem as Cabinets).Id)
                        {
                            id = item.Id;
                            cabinets.Remove(item);
                            break;
                        }
                    }
                    dataGrid.ItemsSource = cabinets;
                }

                dataGrid.Items.Refresh();
                MessageBox.Show("Объект с id " + id + " был удален");
            }
            else
            {
                MessageBox.Show("Выберите объект");
            }
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintPageHandler;

            foreach(var item in schedules)
                result += $"ID - {item.Id}, ID предмета - {item.LessonId}, ID класса - {item.ClassId}, ID дня недели - {item.DayOfWeekId}\n";
            

            PrintDialog printDialog = new PrintDialog();

            if ((bool)printDialog.ShowDialog())
                printDocument.Print();
        }

        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(result, new Font("Arial", 14), System.Drawing.Brushes.Black, 0, 0);
        }
    }
}