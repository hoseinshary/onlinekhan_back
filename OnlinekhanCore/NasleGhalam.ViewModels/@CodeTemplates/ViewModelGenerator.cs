using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
	public class AssayAnswerSheetViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public int AssayId { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public AssayVarient AssayVarient { get; set; }


		[Display(Name = "")]
		public DateTime AssayTime { get; set; }


		[Display(Name = "")]
		public DateTime DateTime { get; set; }


	}
}
