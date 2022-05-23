using NasleGhalam.ViewModels.Province;

namespace NasleGhalam.ViewModels.City
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }

        public ProvinceViewModel Province { get; set; }
    }
}
