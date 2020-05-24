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

namespace ScheduleApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScheduleContext context;

        public MainWindow()
        {
            InitializeComponent();
            context = new ScheduleContext();
            foreach (var day in context.DaysOfWeek)
            {
                dayCB.Items.Add(day.Name);
            }
            foreach (var teacher in context.Teachers)
            {
                teacherCB.Items.Add(teacher.Name);
            }
            foreach (var item in context.Classes)
            {
                classCB.Items.Add(item.Name);
            }
            dayCB.SelectedIndex = 0;
            teacherCB.SelectedIndex = 0;
            classCB.SelectedIndex = 0;
        }

        private void DayOfWeekBtnClick(object sender, RoutedEventArgs e)
        {
            int id = 0;

            foreach (var day in context.DaysOfWeek)
            {
                if (day.Name == (string)dayCB.SelectedValue)
                {
                    id = day.Id;
                }
            }

            List<Schedule> schedules = new List<Schedule>();

            foreach(var schedule in context.Schedule)
            {
                if (schedule.DayOfWeekId == id)
                {
                    schedules.Add(schedule);
                }

            }
            TableForm tableForm = new TableForm(schedules, context);
            tableForm.Show();
        }

        private void TeacherBtnClick(object sender, RoutedEventArgs e)
        {
            Teachers teacher = null;

            foreach (var obj in context.Teachers)
            {
                if (obj.Name == (string)teacherCB.SelectedValue)
                {
                    teacher = obj;
                }
            }

            List<Schedule> schedules = new List<Schedule>();

            foreach (var lessons in teacher.Lessons)
            {
                foreach (var schedule in lessons.Schedule)
                {
                    schedules.Add(schedule);
                }
            }
            TableForm tableForm = new TableForm(schedules, context);
            tableForm.Show();
        }

        private void ClassBtnClick(object sender, RoutedEventArgs e)
        {
            int id = 0;

            foreach (var obj in context.Classes)
            {
                if (obj.Name == (string)dayCB.SelectedValue)
                {
                    id = obj.Id;
                }
            }

            List<Schedule> schedules = new List<Schedule>();

            foreach (var schedule in context.Schedule)
            {
                if (schedule.ClassId == id)
                {
                    schedules.Add(schedule);
                }

            }
            TableForm tableForm = new TableForm(schedules, context);
            tableForm.Show();
        }

        private void ShowSchedule(object sender, RoutedEventArgs e)
        {
            List<Schedule> schedules = context.Schedule.ToList();
            TableForm tableForm = new TableForm(schedules, context);
            tableForm.Show();
        }

        private void TeacherListTbClick(object sender, RoutedEventArgs e)
        {
            List<Teachers> teachers = context.Teachers.ToList();
            TableForm tableForm = new TableForm(teachers, context);
            tableForm.Show();
        }

        private void CabinetsListTbClick(object sender, RoutedEventArgs e)
        {
            List<Cabinets> cabinets = context.Cabinets.ToList();
            TableForm tableForm = new TableForm(cabinets, context);
            tableForm.Show();
        }
    }
}
