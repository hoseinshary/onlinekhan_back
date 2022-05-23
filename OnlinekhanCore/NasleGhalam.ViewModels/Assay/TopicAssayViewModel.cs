namespace NasleGhalam.ViewModels.Assay
{
    public class TopicAssayViewModel
    {
        public int Id { get; set; }

        public int CountOfEasy { get; set; }

        public int CountOfMedium { get; set; }

        public int CountOfHard { get; set; }

        public int CountOfQuestions => CountOfEasy + CountOfMedium + CountOfHard;
    }
}
