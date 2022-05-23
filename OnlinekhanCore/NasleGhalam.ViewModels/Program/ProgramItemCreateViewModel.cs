using NasleGhalam.Common;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.ProgramItem
{
    public class ProgramItemCreateViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "آیتم")]
        public int LookupId_PrgoramItemName { get; set; }


        [Display(Name = "برنامه")]
        public int ProgramId { get; set; }


        [Display(Name = "ساعت")]
        public int Hour { get; set; }

        [Display(Name = "توضیح")]
        public string Description { get; set; }

        [Display(Name = "روز هفته")]
        public DayOfWeak DayOfWeak { get; set; }



    }
}
