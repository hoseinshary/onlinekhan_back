using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.Common
{
    public enum JwtHashAlgorithm
    {
        RS256,
        HS384,
        HS512
    }

    public enum Fonts
    {
        BNazanin = 0,
        Arial = 1,
    }

    public enum KindRequest
    {
        Testi = 0,
        Tashrihi = 1,
        Both = 2
    }

    public enum Tashih
    {
        Correct = 0,
        Wrong = 1,
        Non = 2
    }

    public enum Maghta
    {
        Dah = 0,
        Yazdah = 1,
        Davazdah = 2,
        Konkoor = 3,
    }

    public enum QuestionStatus
    {
        [Display(Name = "بدون مبحث")]
        Imported,
        [Display(Name = "بدون ارزیابی")]
        Topiced,
        [Display(Name = "ارزیابی شده ناقص")]
        JudgedInComplete,
        [Display(Name = "ارزیابی شده فعال")]
        JudgedActive,
        [Display(Name = "ارزیابی شده غیر فعال")]
        JudgedInActive
    }

    public enum QuestionActivity
    {
        Import,
        Topic,
        UpdateAdmin,
        UpdateImport,
        UpdateEditor
    }

    public enum QuestionJudgedState
    {
        [Display(Name = "نا مشخص")]
        Nothing,
        [Display(Name = "تایید")]
        Active,
        [Display(Name = "ویرایش")]
        Update,
        [Display(Name = "حذف")]
        Delete,
    }

    public enum QuestionAnswerType
    {
        [Display(Name = "نا مشخص")]
        Nothing,
        [Display(Name = "کوتاه")]
        Short,
        [Display(Name = "توضیحی")]
        Long
    }

    public enum TypeEducationCenter
    {
        [Display(Name = "دولتی")]
        Dolati = 0,

        [Display(Name = "آزاد")]
        Azad = 1,

        [Display(Name = "غیر انتفاعی")]
        GheyreEntefaie = 2,

        [Display(Name = "پیام نور")]
        Payamnoor = 3,

        [Display(Name = "بین الملل")]
        Beynolmelal = 4,

        [Display(Name = "سایر")]
        Others = 5
    }

    public enum Degree
    {
        ZireDiplom = 0,
        Diplom = 1,
        Kardani = 2,
        Karshenasi = 3,
        Karshenasiarshad = 4,
        Doktora = 5

    }

    public enum DegreeCertificate
    {
        [Display(Name = "دانش آموز")]
        Daneshamooz = 0,

        [Display(Name = "دیپلم")]
        Diplom = 1,

        [Display(Name = "دانشجو کاردانی")]
        KardaniStudent = 2,

        [Display(Name = "کاردانی")]
        Kardani = 3,

        [Display(Name = "دانشجو کارشناسی")]
        KarshenasiStudent = 4,

        [Display(Name = "کارشناسی")]
        Karshenasi = 5,

        [Display(Name = "دانشجو کارشناسی ارشد")]
        KarshenasiArshadStudent = 6,

        [Display(Name = "کارشناسی ارشد")]
        KarshenasiArshad = 7,

        [Display(Name = "دانشجو دکترا")]
        DoktoraStudent = 8,

        [Display(Name = "دکترا")]
        Doktora = 9
    }

    public enum MessageType
    {
        Error = 0,
        Success = 1,
        Warning = 2,
        NotFound = 3,
        Unauthorized = 4
    }

    public enum CrudType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }

    public enum EqualType : byte
    {
        Equal = 0,
        Same = 1
    }

    public enum UserType
    {
        [Display(Name = "سازمانی")]
        Organ = 0,

        [Display(Name = "دانش آموز")]
        Student = 1,

        [Display(Name = "معلم")]
        Teacher = 2
    }

    public enum EducationTreeState
    {
        Grade = 1,
        SubTree = 2,
        EducationGroup = 3,
        GradeLevel = 4,
    }

    public enum AssayVarient
    {
        A = 1,
        B = 2,
        c = 3,
        d = 4
    }

    public enum DayOfWeak
    {
        [Display(Name = "شنبه")]
        SHANBE,
        [Display(Name = "یکشنبه")]
        YEKSHANBE,
        [Display(Name = "دوشنبه")]
        DOSHANBE,
        [Display(Name = "سه شنبه")]
        SESHANBE,
        [Display(Name = "چهارشنبه")]
        CHARSHANBE,
        [Display(Name = "پنجشنبه")]
        PANJSHANBE,
        [Display(Name = "جمعه")]
        JOME

    }

    public enum Field
    {
        [Display(Name = "ریاضی")]
        riyazi = 1,
        [Display(Name = "تجربی")]
        tajrobi = 2,
        [Display(Name = "انسانی")]
        ensani = 3,
        [Display(Name = "هنر")]
        honar = 4,
        [Display(Name = "زبان")]
        zaban = 5

    }

    //public enum AnswerType : byte
    //{
    //}

    //public enum PaperType : byte
    //{
    //    White = 0,
    //    Kahi = 1,
    //    Glasse = 2
    //}

    //public enum PrintType : byte
    //{
    //    Colered = 0,
    //    BlackAndWhite = 1
    //}

    //public enum BookType : byte
    //{

    //}

    //public enum QuestionType : byte
    //{
    //    MultipleChoice = 0,
    //    Detailed = 1
    //}


    //public enum TopicHardnessType : byte
    //{
    //    Easy = 0,
    //    Normal = 1,
    //    Hard = 2
    //}

    //public enum QuestionHardnessType : byte
    //{
    //    Easy = 0,
    //    Normal = 1,
    //    Hard = 2,
    //    Olampiad = 3
    //}

    //public enum AreaType : byte
    //{
    //    Danesh = 0,
    //    DarkVaFahm = 1,
    //    Karbord = 2,
    //    TajzieVaTahlil = 3,
    //    Tarkib = 4,
    //    Arzeshyabi = 5
    //}


    //public enum ReapetnessType : byte
    //{
    //    Low = 0,
    //    Medium = 1,
    //    High = 2P
    //}

    //public enum AuthorType : byte
    //{
    //    Konkoor = 0,
    //    Talif = 1,
    //    Jozve = 2
    //}
}
