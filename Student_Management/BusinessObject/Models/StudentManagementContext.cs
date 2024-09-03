using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models
{
    public partial class StudentManagementContext : DbContext
    {
        public StudentManagementContext()
        {
        }

        public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseSchedule> CourseSchedules { get; set; } = null!;
        public virtual DbSet<Curriculum> Curricula { get; set; } = null!;
        public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; } = null!;
        public virtual DbSet<GradeCategory> GradeCategories { get; set; } = null!;
        public virtual DbSet<Major> Majors { get; set; } = null!;
        public virtual DbSet<RegisterType> RegisterTypes { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<SlotOfWeek> SlotOfWeeks { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<StudentCourseAttendance> StudentCourseAttendances { get; set; } = null!;
        public virtual DbSet<StudentCourseGrade> StudentCourseGrades { get; set; } = null!;
        public virtual DbSet<StudentSubjectCurriculum> StudentSubjectCurricula { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectCurriculum> SubjectCurricula { get; set; } = null!;
        public virtual DbSet<SubjectGrade> SubjectGrades { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=LAPTOP-AS7TTRIH\\SQLEXPRESS;database=StudentManagement;uid=sa;pwd=123;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Admin_Account");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingName).HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Course_Admin");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_Course_Semester");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Course_Status");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Course_Subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Course_Teacher");
            });

            modelBuilder.Entity<CourseSchedule>(entity =>
            {
                entity.ToTable("Course_Schedule");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseSchedules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Course_Schedule_Course");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.CourseSchedules)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Course_Schedule_Room");

                entity.HasOne(d => d.SlotOfWeek)
                    .WithMany(p => p.CourseSchedules)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK_Course_Schedule_SlotOfWeek");
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.ToTable("Curriculum");

                entity.Property(e => e.CurriculumName).HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Curricula)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Curriculum_Major");
            });

            modelBuilder.Entity<DayOfWeek>(entity =>
            {
                entity.ToTable("DayOfWeek");

                entity.Property(e => e.DayOfWeek1)
                    .HasMaxLength(50)
                    .HasColumnName("DayOfWeek");
            });

            modelBuilder.Entity<GradeCategory>(entity =>
            {
                entity.ToTable("GradeCategory");

                entity.Property(e => e.GradeCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.MajorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MajorName).HasMaxLength(50);
            });

            modelBuilder.Entity<RegisterType>(entity =>
            {
                entity.ToTable("RegisterType");

                entity.Property(e => e.RegistrationName).HasMaxLength(100);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.RegistrationName).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Registration_Admin");

                entity.HasOne(d => d.RegisterType)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.RegisterTypeId)
                    .HasConstraintName("FK_Registration_RegisterType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomName).HasMaxLength(50);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_Room_Building");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.SemesterName).HasMaxLength(50);

                entity.Property(e => e.StarDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.FromHour).HasMaxLength(50);

                entity.Property(e => e.SlotName).HasMaxLength(50);

                entity.Property(e => e.ToHour).HasMaxLength(50);
            });

            modelBuilder.Entity<SlotOfWeek>(entity =>
            {
                entity.ToTable("SlotOfWeek");

                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.SlotOfWeeks)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotOfWeek_DayOfWeek");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.SlotOfWeeks)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SlotOfWeek_Slot");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusName).HasMaxLength(50);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Statuses)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Status_Type");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.StudentCode).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Student_Account");

                entity.HasOne(d => d.Curriculum)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CurriculumId)
                    .HasConstraintName("FK_Student_Curriculum");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Student_Major");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("Student_Course");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Student_Course_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Student_Course_Student");
            });

            modelBuilder.Entity<StudentCourseAttendance>(entity =>
            {
                entity.ToTable("Student_Course_Attendance");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CourseSchedule)
                    .WithMany(p => p.StudentCourseAttendances)
                    .HasForeignKey(d => d.CourseScheduleId)
                    .HasConstraintName("FK_Student_Course_Attendance_Course_Schedule");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StudentCourseAttendances)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Student_Course_Attendance_Account");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.StudentCourseAttendances)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Student_Course_Attendance_Status");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.StudentCourseAttendances)
                    .HasForeignKey(d => d.StudentCourseId)
                    .HasConstraintName("FK_Student_Course_Attendance_Student_Course");
            });

            modelBuilder.Entity<StudentCourseGrade>(entity =>
            {
                entity.ToTable("Student_Course_Grade");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourseGrades)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Student_Course_Grade_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourseGrades)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Student_Course_Grade_Student");

                entity.HasOne(d => d.SubjectGrade)
                    .WithMany(p => p.StudentCourseGrades)
                    .HasForeignKey(d => d.SubjectGradeId)
                    .HasConstraintName("FK_Student_Course_Grade_Subject_Grade");
            });

            modelBuilder.Entity<StudentSubjectCurriculum>(entity =>
            {
                entity.ToTable("Student_Subject_Curriculum");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.StudentSubjectCurricula)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Student_Subject_Curriculum_Status");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentSubjectCurricula)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Student_Subject_Curriculum_Student");

                entity.HasOne(d => d.SubjectCurriculum)
                    .WithMany(p => p.StudentSubjectCurricula)
                    .HasForeignKey(d => d.SubjectCurriculumId)
                    .HasConstraintName("FK_Student_Subject_Curriculum_Subject_Curriculum");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectCode).HasMaxLength(50);

                entity.Property(e => e.SubjectName).HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Subject_Admin");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Subject_Major");

                entity.HasOne(d => d.SubjectPrequisiteNavigation)
                    .WithMany(p => p.InverseSubjectPrequisiteNavigation)
                    .HasForeignKey(d => d.SubjectPrequisite)
                    .HasConstraintName("FK_Subject_Subject");
            });

            modelBuilder.Entity<SubjectCurriculum>(entity =>
            {
                entity.ToTable("Subject_Curriculum");

                entity.HasOne(d => d.Curriculum)
                    .WithMany(p => p.SubjectCurricula)
                    .HasForeignKey(d => d.CurriculumId)
                    .HasConstraintName("FK_Subject_Curriculum_Curriculum");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectCurricula)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Subject_Curriculum_Subject");
            });

            modelBuilder.Entity<SubjectGrade>(entity =>
            {
                entity.ToTable("Subject_Grade");

                entity.Property(e => e.GradeItem).HasMaxLength(50);

                entity.HasOne(d => d.GradeCategory)
                    .WithMany(p => p.SubjectGrades)
                    .HasForeignKey(d => d.GradeCategoryId)
                    .HasConstraintName("FK_Subject_Grade_GradeCategory");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectGrades)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Subject_Grade_Subject");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TeacherCode).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Teacher_Account");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Teacher_Major");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
