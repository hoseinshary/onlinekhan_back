using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentMajorListDataViewModel
    {
        public int Id { get; set; }


        public bool Gender { get; set; }
        public int BirthYear { get; set; }
        public int SahmieNahayei { get; set; }
        public int Field { get; set; }
        public int DiplomYear { get; set; }
        public int ProvinceBoomyId { get; set; }
        public int NahyeBoomy { get; set; }
        public int GhotbBoomy { get; set; }
        public bool IsAllowedRoozane { get; set; }
        public bool IsAllowedMajazi { get; set; }
        public bool IsAllowedPayam { get; set; }
        public bool IsAllowedAzad { get; set; }

        public int GorohAzmayeshi { get; set; }
        public bool IsAllowedRozaneGoroh { get; set; }
        public bool IsAllowedMajaziGoroh { get; set; }
        public bool IsAllowedFarhangianGoroh { get; set; }
        public bool IsAllowedPayamGoroh { get; set; }
        public bool IsAllowedAzadGoroh { get; set; }

        public int SahmieNahayeiOne { get; set; }
        public int SahmieNahayeiTwo { get; set; }
        public int SahmieNahayeiThree { get; set; }
        public int SahmieNahayeiFour { get; set; }
        public int SahmieNahayeiFive { get; set; }
        public int KeshvariOne { get; set; }
        public int KeshvariTwo { get; set; }
        public int KeshvariThree { get; set; }
        public int KeshvariFour { get; set; }
        public int KeshvariFive { get; set; }
        public int KolOne { get; set; }
        public int KolTwo { get; set; }
        public int KolThree { get; set; }
        public int KolFour { get; set; }
        public int KolFive { get; set; }
    }
}