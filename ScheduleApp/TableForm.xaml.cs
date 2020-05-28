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

        List<CustomSchedule> customSchedules;
        List<TeacherCabinet> teacherCabinets;

        public TableForm(List<Schedule> list, ScheduleContext context)
        {
            InitializeComponent();

            schedules = list;

            customSchedules = new List<CustomSchedule>();

            foreach (var item in schedules)
            {
                CustomSchedule customSchedule = new CustomSchedule();
                customSchedule.Id = item.Id;
                customSchedule.DayOfWeek = item.DaysOfWeek.Name;
                customSchedule.Lesson = item.Lessons.Lesson;
                customSchedule.Class = item.Classes.Name;

                customSchedules.Add(customSchedule);
            }

            dataGrid.Columns.Add(new DataGridTextColumn { Header="ID", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="Предмет", Binding = new Binding("Lesson") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="Класс", Binding = new Binding("Class") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header="День недели", Binding = new Binding("DayOfWeek") });

            dataGrid.ItemsSource = customSchedules;

            dataGrid.SelectedIndex = 0;

            this.context = context;

            current = 0;

            printBtn.Visibility = Visibility.Visible;
        }

        public TableForm(List<Teachers> list, ScheduleContext context)
        {
            InitializeComponent();

            teachers = list;

            teacherCabinets = new List<TeacherCabinet>();

            foreach (var item in teachers)
            {
                teacherCabinets.Add(new TeacherCabinet {
                    Id = item.Id,
                    Teacher = item.Name,
                    Cabinet = item.Cabinets.Number
                });
            }

            cabinets = new List<Cabinets>();

            foreach (var cabinet in context.Cabinets)
            {
                cabinets.Add(cabinet);
            }

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id учителя", Binding = new Binding("Id") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Имя учителя", Binding = new Binding("Teacher") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер кабинета", Binding = new Binding("Cabinet") });

            dataGrid.ItemsSource = teacherCabinets;

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
                try
                {
                    Schedule schedule = new Schedule
                    {
                        Id = int.Parse(id.Text),
                        ClassId = int.Parse(classId.Text),
                        LessonId = int.Parse(lessonId.Text),
                        DayOfWeekId = int.Parse(dayId.Text)
                    };

                    context.Schedule.Add(schedule);
                    context.SaveChanges();

                    schedules.Add(schedule);

                    CustomSchedule customSchedule = new CustomSchedule();
                    customSchedule.Id = schedule.Id;

                    foreach (var item in context.Classes)
                    {
                        if (item.Id == schedule.ClassId)
                        {
                            customSchedule.Class = item.Name;
                            break;
                        }
                    }

                    foreach (var item in context.DaysOfWeek)
                    {
                        if (item.Id == schedule.DayOfWeekId)
                        {
                            customSchedule.DayOfWeek = item.Name;
                            break;
                        }
                    }

                    foreach (var item in context.Lessons)
                    {
                        if (item.Id == schedule.LessonId)
                        {
                            customSchedule.Lesson = item.Lesson;
                            break;
                        }
                    }

                    customSchedules.Add(customSchedule);

                    dataGrid.ItemsSource = customSchedules;
                    dataGrid.Items.Refresh();

                    MessageBox.Show("Объект с id " + schedule.Id + " был добавлен");
                }
                catch(Exception)
                {
                    MessageBox.Show("При добавлении записи произошла ошибка, проверьте правильность вводимых данных(возможно указан неправильный ID)");
                }
            }
            else if (current == 1)
            {
                try
                {
                    Teachers teacher = new Teachers
                    {
                        Id = int.Parse(id.Text),
                        Name = classId.Text,
                        CabinetId = int.Parse(lessonId.Text),
                    };

                    context.Teachers.Add(teacher);
                    context.SaveChanges();

                    teachers.Add(teacher);


                    TeacherCabinet teacherCabinet = new TeacherCabinet();
                    teacherCabinet.Id = teacher.Id;
                    teacherCabinet.Teacher = teacher.Name;

                    foreach (var item in cabinets)
                    {
                        if (item.Id == teacher.CabinetId)
                        {
                            teacherCabinet.Cabinet = item.Number;
                            break;
                        }
                    }

                    teacherCabinets.Add(teacherCabinet);

                    dataGrid.ItemsSource = teacherCabinets;
                    dataGrid.Items.Refresh();
                    MessageBox.Show("Объект с id " + teacher.Id + " был добавлен");
                }
                catch(Exception)
                {
                    MessageBox.Show("При добавлении записи произошла ошибка, проверьте правильность вводимых данных(возможно указан неправильный ID)");
                }
            }
            else
            {
                try
                {
                    Cabinets cabinet = new Cabinets
                    {
                        Id = int.Parse(id.Text),
                        Number = int.Parse(classId.Text)
                    };
                    cabinets.Add(cabinet);

                    context.Cabinets.Add(cabinet);
                    context.SaveChanges();

                    dataGrid.ItemsSource = cabinets;
                    dataGrid.Items.Refresh();
                    MessageBox.Show("Объект с id " + cabinet.Id + " был добавлен");
                }
                catch(Exception)
                {
                    MessageBox.Show("При добавлении записи произошла ошибка, проверьте правильность вводимых данных(возможно указан неправильный ID)");
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int id = 0;

                if (current == 0)
                {
                    foreach(var item in customSchedules)
                    {
                        if (item.Id == (dataGrid.SelectedItem as CustomSchedule).Id)
                        {
                            id = item.Id;
                            customSchedules.Remove(item);
                            foreach (var schedule in context.Schedule)
                            {
                                if (schedule.Id == id)
                                {
                                    context.Schedule.Remove(schedule);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    dataGrid.ItemsSource = customSchedules;
                }
                else if (current == 1)
                {
                    foreach (var item in teacherCabinets)
                    {
                        if (item.Id == (dataGrid.SelectedItem as TeacherCabinet).Id)
                        {
                            id = item.Id;
                            teacherCabinets.Remove(item);
                            foreach (var teacher in context.Teachers)
                            {
                                if (teacher.Id == id)
                                {
                                    context.Teachers.Remove(teacher);
                                }
                            }
                            break;
                        }
                    }
                    dataGrid.ItemsSource = teacherCabinets;
                }
                else
                {
                    foreach (var item in cabinets)
                    {
                        if (item.Id == (dataGrid.SelectedItem as Cabinets).Id)
                        {
                            id = item.Id;
                            cabinets.Remove(item);
                            foreach (var cabinet in context.Cabinets)
                            {
                                if (cabinet.Id == id)
                                {
                                    context.Cabinets.Remove(cabinet);
                                }
                            }
                            break;
                        }
                    }
                    dataGrid.ItemsSource = cabinets;
                }

                context.SaveChanges();
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

            foreach(var item in customSchedules)
                result += $"ID - {item.Id}, ID предмета - {item.Lesson}, ID класса - {item.Class}, ID дня недели - {item.DayOfWeek}\n";
            

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