namespace ScheduleApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ScheduleContext : DbContext
    {
        public ScheduleContext()
            : base("name=ScheduleContext")
        {
        }

        public virtual DbSet<Cabinets> Cabinets { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeek { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinets>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Cabinets)
                .HasForeignKey(e => e.CabinetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Classes>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Classes>()
                .HasMany(e => e.Schedule)
                .WithOptional(e => e.Classes)
                .HasForeignKey(e => e.ClassId);

            modelBuilder.Entity<DaysOfWeek>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<DaysOfWeek>()
                .HasMany(e => e.Schedule)
                .WithRequired(e => e.DaysOfWeek)
                .HasForeignKey(e => e.DayOfWeekId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lessons>()
                .Property(e => e.Lesson)
                .IsFixedLength();

            modelBuilder.Entity<Lessons>()
                .HasMany(e => e.Schedule)
                .WithRequired(e => e.Lessons)
                .HasForeignKey(e => e.LessonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teachers>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Teachers>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.Teachers)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);
        }
    }
}
