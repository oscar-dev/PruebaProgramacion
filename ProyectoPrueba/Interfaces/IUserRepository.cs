using ProyectoPrueba.DTOs;
using ProyectoPrueba.Models;

namespace ProyectoPrueba.Interfaces
{
    public interface IUserRepository
    {
        void insertUser(User user);
        int ValidateUser(LoginDTO login);
        string getSlugById(int userID);
    }
}
