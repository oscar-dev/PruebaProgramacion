namespace ProyectoPrueba.DTOs
{
    public class LoginResponseDTO
    {
        public class Slug
        {
            public Slug() { this.slugTenant = ""; }
            public string slugTenant { get; set; }
        }
        public LoginResponseDTO()
        {
            this.accessToken = "";
            this.tenants = new List<Slug>();
        }
        public string accessToken { get; set; }
        public List<Slug> tenants { get; set; }
    }
}
