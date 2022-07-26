namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.HistoryEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        RankGoal = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                        EducationTreeId = c.Int(nullable: false),
                        EducationSubGroup_Id = c.Int(),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroup_Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.EducationSubGroup_Id)
                .Index(t => t.City_Id);
            
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
                "dbo.EducationSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EducationTreeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .Index(t => t.EducationTreeId);
            
            CreateTable(
                "dbo.Ratios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Byte(nullable: false),
                        LessonId = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId)
                .Index(t => t.EducationSubGroupId);
            
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
                "dbo.AxillaryBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PublishYear = c.Short(nullable: false),
                        Author = c.String(nullable: false, maxLength: 100),
                        Isbn = c.String(nullable: false, maxLength: 100),
                        Font = c.String(maxLength: 50),
                        ImgName = c.String(maxLength: 200),
                        Price = c.Int(nullable: false),
                        OriginalPrice = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        LookupId_PrintType = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        LookupId_PaperType = c.Int(nullable: false),
                        LookupId_BookType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_BookType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PaperType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PrintType)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.Name, unique: true, name: "UK_AxillaryBook_Name")
                .Index(t => t.LookupId_PrintType)
                .Index(t => t.PublisherId)
                .Index(t => t.LookupId_PaperType)
                .Index(t => t.LookupId_BookType);
            
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
                "dbo.QuestionAnswerJudges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActiveQuestionAnswer = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        QuestionAnswerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsMaster = c.Boolean(nullable: false),
                        LookupId_ReasonProblem = c.Int(nullable: false),
                        Description = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_ReasonProblem)
                .ForeignKey("dbo.QuestionAnswers", t => t.QuestionAnswerId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.QuestionAnswerId)
                .Index(t => t.UserId)
                .Index(t => t.LookupId_ReasonProblem);
            
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
                        CityId = c.Int(nullable: false),
                        ProfilePic = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.Username, unique: true, name: "UK_User_Username")
                .Index(t => t.NationalNo, unique: true, name: "UK_User_NationalNo")
                .Index(t => t.RoleId)
                .Index(t => t.CityId);
            
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
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        InsertTime = c.DateTime(nullable: false),
                        File = c.String(maxLength: 50),
                        LessonId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionJudges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupId_QuestionHardnessType = c.Int(nullable: false),
                        LookupId_RepeatnessType = c.Int(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        IsLearning = c.Boolean(nullable: false),
                        IsActiveQuestion = c.Boolean(nullable: false),
                        IsActiveQuestionAnswer = c.Boolean(nullable: false),
                        ResponseSecond = c.Short(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LookupId_WhereProblem = c.Int(nullable: false),
                        LookupId_ReasonProblem = c.Int(nullable: false),
                        LookupId_QuestionRank = c.Int(nullable: false),
                        EducationGroup = c.String(maxLength: 50),
                        Description = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionHardnessType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionRank)
                .ForeignKey("dbo.Lookups", t => t.LookupId_ReasonProblem)
                .ForeignKey("dbo.Lookups", t => t.LookupId_RepeatnessType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_WhereProblem)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LookupId_QuestionHardnessType)
                .Index(t => t.LookupId_RepeatnessType)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId)
                .Index(t => t.LookupId_WhereProblem)
                .Index(t => t.LookupId_ReasonProblem)
                .Index(t => t.LookupId_QuestionRank);
            
            CreateTable(
                "dbo.QuestionUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        QuestionActivity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
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
                "dbo.QuestionEquals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EqualType = c.Byte(nullable: false),
                        QuestionId1 = c.Int(nullable: false),
                        QuestionId2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId1)
                .ForeignKey("dbo.Questions", t => t.QuestionId2)
                .Index(t => t.QuestionId1)
                .Index(t => t.QuestionId2);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false),
                        IsAnswer = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
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
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Publisher_Name");
            
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
                "dbo.UniversityBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SiteAverage = c.Int(nullable: false),
                        Balance1Low = c.Int(nullable: false),
                        Balance1High = c.Int(nullable: false),
                        Balance2Low = c.Int(nullable: false),
                        Balance2High = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .Index(t => t.Name, unique: true, name: "UK_UniversityBranch_Name")
                .Index(t => t.EducationSubGroupId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        EducationTreeId = c.Int(nullable: false),
                        EducationYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.EducationYears", t => t.EducationYearId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.EducationYearId);
            
            CreateTable(
                "dbo.EducationYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActiveYear = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_EducationYear_Name");
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Province_Name");
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Branch = c.String(maxLength: 50),
                        CreationDateTime = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Family = c.String(nullable: false, maxLength: 150),
                        FatherName = c.String(nullable: false, maxLength: 150),
                        IdNumber = c.String(maxLength: 10),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Mobile = c.String(nullable: false, maxLength: 11),
                        CityBorn = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        Marriage = c.Boolean(nullable: false),
                        Religion = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                        PostCode = c.String(nullable: false, maxLength: 10),
                        FatherJob = c.String(maxLength: 150),
                        FatherDegree = c.Int(nullable: false),
                        FatherPhone = c.String(maxLength: 11),
                        MotherJob = c.String(maxLength: 150),
                        MotherDegree = c.Int(nullable: false),
                        MotherPhone = c.String(maxLength: 11),
                        PartnerJob = c.String(maxLength: 150),
                        PartnerDegree = c.Int(nullable: false),
                        PartnerPhone = c.String(maxLength: 11),
                        Reagent1 = c.String(maxLength: 50),
                        RelationReagent1 = c.String(maxLength: 50),
                        JobReagent1 = c.String(maxLength: 50),
                        PhoneReagent1 = c.String(maxLength: 11),
                        AddressReagent1 = c.String(maxLength: 300),
                        Reagent2 = c.String(maxLength: 50),
                        RelationReagent2 = c.String(maxLength: 50),
                        JobReagent2 = c.String(maxLength: 50),
                        PhoneReagent2 = c.String(maxLength: 11),
                        AddressReagent2 = c.String(maxLength: 300),
                        HaveEducationCertificate = c.Boolean(nullable: false),
                        HaveAnotherCertificate = c.Boolean(nullable: false),
                        AnotherCertificate = c.String(maxLength: 50),
                        HavePublication = c.Boolean(nullable: false),
                        NumberOfPublication = c.Int(nullable: false),
                        HaveTeachingResume = c.Boolean(nullable: false),
                        NumberOfTeachingYear = c.Int(nullable: false),
                        TeachingRequest1 = c.Boolean(nullable: false),
                        PublishingRequest1 = c.Boolean(nullable: false),
                        MaghtaRequest1 = c.Int(nullable: false),
                        KindRequest1 = c.Int(nullable: false),
                        LessonNameRequest1 = c.String(maxLength: 50),
                        TeachingRequest2 = c.Boolean(nullable: false),
                        PublishingRequest2 = c.Boolean(nullable: false),
                        MaghtaRequest2 = c.Int(nullable: false),
                        KindRequest2 = c.Int(nullable: false),
                        LessonNameRequest2 = c.String(maxLength: 50),
                        RequestForAdvice = c.Boolean(nullable: false),
                        MaghtaAdvice = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        EducationCertificateJson = c.String(nullable: false),
                        PublicationJson = c.String(nullable: false),
                        TeachingResumeJson = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.AxillaryBooks_EducationBooks",
                c => new
                    {
                        AxillaryBookId = c.Int(nullable: false),
                        EducationBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AxillaryBookId, t.EducationBookId })
                .ForeignKey("dbo.AxillaryBooks", t => t.AxillaryBookId)
                .ForeignKey("dbo.EducationBooks", t => t.EducationBookId)
                .Index(t => t.AxillaryBookId)
                .Index(t => t.EducationBookId);
            
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
                "dbo.QuestionGroups_Questions",
                c => new
                    {
                        QuestionGroupId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionGroupId, t.QuestionId })
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionGroupId)
                .Index(t => t.QuestionId);
            
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
                "dbo.HistoryEducations_UniversityBranchs",
                c => new
                    {
                        HistoryEducationId = c.Int(nullable: false),
                        UniversityBranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryEducationId, t.UniversityBranchId })
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.UniversityBranches", t => t.UniversityBranchId)
                .Index(t => t.HistoryEducationId)
                .Index(t => t.UniversityBranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resumes", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.HistoryEducations", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "UniversityBranchId", "dbo.UniversityBranches");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.HistoryEducations", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.HistoryEducations", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.EducationTrees", "ParentEducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.EducationTrees", "LookupId_EducationTreeState", "dbo.Lookups");
            DropForeignKey("dbo.EducationTrees_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.EducationTrees_Lessons", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.Exams", "EducationYearId", "dbo.EducationYears");
            DropForeignKey("dbo.Exams", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.UniversityBranches", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.Ratios", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "LookupId_Nezam", "dbo.Lookups");
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonDepartmentId", "dbo.LessonDepartments");
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.EducationBooks", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.AxillaryBooks", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PrintType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PaperType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_BookType", "dbo.Lookups");
            DropForeignKey("dbo.Media", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Media", "UserId", "dbo.Users");
            DropForeignKey("dbo.Topics_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Topics_Questions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Questions", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Questions_Tags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Questions_Tags", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions_Supervisors", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions_Supervisors", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionEquals", "QuestionId2", "dbo.Questions");
            DropForeignKey("dbo.QuestionEquals", "QuestionId1", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswers", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.QuestionAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswerJudges", "UserId", "dbo.Users");
            DropForeignKey("dbo.Writers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Teachers", "Id", "dbo.Users");
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "AnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.QuestionUpdates", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionUpdates", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionJudges", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionJudges", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionJudges", "LookupId_WhereProblem", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "LookupId_RepeatnessType", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "LookupId_ReasonProblem", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "LookupId_QuestionRank", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "LookupId_QuestionHardnessType", "dbo.Lookups");
            DropForeignKey("dbo.QuestionGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionGroupId", "dbo.QuestionGroups");
            DropForeignKey("dbo.QuestionGroups", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lessons_Users", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons_Users", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.QuestionAnswerJudges", "QuestionAnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.QuestionAnswerJudges", "LookupId_ReasonProblem", "dbo.Lookups");
            DropForeignKey("dbo.QuestionAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswers", "LookupId_AnswerType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_RepeatnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionRank", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionHardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_AuthorType", "dbo.Lookups");
            DropForeignKey("dbo.Questions_AreaTypes", "AreaTypeId", "dbo.Lookups");
            DropForeignKey("dbo.Questions_AreaTypes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Topics", "ParentTopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics_Medias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Topics_Medias", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "LookupId_HardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LookupId_AreaType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Topics_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.Topics_EducationBooks", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Media", "LookupId_MediaType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "AxillaryBookId", "dbo.AxillaryBooks");
            DropForeignKey("dbo.Ratios", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.HistoryEducations", "EducationSubGroup_Id", "dbo.EducationSubGroups");
            DropForeignKey("dbo.EducationSubGroups", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.Actions", "ControllerId", "dbo.Controllers");
            DropForeignKey("dbo.Controllers", "ModuleId", "dbo.Modules");
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "UniversityBranchId" });
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "HistoryEducationId" });
            DropIndex("dbo.EducationTrees_Lessons", new[] { "LessonId" });
            DropIndex("dbo.EducationTrees_Lessons", new[] { "EducationTreeId" });
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonDepartmentId" });
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonId" });
            DropIndex("dbo.Topics_Questions", new[] { "QuestionId" });
            DropIndex("dbo.Topics_Questions", new[] { "TopicId" });
            DropIndex("dbo.Questions_Tags", new[] { "TagId" });
            DropIndex("dbo.Questions_Tags", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Supervisors", new[] { "UserId" });
            DropIndex("dbo.Questions_Supervisors", new[] { "QuestionId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionGroupId" });
            DropIndex("dbo.Lessons_Users", new[] { "LessonId" });
            DropIndex("dbo.Lessons_Users", new[] { "UserId" });
            DropIndex("dbo.Questions_AreaTypes", new[] { "AreaTypeId" });
            DropIndex("dbo.Questions_AreaTypes", new[] { "QuestionId" });
            DropIndex("dbo.Topics_Medias", new[] { "MediaId" });
            DropIndex("dbo.Topics_Medias", new[] { "TopicId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "TopicId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "AxillaryBookId" });
            DropIndex("dbo.Resumes", new[] { "CityId" });
            DropIndex("dbo.Provinces", "UK_Province_Name");
            DropIndex("dbo.EducationYears", "UK_EducationYear_Name");
            DropIndex("dbo.Exams", new[] { "EducationYearId" });
            DropIndex("dbo.Exams", new[] { "EducationTreeId" });
            DropIndex("dbo.UniversityBranches", new[] { "EducationSubGroupId" });
            DropIndex("dbo.UniversityBranches", "UK_UniversityBranch_Name");
            DropIndex("dbo.LessonDepartments", "UK_LessonDepartment_Name");
            DropIndex("dbo.Publishers", "UK_Publisher_Name");
            DropIndex("dbo.Tags", "UK_Tag_Name");
            DropIndex("dbo.QuestionOptions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId2" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId1" });
            DropIndex("dbo.Writers", new[] { "User_Id" });
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "StudentId" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "AnswerId" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Roles", "UK_Role_Name");
            DropIndex("dbo.QuestionUpdates", new[] { "QuestionId" });
            DropIndex("dbo.QuestionUpdates", new[] { "UserId" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_QuestionRank" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_ReasonProblem" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_WhereProblem" });
            DropIndex("dbo.QuestionJudges", new[] { "UserId" });
            DropIndex("dbo.QuestionJudges", new[] { "QuestionId" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_RepeatnessType" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_QuestionHardnessType" });
            DropIndex("dbo.QuestionGroups", new[] { "UserId" });
            DropIndex("dbo.QuestionGroups", new[] { "LessonId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", "UK_User_NationalNo");
            DropIndex("dbo.Users", "UK_User_Username");
            DropIndex("dbo.QuestionAnswerJudges", new[] { "LookupId_ReasonProblem" });
            DropIndex("dbo.QuestionAnswerJudges", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswerJudges", new[] { "QuestionAnswerId" });
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.QuestionAnswers", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswers", new[] { "WriterId" });
            DropIndex("dbo.QuestionAnswers", new[] { "LookupId_AnswerType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionRank" });
            DropIndex("dbo.Questions", new[] { "WriterId" });
            DropIndex("dbo.Questions", new[] { "LookupId_AuthorType" });
            DropIndex("dbo.Questions", new[] { "LookupId_RepeatnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionHardnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.Topics", new[] { "LessonId" });
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            DropIndex("dbo.Topics", new[] { "LookupId_AreaType" });
            DropIndex("dbo.Topics", new[] { "LookupId_HardnessType" });
            DropIndex("dbo.Media", new[] { "UserId" });
            DropIndex("dbo.Media", new[] { "WriterId" });
            DropIndex("dbo.Media", new[] { "LookupId_MediaType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_BookType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PaperType" });
            DropIndex("dbo.AxillaryBooks", new[] { "PublisherId" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PrintType" });
            DropIndex("dbo.AxillaryBooks", "UK_AxillaryBook_Name");
            DropIndex("dbo.EducationBooks", new[] { "LessonId" });
            DropIndex("dbo.EducationBooks", "UK_EducationBook_Name");
            DropIndex("dbo.Lessons", new[] { "LookupId_Nezam" });
            DropIndex("dbo.Lessons", "UK_Lesson_Name");
            DropIndex("dbo.Ratios", new[] { "EducationSubGroupId" });
            DropIndex("dbo.Ratios", new[] { "LessonId" });
            DropIndex("dbo.EducationSubGroups", new[] { "EducationTreeId" });
            DropIndex("dbo.EducationTrees", new[] { "LookupId_EducationTreeState" });
            DropIndex("dbo.EducationTrees", new[] { "ParentEducationTreeId" });
            DropIndex("dbo.HistoryEducations", new[] { "City_Id" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationSubGroup_Id" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationTreeId" });
            DropIndex("dbo.HistoryEducations", new[] { "ExamId" });
            DropIndex("dbo.HistoryEducations", new[] { "StudentId" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Modules", "UK_Module_Name");
            DropIndex("dbo.Controllers", new[] { "ModuleId" });
            DropIndex("dbo.Actions", new[] { "ControllerId" });
            DropTable("dbo.HistoryEducations_UniversityBranchs");
            DropTable("dbo.EducationTrees_Lessons");
            DropTable("dbo.Lessons_LessonDepartments");
            DropTable("dbo.Topics_Questions");
            DropTable("dbo.Questions_Tags");
            DropTable("dbo.Questions_Supervisors");
            DropTable("dbo.QuestionGroups_Questions");
            DropTable("dbo.Lessons_Users");
            DropTable("dbo.Questions_AreaTypes");
            DropTable("dbo.Topics_Medias");
            DropTable("dbo.Topics_EducationBooks");
            DropTable("dbo.AxillaryBooks_EducationBooks");
            DropTable("dbo.Resumes");
            DropTable("dbo.Provinces");
            DropTable("dbo.EducationYears");
            DropTable("dbo.Exams");
            DropTable("dbo.UniversityBranches");
            DropTable("dbo.LessonDepartments");
            DropTable("dbo.Publishers");
            DropTable("dbo.Tags");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.QuestionEquals");
            DropTable("dbo.Writers");
            DropTable("dbo.Teachers");
            DropTable("dbo.QuestionAnswerViews");
            DropTable("dbo.Students");
            DropTable("dbo.Roles");
            DropTable("dbo.QuestionUpdates");
            DropTable("dbo.QuestionJudges");
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.Logs");
            DropTable("dbo.Users");
            DropTable("dbo.QuestionAnswerJudges");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.Topics");
            DropTable("dbo.Media");
            DropTable("dbo.Lookups");
            DropTable("dbo.AxillaryBooks");
            DropTable("dbo.EducationBooks");
            DropTable("dbo.Lessons");
            DropTable("dbo.Ratios");
            DropTable("dbo.EducationSubGroups");
            DropTable("dbo.EducationTrees");
            DropTable("dbo.HistoryEducations");
            DropTable("dbo.Cities");
            DropTable("dbo.Modules");
            DropTable("dbo.Controllers");
            DropTable("dbo.Actions");
        }
    }
}
