namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        ActionBit = c.Short(nullable: false),
                        Priority = c.Byte(nullable: false),
                        IsIndex = c.Boolean(nullable: false),
                        ControllerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controllers", t => t.ControllerId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.Controllers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                        Icone = c.String(nullable: false, maxLength: 200),
                        Priority = c.Byte(nullable: false),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Priority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Module_Name");
            
            CreateTable(
                "dbo.AssayAnswerSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssayId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AssayVarient = c.Int(nullable: false),
                        AssayTime = c.DateTime(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        AnswerTimes = c.String(maxLength: 50),
                        Answers = c.String(maxLength: 50),
                        MaybeList = c.String(maxLength: 50),
                        AfterList = c.String(maxLength: 50),
                        CantList = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.AssayId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Assays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Time = c.Int(nullable: false),
                        LookupId_Importance = c.Int(nullable: false),
                        LookupId_Type = c.Int(nullable: false),
                        LookupId_QuestionType = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateTimeCreate = c.DateTime(nullable: false),
                        NumberOfVarient = c.Int(nullable: false),
                        File1 = c.String(maxLength: 50),
                        File2 = c.String(maxLength: 50),
                        File3 = c.String(maxLength: 50),
                        File4 = c.String(maxLength: 50),
                        QuestionsFile1 = c.String(),
                        QuestionsFile2 = c.String(),
                        QuestionsFile3 = c.String(),
                        QuestionsFile4 = c.String(),
                        QuestionsAnswer1 = c.String(),
                        QuestionsAnswer2 = c.String(),
                        QuestionsAnswer3 = c.String(),
                        QuestionsAnswer4 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Importance)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Type)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LookupId_Importance)
                .Index(t => t.LookupId_Type)
                .Index(t => t.LookupId_QuestionType)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AssayQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssayId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.AssayId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        LookupId_QuestionType = c.Int(nullable: false),
                        QuestionPoint = c.Int(nullable: false),
                        LookupId_QuestionHardnessType = c.Int(nullable: false),
                        LookupId_RepeatnessType = c.Int(nullable: false),
                        UseEvaluation = c.Boolean(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        LookupId_AuthorType = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                        ResponseSecond = c.Short(nullable: false),
                        LookupId_QuestionRank = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        FileName = c.String(nullable: false, maxLength: 50),
                        InsertDateTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsExercise = c.Boolean(nullable: false),
                        IsLearning = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        IsHybrid = c.Boolean(nullable: false),
                        AnswerNumber = c.Int(nullable: false),
                        TopicAnswer = c.String(maxLength: 1000),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AuthorType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionHardnessType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionRank)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_RepeatnessType)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.LookupId_QuestionType)
                .Index(t => t.LookupId_QuestionHardnessType)
                .Index(t => t.LookupId_RepeatnessType)
                .Index(t => t.LookupId_AuthorType)
                .Index(t => t.WriterId)
                .Index(t => t.LookupId_QuestionRank);
            
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FatherName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TeacherGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FatherName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        StudentId = c.Int(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.ProgramItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupId_PrgoramItemName = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                        Hour = c.Int(nullable: false),
                        DayOfWeak = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PrgoramItemName)
                .ForeignKey("dbo.Programs", t => t.ProgramId)
                .Index(t => t.LookupId_PrgoramItemName)
                .Index(t => t.ProgramId);
            
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationTrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ParentEducationTreeId = c.Int(),
                        LookupId_EducationTreeState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_EducationTreeState)
                .ForeignKey("dbo.EducationTrees", t => t.ParentEducationTreeId)
                .Index(t => t.ParentEducationTreeId)
                .Index(t => t.LookupId_EducationTreeState);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsMain = c.Boolean(nullable: false),
                        LookupId_Nezam = c.Int(nullable: false),
                        NumberOfJudges = c.Int(nullable: false),
                        File = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Nezam)
                .Index(t => t.Name, unique: true, name: "UK_Lesson_Name")
                .Index(t => t.LookupId_Nezam);
            
            CreateTable(
                "dbo.EducationBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        PublishYear = c.Short(nullable: false),
                        IsExamSource = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsChanged = c.Boolean(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.Name, unique: true, name: "UK_EducationBook_Name")
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        ExamStock = c.Int(nullable: false),
                        ExamStockSystem = c.Int(nullable: false),
                        Importance = c.Short(nullable: false),
                        IsExamSource = c.Boolean(nullable: false),
                        LookupId_HardnessType = c.Int(nullable: false),
                        LookupId_AreaType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ParentTopicId = c.Int(),
                        LessonId = c.Int(nullable: false),
                        DisplayPriority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AreaType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_HardnessType)
                .ForeignKey("dbo.Topics", t => t.ParentTopicId)
                .Index(t => t.LookupId_HardnessType)
                .Index(t => t.LookupId_AreaType)
                .Index(t => t.ParentTopicId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        LookupId_MediaType = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        FileName = c.String(nullable: false, maxLength: 50),
                        Price = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CoverImage = c.String(maxLength: 50),
                        Length = c.String(maxLength: 50),
                        YearOfBook = c.Int(nullable: false),
                        PagesOfBook = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_MediaType)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.LookupId_MediaType)
                .Index(t => t.WriterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 8),
                        Mobile = c.String(maxLength: 11),
                        IsAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                        ProfilePic = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.Username, unique: true, name: "UK_User_Username")
                .Index(t => t.NationalNo, unique: true, name: "UK_User_NationalNo")
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableName = c.String(nullable: false, maxLength: 150),
                        CrudType = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ObjectValue = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Context = c.String(nullable: false),
                        FilePath = c.String(maxLength: 200),
                        LookupId_AnswerType = c.Int(nullable: false),
                        LessonName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 300),
                        WriterId = c.Int(nullable: false),
                        IsMaster = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        QuestionAnswerType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AnswerType)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.LookupId_AnswerType)
                .Index(t => t.WriterId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.QuestionAnswerViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Byte(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionAnswers", t => t.AnswerId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.AnswerId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ProfilePic = c.String(maxLength: 50),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Level = c.Byte(nullable: false),
                        SumOfActionBit = c.String(nullable: false, maxLength: 300),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Role_Name");
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        PaidCode = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sale_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountItem = c.Int(nullable: false),
                        PriceRow = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        SaleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.PackageId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsDelete = c.Boolean(nullable: false),
                        ImageFile = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                        TimeDays = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LessonDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_LessonDepartment_Name");
            
            CreateTable(
                "dbo.StudentMajorlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        StudentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Majors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course = c.String(nullable: false, maxLength: 50),
                        Code = c.Int(nullable: false),
                        MajorTitle = c.String(nullable: false),
                        AdmissionFirst = c.Int(nullable: false),
                        AdmissionSecond = c.Int(nullable: false),
                        Woman = c.String(nullable: false, maxLength: 50),
                        Man = c.String(nullable: false, maxLength: 50),
                        University = c.String(nullable: false),
                        Field = c.Int(nullable: false),
                        Province = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                        Apply = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentMajorList_Major",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        StudentMajorListId = c.Int(nullable: false),
                        MajorsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Majors", t => t.MajorsId)
                .ForeignKey("dbo.StudentMajorlists", t => t.StudentMajorListId, cascadeDelete: true)
                .Index(t => t.StudentMajorListId)
                .Index(t => t.MajorsId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsSource = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Tag_Name");
            
            CreateTable(
                "dbo.AssaySchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssayId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .Index(t => t.AssayId);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorCode = c.Int(nullable: false),
                        Route = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                        Ip = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topics_EducationBooks",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        EducationBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.EducationBookId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.EducationBooks", t => t.EducationBookId)
                .Index(t => t.TopicId)
                .Index(t => t.EducationBookId);
            
            CreateTable(
                "dbo.Lessons_Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LessonId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.UserId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Packages_Assays",
                c => new
                    {
                        PackageId = c.Int(nullable: false),
                        AssayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PackageId, t.AssayId })
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .Index(t => t.PackageId)
                .Index(t => t.AssayId);
            
            CreateTable(
                "dbo.Packages_Lessons",
                c => new
                    {
                        PackageId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PackageId, t.LessonId })
                .ForeignKey("dbo.Packages", t => t.PackageId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.PackageId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Topics_Medias",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.MediaId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .Index(t => t.TopicId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.Topics_Questions",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.QuestionId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.TopicId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Lessons_LessonDepartments",
                c => new
                    {
                        LessonId = c.Int(nullable: false),
                        LessonDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LessonId, t.LessonDepartmentId })
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.LessonDepartments", t => t.LessonDepartmentId)
                .Index(t => t.LessonId)
                .Index(t => t.LessonDepartmentId);
            
            CreateTable(
                "dbo.EducationTrees_Lessons",
                c => new
                    {
                        EducationTreeId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EducationTreeId, t.LessonId })
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.StudentMajorlist_Majors",
                c => new
                    {
                        StudentMajorlistId = c.Int(nullable: false),
                        MajorsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentMajorlistId, t.MajorsId })
                .ForeignKey("dbo.StudentMajorlists", t => t.StudentMajorlistId)
                .ForeignKey("dbo.Majors", t => t.MajorsId)
                .Index(t => t.StudentMajorlistId)
                .Index(t => t.MajorsId);
            
            CreateTable(
                "dbo.TeacherGroup_Students",
                c => new
                    {
                        TeacherGroupId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherGroupId, t.StudentId })
                .ForeignKey("dbo.TeacherGroups", t => t.TeacherGroupId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.TeacherGroupId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Questions_Boxes",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        BoxId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.BoxId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Boxes", t => t.BoxId)
                .Index(t => t.QuestionId)
                .Index(t => t.BoxId);
            
            CreateTable(
                "dbo.Questions_AreaTypes",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        AreaTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.AreaTypeId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Lookups", t => t.AreaTypeId)
                .Index(t => t.QuestionId)
                .Index(t => t.AreaTypeId);
            
            CreateTable(
                "dbo.Questions_Supervisors",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.UserId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions_Tags",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.TagId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.QuestionId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Assays_Lessons",
                c => new
                    {
                        AssayId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AssayId, t.LessonId })
                .ForeignKey("dbo.Assays", t => t.AssayId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.AssayId)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssayAnswerSheets", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssayAnswerSheets", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Assays", "UserId", "dbo.Users");
            DropForeignKey("dbo.Assays", "LookupId_Type", "dbo.Lookups");
            DropForeignKey("dbo.Assays", "LookupId_QuestionType", "dbo.Lookups");
            DropForeignKey("dbo.Assays", "LookupId_Importance", "dbo.Lookups");
            DropForeignKey("dbo.Assays_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Assays_Lessons", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.AssaySchedules", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.AssayQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Questions_Tags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Questions_Tags", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions_Supervisors", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions_Supervisors", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "LookupId_RepeatnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionRank", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionHardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_AuthorType", "dbo.Lookups");
            DropForeignKey("dbo.Questions_AreaTypes", "AreaTypeId", "dbo.Lookups");
            DropForeignKey("dbo.Questions_AreaTypes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions_Boxes", "BoxId", "dbo.Boxes");
            DropForeignKey("dbo.Questions_Boxes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherGroups", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherGroup_Students", "StudentId", "dbo.Students");
            DropForeignKey("dbo.TeacherGroup_Students", "TeacherGroupId", "dbo.TeacherGroups");
            DropForeignKey("dbo.StudentMajorlists", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentMajorlist_Majors", "MajorsId", "dbo.Majors");
            DropForeignKey("dbo.StudentMajorlist_Majors", "StudentMajorlistId", "dbo.StudentMajorlists");
            DropForeignKey("dbo.StudentMajorList_Major", "StudentMajorListId", "dbo.StudentMajorlists");
            DropForeignKey("dbo.StudentMajorList_Major", "MajorsId", "dbo.Majors");
            DropForeignKey("dbo.Programs", "StudentId", "dbo.Students");
            DropForeignKey("dbo.ProgramItems", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.ProgramItems", "LookupId_PrgoramItemName", "dbo.Lookups");
            DropForeignKey("dbo.EducationTrees", "ParentEducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.EducationTrees", "LookupId_EducationTreeState", "dbo.Lookups");
            DropForeignKey("dbo.EducationTrees_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.EducationTrees_Lessons", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.Lessons", "LookupId_Nezam", "dbo.Lookups");
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonDepartmentId", "dbo.LessonDepartments");
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Topics_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Topics_Questions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "ParentTopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics_Medias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Topics_Medias", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Media", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Media", "UserId", "dbo.Users");
            DropForeignKey("dbo.Teachers", "Id", "dbo.Users");
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sale_Package", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sale_Package", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Packages_Lessons", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages_Assays", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Packages_Assays", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.QuestionAnswers", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Writers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "AnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.QuestionAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswers", "LookupId_AnswerType", "dbo.Lookups");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lessons_Users", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons_Users", "UserId", "dbo.Users");
            DropForeignKey("dbo.Media", "LookupId_MediaType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LookupId_HardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LookupId_AreaType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Topics_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.Topics_EducationBooks", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.EducationBooks", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.AssayQuestions", "AssayId", "dbo.Assays");
            DropForeignKey("dbo.Actions", "ControllerId", "dbo.Controllers");
            DropForeignKey("dbo.Controllers", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Assays_Lessons", new[] { "LessonId" });
            DropIndex("dbo.Assays_Lessons", new[] { "AssayId" });
            DropIndex("dbo.Questions_Tags", new[] { "TagId" });
            DropIndex("dbo.Questions_Tags", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Supervisors", new[] { "UserId" });
            DropIndex("dbo.Questions_Supervisors", new[] { "QuestionId" });
            DropIndex("dbo.Questions_AreaTypes", new[] { "AreaTypeId" });
            DropIndex("dbo.Questions_AreaTypes", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Boxes", new[] { "BoxId" });
            DropIndex("dbo.Questions_Boxes", new[] { "QuestionId" });
            DropIndex("dbo.TeacherGroup_Students", new[] { "StudentId" });
            DropIndex("dbo.TeacherGroup_Students", new[] { "TeacherGroupId" });
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "StudentMajorlistId" });
            DropIndex("dbo.EducationTrees_Lessons", new[] { "LessonId" });
            DropIndex("dbo.EducationTrees_Lessons", new[] { "EducationTreeId" });
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonDepartmentId" });
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonId" });
            DropIndex("dbo.Topics_Questions", new[] { "QuestionId" });
            DropIndex("dbo.Topics_Questions", new[] { "TopicId" });
            DropIndex("dbo.Topics_Medias", new[] { "MediaId" });
            DropIndex("dbo.Topics_Medias", new[] { "TopicId" });
            DropIndex("dbo.Packages_Lessons", new[] { "LessonId" });
            DropIndex("dbo.Packages_Lessons", new[] { "PackageId" });
            DropIndex("dbo.Packages_Assays", new[] { "AssayId" });
            DropIndex("dbo.Packages_Assays", new[] { "PackageId" });
            DropIndex("dbo.Lessons_Users", new[] { "LessonId" });
            DropIndex("dbo.Lessons_Users", new[] { "UserId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "TopicId" });
            DropIndex("dbo.AssaySchedules", new[] { "AssayId" });
            DropIndex("dbo.Tags", "UK_Tag_Name");
            DropIndex("dbo.StudentMajorList_Major", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorList_Major", new[] { "StudentMajorListId" });
            DropIndex("dbo.StudentMajorlists", new[] { "StudentId" });
            DropIndex("dbo.LessonDepartments", "UK_LessonDepartment_Name");
            DropIndex("dbo.Sale_Package", new[] { "SaleId" });
            DropIndex("dbo.Sale_Package", new[] { "PackageId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.Roles", "UK_Role_Name");
            DropIndex("dbo.Writers", new[] { "User_Id" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "StudentId" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "AnswerId" });
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.QuestionAnswers", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswers", new[] { "WriterId" });
            DropIndex("dbo.QuestionAnswers", new[] { "LookupId_AnswerType" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", "UK_User_NationalNo");
            DropIndex("dbo.Users", "UK_User_Username");
            DropIndex("dbo.Media", new[] { "UserId" });
            DropIndex("dbo.Media", new[] { "WriterId" });
            DropIndex("dbo.Media", new[] { "LookupId_MediaType" });
            DropIndex("dbo.Topics", new[] { "LessonId" });
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            DropIndex("dbo.Topics", new[] { "LookupId_AreaType" });
            DropIndex("dbo.Topics", new[] { "LookupId_HardnessType" });
            DropIndex("dbo.EducationBooks", new[] { "LessonId" });
            DropIndex("dbo.EducationBooks", "UK_EducationBook_Name");
            DropIndex("dbo.Lessons", new[] { "LookupId_Nezam" });
            DropIndex("dbo.Lessons", "UK_Lesson_Name");
            DropIndex("dbo.EducationTrees", new[] { "LookupId_EducationTreeState" });
            DropIndex("dbo.EducationTrees", new[] { "ParentEducationTreeId" });
            DropIndex("dbo.ProgramItems", new[] { "ProgramId" });
            DropIndex("dbo.ProgramItems", new[] { "LookupId_PrgoramItemName" });
            DropIndex("dbo.Programs", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.TeacherGroups", new[] { "TeacherId" });
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.Boxes", new[] { "TeacherId" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionRank" });
            DropIndex("dbo.Questions", new[] { "WriterId" });
            DropIndex("dbo.Questions", new[] { "LookupId_AuthorType" });
            DropIndex("dbo.Questions", new[] { "LookupId_RepeatnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionHardnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.AssayQuestions", new[] { "QuestionId" });
            DropIndex("dbo.AssayQuestions", new[] { "AssayId" });
            DropIndex("dbo.Assays", new[] { "UserId" });
            DropIndex("dbo.Assays", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.Assays", new[] { "LookupId_Type" });
            DropIndex("dbo.Assays", new[] { "LookupId_Importance" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "UserId" });
            DropIndex("dbo.AssayAnswerSheets", new[] { "AssayId" });
            DropIndex("dbo.Modules", "UK_Module_Name");
            DropIndex("dbo.Controllers", new[] { "ModuleId" });
            DropIndex("dbo.Actions", new[] { "ControllerId" });
            DropTable("dbo.Assays_Lessons");
            DropTable("dbo.Questions_Tags");
            DropTable("dbo.Questions_Supervisors");
            DropTable("dbo.Questions_AreaTypes");
            DropTable("dbo.Questions_Boxes");
            DropTable("dbo.TeacherGroup_Students");
            DropTable("dbo.StudentMajorlist_Majors");
            DropTable("dbo.EducationTrees_Lessons");
            DropTable("dbo.Lessons_LessonDepartments");
            DropTable("dbo.Topics_Questions");
            DropTable("dbo.Topics_Medias");
            DropTable("dbo.Packages_Lessons");
            DropTable("dbo.Packages_Assays");
            DropTable("dbo.Lessons_Users");
            DropTable("dbo.Topics_EducationBooks");
            DropTable("dbo.Errors");
            DropTable("dbo.AssaySchedules");
            DropTable("dbo.Tags");
            DropTable("dbo.StudentMajorList_Major");
            DropTable("dbo.Majors");
            DropTable("dbo.StudentMajorlists");
            DropTable("dbo.LessonDepartments");
            DropTable("dbo.Packages");
            DropTable("dbo.Sale_Package");
            DropTable("dbo.Sales");
            DropTable("dbo.Roles");
            DropTable("dbo.Writers");
            DropTable("dbo.QuestionAnswerViews");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Logs");
            DropTable("dbo.Users");
            DropTable("dbo.Media");
            DropTable("dbo.Topics");
            DropTable("dbo.EducationBooks");
            DropTable("dbo.Lessons");
            DropTable("dbo.EducationTrees");
            DropTable("dbo.Lookups");
            DropTable("dbo.ProgramItems");
            DropTable("dbo.Programs");
            DropTable("dbo.Students");
            DropTable("dbo.TeacherGroups");
            DropTable("dbo.Teachers");
            DropTable("dbo.Boxes");
            DropTable("dbo.Questions");
            DropTable("dbo.AssayQuestions");
            DropTable("dbo.Assays");
            DropTable("dbo.AssayAnswerSheets");
            DropTable("dbo.Modules");
            DropTable("dbo.Controllers");
            DropTable("dbo.Actions");
        }
    }
}
