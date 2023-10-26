namespace ProyectoPrueba.DTOs
{
    public class JwtDTO
    {
        public JwtDTO ()
        {
            this.Key = "";
            this.Issuer = "";
            this.Audience = "";
            this.Subject = "";
        }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
