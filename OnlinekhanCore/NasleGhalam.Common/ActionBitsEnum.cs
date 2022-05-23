namespace NasleGhalam.Common
{
    public enum ActionBits
    {
        PublicAccess = 0,

        LessonReadAccess = 1,
        LessonCreateAccess = 2,
        LessonUpdateAccess = 3,
        LessonDeleteAccess = 4,

        EducationTreeReadAccess = 5,
        EducationTreeCreateAccess = 6,
        EducationTreeUpdateAccess = 7,
        EducationTreeDeleteAccess = 8,

        ProvinceReadAccess = 9,
        ProvinceCreateAccess = 10,
        ProvinceUpdateAccess = 11,
        ProvinceDeleteAccess = 12,

        CityReadAccess = 13,
        CityCreateAccess = 14,
        CityUpdateAccess = 15,
        CityDeleteAccess = 16,

        RoleReadAccess = 17,
        RoleCreateAccess = 18,
        RoleUpdateAccess = 19,
        RoleDeleteAccess = 20,
        RoleChangeAccess = 21,

        UserReadAccess = 22,
        UserCreateAccess = 23,
        UserUpdateAccess = 24,
        UserDeleteAccess = 25,

        TopicReadAccess = 26,
        TopicCreateAccess = 27,
        TopicUpdateAccess = 28,
        TopicDeleteAccess = 29,

        EducationSubGroupReadAccess = 30,
        EducationSubGroupCreateAccess = 31,
        EducationSubGroupUpdateAccess = 32,
        EducationSubGroupDeleteAccess = 33,

        EducationYearReadAccess = 34,
        EducationYearCreateAccess = 35,
        EducationYearUpdateAccess = 36,
        EducationYearDeleteAccess = 37,

        ExamReadAccess = 38,
        ExamCreateAccess = 39,
        ExamUpdateAccess = 40,
        ExamDeleteAccess = 41,

        StudentReadAccess = 42,
        StudentCreateAccess = 43,
        StudentUpdateAccess = 44,
        StudentDeleteAccess = 45,

        QuestionReadAccess = 46,
        QuestionCreateAccess = 47,
        QuestionUpdateAccess = 48,
        QuestionDeleteAccess = 49,
        QuestionUpdateTopicAccess = 82,
        QuestionUpdateImportAccess = 83,
        QuestionUpdateFinalImportAccess = 144,

        //AnswerReadAccess = 50,
        //AnswerCreateAccess = 51,
        //AnswerUpdateAccess = 52,
        //AnswerDeleteAccess = 53,

        PublisherReadAccess = 54,
        PublisherCreateAccess = 55,
        PublisherUpdateAccess = 56,
        PublisherDeleteAccess = 57,

        EducationBookReadAccess = 58,
        EducationBookCreateAccess = 59,
        EducationBookUpdateAccess = 60,
        EducationBookDeleteAccess = 61,

        AxillaryBookReadAccess = 62,
        AxillaryBookCreateAccess = 63,
        AxillaryBookUpdateAccess = 64,
        AxillaryBookDeleteAccess = 65,

        TagReadAccess = 66,
        TagCreateAccess = 67,
        TagUpdateAccess = 68,
        TagDeleteAccess = 69,

        UniversityBranchReadAccess = 70,
        UniversityBranchCreateAccess = 71,
        UniversityBranchUpdateAccess = 72,
        UniversityBranchDeleteAccess = 73,

        QuestionGroupReadAccess = 74,
        QuestionGroupCreateAccess = 75,
        QuestionGroupUpdateAccess = 76,
        QuestionGroupDeleteAccess = 77,

        QuestionJudgeReadAccess = 78,
        QuestionJudgeCreateAccess = 79,
        QuestionJudgeUpdateAccess = 80,
        QuestionJudgeDeleteAccess = 81,

        QuestionAnswerReadAccess = 84,
        QuestionAnswerCreateAccess = 85,
        QuestionAnswerUpdateAccess = 86,
        QuestionAnswerDeleteAccess = 87,

        WriterReadAccess = 88,
        WriterCreateAccess = 89,
        WriterDeleteAccess = 90,
        WriterUpdateAccess = 91,

        Lesson_UserReadAccess = 92,
        Lesson_UserCreateDeleteAccess = 93,

        LessonDepartmentReadAccess = 94,
        LessonDepartmentCreateAccess = 95,
        LessonDepartmentDeleteAccess = 96,
        LessonDepartmentUpdateAccess = 97,

        AssayReadAccess = 98,
        AssayCreateAccess = 99,
        AssayDeleteAccess = 100,
        AssayUpdateAccess = 101,

        ResumeReadAccess = 102,
        ResumeCreateAccess = 103,
        ResumeDeleteAccess = 104,
        ResumeUpdateAccess = 105,

        QuestionAnswerJudgeReadAccess = 106,
        QuestionAnswerJudgeCreateAccess = 107,
        QuestionAnswerJudgeUpdateAccess = 108,
        QuestionAnswerJudgeDeleteAccess = 109,



        PackageReadAccess = 110,
        PackageCreateAccess = 111,
        PackageDeleteAccess = 112,
        PackageUpdateAccess = 113,

        PanelAdminReadAccess = 114,
        PanelTeacherReadAccess = 123,
        PanelExpertReadAccess = 124,
        PanelStudentReadAccess = 164,

        TeacherReadAccess = 115,
        TeacherCreateAccess = 116,
        TeacherDeleteAccess = 117,
        TeacherUpdateAccess = 118,

        ReportReadAccess = 122,


        ProgramReadAccess = 125,
        ProgramCreateAccess = 126,
        ProgramDeleteAccess = 127,
        ProgramUpdateAccess = 128,

        WritersCodeReadAccess = 129,

        MediaReadAccess = 132,
        MediaCreateAccess = 133,
        MediaDeleteAccess = 134,
        MediaUpdateAccess = 135,


        AssayAdvisorReadAccess = 147,


        StudentMajorListReadAccess = 149,
        StudentMajorListCreateAccess = 150,
        StudentMajorListDeleteAccess = 151,
        StudentMajorListUpdateAccess = 152,

        


        AssayAnswerSheetReadAccess = 153,
        AssayAnswerSheetCreateAccess = 154,
        AssayAnswerSheetDeleteAccess = 155,
        AssayAnswerSheetUpdateAccess = 156,
        AssayAnswerSheetResualtAccess = 157,

        TeacherGroupReadAccess = 160,
        TeacherGroupCreateAccess = 161,
        TeacherGroupDeleteAccess = 162,
        TeacherGroupUpdateAccess = 163,

        LessonBuyAccess = 165,
        MyLessonAccess = 166

    }
}
