namespace NasleGhalam.ServiceLayer.Jwt
{
    public class JwtPayload
    {
        public string Value { get; set; }

        public string Access { get; set; }

        public long Exp { get; set; }
    }
}