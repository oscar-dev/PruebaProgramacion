namespace ProyectoPrueba.DTOs
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            this.Email = "";
            this.Password = "";
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
